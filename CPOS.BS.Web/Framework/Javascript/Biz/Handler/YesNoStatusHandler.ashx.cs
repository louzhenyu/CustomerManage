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
    /// YesNoStatusHandler 的摘要说明
    /// </summary>
    public class YesNoStatusHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "ny":
                    content = GetNYStatusData();
                    break;
                default:
                case "yn":
                    content = GetYNStatusData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetYNStatusData
        /// <summary>
        /// Y/N状态
        /// </summary>
        public string GetYNStatusData()
        {
            var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list = new List<BillStatusModel>();
            list.Add(new BillStatusModel() { Id = "1", Description = "是" });
            list.Add(new BillStatusModel() { Id = "0", Description = "否" });

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetNYStatusData
        /// <summary>
        /// N/Y状态
        /// </summary>
        public string GetNYStatusData()
        {
            var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list = new List<BillStatusModel>();
            list.Add(new BillStatusModel() { Id = "1", Description = "否" });
            list.Add(new BillStatusModel() { Id = "0", Description = "是" });

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