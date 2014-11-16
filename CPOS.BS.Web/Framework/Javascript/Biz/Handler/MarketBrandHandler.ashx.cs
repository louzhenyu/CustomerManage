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
    /// MarketBrandHandler 的摘要说明
    /// </summary>
    public class MarketBrandHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "MarketBrand":
                default:
                    content = GetMarketBrandData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetMarketBrandData
        /// <summary>
        /// 集合
        /// </summary>
        public string GetMarketBrandData()
        {
            var service = new MarketBrandBLL(new SessionManager().CurrentUserLoginInfo);
            IList<MarketBrandEntity> list = new List<MarketBrandEntity>();

            list = service.GetAll().ToList();

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