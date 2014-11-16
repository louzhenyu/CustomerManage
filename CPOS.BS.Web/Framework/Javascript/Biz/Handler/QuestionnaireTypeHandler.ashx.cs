using System.Web;
using System.Web.SessionState;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// QuestionnaireTypeHandler 的摘要说明
    /// </summary>
    public class QuestionnaireTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "QuestionnaireType":
                    content = GetQuestionnaireTypeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetQuestionnaireTypeData

        /// <summary>
        /// 视频类型
        /// </summary>
        public string GetQuestionnaireTypeData()
        {
            QuestionnaireBLL service = new QuestionnaireBLL(CurrentUserInfo);
            var list = service.GetAll();

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list != null ? list.Length.ToString() : "0";
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