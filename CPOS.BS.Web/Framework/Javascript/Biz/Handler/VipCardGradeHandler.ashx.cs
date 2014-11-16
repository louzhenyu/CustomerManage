using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// VipCardGradeHandler 的摘要说明
    /// </summary>
    public class VipCardGradeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "VipCardGrade":
                    content = GetVipCardGradeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetVipCardGradeData

        /// <summary>
        /// 新闻类型
        /// </summary>
        public string GetVipCardGradeData()
        {
            var server = new SysVipCardGradeBLL(new SessionManager().CurrentUserLoginInfo);
            var vipCardGradeArray = server.Query(null, null);

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = vipCardGradeArray.Length.ToString();
            jsonData.data = vipCardGradeArray;

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