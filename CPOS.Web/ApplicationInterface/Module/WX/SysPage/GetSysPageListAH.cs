/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/5/09 10:18
 * Description	:获取模板页列表
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
    public class GetSysPageListAH : BaseActionHandler<GetSysPageListRP, GetSysPageListRD>
    {
        protected override GetSysPageListRD ProcessRequest(APIRequest<GetSysPageListRP> pRequest)
        {
            GetSysPageListRD rd = new GetSysPageListRD();
            var userInfo = this.CurrentUserInfo;
            userInfo.CurrentLoggingManager.Connection_String = System.Configuration.ConfigurationManager.AppSettings["Conn_ap"];
            SysPageBLL sysPageBLL = new SysPageBLL(userInfo);

            string Key = pRequest.Parameters.Key;
            string Name = pRequest.Parameters.Name;
            int PageIndex =Convert.ToInt32(pRequest.Parameters.PageIndex)-1<0?0:Convert.ToInt32(pRequest.Parameters.PageIndex);
            int PageSize =Convert.ToInt32(pRequest.Parameters.PageSize)-1<0?15:Convert.ToInt32(pRequest.Parameters.PageSize);
            
            rd = sysPageBLL.GetSysPageList(Key, Name, PageIndex, PageSize,userInfo.CurrentUser.customer_id);
            return rd;
        }
    }
}