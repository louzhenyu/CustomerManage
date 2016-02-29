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
            var responseData = new ResponseData();
            LoggingSessionInfo loggingSessionInfo = null;
            if (CurrentUserInfo != null)
            {
                loggingSessionInfo = CurrentUserInfo;
            }
            else
            {
                if (string.IsNullOrEmpty(Request("CustomerID")))
                {
                    responseData.success = false;
                    responseData.msg = "缺少商户标识";
                    return responseData.ToString();
                }
                else if (string.IsNullOrEmpty(Request("CustomerUserID")))
                {
                    responseData.success = false;
                    responseData.msg = "缺少登陆员工的标识";
                    return responseData.ToString();
                }
                else if (string.IsNullOrEmpty(Request("CustomerUserID")))
                {
                    responseData.success = false;
                    responseData.msg = "缺少登陆员工的标识";
                    return responseData.ToString();
                }
                else
                {
                    loggingSessionInfo = Default.GetBSLoggingSession(Request("CustomerID"), Request("CustomerUserID"));
                }
            }

            var appSysService = new AppSysService(loggingSessionInfo);//用兼容模式
            IList<AppSysModel> list = new List<AppSysModel>();

            string content = string.Empty;
            list = appSysService.GetAllAppSyses();
            //list.Add(new AppSysModel() { Def_App_Id = "1", Def_App_Name = "1" });
            //list.Add(new AppSysModel() { Def_App_Id = "2", Def_App_Name = "2" });

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;
            jsonData.success = true;
            jsonData.msg = "";

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