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
    /// WModelHandler 的摘要说明
    /// </summary>
    public class WModelHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "get_list":
                default:
                    content = GetListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetListData
        /// <summary>
        /// 获取列表
        /// </summary>
        public string GetListData()
        {
            var service = new WModelBLL(new SessionManager().CurrentUserLoginInfo);
            var wApplicationInterfaceBLL = new WApplicationInterfaceBLL(new SessionManager().CurrentUserLoginInfo);
            IList<WModelEntity> data = new List<WModelEntity>();
            string content = string.Empty;

            string key = "";
            if (Request("pid") != null && Request("pid") != string.Empty)
            {
                key = Request("pid").ToString().Trim();
            }
            string type = "";
            if (Request("type") != null && Request("type") != string.Empty)
            {
                type = Request("type").ToString().Trim();
            }
            if (type == "2") { 
                var appList = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity() 
                {
                    WeiXinID = key
                }, null);
                if (appList != null && appList.Length> 0)
                {
                    key = appList[0].ApplicationId;
                }
            }
            data = service.GetWModelListByAppId(key); 

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