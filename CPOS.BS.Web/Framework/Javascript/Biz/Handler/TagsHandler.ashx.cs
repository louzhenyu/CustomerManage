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
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// TagsHandler 的摘要说明
    /// </summary>
    public class TagsHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                default:
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
            var service = new TagsBLL(new SessionManager().CurrentUserLoginInfo);
            //IList<TagsEntity> data = new List<TagsEntity>();
            string content = string.Empty;

            string key = string.Empty;
            var orderBy = new OrderBy[]{
                new OrderBy{ FieldName = "TagsName", Direction=OrderByDirections.Asc }
            };
            var data = service.QueryByEntity(new TagsEntity() { CustomerId = this.CurrentUserInfo.CurrentUser.customer_id }, orderBy);

            var jsonData = new JsonData();
            jsonData.totalCount = data.Length.ToString();
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