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
    /// ESalesChargeVipHandler 的摘要说明
    /// </summary>
    public class ESalesChargeVipHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "get_list":
                    content = GetListData();
                    break;
                default:
                    content = GetListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetListData
        /// <summary>
        /// 获取列表
        /// </summary>
        public string GetListData()
        {
            var service = new ESalesBLL(new SessionManager().CurrentUserLoginInfo);
            
            string content = string.Empty;
            var ds = service.GetESalesChargeVipList();
            IList<ESalesCharge> list = new List<ESalesCharge>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<ESalesCharge>(ds.Tables[0]);
            }
            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

            content = jsonData.ToJSON();
            return content;
        }
        public class ESalesCharge
        {
            public string SalesChargeVipId { get; set; }
            public string SalesChargeVipName { get; set; }
        }
        #endregion
        
    }
}