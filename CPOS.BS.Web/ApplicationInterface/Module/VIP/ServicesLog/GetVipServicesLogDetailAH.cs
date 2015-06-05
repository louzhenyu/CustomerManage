using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Request;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.ServicesLog
{
    public class GetVipServicesLogDetailAH : BaseActionHandler<SetVipServicesLogRP, GetVipServicesLogDetailRD>
    {
        protected override GetVipServicesLogDetailRD ProcessRequest(DTO.Base.APIRequest<SetVipServicesLogRP> pRequest)
        {
            var rd = new GetVipServicesLogDetailRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipServicesLogBLL = new VipServicesLogBLL(loggingSessionInfo);
            VipServicesLogEntity servicesLog = vipServicesLogBLL.GetByID(para.ServicesLogID);
            if (servicesLog != null)
            {
                rd.ServicesLogID = servicesLog.ServicesLogID.ToString();
                rd.VipID = servicesLog.VipID;
                rd.ServicesTime = servicesLog.ServicesTime==DateTime.MinValue?"":servicesLog.ServicesTime.Value.ToString("yyyy-MM-dd hh:mm");

                rd.ServicesMode = servicesLog.ServicesMode;
                rd.UnitID = servicesLog.UnitID;
                rd.UnitName = servicesLog.UnitName;
                rd.UserID = servicesLog.UserID;
                rd.UserName = servicesLog.UserName;
                rd.Content = servicesLog.Content;
                rd.ServicesType = servicesLog.ServicesType.Value;
                rd.Duration = servicesLog.Duration;
            }
            return rd;
        }
    }
}