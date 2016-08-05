using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.WithdrawDeposit
{
    public class SetVipWithDrawRuleAH : BaseActionHandler<SetVipWithDrawRuleRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<SetVipWithDrawRuleRP> pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_REQUEST };
            VipWithDrawRuleBLL bll = new VipWithDrawRuleBLL(loggingSessionInfo);
            if (!bll.SetVipWithDrawRule(pRequest.Parameters))
            {
                throw new APIException(ERROR_CODES.INVALID_BUSINESS, "发生错误");
            }
            return new EmptyResponseData();
        }
    }
}