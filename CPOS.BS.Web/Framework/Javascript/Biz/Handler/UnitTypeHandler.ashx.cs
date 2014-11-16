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
    /// UnitTypeHandler 的摘要说明
    /// </summary>
    public class UnitTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "get_unit_type_list":
                    content = GetCityTypeListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetCityTypeListData
        /// <summary>
        /// 获取门店类型列表
        /// </summary>
        public string GetCityTypeListData()
        {
            var service = new TypeService(new SessionManager().CurrentUserLoginInfo);
            IList<TypeInfo> data = new List<TypeInfo>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("unit_id") != null && Request("unit_id") != string.Empty)
            {
                key = Request("unit_id").ToString().Trim();
            }

            data = service.GetTypeInfoListByDomain("UnitType");

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