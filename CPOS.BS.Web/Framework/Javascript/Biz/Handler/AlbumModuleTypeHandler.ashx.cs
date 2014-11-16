using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// AlbumModuleTypeHandler 的摘要说明
    /// </summary>
    public class AlbumModuleTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "AlbumModuleType": 
                    content = GetAlbumModuleTypeData();
                    break;
                case "AlbumType":
                    content = GetAlbumTypeData();
                    break;
                default:
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetAlbumModuleTypeData
        /// <summary>
        /// 相册模块类型
        /// </summary>
        public string GetAlbumModuleTypeData()
        {
            var list = new[] { 
                new { Id = "1", Description = "活动模块" }
            };

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Length.ToString();
            jsonData.data = list;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetAlbumTypeData
        /// <summary>
        /// 相册类型
        /// </summary>
        public string GetAlbumTypeData()
        {
            var list = new[] { 
                new { Id = "1", Description = "相片" },
                new { Id = "2", Description = "视频" }
            };

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Length.ToString();
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