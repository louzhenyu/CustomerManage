using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.AlipayWapTrade2;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.AlipayWapTrade2
{
    /// <summary>
    /// 功能：页面跳转同步通知页面
    /// 版本：3.3
    /// 日期：2012-07-10
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// ///////////////////////页面功能说明///////////////////////
    /// 该页面可在本机电脑测试
    /// 可放入HTML等美化页面的代码、商户业务逻辑程序代码
    /// 该页面可以使用ASP.NET开发工具调试，也可以使用写文本函数LogResult进行调试
    /// </summary>
    public partial class Call_Back : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseService.WriteLog("页面跳转同步通知页面-----------------------AlipayWapTrade2/Call_Back.aspx");
            AlipayWapTradeResponseBLL alipayServer = new AlipayWapTradeResponseBLL(new Utility.BasicUserInfo());

            Dictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                AlipayNotify aliNotify = new AlipayNotify();
                bool verifyResult = aliNotify.VerifyReturn(sPara, Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                    //商户订单号
                    string out_trade_no = Request.QueryString["out_trade_no"];

                    //支付宝交易号
                    string trade_no = Request.QueryString["trade_no"];

                    //交易状态
                    string result = Request.QueryString["result"];

                    //判断是否在商户网站中已经做过了这次通知返回的处理
                    //如果没有做过处理，那么执行商户的业务程序
                    //如果有做过处理，那么不执行商户的业务程序

                    BaseService.WriteLog("result：" + result);

                    if (!result.Equals("success"))
                    {
                        //交易失败
                        if (!string.IsNullOrEmpty(out_trade_no))
                        {
                            alipayServer.UpdateAlipayWapTradeStatus(Request["out_trade_no"], "3");
                            PostResult(alipayServer, "fail");

                            //推送交易结果集
                            BaseService.WriteLog("推送交易结果集");
                            PostResult(alipayServer, "fail");

                            BaseService.WriteLog("交易失败，更新支付宝交易状态");
                        }

                        Response.Write("fail");
                        return;
                    }
                    else //交易成功，请填写自己的业务代码
                    {
                        if (!string.IsNullOrEmpty(out_trade_no))
                        {
                            try
                            {
                                BaseService.WriteLog("out_trade_no：" + out_trade_no);
                                BaseService.WriteLog("交易成功");

                                //更新交易状态
                                BaseService.WriteLog("更新交易状态");
                                alipayServer.UpdateAlipayWapTradeStatus(out_trade_no, "2");

                                //处理分润业务
                                BaseService.WriteLog("处理分润业务");
                                //RoyaltyBusiness(out_trade_no, trade_no);

                                //推送交易结果集
                                BaseService.WriteLog("推送交易结果集");
                                PostResult(alipayServer, "success");
                            }
                            catch (Exception ex)
                            {
                                BaseService.WriteLog("异常信息： " + ex.ToString());
                            }
                        }
                        else
                        {
                            BaseService.WriteLog("out_trade_no is null!!!!! ");
                        }

                        Response.Redirect(AlipayConfig.Merchant_url);
                    }
                }
                else//验证失败
                {
                    if (Request["out_trade_no"] != null)
                    {
                        alipayServer.UpdateAlipayWapTradeStatus(Request["out_trade_no"], "4");
                        PostResult(alipayServer, "fail");

                        BaseService.WriteLog("验签出错，可能被别人篡改数据。");
                    }

                    Response.Write("验证失败");
                }
            }
            else
            {
                Response.Write("无返回参数");
            }
        }

        //分润业务
        private void RoyaltyBusiness(string out_trade_no, string trade_no)
        {
            var royaltyServer = new AlipayRoyaltyBLL(Default.GetLoggingSession());
            var ds = royaltyServer.GetAlipayRoyalty(out_trade_no);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dr = ds.Tables[0].Rows[0];

                var out_bill_no = GetDataRandom();
                BaseService.WriteLog("分润号： " + out_bill_no);
                var royalty = new DistributeRoyalty();
                var result = royalty.SubmitDistribute(out_bill_no, out_trade_no, trade_no, dr["royalty_parameters"].ToString());
                BaseService.WriteLog("调用分润接口返回结果： " + result);

                var entity = new AlipayRoyaltyEntity()
                {
                    RoyaltyID = dr["royalty_id"].ToString(),
                    TradeNo = trade_no,
                    Result = result
                };

                royaltyServer.Update(entity, false);
            }
            else
            {
                BaseService.WriteLog("查询没有结果集");
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetRequestGet()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

        /// <summary>
        /// 交易结束时 返回交易结果
        /// </summary>
        /// <param name="alipayServer"></param>
        /// <param name="result"></param>
        private void PostResult(AlipayWapTradeResponseBLL alipayServer, string result)
        {
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "out_trade_no", Value = Request["out_trade_no"] });
            var alipays = alipayServer.Query(lstWhereCondition.ToArray(), null);

            if (alipays != null && alipays.Length > 0)
            {
                var alipay = alipays.FirstOrDefault();

                string uriString = alipay.CallBackUrl;
                // 创建一个新的 WebClient 实例.
                WebClient myWebClient = new WebClient();
                string postData = "order_id=" + alipay.OrderID + "&result=" + result + "&out_trade_no=" + Request["out_trade_no"];

                BaseService.WriteLog("交易结束时，返回交易结果。");
                BaseService.WriteLog("CallBackUrl： " + uriString);
                BaseService.WriteLog("postData： " + postData);

                // 注意这种拼字符串的ContentType
                myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                // 转化成二进制数组
                byte[] byteArray = Encoding.ASCII.GetBytes(postData);
                // 上传数据
                myWebClient.UploadData(uriString, "POST", byteArray);
            }
        }

        /// <summary>
        /// 获取16位随机数
        /// </summary>
        /// <returns></returns>
        public string GetDataRandom()
        {
            string strData = string.Empty;
            strData += DateTime.Now.Year;
            strData += DateTime.Now.Month;
            strData += DateTime.Now.Day;
            strData += DateTime.Now.Hour;
            strData += DateTime.Now.Minute;
            strData += DateTime.Now.Second;
            Random r = new Random();
            strData = strData + r.Next(100);
            return strData;
        }
    }
}
