using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Basic.Menu.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Menu
{
    public class GetMenuListAH : BaseActionHandler<EmptyRequestParameter, GetMenuListRD>
    {
        protected override GetMenuListRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetMenuListRD();

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            AppSysService appSysService = new AppSysService(loggingSessionInfo);

            rd.MenuList = appSysService.GetRoleMenus(this.CurrentUserInfo, CurrentUserInfo.CurrentUserRole.RoleId);//根据当前用户的角色，来取他拥有的页面
            return rd;
        }
    }
}