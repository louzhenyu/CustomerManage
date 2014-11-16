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
    /// SysIntegralSourceHandler 的摘要说明
    /// </summary>
    public class SysIntegralSourceHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "SysIntegralSource":
                default:
                    content = GetSysIntegralSourceData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetSysIntegralSourceData
        /// <summary>
        /// 
        /// </summary>
        public string GetSysIntegralSourceData()
        {
            int? typeCode = null;
            IList<BillStatusModel> list = new List<BillStatusModel>();
            SysIntegralSourceBLL service = new SysIntegralSourceBLL(new SessionManager().CurrentUserLoginInfo);
            var dataList = new List<SysIntegralSourceEntity>();
            if (Request("typeCode") != null && Request("typeCode").Trim().Length > 0 &&
                Request("typeCode").Trim() != "null" && Request("typeCode").Trim() != "undefined")
            {
                typeCode = Convert.ToInt32(Request("typeCode"));
                dataList = service.QueryByEntity(new SysIntegralSourceEntity()
                {
                    TypeCode = typeCode
                }, null).ToList();
            }
            else
            {
                dataList = service.GetAll().ToList();
            }
            foreach (var dataItem in dataList)
            {
                list.Add(new BillStatusModel() { 
                    Id = dataItem.IntegralSourceID, 
                    Description = dataItem.IntegralSourceName
                });
            }

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