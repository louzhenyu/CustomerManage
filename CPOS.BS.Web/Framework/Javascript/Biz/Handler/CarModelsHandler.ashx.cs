using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// CarModelsHandler 的摘要说明
    /// </summary>
    public class CarModelsHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "CarModels":
                    content = GetCarModelsData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetCarModelsData

        /// <summary>
        /// 车型号
        /// </summary>
        public string GetCarModelsData()
        {
            var server = new VipCardModelsBLL(new SessionManager().CurrentUserLoginInfo);

            string key = string.Empty;
            string content = string.Empty;
            if (Request("pid") != null && Request("pid") != string.Empty)
            {
                key = Request("pid").ToString().Trim();
            }

            var entity = new VipCardModelsEntity() { CarBrandID = key };
            var carModelsArray = server.QueryByEntity(entity, null);

            var jsonData = new JsonData();
            jsonData.totalCount = carModelsArray.Length.ToString();
            jsonData.data = carModelsArray;

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