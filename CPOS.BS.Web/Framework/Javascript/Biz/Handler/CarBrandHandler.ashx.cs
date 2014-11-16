using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// CarBrandHandler 的摘要说明
    /// </summary>
    public class CarBrandHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "CarBrand":
                    content = GetCarBrandData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetCarBrandData

        /// <summary>
        /// 车品牌
        /// </summary>
        public string GetCarBrandData()
        {
            var server = new VipCardCarBrandBLL(new SessionManager().CurrentUserLoginInfo);
            var carBrandArray = server.Query(null, null);

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = carBrandArray.Length.ToString();
            jsonData.data = carBrandArray;

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