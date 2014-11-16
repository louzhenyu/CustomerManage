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
    /// ItemPriceTypeHandler 的摘要说明
    /// </summary>
    public class ItemPriceTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "get_item_price_list":
                    content = GetItemPriceTypeListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetItemPriceTypeListData
        /// <summary>
        /// 获取商品价格类型
        /// </summary>
        public string GetItemPriceTypeListData()
        {
            var itemPriceTypeService = new ItemPriceTypeService(CurrentUserInfo);
            IList<ItemPriceTypeInfo> data = new List<ItemPriceTypeInfo>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("item_id") != null && Request("item_id") != string.Empty)
            {
                key = Request("item_id").ToString().Trim();
            }

            data = itemPriceTypeService.GetItemPriceTypeList();

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