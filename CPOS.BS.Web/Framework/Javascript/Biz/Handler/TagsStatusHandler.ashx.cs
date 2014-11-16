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
    /// TagsStatusHandler 的摘要说明
    /// </summary>
    public class TagsStatusHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "TagsStatus":
                default:
                    content = GetTagsStatusData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetTagsStatusData
        /// <summary>
        /// 
        /// </summary>
        public string GetTagsStatusData()
        {
            var billService = new TagsStatusBLL(new SessionManager().CurrentUserLoginInfo);

            var list = billService.QueryByEntity(new TagsStatusEntity()
            { 
                
            }, null);

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Length.ToString();
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