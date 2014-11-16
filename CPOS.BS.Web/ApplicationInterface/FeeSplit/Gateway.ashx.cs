using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.ApplicationInterface.FeeSplit
{
    /// <summary>
    /// Gateway 的摘要说明
    /// </summary>
    public class Gateway : BaseGateway
    {
        /// <summary>
        /// 处理会员与分润关系
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string SetVipOrderSubRun(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<VipOrderSubRunRP>>();
            var p = rp.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
#if DEBUG
            loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
#endif
            var bll = new VipOrderSubRunObjectMappingBLL(loggingSessionInfo);
            dynamic o = bll.SetVipOrderSubRun(loggingSessionInfo.ClientID, p.VipId, p.SubRunObjectId, p.SubRunObjectValue);
            Type t = o.GetType();
            var Desc = t.GetProperty("Desc").GetValue(o, null).ToString();
            var IsSuccess = t.GetProperty("IsSuccess").GetValue(o, null).ToString();
            var rd = new FeeSplitRD { Desc = Desc, IsSuccess = IsSuccess };
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 处理订单与分润关系
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string SetOrder2OrderSubRun(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<Order2OrderSubRunRP>>();
            var p = rp.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
#if DEBUG
            loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
#endif
            var bll = new OrderOrderSubRunObjectMappingBLL(loggingSessionInfo);
            dynamic o = bll.SetOrderSub(loggingSessionInfo.ClientID,p.OrderId);
            Type t = o.GetType();
            var Desc = t.GetProperty("Desc").GetValue(o, null).ToString();
            var IsSuccess = t.GetProperty("IsSuccess").GetValue(o, null).ToString();
            var rd = new FeeSplitRD { Desc = Desc, IsSuccess = IsSuccess };
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction.Trim().ToLower())
            {
                case "setvipordersubrun":
                    rst = SetVipOrderSubRun(pRequest);
                    break;
                case "setorder2ordersubrun":
                    rst = SetOrder2OrderSubRun(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
    }
    public class VipOrderSubRunRP : IAPIRequestParameter
    {
        public string VipId { get; set; }
        public int SubRunObjectId { get; set; }
        public string SubRunObjectValue { get; set; }


        public void Validate()
        {
            
        }
    }
    public class Order2OrderSubRunRP : IAPIRequestParameter
    {
        public string OrderId { get; set; }
        public void Validate()
        {
            
        }
    }
    public class FeeSplitRD : IAPIResponseData
    {
        public string IsSuccess { get; set; }
        public string Desc { get; set; }
    }
}