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
using System.Collections;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// ItemStockBalanceNo 的摘要说明
    /// </summary>
    public class ItemStockBalanceNo : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
        IHttpHandler, IRequiresSessionState
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request["method"])
            {
                case "stock_num":
                    content = GetStockNumData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetStockNumData
        /// <summary>
        /// 获取单个SKU库存
        /// </summary>
        public string GetStockNumData()
        {
            var stockService = new StockBalanceService(CurrentUserInfo);
            decimal num = 0;

            string key = string.Empty;
            string content = string.Empty;
            string unit_id = string.Empty;
            string warehouse_id = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }
            if (Request("unit_id") != null && Request("unit_id") != string.Empty)
            {
                unit_id = Request("unit_id").ToString().Trim();
            }
            if (Request("warehouse_id") != null && Request("warehouse_id") != string.Empty)
            {
                warehouse_id = Request("warehouse_id").ToString().Trim();
            }
            var stockInfo = stockService.GetStockBalanceListByUnitIdWarehouseId(unit_id, warehouse_id, null, key, 1, 0);
            if (stockInfo != null && stockInfo.StockBalanceInfoList != null &&
                stockInfo.StockBalanceInfoList.Count > 0)
            {
                num = stockInfo.StockBalanceInfoList[0].end_qty;
            }

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = num;
            content = jsonData.ToJSON();
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
}