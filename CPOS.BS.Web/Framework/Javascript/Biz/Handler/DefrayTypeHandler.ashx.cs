using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// DefrayTypeHandler 的摘要说明
    /// </summary>
    public class DefrayTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "GetDefrayType":
                    content = GetDefrayType();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        private string GetDefrayType()
        {
            var result = new DefrayTypeBLL(CurrentUserInfo).Query(null, null);
            if (result != null)
            {
                return result.Select(it => new { it.DefrayTypeId, it.DefrayTypeName }).ToJSON();
            }
            return "[]";

        }

        new public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}