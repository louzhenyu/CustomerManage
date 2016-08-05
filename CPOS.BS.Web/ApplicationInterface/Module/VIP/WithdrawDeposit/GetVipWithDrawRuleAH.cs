using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.WithdrawDeposit
{
    /// <summary>
    /// 提现规则显示接口
    /// </summary>
    public class GetVipWithDrawRuleAH : BaseActionHandler<EmptyRequestParameter, GetVipWithDrawRuleRD>
    {

        protected override GetVipWithDrawRuleRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_REQUEST };
            VipWithDrawRuleBLL bll = new VipWithDrawRuleBLL(loggingSessionInfo);
            var dbEntity = bll.GetVipWithDrawRule();
            return TransModel(dbEntity);
        }
        /// <summary>
        /// 模型转换
        /// </summary>
        public GetVipWithDrawRuleRD TransModel(VipWithDrawRuleEntity dbEntity)
        {
            GetVipWithDrawRuleRD result = new GetVipWithDrawRuleRD();
            if (dbEntity == null)
                return result;
            result.BeforeWithDrawDays = dbEntity.BeforeWithDrawDays;
            result.MinAmountCondition = dbEntity.MinAmountCondition;
            result.WithDrawMaxAmount = dbEntity.WithDrawMaxAmount;
            result.WithDrawNum = dbEntity.WithDrawNum;
            return result;
        }
    }
}