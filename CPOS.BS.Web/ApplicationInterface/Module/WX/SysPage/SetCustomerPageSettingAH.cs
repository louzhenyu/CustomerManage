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
    public class SetCustomerPageSettingAH : BaseActionHandler<SetCustomerPageSettingRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<SetCustomerPageSettingRP> pRequest)
        {
           EmptyResponseData rdata = new EmptyResponseData();
           SysPageBLL sysPageBLL = new SysPageBLL(this.CurrentUserInfo);
           for (int i = 0; i < pRequest.Parameters.Node.Length; i++)
           {
               int Res = sysPageBLL.SetCustomerPageSetting(CurrentUserInfo.ClientID, pRequest.Parameters.MappingId, pRequest.Parameters.PageKey, pRequest.Parameters.Node[i].ToString(), pRequest.Parameters.NodeValue[i].ToString(), CurrentUserInfo.UserID);
           }
           return rdata;
        }
    }
}