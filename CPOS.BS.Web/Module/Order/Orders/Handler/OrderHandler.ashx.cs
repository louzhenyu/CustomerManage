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

namespace JIT.CPOS.BS.Web.Module.Order.Orders.Handler
{
    /// <summary>
    /// OrderHandler
    /// </summary>
    public class OrderHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "order_delete": //订单删除
                    content = OrderDeleteData();
                    break;
                case "order_pass": //订单审核
                    content = OrderPassData();
                    break;
                case "search_purchase_order":  //采购订单查询
                    content = GetPurchaseOrderData();
                    break;
                case "purchase_return_order":  //采购退货单查询
                    content = GetPurchaseReturnOrderData();
                    break;
                case "sales_order":  //销售订单查询
                    content = GetSalesOrderData();
                    break;
                case "sales_return_order":  //销售退货单查询
                    content = GetSalesReturnOrderData();
                    break;
                case "purchase_order_save": //采购订单保存
                    content = SavePurchaseOrder();
                    break;
                case "purchase_return_order_save": //采购退货订单保存
                    content = SavePurchaseReturnOrder();
                    break;
                case "sales_order_save": //销售订单保存
                    content = SaveSalesOrder();
                    break;
                case "sales_return_order_save": //销售退货订单保存
                    content = SaveSalesReturnOrder();
                    break;
                case "get_order_info_by_id": //获取单个订单主信息
                    content = GetOrderInfoById();
                    break;
                case "get_order_detail_info_by_id": //获取单个订单明细
                    content = GetOrderDetailInfoById();
                    break;
                case "purchase_return_out_order_save": //采购退货出库单保存 
                    content = SavePurchaseReturnOutOrder();
                    break;
                case "get_sku_by_id":
                    content = GetSkuByIdData();
                    break;
            }

            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetList
        private string GetList(NameValueCollection rParams)
        {
            //OrderInfo entity = rParams["form"].DeserializeJSONTo<OrderInfo>();
            //int pageSize = rParams["limit"].ToInt();
            //int pageIndex = rParams["page"].ToInt();
            //int rowCount = 0;

            return string.Empty;

            //return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
            //   new BrandBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
            //    rowCount);
        }
        #endregion

        #region Delete
        private string Delete(string ids)
        {
            string res = "[{success:false}]";
            //if (new BrandBLL(CurrentUserInfo).Delete(ids))
            //{
            //    res = "[{success:true}]";
            //}
            return res;
        }
        #endregion

        #region GetBrandByID
        private string GetBrandByID(string id)
        {
            return string.Empty;
            //return "[" + new BrandBLL(CurrentUserInfo).GetByID(id).ToJSON() + "]";
        }
        #endregion

        #region Edit
        private string Edit(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";

            ////组装参数
            //BrandEntity entity = new BrandEntity();
            //if (!string.IsNullOrEmpty(rParams["id"]))
            //{
            //    entity = new BrandBLL(CurrentUserInfo).GetByID(rParams["id"]);
            //}
            //entity = DataLoader.LoadFrom<BrandEntity>(rParams, entity);
            //entity.ClientID = CurrentUserInfo.ClientID;
            //if (!string.IsNullOrEmpty(rParams["id"]))
            //{
            //    new BrandBLL(CurrentUserInfo).Update(entity);
            //    res = "{success:true,msg:'编辑成功'}";
            //}
            //else
            //{
            //    new BrandBLL(CurrentUserInfo).Create(entity);
            //    res = "{success:true,msg:'编辑成功'}";
            //}
            return res;
        }
        #endregion

        #region GetOrderData 查询
        /// <summary>
        /// 获取采购订单
        /// </summary>
        public string GetPurchaseOrderData()
        {
            return GetOrderData(
                "D0E57A26D0C34A818ECA346DCFE33BA5",
                "1D193727FCD3469294B59F602C7E3F1A", "1");
        }
        /// <summary>
        /// 采购退货订单
        /// </summary>
        /// <returns></returns>
        public string GetPurchaseReturnOrderData()
        {
            return GetOrderData(
                    "D0E57A26D0C34A818ECA346DCFE33BA5",
                    "21B88CE9916A4DB4A1CAD8E3B4618C10", "-1");
        }

        /// <summary>
        /// 获取销售订单
        /// </summary>
        public string GetSalesOrderData()
        {
            return GetOrderData(
                "65CB7CD396894172B1449B1B95D44171",
                "E8F2E4C139394CEDBB7B0FCC7623FFF8", "1");
        }
        /// <summary>
        /// 销售退货订单
        /// </summary>
        /// <returns></returns>
        public string GetSalesReturnOrderData()
        {
            return GetOrderData(
                    "65CB7CD396894172B1449B1B95D44171",
                    "378F810BD7844C48A10E0FF71E6F5353", "-1");
        }

        #region GetOrderData
        /// <summary>
        /// 订单查询
        /// </summary>
        public string GetOrderData(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var form = Request("form").DeserializeJSONTo<OrderQueryEntity>();

            OrderService orderService = new OrderService(CurrentUserInfo);

            OrderInfo data;
            string content = string.Empty;

            string order_no = FormatParamValue(form.order_no);
            string sales_unit_id = "";
            string purchase_unit_id = "";

            if (order_type_id.Equals("65CB7CD396894172B1449B1B95D44171"))   //销售
            {
                sales_unit_id = FormatParamValue(Request("sales_unit_id"));
                purchase_unit_id = FormatParamValue(form.purchase_unit_id);
            }
            else if (order_type_id.Equals("D0E57A26D0C34A818ECA346DCFE33BA5"))  //采购
            {
                sales_unit_id = FormatParamValue(form.sales_unit_id);
                purchase_unit_id = FormatParamValue(Request("purchase_unit_id"));
            }

            string order_status = FormatParamValue(form.order_status);
            string order_date_begin = FormatParamValue(form.order_date_begin);
            string order_date_end = FormatParamValue(form.order_date_end);
            string request_date_begin = FormatParamValue(form.request_date_begin);
            string request_date_end = FormatParamValue(form.request_date_end);
            string data_from_id = FormatParamValue(form.data_from_id);
            string ref_order_no = FormatParamValue(form.ref_order_no);
            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("start")));

            string key = string.Empty;
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                key = FormatParamValue(Request("id")).ToString().Trim();
            }

            data = orderService.SearchOrderInfo(
                order_no
                , order_type_id
                , order_reason_type_id
                , red_flag
                , order_status
                , purchase_unit_id
                , sales_unit_id
                , order_date_begin
                , order_date_end
                , request_date_begin
                , request_date_end
                , maxRowCount
                , startRowIndex);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.orderInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #endregion

        #region OrderDeleteData 删除订单单据
        /// <summary>
        /// 删除订单单据
        /// </summary>
        public string OrderDeleteData()
        {
            OrderService orderService = new OrderService(CurrentUserInfo);

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
                orderService.SetOrderStatusUpdate(id.Trim(), BillActionType.Cancel, out error);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region OrderPassData 审核订单
        /// <summary>
        /// 审核订单单据
        /// </summary>
        public string OrderPassData()
        {
            var orderService = new OrderService(CurrentUserInfo);
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
                orderService.SetOrderStatusUpdate(id.Trim(), BillActionType.Approve, out error);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetOrderInfoById 获取订单主信息
        /// <summary>
        /// 获取订单单据主信息
        /// </summary>
        public string GetOrderInfoById()
        {
            var orderService = new OrderService(CurrentUserInfo);
            OrderInfo data;
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                key = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            data = orderService.GetOrderInfoById(key);

            var jsonData = new JsonData();
            jsonData.totalCount = data == null ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                data == null ? "0" : "1");
            return content;
        }
        #endregion

        #region GetOrderDetailInfoById 获取订单明细
        /// <summary>
        /// 获取订单单据明细信息
        /// </summary>
        public string GetOrderDetailInfoById()
        {
            var orderService = new OrderService(CurrentUserInfo);
            IList<OrderDetailInfo> data = null;
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                key = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            var order = orderService.GetOrderInfoById(key);
            if (order != null)
            {
                data = order.orderDetailList;
                foreach (var orderDetailItem in order.orderDetailList)
                {
                    orderDetailItem.display_name = SkuService.GetItemAllName(orderDetailItem);
                }
            }

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                order.orderDetailList.ToJSON(),
                order.orderDetailList.Count);
            return content;
        }
        #endregion

        #region 订单保存
        /// <summary>
        /// 保存采购订单
        /// </summary>
        public string SavePurchaseOrder()
        {
            return SaveOrder(
                "D0E57A26D0C34A818ECA346DCFE33BA5",
                "1D193727FCD3469294B59F602C7E3F1A",
                "1");
        }
        /// <summary>
        /// 采购退货订单保存
        /// </summary>
        /// <returns></returns>
        public string SavePurchaseReturnOrder()
        {
            return SaveOrder(
                "D0E57A26D0C34A818ECA346DCFE33BA5",
                "21B88CE9916A4DB4A1CAD8E3B4618C10",
                "-1");
        }
        /// <summary>
        /// 销售订单保存
        /// </summary>
        /// <returns></returns>
        public string SaveSalesOrder()
        {
            return SaveOrder(
                    "65CB7CD396894172B1449B1B95D44171",
                    "E8F2E4C139394CEDBB7B0FCC7623FFF8",
                    "1");
        }
        /// <summary>
        /// 销售退货订单保存
        /// </summary>
        /// <returns></returns>
        public string SaveSalesReturnOrder()
        {
            return SaveOrder(
                    "65CB7CD396894172B1449B1B95D44171",
                    "378F810BD7844C48A10E0FF71E6F5353",
                    "-1");
        }

        #region SaveOrder 保存订单
        /// <summary>
        /// 保存订单单据
        /// </summary>
        public string SaveOrder(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var orderService = new OrderService(CurrentUserInfo);
            OrderInfo order = new OrderInfo();
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

            order = key.DeserializeJSONTo<OrderInfo>(); // JsonConvert.Import<OrderInfo>(key);
            order.order_type_id = order_type_id;
            order.order_reason_type_id = order_reason_type_id;

            if (order_id.Trim().Length == 0)
            {
                order.order_id = Utils.NewGuid();
                order.create_unit_id = CurrentUserInfo.CurrentUserRole.UnitId;
                order.red_flag = red_flag;
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
            if (order.request_date == null || order.request_date.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "预计日期不能为空";
                return responseData.ToJSON();
            }
            if (order.sales_unit_id == null || order.sales_unit_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "供应商不能为空";
                return responseData.ToJSON();
            }
            if (order.purchase_unit_id == null || order.purchase_unit_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "采购单位不能为空";
                return responseData.ToJSON();
            }

            if (order.orderDetailList == null || order.orderDetailList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "商品不能为空";
                return responseData.ToJSON();
            }

            orderService.SetOrderInfo(order, false, out error);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion


        /// <summary>
        /// 采购退货出库单保存
        /// Created By Yuangxi @ 2012-12-26
        /// </summary>
        /// <returns></returns>
        public string SavePurchaseReturnOutOrder()
        {
            return SaveOrder(
                "1F0A100C42484454BAEA211D4C14B80F",
                "E36E70EFDFA241E6A36A724EB1CD4D2D",
                "-1");
        }

        #endregion

        #region GetSkuByIdData
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
    public class OrderQueryEntity
    {
        public string order_id;
        public string order_no;
        public string order_type_id;
        public string order_reason_type_id;
        public string red_flag;
        public string ref_order_id;
        public string ref_order_no;
        public string order_date_begin;
        public string order_date_end;
        public string request_date_begin;
        public string request_date_end;
        public string complete_date_begin;
        public string complete_date_end;
        public string create_unit_id;
        public string unit_id;
        public string related_unit_id;
        public string ref_unit_id;
        public string total_amount;
        public string discount_rate;
        public string actual_amount;
        public string receive_points;
        public string pay_points;
        public string pay_id;
        public string print_times;
        public string carrier_id;
        public string remark;
        public string order_status;
        public string order_status_desc;
        public string create_time;
        public string create_user_id;
        public string approve_time;
        public string approve_user_id;
        public string send_user_id;
        public string send_time;
        public string accpect_user_id;
        public string accpect_time;
        public string modify_user_id;
        public string modify_time;
        public string total_qty;
        public string total_retail;
        public string sales_unit_id;
        public string purchase_unit_id;
        public string if_flag;
        public string sales_unit_name;
        public string purchase_unit_name;
        public string data_from_id;
    }
    #endregion

}