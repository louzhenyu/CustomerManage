using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Logistics.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.Logistics
{
    public class SetLogisticsCompanyAH : BaseActionHandler<SetLogisticsCompanyRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetLogisticsCompanyRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var logisticsBLL = new T_LogisticsCompanyBLL(loggingSessionInfo);
            var logisticsSettingBLL = new T_LogisticsSettingBLL(loggingSessionInfo);

            T_LogisticsCompanyEntity logisticsCompany = null;
            T_LogisticsSettingEntity logisticsSetting = null;

            switch (para.OperationType)
            {
                case 1://添加配送商
                    logisticsSetting = new T_LogisticsSettingEntity()
                    {
                        LogisticsID =new Guid(para.LogisticsID),
                        CustomerID = loggingSessionInfo.ClientID
                    };
                    logisticsSettingBLL.Create(logisticsSetting);
                    break;
                case 2://新增配送商

                    //创建配送商
                    logisticsCompany = new T_LogisticsCompanyEntity()
                    {
                        LogisticsName = para.LogisticsName,
                        LogisticsShortName = para.LogisticsShortName,
                        IsSystem = para.IsSystem
                    };
                    logisticsBLL.Create(logisticsCompany);
                    //绑定到商户
                    logisticsSetting = new T_LogisticsSettingEntity()
                   {
                       LogisticsID = logisticsCompany.LogisticsID,
                       CustomerID = loggingSessionInfo.ClientID
                   };
                    logisticsSettingBLL.Create(logisticsSetting);
                    break;

                case 3://移除
                    logisticsSetting = logisticsSettingBLL.QueryByEntity(new T_LogisticsSettingEntity() { CustomerID = loggingSessionInfo.ClientID, LogisticsID =new Guid(para.LogisticsID) },null).FirstOrDefault();
                    if (logisticsSetting != null)
                        logisticsSettingBLL.Delete(logisticsSetting);
                    break;
                default:
                    break;
            }
            return rd;
        }
    }
}