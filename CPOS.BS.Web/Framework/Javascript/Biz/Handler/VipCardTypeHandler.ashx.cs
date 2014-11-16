using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// VipCardTypeHandler 的摘要说明
    /// </summary>
    public class VipCardTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "VipCardType":
                    content = GetVipCardTypeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetVipCardTypeData

        /// <summary>
        /// 新闻类型
        /// </summary>
        public string GetVipCardTypeData()
        {
            var server = new SysVipCardTypeBLL(new SessionManager().CurrentUserLoginInfo);
            var vipCardTypeArray = server.Query(null, null);

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = vipCardTypeArray.Length.ToString();
            jsonData.data = vipCardTypeArray;

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