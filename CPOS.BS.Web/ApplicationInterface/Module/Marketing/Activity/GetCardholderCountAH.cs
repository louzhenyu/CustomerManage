using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Marketing.Request;
using JIT.CPOS.DTO.Module.Marketing.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class GetCardholderCountAH : BaseActionHandler<GetCardholderCountRP, GetCardholderCountRD>
    {
        protected override GetCardholderCountRD ProcessRequest(DTO.Base.APIRequest<GetCardholderCountRP> pRequest)
        {
            var rd = new GetCardholderCountRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var ActivityBLL = new C_ActivityBLL(loggingSessionInfo);

            rd.Count = ActivityBLL.GetTargetCount(para.VipCardTypeIDList, para.ActivityType, para.StartTime,
                para.EndTime, para.IsLongTime);

            return rd;
        }
    }
}