using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Message;
using System;

namespace JIT.CPOS.BS.BLL.CS
{
    public class PushAndroidMessage : IPushMessage
    {
        private LoggingSessionInfo loggingSessionInfo;
        public PushAndroidMessage(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }
        public void PushMessage(string memberID, string messageContent)
        {
            PushAndroidBasicBLL pushBll = new PushAndroidBasicBLL(loggingSessionInfo);
            var userBasic = pushBll.GetByID(memberID);

            if (userBasic != null)
            {
                if (!string.IsNullOrEmpty(userBasic.UserIDBaiDu) && !string.IsNullOrEmpty(userBasic.ChannelIDBaiDu))
                {
                    string method = "Process";
                    //Android消息推送
                    //string url = "http://121.199.42.125:9000/PushService.svc";
                    string url = System.Configuration.ConfigurationManager.AppSettings["pushMessageUrl"];
                    int channelId = 1;
                    if (!string.IsNullOrEmpty(userBasic.Channel))
                    {
                        channelId = Convert.ToInt32(userBasic.Channel);
                    }
                    PushRequest pRequest2 = RequestBuilder.CreateAndroidUnicastNotificationRequest(1, channelId, userBasic.UserIDBaiDu, userBasic.ChannelIDBaiDu, "消息", messageContent);
                    var json = "{\"pRequest\":" + pRequest2.ToJSON() + "}";
                    var response2 = PushIOSMessage.SendHttpRequest(url, method, json);
                    var msg = "会员ID：" + memberID + " DeviceToken：" + userBasic.DeviceToken;
                    Loggers.DEFAULT.Debug(new DebugLogInfo
                    {
                        Message = msg + "  消息发送Android结果： " + response2
                    });
                }
                else
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("PushMessage: {0}", "VipID:" + memberID + "没有保存百度推送消息参数")
                    });
                }
            }
        }
    }
}
