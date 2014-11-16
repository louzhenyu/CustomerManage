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
    /// OrderStatusHandler 的摘要说明
    /// </summary>
    public class OrderStatusHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "order_status_po":
                    content = GetOrderStatusData("PO");
                    break;
                case "order_status_ro":
                    content = GetOrderStatusData("RO");
                    break;
                case "order_status_cc":
                    content = GetOrderStatusData("CC");
                    break;
                case "order_status_aj":
                    content = GetOrderStatusData("AJ");
                    break;
                case "order_status_so":
                    content = GetOrderStatusData("SO");
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetOrderStatusData
        /// <summary>
        /// 单据状态
        /// </summary>
        public string GetOrderStatusData(string typeCode)
        {
            var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list;

            string content = string.Empty;
            list = billService.GetBillStatusByKindCode(typeCode);

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