using System;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL.CS
{
    /// <summary>
    /// 外发离线消息业务处理
    /// </summary>
    public class SendOfflineMessageBLL
    {
        private LoggingSessionInfo loggingSessionInfo;
        public SendOfflineMessageBLL(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }
        /// <summary>
        /// 外发
        /// </summary>
        public void Send()
        {
            CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
            var conversations = conversationBll.Query(new IWhereCondition[]
                {
                    new DirectCondition("IsPush=0 or IsPush is NULL") 
                }, new[]
                {
                    new OrderBy
                        {
                            FieldName = "CreateTime",
                            Direction = OrderByDirections.Desc
                        }
                });
            foreach (var conversationEntity in conversations)
            {
                CSMessageBLL messageBll = new CSMessageBLL(loggingSessionInfo);
                CSMessageEntity messageEntity = messageBll.GetByID(conversationEntity.CSMessageID);
                try
                {
                    switch (messageEntity.CSPipelineID)
                    {
                        //微信
                        case 1:
                            IPushMessage pushWXMessage = new PushWeiXinMessage(loggingSessionInfo);
                            pushWXMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
                            break;
                        //短信
                        case 2:
                            IPushMessage pushSMSMessage = new PushSMSMessage(loggingSessionInfo);
                            pushSMSMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
                            break;
                        //IOS
                        case 3:
                            IPushMessage pusIOSMessage = new PushIOSMessage(loggingSessionInfo);
                            pusIOSMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
                            break;
                        //Android
                        case 4:
                            IPushMessage pusAndroidMessage = new PushAndroidMessage(loggingSessionInfo);
                            pusAndroidMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
                            break;
                    }
                    //更新已经推送
                    conversationEntity.IsPush = 1;
                    conversationBll.Update(conversationEntity);
                }
                catch (Exception ex)
                {
                    Loggers.DEFAULT.Debug(new DebugLogInfo
                    {
                        Message = "离线消息推送错误：" + ex.Message + "|" + conversationEntity.PersonID + "/" + messageEntity.CSPipelineID + "/" + conversationEntity.ToJSON()
                    });
                }

            }
        }
    }
}
