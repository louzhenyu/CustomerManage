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
    /// MarketSendTypeHandler 的摘要说明
    /// </summary>
    public class MarketSendTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
            var service = new MarketSendTypeBLL(new SessionManager().CurrentUserLoginInfo);
            IList<MarketSendTypeEntity> data = new List<MarketSendTypeEntity>();
            string content = string.Empty;

            data = service.GetAll().ToList();

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

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