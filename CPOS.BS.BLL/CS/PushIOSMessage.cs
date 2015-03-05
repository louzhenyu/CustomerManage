using System;
using System.IO;
using System.Net;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Message;
using JIT.Utility.Message.IOS;

namespace JIT.CPOS.BS.BLL.CS
{
    public class PushIOSMessage : IPushMessage
    {
        private LoggingSessionInfo loggingSessionInfo;
        public PushIOSMessage(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }
        public void PushMessage(string memberID, string messageContent)
        {
            PushUserBasicBLL pushUserBasicBll = new PushUserBasicBLL(loggingSessionInfo);
            
            Loggers.Debug(new DebugLogInfo() { Message = "memberID:" + memberID });

            //Updated by Willie Yan on 2014-05-09   同一账号在多个设备登录，应当取时间最新的
            var userBasicArray = pushUserBasicBll.QueryByEntity(
                new PushUserBasicEntity() { UserId = memberID }
                , new JIT.Utility.DataAccess.Query.OrderBy[] { new JIT.Utility.DataAccess.Query.OrderBy() { FieldName = "LastUpdateTime", Direction = JIT.Utility.DataAccess.Query.OrderByDirections.Desc } });

            PushUserBasicEntity userBasic = null;
            if (userBasicArray.Length > 0)
                userBasic = userBasicArray[0];
            else
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("没有找到该memberID:" + memberID) });

            /*没有DeviceToken时，记录日志，不直接抛出异常 qianzhi  2014-03-14
            if (userBasic == null)
            {
                throw new Exception("VipID:" + memberID + "没有保存DeviceToken");
            }
            if (!string.IsNullOrEmpty(userBasic.DeviceToken))
            {
                // IOSNotificationService.Default.SendNotification(IOSNotificationBuilder.CreateNotification(userBasic.DeviceToken, messageContent));
                string method = "Process";
                //IOS消息推送
                string url = "http://121.199.42.125:9000/PushService.svc";
                PushRequest pRequest2 = RequestBuilder.CreateIOSUnicastNotificationRequest(1, 2, userBasic.DeviceToken, messageContent);
                var json = "{\"pRequest\":" + pRequest2.ToJSON() + "}";
                var response2 = SendHttpRequest(url, method, json);
                Loggers.DEFAULT.Debug(new DebugLogInfo
                    {
                        Message = "消息发送结果" + response2
                    });
            }
            else
            {
                throw new Exception("VipID:" + userBasic.UserId + "没有获取到DeviceToken");
            }
            */
            if (userBasic != null)
            {
                Loggers.Debug(new DebugLogInfo() { Message = "推送客服， userBasic:" + userBasic.ToJSON() });
                if (!string.IsNullOrEmpty(userBasic.DeviceToken))
                {
                    // IOSNotificationService.Default.SendNotification(IOSNotificationBuilder.CreateNotification(userBasic.DeviceToken, messageContent));
                    string method = "Process";
                    //IOS消息推送
                    //string url = "http://121.199.42.125:9000/PushService.svc";
                    string url = System.Configuration.ConfigurationManager.AppSettings["pushMessageUrl"];
                    int channelId = 2;
                    if (!string.IsNullOrEmpty(userBasic.Channel))
                    {
                        channelId = Convert.ToInt32(userBasic.Channel);
                    }
                    PushRequest pRequest2 = RequestBuilder.CreateIOSUnicastNotificationRequest(1, channelId, userBasic.DeviceToken, messageContent);
                    var json = "{\"pRequest\":" + pRequest2.ToJSON() + "}";
                    var response2 = SendHttpRequest(url, method, json);
                    var msg = "会员ID：" + memberID + " DeviceToken：" + userBasic.DeviceToken;
                    Loggers.DEFAULT.Debug(new DebugLogInfo
                    {
                        Message = msg + "  消息发送结果： " + response2
                    });
                }
                else
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("PushMessage: {0}", "VipID:" + memberID + "没有保存DeviceToken")
                    });
                }
            }
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
