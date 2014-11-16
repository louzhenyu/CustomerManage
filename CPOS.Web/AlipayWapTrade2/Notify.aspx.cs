using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.AlipayWapTrade2;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.AlipayWapTrade2
{
    /// <summary>
    /// 功能：服务器异步通知页面
    /// 版本：3.3
    /// 日期：2012-07-10
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// ///////////////////页面功能说明///////////////////
    /// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
    /// 该页面不能在本机电脑测试，请到服务器上做测试。请确保外部可以访问该页面。
    /// 该页面调试工具请使用写文本函数logResult。
    /// 如果没有收到该页面返回的 success 信息，支付宝会在24小时内按一定的时间策略重发通知
    /// </summary>
    public partial class Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseService.WriteLog("服务器异步通知页面-----------------------AlipayWapTrade2/Notify.aspx");

            Dictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                BaseService.WriteLog("开始验证");

                AlipayNotify aliNotify = new AlipayNotify();
                bool verifyResult = aliNotify.VerifyNotify(sPara, Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    BaseService.WriteLog("验证成功");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //解密（如果是RSA签名需要解密，如果是MD5签名则下面一行清注释掉）
                    sPara = aliNotify.Decrypt(sPara);

                    //XML解析notify_data数据
                    try
                    {
                        BaseService.WriteLog("notify_data：" + sPara["notify_data"]);

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(sPara["notify_data"]);
                        //商户订单号
                        string out_trade_no = xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText;
                        //支付宝交易号
                        string trade_no = xmlDoc.SelectSingleNode("/notify/trade_no").InnerText;
                        //交易状态
                        string trade_status = xmlDoc.SelectSingleNode("/notify/trade_status").InnerText;

                        //交易成功，更新支付宝交易状态
                        UpdateAlipayWapTrade(xmlDoc, "2");

                        BaseService.WriteLog("交易状态trade_status：" + trade_status);

                        if (trade_status == "TRADE_FINISHED")
                        {
                            //判断该笔订单是否在商户网站中已经做过处理
                            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                            //如果有做过处理，不执行商户的业务程序

                            if (!string.IsNullOrEmpty(out_trade_no))
                            {
                                try
                                {
                                    var alipayServer = new AlipayWapTradeResponseBLL(new Utility.BasicUserInfo());
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
                                    PostResult(alipayServer, "success", out_trade_no);
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

                            //注意：
                            //该种交易状态只在两种情况下出现
                            //1、开通了普通即时到账，买家付款成功后。
                            //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。
                            Response.Write("success");  //请不要修改或删除
                        }
                        else if (trade_status == "TRADE_SUCCESS")
                        {
                            //判断该笔订单是否在商户网站中已经做过处理
                            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                            //如果有做过处理，不执行商户的业务程序

                            if (!string.IsNullOrEmpty(out_trade_no))
                            {
                                try
                                {
                                    var alipayServer = new AlipayWapTradeResponseBLL(new Utility.BasicUserInfo());
                                    BaseService.WriteLog("out_trade_no：" + out_trade_no);
                                    BaseService.WriteLog("交易成功");

                                    //更新交易状态
                                    BaseService.WriteLog("更新交易状态");
                                    alipayServer.UpdateAlipayWapTradeStatus(out_trade_no, "2");

                                    //处理分润业务
                                    BaseService.WriteLog("处理分润业务");
                                    RoyaltyBusiness(out_trade_no, trade_no);

                                    //推送交易结果集
                                    BaseService.WriteLog("推送交易结果集");
                                    PostResult(alipayServer, "success", out_trade_no);
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

                            //注意：
                            //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。
                            Response.Write("success");  //请不要修改或删除
                        }
                        else
                        {
                            Response.Write(trade_status);
                        }
                    }
                    catch (Exception exc)
                    {
                        BaseService.WriteLog("异常信息：" + exc.ToString());
                        Response.Write(exc.ToString());
                    }
                }
                else//验证失败
                {
                    BaseService.WriteLog("验证失败");
                    Response.Write("fail");
                }
            }
            else
            {
                BaseService.WriteLog("无通知参数");
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 交易成功，更新支付宝交易状态
        /// </summary>
        /// <param name="xmlDoc"></param>
        private void UpdateAlipayWapTrade(XmlDocument xmlDoc, string status)
        {
            var alipayEntity = new AlipayWapTradeResponseEntity()
            {
                OutTradeNo = xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText,
                Subject = xmlDoc.SelectSingleNode("/notify/subject").InnerText,
                TotalFee = xmlDoc.SelectSingleNode("/notify/total_fee").InnerText,
                PaymentType = xmlDoc.SelectSingleNode("/notify/payment_type").InnerText,
                TradeNo = xmlDoc.SelectSingleNode("/notify/trade_no").InnerText,
                BuyerEmail = xmlDoc.SelectSingleNode("/notify/buyer_email").InnerText,
                GmtCreate = xmlDoc.SelectSingleNode("/notify/gmt_create").InnerText,
                NotifyType = xmlDoc.SelectSingleNode("/notify/notify_type").InnerText,
                Quantity = xmlDoc.SelectSingleNode("/notify/quantity").InnerText,
                NotifyTime = xmlDoc.SelectSingleNode("/notify/notify_time").InnerText,
                SellerID = xmlDoc.SelectSingleNode("/notify/seller_id").InnerText,
                TradeStatus = xmlDoc.SelectSingleNode("/notify/trade_status").InnerText,
                IsTotalFeeAdjust = xmlDoc.SelectSingleNode("/notify/is_total_fee_adjust").InnerText,
                GmtPayment = xmlDoc.SelectSingleNode("/notify/gmt_payment").InnerText,
                SellerEmail = xmlDoc.SelectSingleNode("/notify/seller_email").InnerText,
                //GmtClose = xmlDoc.SelectSingleNode("/notify/gmt_close").InnerText,
                Price = xmlDoc.SelectSingleNode("/notify/price").InnerText,
                BuyerID = xmlDoc.SelectSingleNode("/notify/buyer_id").InnerText,
                NotifyID = xmlDoc.SelectSingleNode("/notify/notify_id").InnerText,
                UseCoupon = xmlDoc.SelectSingleNode("/notify/use_coupon").InnerText,
                Status = status
            };

            BaseService.WriteLog("交易成功，更新支付宝交易状态");

            AlipayWapTradeResponseBLL alipayServer = new AlipayWapTradeResponseBLL(new Utility.BasicUserInfo());
            alipayServer.UpdateAlipayWapTrade(alipayEntity);
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetRequestPost()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
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

        /// <summary>
        /// 交易结束时 返回交易结果
        /// </summary>
        /// <param name="alipayServer"></param>
        /// <param name="result"></param>
        private void PostResult(AlipayWapTradeResponseBLL alipayServer, string result, string out_trade_no)
        {
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "out_trade_no", Value = out_trade_no });
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
    }
}