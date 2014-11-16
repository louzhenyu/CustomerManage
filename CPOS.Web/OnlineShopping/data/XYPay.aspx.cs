using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.OnlineShopping.data
{
    public partial class XYPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("支付跳转: {0}", "come in")
            });
            if (!IsPostBack)
            {
                 SetPaymentInfo();
            }
        }

        private void SetPaymentInfo()
        {
            // 创建一个新的 WebClient 实例.
            WebClient myWebClient = new WebClient();

            try
            {
                string strError = string.Empty;
                //var loggingSessionInfo = Default.GetBSLoggingSession("29E11BDC6DAC439896958CC6866FF64E", "1");

                    
                //请求参数：
                //order_id：  订单号
                //prod_name：  产品名称
                //prod_price：  产品价格
                //merchant_url：  用户付款中途退出或者付款成功后返回URL
                //call_back_url：  交易结果返回URL（最终将会以POST方式推送交易结果到该地址）
                string webUrl = ConfigurationManager.AppSettings["website_url"];
                var uriString = webUrl + "AlipayWapTrade2/Trade.aspx";

                var postData = "order_id=123&prod_name=洗衣付款&prod_price=0.1&merchant_url=" + webUrl + "wap/xiyi/success.html&call_back_url=" + webUrl + "wap/xiyi/success.html";
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("支付跳转-链接字符串: {0}", postData)
                });
                // 注意这种拼字符串的ContentType
                myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                // 转化成二进制数组
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // 上传数据，并获取返回的二进制数据.
                byte[] responseArray = myWebClient.UploadData(uriString, "POST", byteArray);
                var data = System.Text.Encoding.UTF8.GetString(responseArray);
                Response.Write(data);
                strError = "ok";
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("支付跳转--出错: {0}", ex.ToString())
                });
            }
        }

    }
}