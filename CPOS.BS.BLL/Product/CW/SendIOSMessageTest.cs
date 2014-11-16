using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using JIT.Utility.Log;
using JIT.Utility.Message;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.BLL.Product.CW
{
    public class SendIOSMessageTest
    {
        public void Send(string userID, int channelId, string deviceToken, string messageContent)
        {
            string method = "Process";
            //IOS消息推送
          //  string url = "http://121.199.42.125:9000/PushService.svc";
            string url = System.Configuration.ConfigurationManager.AppSettings["pushMessageUrl"];
            //if (!string.IsNullOrEmpty(userBasic.Channel))
            //{
            //    channelId = Convert.ToInt32(userBasic.Channel);
            //}
            PushRequest pRequest2 = RequestBuilder.CreateIOSUnicastNotificationRequest(1, channelId, deviceToken, messageContent);
            var json = "{\"pRequest\":" + pRequest2.ToJSON() + "}";
            var response2 = SendHttpRequest(url, method, json);
            var msg = "会员ID：" + userID + " DeviceToken：" + deviceToken;
            Loggers.DEFAULT.Debug(new DebugLogInfo
            {
                Message = msg + "  消息发送结果： " + response2
            });
        }

        public static string SendHttpRequest(string requestURI, string requestMethod, string json)
        {
            //json格式请求数据
            string requestData = json;
            //拼接URL
            string serviceUrl = string.Format("{0}/{1}", requestURI, requestMethod);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //post请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            myRequest.Accept = "application/json";
            myRequest.ContentType = "application/json";

            //Content-type: application/json; charset=utf-8

            //myRequest.ContentType = "text/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }
    }
}
