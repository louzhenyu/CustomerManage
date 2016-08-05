using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class DelActivityAH : BaseActionHandler<GetActivityDeatilRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<GetActivityDeatilRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var ActivityBLL = new C_ActivityBLL(loggingSessionInfo);
            var ActivityMessageBLL = new C_ActivityMessageBLL(loggingSessionInfo);
            var ActivityTargetGroupBLL = new C_TargetGroupBLL(loggingSessionInfo);
            var ActivityPrizesBLL = new C_PrizesBLL(loggingSessionInfo);
            var ActivityPrizesDetailBLL = new C_PrizesDetailBLL(loggingSessionInfo);
            var ActivityRechargeStrategyBLL = new RechargeStrategyBLL(loggingSessionInfo);
            var pTran = ActivityBLL.GetTran();
            using (pTran.Connection)
            {
                try
                {
                    //删除
                    C_ActivityEntity DelData = ActivityBLL.GetByID(para.ActivityID);
                    if (DelData == null)
                    {
                        throw new APIException("会员活动对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }
                    //执行
                    List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                    complexCondition.Add(new EqualsCondition() { FieldName = "ActivityID", Value = para.ActivityID });
                    ActivityMessageBLL.Delete(ActivityMessageBLL.Query(complexCondition.ToArray(), null), pTran);
                    ActivityTargetGroupBLL.Delete(ActivityTargetGroupBLL.Query(complexCondition.ToArray(), null), pTran);
                    ActivityRechargeStrategyBLL.Delete(ActivityRechargeStrategyBLL.Query(complexCondition.ToArray(), null), pTran);
                    var prize = ActivityPrizesBLL.Query(complexCondition.ToArray(), null).FirstOrDefault();
                    if (prize != null)
                    {
                        if (!string.IsNullOrWhiteSpace(prize.PrizesID.ToString()))
                        {
                            List<IWhereCondition> cCondition = new List<IWhereCondition> {};
                            cCondition.Add(new EqualsCondition() {FieldName = "PrizesID", Value = prize.PrizesID});
                            ActivityPrizesDetailBLL.Delete(ActivityPrizesDetailBLL.Query(cCondition.ToArray(), null),
                                pTran);
                        }
                        ActivityPrizesBLL.Delete(ActivityPrizesBLL.Query(complexCondition.ToArray(), null), pTran);
                    }
                    ActivityBLL.Delete(DelData, pTran);
                    pTran.Commit();
                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
            }
            return rd;
        }
    }
}