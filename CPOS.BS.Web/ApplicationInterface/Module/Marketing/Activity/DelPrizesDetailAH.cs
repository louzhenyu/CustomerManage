using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Activity.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class DelPrizesDetailAH : BaseActionHandler<DelPrizesDetailRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<DelPrizesDetailRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var PrizesDetailBLL = new C_PrizesDetailBLL(loggingSessionInfo);
            var ActivityBLL = new C_ActivityBLL(loggingSessionInfo);

            try
            {
                //删除
                C_PrizesDetailEntity DelData = PrizesDetailBLL.GetByID(para.PrizesDetailID);
                if (DelData == null)
                {
                    throw new APIException("奖品明细对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
                //执行
                PrizesDetailBLL.Delete(DelData);
                if (ActivityBLL.IsActivityValid(para.ActivityID))
                {
                    var activity = ActivityBLL.GetByID(para.ActivityID);
                    if (activity != null)
                    {
                        activity.Status = 0;
                    }
                    ActivityBLL.Update(activity);
                }
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }

            return rd;
        }
    }
}