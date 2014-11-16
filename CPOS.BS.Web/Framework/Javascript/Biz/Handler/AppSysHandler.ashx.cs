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
    /// AppSysHandler 的摘要说明
    /// </summary>
    public class AppSysHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "get_app_sys_list":
                    content = GetAppSysListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetAppSysListData
        /// <summary>
        /// 应用系统列表
        /// </summary>
        public string GetAppSysListData()
        {
            var appSysService = new AppSysService(new SessionManager().CurrentUserLoginInfo);
            IList<AppSysModel> list = new List<AppSysModel>();

            string content = string.Empty;
            list = appSysService.GetAllAppSyses();
            //list.Add(new AppSysModel() { Def_App_Id = "1", Def_App_Name = "1" });
            //list.Add(new AppSysModel() { Def_App_Id = "2", Def_App_Name = "2" });

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

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