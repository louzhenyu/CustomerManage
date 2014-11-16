using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Message;
using JIT.Utility.Message.Baidu.ValueObject;
using System;
using System.Data.SqlClient;
using JIT.Utility.Message.Baidu;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace JIT.CPOS.BS.BLL.CS
{
    public class CEIBSPushAndroidMessage : IPushMessageCommon
    {
        public static void PrepareMessage(LoggingSessionInfo loggingSessionInfo, dynamic paramter, out int channelID, out string message)
        {
            channelID = 0;
            message = "";
            TimingPushMessageRuleEntity timingPushMessageRuleEntity = (TimingPushMessageRuleEntity)paramter;
            if (timingPushMessageRuleEntity.TimingPushMessageRuleID.HasValue)
            {
                String sql = @"SELECT tpm.ChannelID, pam.Message FROM [TimingPushMessageRuleModelMapping] tpmrmm  
                                LEFT JOIN [TimingPushMessage] tpm ON tpmrmm.TimingPushMessageRuleModelMappingID = tpm.TimingPushMessageRuleModelMappingID
                                LEFT JOIN PushAndroidMessage pam ON tpm.ObjectID = pam.AndroidMessageID
                            WHERE TimingPushMessageRuleID='{0}' AND tpm.ClientID = '{1}'";

                TimingPushMessageDAO timingPushMessageDAO = new TimingPushMessageDAO(loggingSessionInfo);
                SqlDataReader sqlDataReader = timingPushMessageDAO.ExcuteSQL(string.Format(sql, timingPushMessageRuleEntity.TimingPushMessageRuleID.ToString(), loggingSessionInfo.ClientID));

                if (sqlDataReader.Read())
                {
                    channelID = int.Parse(sqlDataReader["ChannelID"].ToString());
                    message = sqlDataReader["Message"].ToString();
                }
            }
        }

        public void PushMessage(LoggingSessionInfo loggingSessionInfo, dynamic paramter)
        {
            DynamicInterfaceBLL dynamicInterfaceBLL = new DynamicInterfaceBLL(loggingSessionInfo);
            var eventList = dynamicInterfaceBLL.getEventList(new ReqData<getActivityListEntity>() { common = new ReqCommonData() { customerId = loggingSessionInfo.ClientID }, special = new getActivityListEntity() { type = "new", page = 1, pageSize = 5 } });

            if (eventList.ItemList != null && eventList.ItemList.Length > 0)
            {
                int channelID = 0;
                string message = "";

                PrepareMessage(loggingSessionInfo, paramter, out channelID, out message);

                if (channelID > 0 && !string.IsNullOrEmpty(message))
                {
                    string method = "Process";
                    //Android消息推送
                  //  string url = "http://121.199.42.125:9000/PushService.svc";
                    string url = System.Configuration.ConfigurationManager.AppSettings["pushMessageUrl"];
                    //string url = "http://localhost:1475/PushService.svc";

                    //3488619   为中欧校友汇在百度推送中的ID
                    message += "#http://url.cn/TzrnuO"; //短链接
                    //http://dev.o2omarketing.cn:9004/HtmlApps/html/public/xiehuibao/activity.html?customerId=75a232c2cf064b45b1b6393823d2431e&type=new";
                    PushRequest pRequest2 = RequestBuilder.CreateAndroidUnicastMessageRequest(1, channelID, "", "3488619", message, PushTypes.Broadcast);

                    var json = "{\"pRequest\":" + pRequest2.ToJSON() + "}";
                    var response2 = PushIOSMessage.SendHttpRequest(url, method, json);
                }
            }
            else
            {
                SendMarketingMessageBLL.log.WriteEntry("无新活动", System.Diagnostics.EventLogEntryType.Information);
            }
        }
    }
}
