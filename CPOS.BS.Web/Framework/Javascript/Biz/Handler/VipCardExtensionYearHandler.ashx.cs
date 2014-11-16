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
    /// VipCardExtensionYearHandler 的摘要说明
    /// </summary>
    public class VipCardExtensionYearHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
            IList<BillStatusModel> list = new List<BillStatusModel>();

            list.Add(new BillStatusModel() { Id = "1", Description = "1年" });
            list.Add(new BillStatusModel() { Id = "2", Description = "2年" });
            list.Add(new BillStatusModel() { Id = "3", Description = "3年" });
            list.Add(new BillStatusModel() { Id = "4", Description = "4年" });
            list.Add(new BillStatusModel() { Id = "5", Description = "5年" });

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