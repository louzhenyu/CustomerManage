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
    /// WQRCodeTypeHandler 的摘要说明
    /// </summary>
    public class WQRCodeTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "WQRCodeType":
                default:
                    content = GetWQRCodeTypeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetWQRCodeTypeData
        /// <summary>
        /// 
        /// </summary>
        public string GetWQRCodeTypeData()
        {
            var billService = new WQRCodeTypeBLL(new SessionManager().CurrentUserLoginInfo);
            var wApplicationInterfaceBLL = new WApplicationInterfaceBLL(new SessionManager().CurrentUserLoginInfo);
            //IList<BillStatusModel> list = new List<BillStatusModel>();
            //list.Add(new BillStatusModel() { Id = "1", Description = "基本" });
            //list.Add(new BillStatusModel() { Id = "2", Description = "高级" });

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

            var appId = "";
            if (type == "2")
            {
                var appObjs = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity()
                {
                    ApplicationId = key
                }, null);
                if (appObjs != null && appObjs.Length > 0)
                {
                    appId = appObjs[0].ApplicationId;
                }
                else
                {
                    appId = "-99";
                }
            }
            else
            {
                var appObjs = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity()
                {
                    WeiXinID = key
                }, null);
                if (appObjs != null && appObjs.Length > 0)
                {
                    appId = appObjs[0].ApplicationId;
                }
                else
                {
                    appId = "-99";
                }
            }

            var list = billService.GetList(new WQRCodeTypeEntity()
            {
                ApplicationId = appId
            }, 0, 1000);

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
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