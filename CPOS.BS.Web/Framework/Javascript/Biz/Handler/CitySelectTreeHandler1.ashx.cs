using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// CitySelectTreeHandler1 的摘要说明
    /// </summary>
    public class CitySelectTreeHandler1 : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "GetCityByCityCode":
                    content = GetCityByCityCode(pContext);
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetCityByCityCode
        private string GetCityByCityCode(HttpContext pContext)
        {
            string res = "";
            if (!string.IsNullOrEmpty(pContext.Request["citycode"]))
            {
                res = new CityService(new SessionManager().CurrentUserLoginInfo).GetCityByCityCode(pContext.Request["citycode"]);
            }
            return res;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}