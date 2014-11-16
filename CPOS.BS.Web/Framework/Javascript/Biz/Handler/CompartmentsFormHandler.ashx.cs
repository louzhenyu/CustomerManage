using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// CompartmentsFormHandler 的摘要说明
    /// </summary>
    public class CompartmentsFormHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "CompartmentsForm":
                    content = GetCompartmentsFormData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetCompartmentsFormData

        /// <summary>
        /// 车厢形式
        /// </summary>
        public string GetCompartmentsFormData()
        {
            var array = new[] { 
                new { Id = "两厢", Description = "两厢" }, 
                new { Id = "三厢", Description = "三厢" },
                new { Id = "其它", Description = "其它" }
            };

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = array.Length.ToString();
            jsonData.data = array;

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