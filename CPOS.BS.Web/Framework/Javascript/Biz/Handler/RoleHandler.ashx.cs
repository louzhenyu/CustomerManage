using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using System.Web.SessionState;
using JIT.Utility.Web.ComponentModel;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// RoleHandler 的摘要说明
    /// </summary>
    public class RoleHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
        IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="context"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request["method"])
            {
                case "get_role_list":
                    content = GetRoleListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetRoleListData
        /// <summary>
        /// 角色列表
        /// </summary>
        public string GetRoleListData()
        {
            var loggininfo = new SessionManager().CurrentUserLoginInfo;
            var appSysService = new AppSysService(new SessionManager().CurrentUserLoginInfo);
            RoleModel list = new RoleModel();

            string content = string.Empty;
            string key = string.Empty;
            if (Request("app_sys_id") != null && Request("app_sys_id") != string.Empty)
            {
                key = Request("app_sys_id").ToString().Trim();
                list = appSysService.GetRolesByAppSysId(key, 1000, 0, "", "", loggininfo.UserID);
            }
            else
            {
                list = new RoleModel();
                list.RoleInfoList = new List<RoleModel>();
            }

            var jsonData = new JsonData();
            jsonData.totalCount = list.RoleInfoList.Count.ToString();
            jsonData.data = list.RoleInfoList;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        new public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}