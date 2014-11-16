using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Order
{
    /// <summary>
    /// OrderGateway 的摘要说明
    /// </summary>
    public class OrderGateway : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {             
                case "SendWeixinMessage": //提交订单后推送微信消息
                    rst = this.SendWeixinMessage(pRequest);
                    break;

                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }

        public string SendWeixinMessage(string pRequest)
        {
            var rd = new EmptyResponseData();


            var rp = pRequest.DeserializeJSONTo<APIRequest<SendWeixinMessageRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var orderId = rp.Parameters.OrderId;

            var tInoutBll = new TInoutBLL(loggingSessionInfo);

            var tInoutEntity = tInoutBll.GetByID(orderId);

            if (tInoutEntity == null)
            {
                Loggers.Debug(new DebugLogInfo() { Message = orderId + "无效的订单ID;" });
                return rd.ToJSON();
            }
            var vipBll = new VipBLL(loggingSessionInfo);

            var vipEntity = vipBll.GetByID(rp.UserID);

            if (vipEntity == null)
            {
                Loggers.Debug(new DebugLogInfo() { Message = rp.UserID + "无效的会员ID;" });
                return rd.ToJSON();
            }


            var message = "亲爱的 " + vipEntity.VipName + "您好，感谢您选择花间堂！您的订单【" + tInoutEntity.OrderNo
                          + "】已收到，正在玩命确认中。您可以进入个人中心页面随时关注订单状态，如有疑问请致电4000 767 123。";

            string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, "1", loggingSessionInfo, vipEntity);

            Loggers.Debug(new DebugLogInfo() { Message = "消息推送完成，code=" + code + ", message=" + message });
            switch (code)
            {
                case "103":
                    Loggers.Debug(new DebugLogInfo() { Message = vipEntity.VipName + "未查询到匹配的公众账号信息;" });
                    break;
                case "203":
                    Loggers.Debug(new DebugLogInfo() { Message = vipEntity.VipName + "发送失败;" });
                    break;
                default:
                    break;
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
    }


    public class SendWeixinMessageRP : IAPIRequestParameter
    {
        public string OrderId { get; set; }

        public void Validate()
        {
        }
    }
}