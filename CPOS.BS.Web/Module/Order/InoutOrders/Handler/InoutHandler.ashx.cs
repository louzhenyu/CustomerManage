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
using System.Text;
using Aspose.Cells;
using System.IO;

namespace JIT.CPOS.BS.Web.Module.Order.InoutOrders.Handler
{
    /// <summary>
    /// InoutHandler 的摘要说明
    /// </summary>
    public class InoutHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "InoutOrderPass"://审核出入库单据
                    content = InoutOrderPassData();
                    break;
                case "InoutOrderDelete"://删除出入库单据
                    content = InoutOrderDeleteData();
                    break;

                case "PurchaseInOrder": //获取采购入库单
                    content = GetPurchaseInOrderData();
                    break;
                case "AjOrder": //获取库存调整单
                    content = GetAjOrderData();
                    break;
                case "MvInOutOrder": //获取调拨出入库单
                    content = GetMvInOutOrderData();
                    break;
                case "PosOrder": //GetPosOrderData()
                    content = GetPosOrderData();
                    break;
                case "PurchaseReturnOutOrder"://采购退货出库(PurchaseReturnOutOrder) Create By Yuangxi @ 20121224
                    content = GetPurchaseReturnOutOrderData();
                    break;
                case "SalesOutOrder": //销售出库单
                    content = GetSalesOutOrder();
                    break;
                case "SalesReturnInOrder":
                    content = GetSalesReturnInOrder();  //销售退货入库单
                    break;
                case "RetailOrder":
                    content = GetRetailOrder(); //零售销货单查询
                    break;
                case "RetailReturnOrder":
                    content = GetRetailReturnOrder(); //零售退货单查询
                    break;
                case "BatchOrder":
                    content = GetBatchOrder(); //批发单查询
                    break;
                case "BatchReturnOrder":
                    content = GetBatchReturnOrder(); //批发退货单查询
                    break;
                case "PurchaseInOrder_Save":
                    content = SavePurchaseInOrder();
                    break;
                case "SalesOutOrder_Save":  //销售出库单
                    content = SalesOutOrder();
                    break;
                case "SalesOrder_Create":  //新建订单
                    content = SalesOrderCreate();
                    break;
                case "SalesReturnInOrder_Save":
                    content = SaveSalesReturnInOrder(); //销售退货入库保存
                    break;
                case "PurchaseReturnOutOrder_Save"://采购退货出库
                    content = SavePurchaseReturnOutOrder();
                    break;
                case "RetailOrder_Save":    //零售销货单保存
                    content = SaveRetailOrder();
                    break;
                case "RetailReturnOrder_Save":    //零售退货单保存
                    content = SaveRetailReturnOrder();
                    break;
                case "BatchOrder_Save":    //批发单保存
                    content = SaveBatchOrder();
                    break;
                case "BatchReturnOrder_Save":    //批发退货单保存
                    content = SaveBatchReturnOrder();
                    break;
                case "AjOrder_Save":
                    content = SaveAjOrder();
                    break;
                case "MvInOutOrder_Save":
                    content = SaveMvInOutOrder();
                    break;

                case "GetInoutInfoById":
                    content = GetInoutInfoById();
                    break;
                case "GetInoutDetailInfoById":
                    content = GetInoutDetailInfoById();
                    break;
                case "get_sku_by_id": //获取sku信息
                    content = GetSkuByIdData();
                    break;
                case "StoreQuery":
                    //content = GetStoreQueryData();
                    break;
                case "DeliveryOrder": //配送订单
                    content = GetDeliveryOrder();
                    break;
                case "VoucherOrder": //结算凭证
                    content = GetVoucherOrder();
                    break;
                case "PosOrderDeliveryUpdate":
                    content = PosOrderDeliveryUpdate();
                    break;
                case "GetPosOrderTotalCount":
                    content = GetPosOrderTotalCount();
                    break;
                case "SavePosDelivery":
                    content = SavePosDelivery();
                    break;
                case "UpdateOrderUnit":
                    content = UpdateOrderUnit();
                    break;
                case "GetOrderTable":
                    content = GetOrderTable();
                    break;
                case "Export":
                    Export(pContext);
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();



        }

        #region GetOrderTable
        /// <summary>
        /// GetOrderTable
        /// </summary>
        public string GetOrderTable()
        {
            var service = new GOrderBLL(CurrentUserInfo);
            var content = new StringBuilder();

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }
            VwUnitPropertyBLL server = new VwUnitPropertyBLL(this.CurrentUserInfo);
            
            var data = new GOrderEntity();
            var list = server.GetUnitPropertyByOrderId(key);
            content.AppendFormat("<table class=\"z_tk_2\" style=\"width:100%;\" >");
            content.AppendFormat("<tr>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:5%;font-weight:bold;\">&nbsp;</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:13%;font-weight:bold;\">门店名</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:5%;font-weight:bold;\">库存</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:5%;font-weight:bold;\">距离</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:10%;font-weight:bold;\">会籍店</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:20%;font-weight:bold;\">门店地址</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:20%;font-weight:bold;\">门店联系电话</td>");
            content.AppendFormat("</tr>");
            content.AppendFormat("");
            int StockCount = 50;
            int Distance = 5;
            if (list != null && list.Count > 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    content.AppendFormat("<tr>");
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"text-align:center;\"><input type=\"radio\" name=\"order\" value=\"{0}\" /></td>", list[i].UnitId);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", list[i].UnitName);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", StockCount);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", Distance);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", list[i].IsVipStore);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", list[i].ADDRESS);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", list[i].Tel);
                    content.AppendFormat("</tr>");
                    StockCount = StockCount + 50;
                    Distance = Distance + 10;
                }
            }
            content.AppendFormat("</table>");
            return content.ToString();
        }
        #endregion

        #region GetInOutOrderData
        /// <summary>
        /// 获取出入库单据
        /// </summary>
        public string GetInOutOrderData(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo data;
            string content = string.Empty;
            var form = Request("form").DeserializeJSONTo<InoutQueryEntity>();

            string purchase_unit_id = string.Empty;
            if (FormatParamValue(Request("purchase_unit_id")).Equals("") && FormatParamValue(Request("unit_id")) == null)
            {
                purchase_unit_id = FormatParamValue(form.purchase_unit_id);
            }
            else {
                purchase_unit_id = FormatParamValue(Request("purchase_unit_id"));
            }
            string sales_unit_id = string.Empty;
            if (FormatParamValue(Request("sales_unit_id")).Equals("") && FormatParamValue(Request("sales_unit_id")) == null)
            {
                sales_unit_id = FormatParamValue(form.sales_unit_id);
            }
            else
            {
                sales_unit_id = FormatParamValue(Request("sales_unit_id"));
            }

            if (order_reason_type_id.Equals("") || order_reason_type_id == null)
            {
                order_reason_type_id = FormatParamValue(form.order_reason_id);
            }

            string order_no = FormatParamValue(form.order_no);
            //string sales_unit_id = form.sales_unit_id; //"0d2bf77f765849249a0270c0a07fef07";//
            string warehouse_id = FormatParamValue(form.warehouse_id);//Request("warehouse_id");//
            //string purchase_unit_id = form.purchase_unit_id;
            string status = FormatParamValue(form.status);
            string order_date_begin = FormatParamValue(form.order_date_begin);
            string order_date_end = FormatParamValue(form.order_date_end);
            string complete_date_begin = FormatParamValue(form.complete_date_begin);
            string complete_date_end = FormatParamValue(form.complete_date_end);
            string data_from_id = FormatParamValue(form.data_from_id);
            string ref_order_no = FormatParamValue(form.ref_order_no);

            string Field7 = FormatParamValue(Request("Field7"));
            string DefrayTypeId = FormatParamValue(form.DefrayTypeId);
            string DeliveryId = FormatParamValue(form.DeliveryId);

            string Field9_begin = FormatParamValue(form.Field9_begin);
            string Field9_end = FormatParamValue(form.Field9_end);
            string ModifyTime_begin = FormatParamValue(form.ModifyTime_begin);
            string ModifyTime_end = FormatParamValue(form.ModifyTime_end);

            string purchase_warehouse_id = FormatParamValue(form.purchase_warehouse_id);
            string sales_warehouse_id = FormatParamValue(form.sales_warehouse_id);

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            data = inoutService.SearchInoutInfo(
                order_no,
                order_reason_type_id,
                sales_unit_id,
                warehouse_id,
                purchase_unit_id,
                status,
                order_date_begin,
                order_date_end,
                complete_date_begin,
                complete_date_end,
                data_from_id,
                ref_order_no,
                order_type_id,
                red_flag,
                maxRowCount,
                startRowIndex
                ,purchase_warehouse_id
                ,sales_warehouse_id
                , Field7, DeliveryId, DefrayTypeId, Field9_begin, Field9_end, ModifyTime_begin, ModifyTime_end, "", "", 
                CurrentUserInfo.CurrentUserRole.UnitId, null,false);

            //var jsonData = new JsonData();
            //jsonData.totalCount = data.ICount.ToString();
            //jsonData.data = data.InoutInfoList;

            //Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            //Jayrock.Json.Conversion.JsonConvert.Export(jsonData, writer);
            //content = writer.ToString();
            //return content;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.InoutInfoList.ToJSON(),
               data.ICount);
            return content;
        }
        #endregion

        #region 各种单据查询
        #region GetPurchaseInOrderData
        /// <summary>
        /// 获取采购入库单
        /// </summary>
        public string GetPurchaseInOrderData()
        {
            return GetInOutOrderData(
                "C1D407738E1143648BC7980468A399B8",
                "744E8F0352C74B1DB8C415310477A486", "1");
        }
        #endregion

        #region GetAjOrderData
        /// <summary>
        /// 获取库存调整单
        /// </summary>
        public string GetAjOrderData()
        {
            return GetInOutOrderData(
                "5F11A199E3CD42DE9CAE70442FC3D991",
                "F486F9D7A0374B3599D1B3F0016645AA", "1");
        }
        #endregion

        #region GetMvInOutOrderData
        /// <summary>
        /// 获取调拨出入库单
        /// </summary>
        public string GetMvInOutOrderData()
        {
            return GetInOutOrderData(
                "C9DD9DE2B8A64ACEA18EDB4DF47DBF79",
                "", null); // F486F9D7A0374B3599D1B3F0016645AA,12C8C84572934D1F800D84EAFF74CB68
        }
        #endregion

        #region GetPosOrderData
        /// <summary>
        /// POS小票查询
        /// </summary>
        /// <returns></returns>
        public string GetPosOrderData()
        {
            return GetInOutOrderData(
                "1F0A100C42484454BAEA211D4C14B80F",
                "2F6891A2194A4BBAB6F17B4C99A6C6F5", "1");
        }
        #endregion

        #region GetPurchaseInOrderData
        /// <summary>
        ///  采购退货出库 Create By Yuangxi @ 2012-12-24
        /// </summary>
        public string GetPurchaseReturnOutOrderData()
        {
            return GetInOutOrderData(
                "1F0A100C42484454BAEA211D4C14B80F",
                "E36E70EFDFA241E6A36A724EB1CD4D2D", "-1");
        }
        #endregion

        #region GetSalesOutOrder()
        /// <summary>
        /// 销售出库单
        /// </summary>
        /// <returns></returns>
        public string GetSalesOutOrder()
        {
            return GetInOutOrderData(
                "1F0A100C42484454BAEA211D4C14B80F",
                "95C58F1A26174AB08DCCDC6864FEAE93", "1");
        }
        #endregion

        #region GetSalesReturnInOrder 销售退货入库单
        /// <summary>
        /// 销售退货入库单
        /// </summary>
        /// <returns></returns>
        public string GetSalesReturnInOrder()
        {
            return GetInOutOrderData(
               "C1D407738E1143648BC7980468A399B8",
               "A6457C8D9D4F40399E6D875E84A00D6A", "-1");
        }
        #endregion

        #region GetRetailOrder 零售销货单查询
        /// <summary>
        /// 零售销货单查询
        /// </summary>
        /// <returns></returns>
        public string GetRetailOrder()
        {
            return GetInOutOrderData(
                    "120B8FBFC288468DAEB9D620AD8C3DD7",
                    "39427EB4430E4A9FA966FD32ECDECC0F", "1");
        }
        #endregion

        #region GetRetailReturnOrder 零售退货单查询
        /// <summary>
        /// 零售退货单查询
        /// </summary>
        /// <returns></returns>
        public string GetRetailReturnOrder()
        {
            return GetInOutOrderData(
                    "120B8FBFC288468DAEB9D620AD8C3DD7",
                    "B8870FD2FB154D0A92DE3C0F456F735F", "-1");
        }
        #endregion

        #region GetBatchOrder() 批发单查询
        public string GetBatchOrder()
        {
            return GetInOutOrderData(
                        "A20110E502C944B5BB9B3E4A49CB5BCC",
                        "085EBC461C1F4CEC88A4F1A553993B6D", "1");
        }
        #endregion

        #region GetBatchReturnOrder() //批发退货单查询
        public string GetBatchReturnOrder()
        {
            return GetInOutOrderData(
                        "A20110E502C944B5BB9B3E4A49CB5BCC",
                        "0C3DFFBD98E44B6C93767B17DC804174", "-1");
        }
        #endregion
        #endregion

        #region 查询单个单据信息
        #region GetInoutInfoById
        /// <summary>
        /// 获取出入库单据信息
        /// </summary>
        public string GetInoutInfoById()
        {
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo data;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("order_id") != null && Request("order_id") != string.Empty)
            {
                key = Request("order_id").ToString().Trim();
            }

            data = inoutService.GetInoutInfoById(key);

            var jsonData = new JsonData();
            jsonData.totalCount = data == null ? "0" : "1";
            jsonData.data = data;

            //Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            //Jayrock.Json.Conversion.JsonConvert.Export(jsonData, writer);
            //content = writer.ToString();
            //return content;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                data == null ? "0" : "1");
            return content;

        }
        #endregion

        #region GetInoutDetailInfoById
        /// <summary>
        /// 获取出入库单据明细信息
        /// </summary>
        public string GetInoutDetailInfoById()
        {
            var inoutService = new InoutService(CurrentUserInfo);
            IList<InoutDetailInfo> data = null;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("order_id") != null && Request("order_id") != string.Empty)
            {
                key = Request("order_id").ToString().Trim();
            }

            var order = inoutService.GetInoutInfoById(key);
            if (order != null)
            {
                data = order.InoutDetailList;
                foreach (var inoutDetailItem in order.InoutDetailList)
                {
                    inoutDetailItem.display_name = SkuService.GetItemAllName(inoutDetailItem);
                }
            }

            var jsonData = new JsonData();
            jsonData.totalCount = order.InoutDetailList == null ? "0" : order.InoutDetailList.Count.ToString();
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                order.InoutDetailList.ToJSON(),
                order.InoutDetailList.Count);
            return content;

        }
        #endregion
        #endregion

        #region SaveInoutOrder
        /// <summary>
        /// 保存出入库单据
        /// </summary>
        public string SaveInoutOrder(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string order_id = string.Empty;
            if (Request("order") != null && Request("order") != string.Empty)
            {
                key = Request("order").ToString().Trim();
            }
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                order_id = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            order = key.DeserializeJSONTo<InoutInfo>();
            order.order_type_id = order_type_id;
            order.order_reason_id = order_reason_type_id;

            if (order_id.Trim().Length == 0 || order_id.Trim().Equals("null"))
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
            if (order.complete_date == null || order.complete_date.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "入库日期不能为空";
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
            if (order.warehouse_id == null || order.warehouse_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "仓库不能为空";
                return responseData.ToJSON();
            }
            if (order.InoutDetailList == null || order.InoutDetailList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "商品不能为空";
                return responseData.ToJSON();
            }

            inoutService.SetInoutInfo(order, false, out error);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }


        /// <summary>
        /// 新建订单
        /// </summary>
        public string SaveOrderCreate(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string order_id = string.Empty;
            if (Request("order") != null && Request("order") != string.Empty)
            {
                key = Request("order").ToString().Trim();
            }
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                order_id = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            order = key.DeserializeJSONTo<InoutInfo>();
            order.order_type_id = order_type_id;
            order.order_reason_id = order_reason_type_id;

            if (order_id.Trim().Length == 0 || order_id.Trim().Equals("null"))
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
            //if (order.complete_date == null || order.complete_date.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "入库日期不能为空";
            //    return responseData.ToJSON();
            //}
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
            //if (order.warehouse_id == null || order.warehouse_id.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "仓库不能为空";
            //    return responseData.ToJSON();
            //}
            if (order.InoutDetailList == null || order.InoutDetailList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "商品不能为空";
                return responseData.ToJSON();
            }

            order.Field7 = "700";
            order.data_from_id = "8";
            order.Field1 = "1";

            inoutService.SetInoutInfo(order, false, out error);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 单据保存类型
        #region SavePurchaseInOrder
        /// <summary>
        /// 保存采购入库单
        /// </summary>
        public string SavePurchaseInOrder()
        {
            return SaveInoutOrder(
                "C1D407738E1143648BC7980468A399B8",
                "744E8F0352C74B1DB8C415310477A486",
                "1");
        }
        #endregion

        #region SalesOutOrder()
        /// <summary>
        /// 销售出库单保存
        /// </summary>
        /// <returns></returns>
        public string SalesOutOrder()
        {
            return SaveInoutOrder(
                    "1F0A100C42484454BAEA211D4C14B80F",
                    "95C58F1A26174AB08DCCDC6864FEAE93",
                    "1");
        }


        /// <summary>
        /// 新建订单
        /// </summary>
        /// <returns></returns>
        public string SalesOrderCreate()
        {
            return SaveOrderCreate(
                    "1F0A100C42484454BAEA211D4C14B80F",
                    "95C58F1A26174AB08DCCDC6864FEAE93",
                    "1");
        }
        #endregion

        #region SaveSalesReturnInOrder 销售退货入库保存
        public string SaveSalesReturnInOrder()
        {
            return SaveInoutOrder(
                   "C1D407738E1143648BC7980468A399B8",
                   "A6457C8D9D4F40399E6D875E84A00D6A",
                   "-1");
        }
        #endregion

        #region SavePurchaseReturnOutOrder
        /// <summary>
        /// 保存采购入库单
        /// </summary>
        public string SavePurchaseReturnOutOrder()
        {
            return SaveInoutOrder(
                "1F0A100C42484454BAEA211D4C14B80F",
                "E36E70EFDFA241E6A36A724EB1CD4D2D",
                "-1");
        }
        #endregion

        #region SaveRetailOrder 零售销货单

        public string SaveRetailOrder()
        {
            return SaveInoutOrder(
                "120B8FBFC288468DAEB9D620AD8C3DD7",
                "39427EB4430E4A9FA966FD32ECDECC0F",
                "1");
        }
        #endregion

        #region SaveRetailReturnOrder 零售退货单

        public string SaveRetailReturnOrder()
        {
            return SaveInoutOrder(
                "120B8FBFC288468DAEB9D620AD8C3DD7",
                "B8870FD2FB154D0A92DE3C0F456F735F",
                "-1");
        }
        #endregion

        #region SaveBatchOrder 批发单保存

        public string SaveBatchOrder()
        {
            return SaveInoutOrder(
                "A20110E502C944B5BB9B3E4A49CB5BCC",
                "085EBC461C1F4CEC88A4F1A553993B6D",
                "1");
        }
        #endregion

        #region SaveBatchReturnOrder 批发退货单保存

        public string SaveBatchReturnOrder()
        {
            return SaveInoutOrder(
                "A20110E502C944B5BB9B3E4A49CB5BCC",
                "0C3DFFBD98E44B6C93767B17DC804174",
                "-1");
        }
        #endregion
        #endregion

        #region SaveAjOrder
        /// <summary>
        /// 保存库存调整单
        /// </summary>
        public string SaveAjOrder()
        {
            string order_type_id = "5F11A199E3CD42DE9CAE70442FC3D991";
            string order_reason_type_id = "F486F9D7A0374B3599D1B3F0016645AA";
            string red_flag = "1";
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string order_id = string.Empty;
            if (Request("order") != null && Request("order") != string.Empty)
            {
                key = Request("order").ToString().Trim();
            }
            if (Request("order_id") != null && Request("order_id") != string.Empty)
            {
                order_id = Request("order_id").ToString().Trim();
            }

            order = key.DeserializeJSONTo<InoutInfo>(); 
            order.order_type_id = order_type_id;
            order.order_reason_id = order_reason_type_id;

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
                responseData.msg = "调整日期不能为空";
                return responseData.ToJSON();
            }
            if (order.purchase_unit_id == null || order.purchase_unit_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "调整单位不能为空";
                return responseData.ToJSON();
            }
            if (order.warehouse_id == null || order.warehouse_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "仓库不能为空";
                return responseData.ToJSON();
            }
            if (order.InoutDetailList == null || order.InoutDetailList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "商品不能为空";
                return responseData.ToJSON();
            }

            inoutService.SetInoutInfo(order, false, out error);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region SaveMvInOutOrder
        /// <summary>
        /// 保存调拨出入库单
        /// </summary>
        public string SaveMvInOutOrder()
        {
            string order_type_id = "C9DD9DE2B8A64ACEA18EDB4DF47DBF79";
            string red_flag = "1";
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string order_id = string.Empty;
            if (Request("order") != null && Request("order") != string.Empty)
            {
                key = Request("order").ToString().Trim();
            }
            if (Request("order_id") != null && Request("order_id") != string.Empty)
            {
                order_id = Request("order_id").ToString().Trim();
            }

            order = key.DeserializeJSONTo<InoutInfo>();
            order.order_type_id = order_type_id;

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

            if (order.order_reason_id == "AE6014B8F8194489A74B33C67526BF39") // 调拨出库
            {
                order.red_flag = "-1";
            }
            else
            {
                order.red_flag = "1";
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
            if (order.order_reason_id == null || order.order_reason_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "单据类型不能为空";
                return responseData.ToJSON();
            }
            if (order.sales_unit_id == null || order.sales_unit_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "出库单位不能为空";
                return responseData.ToJSON();
            }
            if (order.sales_warehouse_id == null || order.sales_warehouse_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "出库仓库不能为空";
                return responseData.ToJSON();
            }
            if (order.purchase_unit_id == null || order.purchase_unit_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "入库单位不能为空";
                return responseData.ToJSON();
            }
            if (order.purchase_warehouse_id == null || order.purchase_warehouse_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "入库仓库不能为空";
                return responseData.ToJSON();
            }
            if (order.sales_unit_id == order.purchase_unit_id)
            {
                responseData.success = false;
                responseData.msg = "出入库单位不能相同";
                return responseData.ToJSON();
            }
            if (order.sales_warehouse_id == order.purchase_warehouse_id)
            {
                responseData.success = false;
                responseData.msg = "出入库仓库不能相同";
                return responseData.ToJSON();
            }
            if (order.InoutDetailList == null || order.InoutDetailList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "商品不能为空";
                return responseData.ToJSON();
            }

            inoutService.SetInoutInfo(order, false, out error);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region InoutOrderPassData
        /// <summary>
        /// 审核出入库单据
        /// </summary>
        public string InoutOrderPassData()
        {
            var inoutService = new InoutService(CurrentUserInfo);
            var stockBalanceService = new StockBalanceService(CurrentUserInfo);
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (Request("order_id") != null && Request("order_id") != string.Empty)
            {
                key = Request("order_id").ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "单据ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            var statusFlag = false;
            foreach (var id in ids)
            {
                statusFlag = inoutService.SetInoutOrderStatus(id.Trim(), BillActionType.Approve);
                if (statusFlag) stockBalanceService.SetStockBalance(id.Trim());
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region InoutOrderDeleteData
        /// <summary>
        /// 删除出入库单据
        /// </summary>
        public string InoutOrderDeleteData()
        {
            var inoutService = new InoutService(CurrentUserInfo);
            string content = string.Empty;
            string error = "ok";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (Request("ids") != null && Request("ids") != string.Empty)
            {
                key = Request("ids").ToString().Trim();
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
                inoutService.SetInoutOrderStatus(id.Trim(), BillActionType.Cancel);
            }

            responseData.success = true;
            responseData.msg = error;
            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetStoreQueryData
        /// <summary>
        /// 获取库存
        /// </summary>
        //public string GetStoreQueryData()
        //{
        //    var stockService = new StockBalanceService(CurrentUserInfo);
        //    StockBalanceInfo data = new StockBalanceInfo();
        //    data.icount = 0;
        //    string content = string.Empty;

        //    string unit_id = Request["unit_id"];
        //    string warehouse_id = Request["warehouse_id"];
        //    string item_name = Request["item_name"];
        //    int maxRowCount = OrderPageSize;
        //    int startRowIndex = Utils.GetIntVal(Request["start"]);

        //    string key = string.Empty;
        //    if (Request["id"] != null && Request["id"] != string.Empty)
        //    {
        //        key = Request["id"].ToString().Trim();
        //    }
        //    if (item_name == null)
        //    {
        //        item_name = string.Empty;
        //    }

        //    data = stockService.SearchStockBalance(
        //        unit_id,
        //        warehouse_id,
        //        item_name,
        //        key,
        //        maxRowCount,
        //        startRowIndex);

        //    var jsonData = new JsonData();
        //    jsonData.totalCount = data.icount.ToString();
        //    jsonData.data = data.StockBalanceInfoList;

        //    Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
        //    Jayrock.Json.Conversion.JsonConvert.Export(jsonData, writer);
        //    content = writer.ToString();
        //    return content;
        //}
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
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
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

        #region GetDeliveryOrder()
        /// <summary>
        /// 配送订单
        /// </summary>
        /// <returns></returns>
        public string GetDeliveryOrder()
        {
            return GetInOutOrderData(
                "6F4991A2F4A84CC3902BD880BF540DF1",
                "BAFA1B7A50914599BD7DC830B53203FA", "1");
        }
        #endregion

        #region GetVoucherOrder()
        /// <summary>
        /// 结算凭证
        /// </summary>
        /// <returns></returns>
        public string GetVoucherOrder()
        {
            return GetInOutOrderData(
                "1F0A100C42484454BAEA211D4C14B80F",
                "2F6891A2194A4BBAB6F17B4C99A6C6F5", "1");
        }
        #endregion

        #region PosOrderDeliveryUpdate
        /// <summary>
        /// PosOrderDeliveryUpdate
        /// </summary>
        public string PosOrderDeliveryUpdate()
        {
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string ids = string.Empty;
            string Field7 = string.Empty;
            if (Request("order") != null && Request("order") != string.Empty)
            {
                key = Request("order").ToString().Trim();
            }
            if (Request("ids") != null && Request("ids") != string.Empty)
            {
                ids = Request("ids").ToString().Trim();
            }
            if (Request("Field7") != null && Request("Field7") != string.Empty)
            {
                Field7 = Request("Field7").ToString().Trim();
            }

            if (ids == null || ids.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "单据ID不能为空";
                return responseData.ToJSON();
            }

            var idList = ids.Split(',');
            foreach (var id in idList)
            {
                if (id != null && id.Trim().Length > 0)
                {
                    inoutService.UpdateOrderDeliveryStatus(id, Field7,null,null);
                }
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetPosOrderTotalCount
        /// <summary>
        /// GetPosOrderTotalCount
        /// </summary>
        public string GetPosOrderTotalCount()
        {
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo data;
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            var form = Request("form").DeserializeJSONTo<InoutQueryEntity>();
            
            var order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            var order_reason_type_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            var red_flag = "1";

            string purchase_unit_id = string.Empty;
            if (FormatParamValue(Request("purchase_unit_id")).Equals("") && FormatParamValue(Request("unit_id")) == null)
            {
                purchase_unit_id = FormatParamValue(form.purchase_unit_id);
            }
            else
            {
                purchase_unit_id = FormatParamValue(Request("purchase_unit_id"));
            }
            string sales_unit_id = string.Empty;
            if (FormatParamValue(Request("sales_unit_id")).Equals("") && FormatParamValue(Request("sales_unit_id")) == null)
            {
                sales_unit_id = FormatParamValue(form.sales_unit_id);
            }
            else
            {
                sales_unit_id = FormatParamValue(Request("sales_unit_id"));
            }

            if (order_reason_type_id.Equals("") || order_reason_type_id == null)
            {
                order_reason_type_id = FormatParamValue(form.order_reason_id);
            }

            string order_no = FormatParamValue(form.order_no);
            //string sales_unit_id = form.sales_unit_id; //"0d2bf77f765849249a0270c0a07fef07";//
            string warehouse_id = FormatParamValue(form.warehouse_id);//Request("warehouse_id");//
            //string purchase_unit_id = form.purchase_unit_id;
            string status = FormatParamValue(form.status);
            string order_date_begin = FormatParamValue(form.order_date_begin);
            string order_date_end = FormatParamValue(form.order_date_end);
            string complete_date_begin = FormatParamValue(form.complete_date_begin);
            string complete_date_end = FormatParamValue(form.complete_date_end);
            string data_from_id = FormatParamValue(form.data_from_id);
            string ref_order_no = FormatParamValue(form.ref_order_no);

            string Field7 = FormatParamValue(Request("Field7"));
            string DefrayTypeId = FormatParamValue(form.DefrayTypeId);
            string DeliveryId = FormatParamValue(form.DeliveryId);

            string purchase_warehouse_id = FormatParamValue(form.purchase_warehouse_id);
            string sales_warehouse_id = FormatParamValue(form.sales_warehouse_id);

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            data = inoutService.SearchInoutInfo(
                order_no,
                order_reason_type_id,
                sales_unit_id,
                warehouse_id,
                purchase_unit_id,
                status,
                order_date_begin,
                order_date_end,
                complete_date_begin,
                complete_date_end,
                data_from_id,
                ref_order_no,
                order_type_id,
                red_flag,
                maxRowCount,
                startRowIndex
                , purchase_warehouse_id
                , sales_warehouse_id
                , Field7, DeliveryId, DefrayTypeId,"","","","","","",CurrentUserInfo.CurrentUserRole.UnitId, null,false);


            var d = new GetPosOrderTotalCountEntity();
            d.num1 = data.StatusCount1;
            d.num2 = data.StatusCount2;
            d.num3 = data.StatusCount3;
            d.num4 = data.StatusCount0;
            responseData.data = d;

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        private class GetPosOrderTotalCountEntity
        {
            public int num1;
            public int num2;
            public int num3;
            public int num4;
        }
        #endregion

        #region SavePosDelivery
        /// <summary>
        /// 保存出入库单据
        /// </summary>
        public string SavePosDelivery()
        {
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string order_id = string.Empty;
            if (Request("order") != null && Request("order") != string.Empty)
            {
                key = Request("order").ToString().Trim();
            }
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                order_id = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            order = key.DeserializeJSONTo<InoutInfo>();
            order.order_id = order_id;

            inoutService.UpdateOrderDelivery(order.order_id, order.carrier_id, order.Field2);
            inoutService.UpdateOrderDeliveryStatus(order.order_id, order.Field7, null,null);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region UpdateOrderUnit
        /// <summary>
        /// 修改单位
        /// </summary>
        public string UpdateOrderUnit()
        {
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string order_id = string.Empty;
            if (Request("order") != null && Request("order") != string.Empty)
            {
                key = Request("order").ToString().Trim();
            }
            if (FormatParamValue(Request("order_id")) != null && FormatParamValue(Request("order_id")) != string.Empty)
            {
                order_id = FormatParamValue(Request("order_id")).ToString().Trim();
            }

            order = key.DeserializeJSONTo<InoutInfo>();
            order.order_id = order_id;
            order.modify_time = Utils.GetNow();
            order.modify_user_id = CurrentUserInfo.CurrentUser.User_Id;

            inoutService.Update(order, out error);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region  导出订单
        #region 导出Excel文件
        /// <summary>
        /// 导出Excel数据功能 
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private void Export(HttpContext pContext)
        {
            try
            {
            #region 获取信息
            string order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            string order_reason_type_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            string red_flag = "1";
            var inoutService = new InoutService(CurrentUserInfo);
            InoutInfo data;
            string content = string.Empty;
            var form = Request("param").DeserializeJSONTo<dynamic>();
            //var param = Request("param").DeserializeJSONTo<dynamic>();

            string purchase_unit_id = string.Empty;
            if (FormatParamValue(Request("purchase_unit_id")).Equals("") && FormatParamValue(Request("unit_id")) == null)
            {
                purchase_unit_id = FormatParamValue(form.purchase_unit_id);
            }
            else
            {
                purchase_unit_id = FormatParamValue(Request("purchase_unit_id"));
            }
            string sales_unit_id = string.Empty;
            if (FormatParamValue(Request("sales_unit_id")).Equals("") && FormatParamValue(Request("sales_unit_id")) == null)
            {
                sales_unit_id = FormatParamValue(form.sales_unit_id);
            }
            else
            {
                sales_unit_id = FormatParamValue(Request("sales_unit_id"));
            }

            if (order_reason_type_id.Equals("") || order_reason_type_id == null)
            {
                order_reason_type_id = FormatParamValue(form.order_reason_id);
            }

            string order_no = form.order_no;
            //string sales_unit_id = form.sales_unit_id; //"0d2bf77f765849249a0270c0a07fef07";//
            string warehouse_id = null;//form.warehouse_id;//Request("warehouse_id");//
            //string purchase_unit_id = form.purchase_unit_id;
            string status = null;//form.status;
            string order_date_begin = form.order_date_begin;
            string order_date_end = form.order_date_end;
            string complete_date_begin = null;//FormatParamValue(form.complete_date_begin);
            string complete_date_end = null;//FormatParamValue(form.complete_date_end);
            string data_from_id = null;//FormatParamValue(form.data_from_id);
            string ref_order_no = null;//FormatParamValue(form.ref_order_no);

            string Field7 = FormatParamValue(Request("Field7"));
            string DefrayTypeId = null;//FormatParamValue(form.DefrayTypeId);
            string DeliveryId = null;//FormatParamValue(form.DeliveryId);

            string Field9_begin = form.Field9_begin;
            string Field9_end = form.Field9_end;
            string ModifyTime_begin = form.ModifyTime_begin;
            string ModifyTime_end = form.ModifyTime_end;

            string purchase_warehouse_id = null;//FormatParamValue(form.purchase_warehouse_id);
            string sales_warehouse_id = null;//FormatParamValue(form.sales_warehouse_id);

            int maxRowCount = 10000;
            int startRowIndex = 0;

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            data = inoutService.SearchInoutInfo(
                order_no,
                order_reason_type_id,
                sales_unit_id,
                warehouse_id,
                purchase_unit_id,
                status,
                order_date_begin,
                order_date_end,
                complete_date_begin,
                complete_date_end,
                data_from_id,
                ref_order_no,
                order_type_id,
                red_flag,
                maxRowCount,
                startRowIndex
                , purchase_warehouse_id
                , sales_warehouse_id
                , Field7, DeliveryId, DefrayTypeId, Field9_begin, Field9_end, ModifyTime_begin, ModifyTime_end, "", "",
                CurrentUserInfo.CurrentUserRole.UnitId, null,false);
            #endregion
            string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");

            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            Cells cells = sheet.Cells;//单元格
            #region
            //为标题设置样式    
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            styleTitle.Font.Name = "宋体";//文字字体
            styleTitle.Font.Size = 18;//文字大小
            styleTitle.Font.IsBold = true;//粗体

            //样式2
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style2.Font.Name = "宋体";//文字字体
            style2.Font.Size = 14;//文字大小
            style2.Font.IsBold = true;//粗体
            style2.IsTextWrapped = true;//单元格内容自动换行
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style3.Font.Name = "宋体";//文字字体
            style3.Font.Size = 12;//文字大小
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            #endregion
            //生成行1 标题行  
            cells.Merge(0, 0, 1, 9);//合并单元格
            if (Field7.Equals("1"))
            {
                cells[0, 0].PutValue("未付款订单集合");//填写内容
            }
            else {
                cells[0, 0].PutValue("已付款订单集合");//填写内容
            }
            cells[0, 0].SetStyle(styleTitle);
            cells.SetRowHeight(0, 38);

            //生成行2 列名行
            for (int i = 0; i < 9; i++)
            {
                //cells[1, i].PutValue(dt.Columns[i].ColumnName);
                //cells[1, i].SetStyle(style2);
                //cells.SetRowHeight(1, 25);
                cells.SetColumnWidth(i, 30);
            }
            #region 列明
            cells[1, 0].PutValue("单据号码");
            cells[1, 0].SetStyle(style2);
            cells.SetRowHeight(1, 25);
            
            cells[1, 1].PutValue("单据日期");
            cells[1, 1].SetStyle(style2);
            cells.SetRowHeight(1, 25);
            cells[1, 2].PutValue("消费金额");
            cells[1, 2].SetStyle(style2);
            cells.SetRowHeight(1, 25);
            cells[1, 3].PutValue("消费门店");
            cells[1, 3].SetStyle(style2);
            cells.SetRowHeight(1, 25);
            cells[1, 4].PutValue("会员");
            cells[1, 4].SetStyle(style2);
            cells[1, 5].PutValue("交易时间");
            cells[1, 5].SetStyle(style2);
            cells[1, 6].PutValue("支付方式");
            cells[1, 6].SetStyle(style2);
            cells[1, 7].PutValue("配送方式");
            cells[1, 7].SetStyle(style2);
            cells[1, 8].PutValue("来源");
            cells[1, 8].SetStyle(style2);
            cells[1, 9].PutValue("操作人");
            cells[1, 9].SetStyle(style2);
            cells.SetRowHeight(1, 25);
            #endregion

            #region 生成数据行
            for (int i = 0; i < data.InoutInfoList.Count; i++)
            {
                cells[2 + i, 0].PutValue(data.InoutInfoList[i].order_no);
                cells[2 + i, 0].SetStyle(style3);

                cells[2 + i, 1].PutValue(data.InoutInfoList[i].order_date);
                cells[2 + i, 1].SetStyle(style3);

                cells[2 + i, 2].PutValue(data.InoutInfoList[i].total_amount);
                cells[2 + i, 2].SetStyle(style3);

                cells[2 + i, 3].PutValue(data.InoutInfoList[i].sales_unit_name);
                cells[2 + i, 3].SetStyle(style3);
   
                cells[2 + i, 4].PutValue(data.InoutInfoList[i].vip_name);
                cells[2 + i, 4].SetStyle(style3);

                cells[2 + i, 5].PutValue(data.InoutInfoList[i].create_time);
                cells[2 + i, 5].SetStyle(style3);

                cells[2 + i, 6].PutValue(data.InoutInfoList[i].DefrayTypeName);
                cells[2 + i, 6].SetStyle(style3);

                cells[2 + i, 7].PutValue(data.InoutInfoList[i].DeliveryName);
                cells[2 + i, 7].SetStyle(style3);

                cells[2 + i, 8].PutValue(data.InoutInfoList[i].data_from_name);
                cells[2 + i, 8].SetStyle(style3);

                cells[2 + i, 9].PutValue(data.InoutInfoList[i].create_user_name);
                cells[2 + i, 9].SetStyle(style3);

                cells.SetRowHeight(2 + i, 24);
                //for (int k = 0; k < Colnum; k++)
                //{
                //    cells[2 + i, k].PutValue(dt.Rows[i][k].ToString());
                //    cells[2 + i, k].SetStyle(style3);
                //}
                //cells.SetRowHeight(2 + i, 24);
            }
            #endregion
            workbook.Save(MapUrl);
           
                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件

                //this.CurrentContext.Response.Clear();

                //this.CurrentContext.Response.ContentType = "application/vnd.ms-excel";

                //this.CurrentContext.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.xls", DateTime.Now.ToSerialString()));

                ////var excel = workbook.Worksheets[0].a .WriteXLS();

                //using (var stream = workbook.SaveToStream())
                //{

                //    var bytes = stream.ToArray();

                //    this.CurrentContext.Response.OutputStream.Write(bytes, 0, bytes.Length);

                //}

                //this.CurrentContext.Response.End();

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
        #endregion
    }

    #region QueryEntity
    public class InoutQueryEntity
    {
        public string order_id; 
        public string order_no; 
        public string order_type_id; 
        public string order_reason_id; 
        public string red_flag; 
        public string ref_order_id; 
        public string ref_order_no; 
        public string warehouse_id; 
        public string order_date; 
        public string request_date; 
        public string complete_date; 
        public string create_unit_id; 
        public string unit_id; 
        public string related_unit_id; 
        public string ref_unit_id; 
        public string pos_id; 
        public string shift_id; 
        public string sales_user; 
        public string total_amount; 
        public string discount_rate; 
        public string actual_amount; 
        public string receive_points; 
        public string pay_points; 
        public string pay_id; 
        public string print_times; 
        public string carrier_id; 
        public string remark; 
        public string status; 
        public string status_desc; 
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
        public string keep_the_change; 
        public string wiping_zero; 
        public string vip_no; 
        public string data_from_id; 
        public string sales_unit_id; 
        public string purchase_unit_id; 
        public string if_flag;
        public string sales_unit_name;
        public string purchase_unit_name;
        public string warehouse_name;
        public string order_date_begin;
        public string order_date_end;
        public string complete_date_begin;
        public string complete_date_end;
        public string purchase_warehouse_id;
        public string sales_warehouse_id;
        public string DefrayTypeId;
        public string DeliveryId;
        public string Field9_begin;
        public string Field9_end;
        public string ModifyTime_begin;
        public string ModifyTime_end;
    }
    #endregion

    public enum SaveType
    {
        Default = 0,
        OpenInExcel = 1,
        OpenInBrowser = 2,
    }
}