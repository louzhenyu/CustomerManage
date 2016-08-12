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
    public class MultiCompleteAH : BaseActionHandler<MultiCompleteRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<MultiCompleteRP> pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_REQUEST };
            VipWithdrawDepositApplyBLL bll = new VipWithdrawDepositApplyBLL(CurrentUserInfo);
            var result = bll.MultiComplete(pRequest.Parameters.Ids);
            if (!result)
            {
                throw new APIException(ERROR_CODES.INVALID_BUSINESS, "操作失败");
            }
            else
            {
                return new EmptyResponseData();
            }
        }
    }
}