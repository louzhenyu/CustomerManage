using System;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL.CS
{
    /// <summary>
    /// 推送微信消息
    /// </summary>
    public class PushWeiXinMessage : IPushMessage
    {
        private LoggingSessionInfo loggingSessionInfo;
        public PushWeiXinMessage(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }
        public void PushMessage(string memberID, string messageContent)
        {
            WApplicationInterfaceEntity[] wApplicationInterfaceEntities = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity { CustomerId = loggingSessionInfo.ClientID }, null);
            if (wApplicationInterfaceEntities.Length > 0)
            {
                VipEntity vipEntity = new VipBLL(loggingSessionInfo).GetByID(memberID);


                if (string.IsNullOrEmpty(vipEntity.WeiXinUserId))
                {
                    throw new Exception("VipID:" + vipEntity.VIPID + "没有关注公众号");
                }

                if (new CSPublicInterface(loggingSessionInfo).IsWeixinCSMessageActiveUser(memberID))
                {
                    CommonBLL commonBll = new CommonBLL();
                    SendMessageEntity messageEntity = new SendMessageEntity();
                    messageEntity.content = messageContent;
                    messageEntity.touser = vipEntity.WeiXinUserId;
                    messageEntity.msgtype = "text";
                    commonBll.SendMessage(messageEntity, wApplicationInterfaceEntities[0].AppID,
                                          wApplicationInterfaceEntities[0].AppSecret, loggingSessionInfo);
                }
                else
                {
                    throw new Exception("VipID:" + vipEntity.VIPID + "已经超过48小时有效期");
                }
            }


        }
    }
}
