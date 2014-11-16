using System.Web;
using System.Web.SessionState;
using JIT.Utility.ExtensionMethod;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// QuestionTypeHandler 的摘要说明
    /// </summary>
    public class QuestionTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "QuestionType":
                    content = GetQuestionTypeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetQuestionTypeData

        /// <summary>
        /// 视频类型  update by changjian.tian 2014-5-26增加五角星
        /// </summary>
        public string GetQuestionTypeData()
        {
            var typeArray = new[] { 
                new { Id = "1", Description = "单行文本" },
                new { Id = "2", Description = "文本域（多行文本）" },
                new { Id = "3", Description = "单选项" },
                new { Id = "4", Description = "复选框" },
                new { Id = "5", Description = "单选项，但是显示是一行一个" },
                new { Id = "6", Description = "五角星" }
            };
            if (ConfigurationManager.AppSettings["ProjectName"] == "EMBA")
            {
                typeArray = new[] { 
                    new { Id = "1", Description = "单选题" },
                    new { Id = "2", Description = "问答题" },
                    new { Id = "3", Description = "个人发起活动" }
                };
            }

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = typeArray.Length.ToString();
            jsonData.data = typeArray;

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