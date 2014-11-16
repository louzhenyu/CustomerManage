using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.AlipayWapTrade;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.AlipayWapTrade
{
    /// <summary>
    /// 功能：页面跳转同步通知页面
    /// 
    /// ///////////////////////页面功能说明///////////////////////
    /// 
    /// 该页面称作“页面跳转同步通知页面”，是由支付宝服务器同步调用，可当作是支付完成后的提示信息页。
    /// 可放入HTML等美化页面的代码和订单交易完成后的数据库更新程序代码
    /// 该页面可以使用ASP.NET开发工具调试
    /// TRADE_FINISHED(表示交易已经成功结束);
    /// </summary>
    public partial class Call_Back : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseService.WriteLog("页面跳转同步通知页面-----------------------Call_Back.aspx");
            AlipayWapTradeResponseBLL alipayServer = new AlipayWapTradeResponseBLL(new Utility.BasicUserInfo());

            //获取签名
            string sign = Request["sign"];
            BaseService.WriteLog("签名sign：" + sign);

            //获取所有参数
            SortedDictionary<string, string> sArrary = GetRequestGet();

            BaseService.WriteLog("开始验证签名：");
            bool isVerify = Function.Verify(sArrary, sign, Config.Alipaypublick, Config.Input_charset_UTF8);
            BaseService.WriteLog("结束验证签名：");
            BaseService.WriteLog("验签结果：" + isVerify);

            if (!isVerify)
            {
                //验签出错，可能被别人篡改数据
                if (Request["out_trade_no"] != null)
                {
                    alipayServer.UpdateAlipayWapTradeStatus(Request["out_trade_no"], "4");
                    PostResult(alipayServer, "fail");

                    BaseService.WriteLog("验签出错，可能被别人篡改数据。");
                }

                Response.Write("fail");
                return;
            }

            string result = Request["result"];
            BaseService.WriteLog("result：" + result);

            if (!result.Equals("success"))
            {
                //交易失败
                if (Request["out_trade_no"] != null)
                {
                    alipayServer.UpdateAlipayWapTradeStatus(Request["out_trade_no"], "3");
                    PostResult(alipayServer, "fail");

                    BaseService.WriteLog("交易失败，更新支付宝交易状态");
                }

                Response.Write("fail");
                return;
            }
            else //交易成功，请填写自己的业务代码
            {
                if (Request["out_trade_no"] != null)
                {
                    BaseService.WriteLog("out_trade_no：" + Request["out_trade_no"]);
                    BaseService.WriteLog("交易成功");

                    alipayServer.UpdateAlipayWapTradeStatus(Request["out_trade_no"], "2");
                    PostResult(alipayServer, "success");
                }
                else
                {
                    BaseService.WriteLog("out_trade_no is null!!!!! ");
                }

                Response.Redirect(Config.Merchant_url);
            }
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
        /// 获取所有Callback参数
        /// </summary>
        /// <returns>SortedDictionary格式参数</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            string query = HttpContext.Current.Request.Url.Query.Replace("?", "");
            if (!string.IsNullOrEmpty(query))
            {
                string[] coll = query.Split('&');

                string[] temp = { };

                for (int i = 0; i < coll.Length; i++)
                {
                    temp = coll[i].Split('=');

                    sArray.Add(temp[0], temp[1]);
                    BaseService.WriteLog(temp[0] + "：" + temp[1]);
                }
            }
            return sArray;
        }
    }
}
