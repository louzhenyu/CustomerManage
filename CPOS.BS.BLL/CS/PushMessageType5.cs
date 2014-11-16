using System;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL.CS
{
    /// <summary>
    /// 推送客服消息类型为5的消息，微信优先、短信其次
    /// </summary>
    public class PushMessageType5
    {
        private LoggingSessionInfo loggingSessionInfo;
        public PushMessageType5(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }
        public void PushMessage(CSConversationEntity conversationEntity)
        {
            try
            {
                IPushMessage pushWeixinMessage = new PushWeiXinMessage(loggingSessionInfo);
                pushWeixinMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
            }
            catch (Exception)
            {
                IPushMessage pushSMSMessage = new PushSMSMessage(loggingSessionInfo);
                pushSMSMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
            }
        }
    }
}
