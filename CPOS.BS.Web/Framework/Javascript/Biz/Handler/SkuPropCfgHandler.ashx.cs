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
    /// SkuPropCfgHandler 的摘要说明
    /// </summary>
    public class SkuPropCfgHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                case "sku_prop_cfg":
                    string orderId = pContext.Request["orderId"];
                    content = GetSkuPropCfgData(orderId);
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetSkuPropCfgData
        /// <summary>
        /// 获取SKU属性定义
        /// </summary>
        public string GetSkuPropCfgData(string orderId)
        {
            var skuService = new SkuService(new SessionManager().CurrentUserLoginInfo);
            SkuPropCfgInfo item = new SkuPropCfgInfo();
            item.sku_prop_1 = "0";
            item.sku_prop_2 = "0";
            item.sku_prop_3 = "0";
            item.sku_prop_4 = "0";
            item.sku_prop_5 = "0";

            string key = string.Empty;
            string content = string.Empty;

            var list = skuService.GetSkuInfoByOne(orderId);
            var sku = new SkuInfo();
            if (list != null && list.Count > 0)
            {
                sku = list[0];
                if (sku.prop_1_id != null && sku.prop_1_id.Length > 0)
                {
                    item.sku_prop_1 = "1";
                    item.sku_prop_1_name = sku.prop_1_name;
                }
                if (sku.prop_2_id != null && sku.prop_2_id.Length > 0)
                {
                    item.sku_prop_2 = "1";
                    item.sku_prop_2_name = sku.prop_2_name;
                }
                if (sku.prop_3_id != null && sku.prop_3_id.Length > 0)
                {
                    item.sku_prop_3 = "1";
                    item.sku_prop_3_name = sku.prop_3_name;
                }
                if (sku.prop_4_id != null && sku.prop_4_id.Length > 0)
                {
                    item.sku_prop_4 = "1";
                    item.sku_prop_4_name = sku.prop_4_name;
                }
                if (sku.prop_5_id != null && sku.prop_5_id.Length > 0)
                {
                    item.sku_prop_5 = "1";
                    item.sku_prop_5_name = sku.prop_5_name;
                }
            }

            var jsonData = new JsonData();
            jsonData.totalCount = item == null ? "0" : "1";
            jsonData.data = item;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        //#region GetSkuPropCfgData
        ///// <summary>
        ///// 单据状态
        ///// </summary>
        //public string GetSkuPropCfgData(string typeCode)
        //{
        //    var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
        //    IList<BillStatusModel> list;

        //    string content = string.Empty;
        //    list = billService.GetBillStatusByKindCode(typeCode);

        //    var jsonData = new JsonData();
        //    jsonData.totalCount = list.Count.ToString();
        //    jsonData.data = list;

        //    content = jsonData.ToJSON();
        //    return content;
        //}
        //#endregion

        new public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}