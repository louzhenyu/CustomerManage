using System.Collections.Generic;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Module.Order.CcOrders.Handler
{
    /// <summary>
    /// CcOrderHandler
    /// </summary>
    public class CcOrderHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "cc_order_delete":     //库存盘点单删除
                    content = CcOrderDeleteData();
                    break;
                case "cc_order_pass":       //库存盘点单审核
                    content = CcOrderPassData();
                    break;
                case "cc_order":            //库存盘点单查询
                    content = GetCcOrderData();
                    break;
                case "cc_order_save":       //库存盘点单保存
                    content = SaveCcOrder();
                    break;
                case "get_cc_info_by_id":   //获取单个库存盘点单主信息
                    content = GetCcInfoById();
                    break;
                case "get_cc_detail_info_by_id": //获取单个库存盘点单明细
                    content = GetCcDetailInfoById();
                    break;
                case "get_sku_by_id":       //通过ID获取SKU
                    content = GetSkuByIdData();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region CcOrderDeleteData 删除库存盘点单

        /// <summary>
        /// 删除库存盘点单
        /// </summary>
        public string CcOrderDeleteData()
        {
            var inoutService = new CCService(CurrentUserInfo);
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "单据ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            foreach (var id in ids)
            {
                inoutService.SetCCStatusUpdate(id.Trim(), BillActionType.Cancel, out error);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region CcOrderPassData 审核库存盘点单

        /// <summary>
        /// 审核库存盘点单
        /// </summary>
        public string CcOrderPassData()
        {
            var ccService = new CCService(CurrentUserInfo);
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                key = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "单据ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            foreach (var id in ids)
            {
                ccService.SetCCStatusUpdate(id.Trim(), BillActionType.Approve, out error);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetCcOrderData 查询库存盘点单

        /// <summary>
        /// 查询库存盘点单
        /// </summary>
        public string GetCcOrderData()
        {
            var form = Request("form").DeserializeJSONTo<CcOrderQueryEntity>();
            var ccService = new CCService(CurrentUserInfo);

            CCInfo data;
            string content = string.Empty;

            string order_no = FormatParamValue(form.order_no);
            string status = FormatParamValue(form.status);
            string unit_id = FormatParamValue(Request("unit_id"));
            string warehouse_id = FormatParamValue(form.warehouse_id);
            string order_date_begin = FormatParamValue(form.order_date_begin);
            string order_date_end = FormatParamValue(form.order_date_end);
            string complete_date_begin = FormatParamValue(form.complete_date_begin);
            string complete_date_end = FormatParamValue(form.complete_date_end);
            string data_from_id = FormatParamValue(form.data_from_id);
            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("start")));

            string key = string.Empty;
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                key = FormatParamValue(Request("id")).ToString().Trim();
            }

            data = ccService.SearchCCInfo(CurrentUserInfo,
                order_no,
                status,
                unit_id,
                warehouse_id,
                order_date_begin,
                order_date_end,
                complete_date_begin,
                complete_date_end,
                data_from_id,
                maxRowCount,
                startRowIndex);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.CCInfoList.ToJSON(),
                data.ICount);

            return content;
        }

        #endregion

        #region SaveCcOrder 保存库存盘点单

        /// <summary>
        /// 保存库存盘点单
        /// </summary>
        public string SaveCcOrder()
        {
            var inoutService = new CCService(CurrentUserInfo);
            CCInfo order = new CCInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string order_id = string.Empty;
            if (FormatParamValue(Request("order")) != null && FormatParamValue(Request("order")) != string.Empty)
            {
                key = FormatParamValue(Request("order")).ToString().Trim();
            }
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                order_id = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            order = key.DeserializeJSONTo<CCInfo>();

            if (order_id.Trim().Length == 0)
            {
                order.order_id = Utils.NewGuid();
            }
            else
            {
                order.order_id = order_id;
            }

            if (order.order_no == null || order.order_no.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "单据号码不能为空";
                return responseData.ToJSON();
            }
            if (order.order_date == null || order.order_date.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "单据日期不能为空";
                return responseData.ToJSON();
            }
            if (order.complete_date == null || order.complete_date.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "入库日期不能为空";
                return responseData.ToJSON();
            }
            if (order.unit_id == null || order.unit_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "盘点单位不能为空";
                return responseData.ToJSON();
            }
            if (order.warehouse_id == null || order.warehouse_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "仓库不能为空";
                return responseData.ToJSON();
            }
            if (order.CCDetailInfoList == null || order.CCDetailInfoList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "商品不能为空";
                return responseData.ToJSON();
            }

            inoutService.SetCCInfo(order, false, out error);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetCcInfoById 获取库存盘点单主信息

        /// <summary>
        /// 获取库存盘点单主信息
        /// </summary>
        public string GetCcInfoById()
        {
            var inoutService = new CCService(CurrentUserInfo);
            CCInfo data;
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                key = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            data = inoutService.GetCCInfoById(key, 10000, 0);

            var jsonData = new JsonData();
            jsonData.totalCount = data == null ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                data == null ? "0" : "1");

            return content;
        }

        #endregion

        #region GetCcDetailInfoById 获取库存盘点单明细

        /// <summary>
        /// 获取库存盘点单明细
        /// </summary>
        public string GetCcDetailInfoById()
        {
            var ccService = new CCService(CurrentUserInfo);
            IList<CCDetailInfo> data = null;
            string content = string.Empty;

            string order_id = string.Empty;
            string unit_id = string.Empty;
            string warehouse_id = string.Empty;
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                order_id = FormatParamValue(Request("order_id")).ToString().Trim();
            }
            if (FormatParamValue(Request("unit_id")) != null && FormatParamValue(Request("unit_id")) != string.Empty)
            {
                unit_id = FormatParamValue(Request("unit_id")).ToString().Trim();
            }
            if (FormatParamValue(Request("warehouse_id")) != null && FormatParamValue(Request("warehouse_id")) != string.Empty)
            {
                warehouse_id = FormatParamValue(Request("warehouse_id")).ToString().Trim();
            }

            int maxRowCount = 100000;
            int startRowIndex = 0;

            if (order_id == null || order_id.Trim().Length == 0) order_id = string.Empty;
            CCInfo order = null;
            CCDetailInfo dataStock = null;

            if (order_id.Trim().Length > 0)
            {
                order = ccService.GetCCDetailInfoByOrderId(order_id, maxRowCount, startRowIndex);
            }
            else if (unit_id.Trim().Length > 0 && warehouse_id.Trim().Length > 0)
            {
                dataStock = ccService.GetCCDetailListStockBalance(CurrentUserInfo,
                    order_id,
                    unit_id,
                    warehouse_id,
                    maxRowCount,
                    startRowIndex);
            }

            if (order != null)
            {
                data = order.CCDetailInfoList;
                foreach (var detailItem in order.CCDetailInfoList)
                {
                    detailItem.display_name = SkuService.GetItemAllName(detailItem);
                }
            }
            else if (dataStock != null)
            {
                data = dataStock.CCDetailInfoList;
                foreach (var detailItem in dataStock.CCDetailInfoList)
                {
                    detailItem.display_name = SkuService.GetItemAllName(detailItem);
                }
            }

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                data.Count);

            return content;
        }

        #endregion

        #region GetSkuByIdData 通过ID获取SKU

        /// <summary>
        /// 通过ID获取SKU
        /// </summary>
        public string GetSkuByIdData()
        {
            var skuService = new SkuService(CurrentUserInfo);
            SkuInfo item;

            string key = string.Empty;
            string content = string.Empty;
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                key = FormatParamValue(Request("id")).ToString().Trim();
            }

            item = skuService.GetSkuInfoById(key);
            item.display_name = SkuService.GetItemAllName(item);

            var jsonData = new JsonData();
            jsonData.totalCount = item == null ? "0" : "1";
            jsonData.data = item;

            content = jsonData.ToJSON();
            return content;
        }

        #endregion
    }

    #region QueryEntity

    public class CcOrderQueryEntity
    {
        public string order_id;
        public string order_no;
        public string order_type_id;
        public string order_reason_id;
        public string ref_order_id;
        public string ref_order_no;
        public string order_date_begin;
        public string order_date_end;
        public string request_date_begin;
        public string request_date_end;
        public string complete_date_begin;
        public string complete_date_end;
        public string unit_id;
        public string pos_id;
        public string warehouse_id;
        public string remark;
        public string status;
        public string status_desc;
        public string create_time;
        public string create_user_id;
        public string modify_time;
        public string modify_user_id;
        public string data_from_id;
        public string send_user_id;
        public string send_time;
        public string approve_user_id;
        public string approve_time;
        public string accept_user_id;
        public string accept_time;
        public string if_flag;
        public string unit_name;
        public string warehouse_name;
        public string total_qty;
    }

    #endregion
}