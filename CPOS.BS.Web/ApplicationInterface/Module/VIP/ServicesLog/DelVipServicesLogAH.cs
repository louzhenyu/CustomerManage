using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.ServicesLog
{
    public class DelVipServicesLogAH : BaseActionHandler<SetVipServicesLogRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetVipServicesLogRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipServicesLogBLL = new VipServicesLogBLL(loggingSessionInfo);
            VipServicesLogEntity servicesLog = vipServicesLogBLL.GetByID(para.ServicesLogID);
            if (servicesLog != null)
                vipServicesLogBLL.Delete(servicesLog);
            return rd;
        }
    }
}