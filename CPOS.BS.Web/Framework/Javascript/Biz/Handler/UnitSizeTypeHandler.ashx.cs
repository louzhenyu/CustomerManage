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
    /// UnitSizeTypeHandler 的摘要说明
    /// </summary>
    public class UnitSizeTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "EventType":
                default:
                    content = GetEventTypeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetEventTypeData
        /// <summary>
        /// 集合
        /// </summary>
        public string GetEventTypeData()
        {
            //var service = new QuesOptionBLL(new SessionManager().CurrentUserLoginInfo);

            IList<BillStatusModel> list = new List<BillStatusModel>();
            list.Add(new BillStatusModel() { Id = "41", Description = "10以下" });
            list.Add(new BillStatusModel() { Id = "42", Description = "10~100" });
            list.Add(new BillStatusModel() { Id = "49", Description = "100~300" });
            list.Add(new BillStatusModel() { Id = "50", Description = "300~1000" });
            list.Add(new BillStatusModel() { Id = "51", Description = "1000以上" });

            //list.Add(new BillStatusModel() { Id = "41", Description = "50以下" });
            //list.Add(new BillStatusModel() { Id = "42", Description = "50-60" });
            //list.Add(new BillStatusModel() { Id = "49", Description = "61-70" });
            //list.Add(new BillStatusModel() { Id = "50", Description = "70以上" });

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