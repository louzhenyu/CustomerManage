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
using JIT.CPOS.Common;


namespace JIT.CPOS.Web.ApplicationInterface.Module.Customer.Basic
{
    public class GetCustomerVersionAH : BaseActionHandler<EmptyRequestParameter, GetCustomerVersionRD>
    {
        protected override GetCustomerVersionRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            //初始化
            GetCustomerVersionRD rd = new GetCustomerVersionRD();
            SysPageBLL bll = new SysPageBLL(this.CurrentUserInfo);

            //查询Version
            string strVersion = bll.GetCreateCustomerVersion(CurrentUserInfo.ClientID, CurrentUserInfo.CurrentLoggingManager.Customer_Name);
            strVersion = Regex.Replace(strVersion, @"\s", "");
            strVersion = strVersion.Replace("\"", "'");

            rd.VersionValue = strVersion;

            return rd;
        }
    }
}