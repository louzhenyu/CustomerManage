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
    /// TUnitHandler 的摘要说明
    /// </summary>
    public class TUnitHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "GetTUnit":
                    content = GetTUnit();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        private string GetTUnit()
        {
            var type = HttpContext.Current.Request.Params["Type"];
            var result = new UnitService(CurrentUserInfo).GetUnitInfoListByTypeCode(type);
            if (result!= null)
            {
                return result.Select(it => new { unit_id= it.Id, unit_name= it.Name}).ToJSON();
            }
            return "[]";
        }

        public new bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}