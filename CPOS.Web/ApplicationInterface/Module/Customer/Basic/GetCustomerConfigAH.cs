using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.Basic.Customer.Request;
using JIT.CPOS.DTO.Module.Basic.Customer.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Customer.Basic
{
    public class GetCustomerConfigAH : BaseActionHandler<EmptyRequestParameter, GetCustomerConfigRD>
    {
        protected override GetCustomerConfigRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            //初始化
            GetCustomerConfigRD rd = new GetCustomerConfigRD();
            SysPageBLL bll = new SysPageBLL(this.CurrentUserInfo);

            //查询Config
            string strConfig = bll.GetCreateCustomerConfig(CurrentUserInfo.ClientID);
            strConfig = Regex.Replace(strConfig, @"\s", "");
            strConfig = strConfig.Replace("\"", "'");

            rd.ConfigValue = strConfig;

            return rd;
        }

    }
}