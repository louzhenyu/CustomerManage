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
using JIT.CPOS.BS.Entity.Pos;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// WarehouseHandler 的摘要说明
    /// </summary>
    public class WarehouseHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "get_warehouse":
                    content = GetWarehouseData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetWarehouseData
        /// <summary>
        /// 获取仓库列表
        /// </summary>
        private string GetWarehouseData()
        {
            var warehouseService = new WarehouseService(new SessionManager().CurrentUserLoginInfo);
            IList<WarehouseInfo> list;

            string key = string.Empty;
            string content = string.Empty;
            if (Request("pid") != null && Request("pid") != string.Empty)
            {
                key = Request("pid").ToString().Trim();
            }

            list = warehouseService.GetWarehouseByUnitID(key);

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