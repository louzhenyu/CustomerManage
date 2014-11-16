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
    /// EventTypeHandler 的摘要说明
    /// </summary>
    public class EventTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "EventType":
                default:
                    content = GetEventTypeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetEventTypeData
        /// <summary>
        /// 集合
        /// </summary>
        public string GetEventTypeData()
        {
            //var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list = new List<BillStatusModel>();
            list.Add(new BillStatusModel() { Id = "微信", Description = "微信" });
            list.Add(new BillStatusModel() { Id = "微博", Description = "微博" });
            list.Add(new BillStatusModel() { Id = "EDM", Description = "EDM" });
            list.Add(new BillStatusModel() { Id = "SMS", Description = "SMS" });
            list.Add(new BillStatusModel() { Id = "Telemarketing", Description = "Telemarketing" });
            list.Add(new BillStatusModel() { Id = "DM", Description = "DM" });
            list.Add(new BillStatusModel() { Id = "MMS", Description = "MMS" });

            string content = string.Empty;

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