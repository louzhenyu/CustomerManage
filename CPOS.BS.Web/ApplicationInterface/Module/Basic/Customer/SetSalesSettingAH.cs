using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Basic.Customer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Customer
{
    public class SetSalesSettingAH : BaseActionHandler<SetSalesSettingRP, EmptyResponseData>
    {


        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetSalesSettingRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);
            ResponseData res = new ResponseData();
            List<CustomerBasicSettingEntity> list = new List<CustomerBasicSettingEntity>();
              list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "SocialSalesType",
                SettingValue = para.SocialSalesType.ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "EnableEmployeeSales",
                SettingValue = para.EnableEmployeeSales.ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "EDistributionPricePer",
                SettingValue = para.EDistributionPricePer.ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "EOrderCommissionPer",
                SettingValue = para.EOrderCommissionPer.ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "EnableVipSales",
                SettingValue = para.EnableVipSales.ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "VDistributionPricePer",
                SettingValue = para.VDistributionPricePer.ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "VOrderCommissionPer",
                SettingValue = para.VOrderCommissionPer.ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "GetVipUserOrderPer",
                SettingValue = para.GetVipUserOrderPer.ToString()
            });
            int i = customerBasicSettingBLL.SaveustomerBasicrInfo(list);

            return rd;
        }
    }
}