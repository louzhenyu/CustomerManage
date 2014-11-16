using System.Web;
using System.Linq;
using System.Web.SessionState;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using System.Collections.Generic;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// NewsTypeHandler 的摘要说明
    /// </summary>
    public class NewsTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "NewsType":
                    content = GetNewsTypeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetNewsTypeData

        /// <summary>
        /// 新闻类型
        /// </summary>
        public string GetNewsTypeData()
        {
            //var newsTypeArray = new[] {  
            //    new { Id = "", Description = "" }
            //    new { Id = "1", Description = "首页新闻模块" }, 
            //    new { Id = "2", Description = "校友故事模块" }, 
            //    new { Id = "3", Description = "发布会" }
            //};

            //string content = string.Empty;

            //var jsonData = new JsonData();
            //jsonData.totalCount = newsTypeArray.Length.ToString();
            //jsonData.data = newsTypeArray;

            //content = jsonData.ToJSON();
            //return content;

            var loggingSession = new SessionManager().CurrentUserLoginInfo;
            var service = new LNewsTypeBLL(loggingSession);
            IList<LNewsTypeEntity> data = new List<LNewsTypeEntity>();
            string content = string.Empty;

            data = service.QueryByEntity(new LNewsTypeEntity { CustomerId = loggingSession.ClientID }, null).ToList();

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