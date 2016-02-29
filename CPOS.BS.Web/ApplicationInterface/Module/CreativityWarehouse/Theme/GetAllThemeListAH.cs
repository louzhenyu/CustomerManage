using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Response;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.Theme
{
    public class GetAllThemeListAH : BaseActionHandler<EmptyRequestParameter, ThemeListRD>
    {
        protected override ThemeListRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new ThemeListRD();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_CTW_LEventThemeBLL bllTheme = new T_CTW_LEventThemeBLL(loggingSessionInfo);
            T_CTW_LEventThemeEntity[] listTheme = bllTheme.GetAll();
            if (listTheme != null )
            {
                rd.ThemeList = listTheme.ToList().OrderBy(a=>a.StartDate).ToList();
            }
            return rd;
        }
    }
}