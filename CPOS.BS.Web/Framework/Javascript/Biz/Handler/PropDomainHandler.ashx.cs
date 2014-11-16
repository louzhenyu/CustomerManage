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
    /// PropDomainHandler 的摘要说明
    /// </summary>
    public class PropDomainHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                default:
                    content = GetData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        public string GetData()
        {
            var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list = new List<BillStatusModel>();
            list.Add(new BillStatusModel() { Id = "ITEM", Description = "商品" });
            list.Add(new BillStatusModel() { Id = "UNIT", Description = "门店" });
            list.Add(new BillStatusModel() { Id = "SKU", Description = "SKU" });
            list.Add(new BillStatusModel() { Id = "Hotel", Description = "Hotel" });
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