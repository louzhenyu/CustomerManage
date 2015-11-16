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
    public class SetVipServicesLogAH : BaseActionHandler<SetVipServicesLogRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetVipServicesLogRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipServicesLogBLL = new VipServicesLogBLL(loggingSessionInfo);
            VipServicesLogEntity servicesLog = null;
            if (string.IsNullOrEmpty(para.ServicesLogID))//创建
            {
                servicesLog = new VipServicesLogEntity();
                if (!string.IsNullOrEmpty(para.ServicesTime))
                    servicesLog.ServicesTime = DateTime.Parse(para.ServicesTime);
                servicesLog.VipID = para.VipID;
                servicesLog.ServicesMode = para.ServicesMode;
                servicesLog.UnitID = loggingSessionInfo.CurrentUserRole.UnitId;
                servicesLog.UserID = loggingSessionInfo.UserID;
                //servicesLog.UserID = para.UserID;
                servicesLog.ServicesType = para.ServicesType;
                servicesLog.Duration = para.Duration;
                servicesLog.Content = para.Content;
                servicesLog.CustomerID = loggingSessionInfo.ClientID;
                vipServicesLogBLL.Create(servicesLog);
            }
            else//编辑
            {
                servicesLog = vipServicesLogBLL.GetByID(new Guid(para.ServicesLogID));
                if (servicesLog != null)
                {
                    if (!string.IsNullOrEmpty(para.ServicesTime))
                        servicesLog.ServicesTime = DateTime.Parse(para.ServicesTime);
                    if (!string.IsNullOrEmpty(para.VipID))
                        servicesLog.VipID = para.VipID;
                    if (!string.IsNullOrEmpty(para.ServicesMode))
                        servicesLog.ServicesMode = para.ServicesMode;
                    //if (!string.IsNullOrEmpty(para.UnitID))
                    //    servicesLog.UnitID = para.UnitID;
                    //servicesLog.UserID = loggingSessionInfo.UserID;
                    //servicesLog.UserID = para.UserID;
                    if (para.ServicesType > 0)
                        servicesLog.ServicesType = para.ServicesType;
                    if (!string.IsNullOrEmpty(para.Duration))
                        servicesLog.Duration = para.Duration;
                    if (!string.IsNullOrEmpty(para.Content))
                        servicesLog.Content = para.Content;
                    vipServicesLogBLL.Update(servicesLog);
                }
            }
            return rd;
        }

    }
}