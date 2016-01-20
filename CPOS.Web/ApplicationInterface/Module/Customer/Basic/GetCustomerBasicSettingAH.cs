using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.Basic.Customer.Request;
using JIT.CPOS.DTO.Module.Basic.Customer.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Customer.Basic
{
    public class GetCustomerBasicSettingAH : BaseActionHandler<GetCustomerBasicSettingRP, GetCustomerBasicSettingRD>
    {
        protected override GetCustomerBasicSettingRD ProcessRequest(DTO.Base.APIRequest<GetCustomerBasicSettingRP> pRequest)
        {
            var rd = new GetCustomerBasicSettingRD();
            var para = pRequest.Parameters;
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(CurrentUserInfo);

            if (!string.IsNullOrWhiteSpace(para.SettingCode))
            {
                //查询参数
                var complexCondition = new List<IWhereCondition> { };
                complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
                complexCondition.Add(new DirectCondition("SettingCode='" + para.SettingCode + "' "));
                var Data = customerBasicSettingBLL.Query(complexCondition.ToArray(),null).FirstOrDefault();
                if (Data != null)
                    rd.SettingValue = Data.SettingValue;
            }
            return rd;
        }
    }
}