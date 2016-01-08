
/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/5/21 15:12
 * Description	:获取套餐列表
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.WX.SysPage
{
    public class GetVocationVersionMappingListAH : BaseActionHandler<GetVocationVersionMappingListRP, GetVocationVersionMappingListRD>
    {

        protected override GetVocationVersionMappingListRD ProcessRequest(APIRequest<GetVocationVersionMappingListRP> pRequest)
        {
            GetVocationVersionMappingListRD rddata = new GetVocationVersionMappingListRD();
            var userInfo = this.CurrentUserInfo;
            userInfo.CurrentLoggingManager.Connection_String = System.Configuration.ConfigurationManager.AppSettings["Conn_ap"];
            SysPageBLL PageBLL = new SysPageBLL(userInfo);

            int PageIndex = Convert.ToInt32(pRequest.Parameters.PageIndex - 1) < 0 ? 0 : Convert.ToInt32(pRequest.Parameters.PageIndex);
            int PageSize = Convert.ToInt32(pRequest.Parameters.PageSize) == 0 ? 15 :Convert.ToInt32(pRequest.Parameters.PageSize);
            rddata = PageBLL.GetVocationVersionMappingList(PageIndex, PageSize);
            return rddata;
        }
    }
}