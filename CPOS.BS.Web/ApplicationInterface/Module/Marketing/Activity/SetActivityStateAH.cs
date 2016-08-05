using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Activity.Request;
using JIT.CPOS.DTO.Module.Marketing.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class SetActivityStateAH : BaseActionHandler<GetActivityDeatilRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<GetActivityDeatilRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var ActivityBLL = new C_ActivityBLL(loggingSessionInfo);
            try
            {
                C_ActivityEntity Data = ActivityBLL.GetByID(para.ActivityID);
                if (Data == null)
                    throw new APIException("营销活动对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                if (Data.Status == 0)
                {
                    Data.Status = 1;
                }
                else 
                {
                    Data.Status = 0;
                }
                ActivityBLL.Update(Data);
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }
            return rd;
        }
    }
}