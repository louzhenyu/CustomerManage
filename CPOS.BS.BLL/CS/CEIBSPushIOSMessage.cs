using System;
using System.IO;
using System.Net;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Message;
using JIT.Utility.Message.IOS;
using JIT.CPOS.BS.DataAccess;
using System.Data.SqlClient;
using System.Threading;
using System.Diagnostics;

namespace JIT.CPOS.BS.BLL.CS
{
    public class CEIBSPushIOSMessage : IPushMessageCommon
    {
        public void PushMessage(LoggingSessionInfo loggingSessionInfo, dynamic paramter)
        {
            DynamicInterfaceBLL dynamicInterfaceBLL = new DynamicInterfaceBLL(loggingSessionInfo);
            var eventList = dynamicInterfaceBLL.getEventList(new ReqData<getActivityListEntity>() { common = new ReqCommonData() { customerId = loggingSessionInfo.ClientID }, special = new getActivityListEntity() { type = "new", page = 1, pageSize = 5 } });

            if (eventList.ItemList != null && eventList.ItemList.Length > 0)
            {
                int channelID = 0;
                string message = "";

                PrepareMessage(loggingSessionInfo, paramter, out channelID, out message);

                PushUserBasicBLL pushUserBasicBLL = new PushUserBasicBLL(loggingSessionInfo);
                PushUserBasicEntity[] pushUserBasicEntity = pushUserBasicBLL.Query(new JIT.Utility.DataAccess.Query.IWhereCondition[] { 
                    new JIT.Utility.DataAccess.Query.EqualsCondition() { FieldName="IsDelete", Value="0" }
                    , new JIT.Utility.DataAccess.Query.EqualsCondition() { FieldName="CustomerId", Value=loggingSessionInfo.ClientID }
                    , new JIT.Utility.DataAccess.Query.DirectCondition() { Expression="Plat<>'Android'" }
                }, null);

                string json = "";

                foreach (var item in pushUserBasicEntity)
                {
                    PushRequest pRequest2 = RequestBuilder.CreateIOSUnicastNotificationRequest(1, channelID, item.DeviceToken, message);
                    var request = pRequest2.Request.DeserializeJSONTo<JdSoft.Apple.Apns.Notifications.Notification>();
                    request.Payload.CustomItems.Add("EventUrl", new string[] { "http://o2oapi.aladingyidong.com/HtmlApps/html/public/xiehuibao/activity.html?customerId=" + loggingSessionInfo.ClientID + "&type=new" });
                    pRequest2.Request = request.ToJSON();
                    json = "{\"pRequest\":" + pRequest2.ToJSON() + "}";

                    ThreadPool.QueueUserWorkItem(new WaitCallback(SendHttpRequest), json);
                }

                //var response2 = SendHttpRequest(url, method, json);
            }
            else
            {
                SendMarketingMessageBLL.log.WriteEntry("无新活动", System.Diagnostics.EventLogEntryType.Information);
            }
        }

        public static void PrepareMessage(LoggingSessionInfo loggingSessionInfo, dynamic paramter, out int channelID, out string message)
        {
            channelID = 0;
            message = "";
            TimingPushMessageRuleEntity timingPushMessageRuleEntity = (TimingPushMessageRuleEntity)paramter;
            if (timingPushMessageRuleEntity.TimingPushMessageRuleID.HasValue)
            {
                String sql = @"SELECT tpm.ChannelID, pim.MessageText FROM [TimingPushMessageRuleModelMapping] tpmrmm  
                                LEFT JOIN [TimingPushMessage] tpm ON tpmrmm.TimingPushMessageRuleModelMappingID = tpm.TimingPushMessageRuleModelMappingID
                                LEFT JOIN PushIOSMessage pim ON tpm.ObjectID = pim.IOSMessageID
                            WHERE TimingPushMessageRuleID='{0}' AND tpm.ClientID = '{1}'";

                TimingPushMessageDAO timingPushMessageDAO = new TimingPushMessageDAO(loggingSessionInfo);
                SqlDataReader sqlDataReader = timingPushMessageDAO.ExcuteSQL(string.Format(sql, timingPushMessageRuleEntity.TimingPushMessageRuleID.ToString(), loggingSessionInfo.ClientID));

                if (sqlDataReader.Read())
                {
                    channelID = int.Parse(sqlDataReader["ChannelID"].ToString());
                    message = sqlDataReader["MessageText"].ToString();
                }
            }
        }

        public static void SendHttpRequest(object json)
        {
            //IOS消息推送
          //  string requestURI = "http://121.199.42.125:9000/PushService.svc";
            string requestURI = System.Configuration.ConfigurationManager.AppSettings["pushMessageUrl"];
            //string requestURI = "http://localhost:1475/PushService.svc";

            //json格式请求数据
            string requestData = (string)json;
            //拼接URL
            string serviceUrl = string.Format("{0}/{1}", requestURI, "Process");
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

        }

    }
}
