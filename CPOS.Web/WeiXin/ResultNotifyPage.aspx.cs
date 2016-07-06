using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.Utility.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.CPOS.Web.WeiXin
{
    public partial class ResultNotifyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.IO.Stream s = Context.Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            JIT.Utility.Log.Loggers.Debug(new DebugLogInfo() { Message = "请求:" + this.GetType().ToString() + "Receive data from WeChat : " + builder.ToString() });

            WxNativeNotify resultNotify = new WxNativeNotify();
            string strRsp = resultNotify.ProcessNotify(builder.ToString());

            Response.Write(strRsp);
            Response.Flush();

            if (!strRsp.Contains("SUCCESS"))
            {
                Response.End();
                return;
            }

            string url = ConfigurationManager.AppSettings["paymentcenterUrl"];
            string jsonString = string.Format("action=CreateOrder&request={{\"AppID\":{0},\"ClientID\":\"{1}\",\"UserID\":\"{2}\",\"Token\":null,\"Parameters\":{{\"PayChannelID\":{3},\"AppOrderID\":\"{4}\",\"AppOrderTime\":\"{5}\",\"AppOrderAmount\":{6},\"AppOrderDesc\":\"{7}\",\"Currency\":{8},\"MobileNO\":\"{9}\",\"ReturnUrl\":\"{10}\",\"DynamicID\":null,\"DynamicIDType\":null}}}}", 1,
                resultNotify.inoutEntity.customer_id, resultNotify.inoutEntity.create_user_id,
                resultNotify.paymentTypeEntity.ChannelId, resultNotify.inoutEntity.order_id,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"),
                TypeParse.ToInt(resultNotify.inoutEntity.total_amount * 100), "ScanWxPayOrder", "1", "", "");
            try
            {

                var rsp = CommonBLL.GetHttpResponse(url, jsonString, 5000);
                string message = "ResultNotifyPage 请求:" + url + "?" + jsonString + "；响应：" + rsp;
                JIT.Utility.Log.Loggers.Debug(new DebugLogInfo() { Message = message });
            }
            catch (Exception ex)
            {
                JIT.Utility.Log.Loggers.Debug(new DebugLogInfo() { Message = "ResultNotifyPage请求:" + this.GetType().ToString() + "::" + ex.Message + " param:" + url + "?" + jsonString });
            }
            Response.End();
        }
    }
}