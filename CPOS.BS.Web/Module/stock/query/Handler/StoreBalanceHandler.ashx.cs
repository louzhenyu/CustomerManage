using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.Web.Module.stock.query.Handler
{
    /// <summary>
    /// StoreBalanceHandler 的摘要说明
    /// </summary>
    public class StoreBalanceHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "StoreQuery":
                    content = GetStoreQueryData();
                    break;
            }

            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetStoreQueryData
        /// <summary>
        /// 获取库存
        /// </summary>
        public string GetStoreQueryData()
        {
            var stockService = new StockBalanceService(CurrentUserInfo);
            StockBalanceInfo data = new StockBalanceInfo();
            data.icount = 0;
            string content = string.Empty;

            var form = Request("form").DeserializeJSONTo<StockBalanceQueryEntity>();

            string unit_id = FormatParamValue(Request("unit_id")); ;//"0d2bf77f765849249a0270c0a07fef07";//form.unit_id;
            string warehouse_id = FormatParamValue(form.warehouse_id);
            string item_name = FormatParamValue(form.item_name);
            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (form.id != null && form.id != string.Empty)
            {
                key = form.id.ToString().Trim();
            }
            if (item_name == null)
            {
                item_name = string.Empty;
            }

            data = stockService.SearchStockBalance(
                unit_id,
                warehouse_id,
                item_name,
                key,
                maxRowCount,
                startRowIndex);
            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.StockBalanceInfoList.ToJSON(),
               data.icount);
            return content;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    #region QueryEntity 定义查询对象
    public class StockBalanceQueryEntity
    {
        public string id;
        public string warehouse_id;
        public string item_name;
        public string unit_id;
    }
    #endregion
}