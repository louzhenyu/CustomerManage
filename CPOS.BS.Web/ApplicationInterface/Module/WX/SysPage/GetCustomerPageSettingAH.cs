using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.SysPage
{
    public class GetCustomerPageSettingAH : BaseActionHandler<GetCustomerPageSettingRP, GetCustomerPageSettingRD>
    {

        protected override GetCustomerPageSettingRD ProcessRequest(APIRequest<GetCustomerPageSettingRP> pRequest)
        {
            GetCustomerPageSettingRD rddata = new GetCustomerPageSettingRD();
            SysPageBLL sysPageBLL = new SysPageBLL(this.CurrentUserInfo);
            rddata = sysPageBLL.GetCustomerPageSetting(pRequest.Parameters.PageKey,CurrentUserInfo.ClientID);
            return rddata;
        }
    }
}