using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Module.Request;
using JIT.CPOS.DTO.Module.WeiXin.Module.Response;
using JIT.CPOS.BS.BLL;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Module
{
    public class GetSysModuleListAH : BaseActionHandler<GetSysModuleListRP, GetSysModuleListRD>
    {
        protected override GetSysModuleListRD ProcessRequest(APIRequest<GetSysModuleListRP> pRequest)
        {
            GetSysModuleListRD rd = new GetSysModuleListRD();
            var bll = new SysPageBLL(CurrentUserInfo);
            var list = bll.GetPagesByCustomerID(CurrentUserInfo.ClientID);  //增加根据customer_id查询 update by Henry 2014-11-19
            var temp = list.GroupBy(t => new { t.ModuleName, t.PageCode, t.PageID, t.URLTemplate }).Select(t => new ModulePageInfo()
            {
                ModuleName = t.Key.ModuleName,
                PageCode = t.Key.PageCode,
                PageID = t.Key.PageID.Value.ToString("N"),
                URLTemplate = t.Key.URLTemplate
            }).Distinct().ToArray();
            rd.SysModuleList = temp;
            return rd;
        }
    }
}