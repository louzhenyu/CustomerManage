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
    /// StatusHandler 的摘要说明
    /// </summary>
    public class StatusHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "user_status":
                    content = GetUserStatusData();
                    break;
                case "normal_status":
                default:
                    content = GetStatusData();
                    break;
                case "vip_status":
                    content = GetVipStatusData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetStatusData
        /// <summary>
        /// 状态
        /// </summary>
        public string GetStatusData()
        {
            var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list = new List<BillStatusModel>();
            list.Add(new BillStatusModel() { Id = "1", Description = "正常" });
            list.Add(new BillStatusModel() { Id = "-1", Description = "停用" });

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetUserStatusData
        /// <summary>
        /// 用户状态
        /// </summary>
        public string GetUserStatusData()
        {
            var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list = new List<BillStatusModel>();
            list.Add(new BillStatusModel() { Id = "1", Description = "正常" });
            list.Add(new BillStatusModel() { Id = "-1", Description = "停用" });

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetVipStatusData
        /// <summary>
        /// 用户状态
        /// </summary>
        public string GetVipStatusData()
        {
            var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list = new List<BillStatusModel>();
            list.Add(new BillStatusModel() { Id = "1", Description = "潜在会员" });
            list.Add(new BillStatusModel() { Id = "2", Description = "正式会员" });

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