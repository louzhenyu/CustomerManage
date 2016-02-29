using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.Theme
{
    public class NextSeasonListAH : BaseActionHandler<EmptyRequestParameter, ThemeListRD>
    {
        protected override ThemeListRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new ThemeListRD();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_CTW_LEventThemeBLL bllTheme = new T_CTW_LEventThemeBLL(loggingSessionInfo);
            DataSet ds = bllTheme.GetNextSeasonThemeList();
            if (ds != null && ds.Tables.Count > 0)
            {
                rd.ThemeList = DataTableToObject.ConvertToList<T_CTW_LEventThemeEntity>(ds.Tables[0]).OrderBy(a => a.StartDate).ToList();
            }
            return rd;
        }
    }
}