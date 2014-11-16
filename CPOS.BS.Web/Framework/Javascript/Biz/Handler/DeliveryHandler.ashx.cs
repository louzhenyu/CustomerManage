using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// DeliveryHandler 的摘要说明
    /// </summary>
    public class DeliveryHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "GetDeliveryType":
                    content = GetDeliveryType();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        private string GetDeliveryType()
        {
            var result = new DeliveryBLL(CurrentUserInfo).Query(null, null);
            if (result!= null )
            {
                return result.Select(it => new {it.DeliveryId, it.DeliveryName}).ToJSON();
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