using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.BS.BLL;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.SysPage
{
    public class GetCustomerPageListAH :BaseActionHandler<GetSysPageListRP, GetSysPageListRD>
    {
        protected override GetSysPageListRD ProcessRequest(APIRequest<GetSysPageListRP> pRequest)
        {
            GetSysPageListRD rddata = new GetSysPageListRD();
            SysPageBLL bll = new SysPageBLL(this.CurrentUserInfo);

            string Key = pRequest.Parameters.Key;
            string Name = pRequest.Parameters.Name;
            int PageIndex = Convert.ToInt32(pRequest.Parameters.PageIndex)-1 < 0 ? 0 : Convert.ToInt32(pRequest.Parameters.PageIndex);
            int PageSize = Convert.ToInt32(pRequest.Parameters.PageSize)-1 < 0 ? 15 : Convert.ToInt32(pRequest.Parameters.PageSize);
            rddata= bll.GetCustomerPageList(Key, Name, PageIndex, PageSize, CurrentUserInfo.ClientID);
            return rddata;
        }
    }
}