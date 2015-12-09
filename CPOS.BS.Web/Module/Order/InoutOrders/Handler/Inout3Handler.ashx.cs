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
using JIT.CPOS.BS.Web.Session;
using System.Data;
using System.Threading;
using System.Configuration;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.BS.Web.Module.Order.InoutOrders.Handler
{
    /// <summary>
    /// InoutHandler 的摘要说明
    /// </summary>
    public class Inout3Handler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            string method = pContext.Request.QueryString["method"];
            switch (pContext.Request.QueryString["method"])
            {
                case "PrintDelivery":
                    content = GetDeliveryData();
                    break;
                case "ExportDelivery":
                    ExportDelivery2(pContext);
                    break;
                case "PosOrder": //GetPosOrderData()  订单查询
                    content = GetPosOrderData();
                    break;

                case "PosOrder_lj": //GetPosOrder3Data()  订单查询
                    content = GetPosOrder3Data2();
                    break;
                case "GetPosOrderTotalCount_lj": //统计数量
                    content = GetPosOrder3TotalCount();
                    break;

                case "Export":  //导出数据
                    Export(pContext);
                    break;

                case "Export_lj":  //导出数据
                    Export_lj(pContext);
                    break;

                case "GetPosOrderTotalCount": //统计数量
                    content = GetPosOrderTotalCount();
                    break;


                case "SetUnit":
                    content = SetUnit();
                    break;
                case "GetPosOrderUnAuditTotalCount": //统计未审核数量 add by donal 2014-10-10 15:40:00
                    content = GetPosOrderUnAuditTotalCount();
                    break;

                case "GetInoutInfoById":
                    content = GetInoutInfoById();
                    break;

                case "GetInoutInfoById_lj":
                    content = GetInoutInfoById_lj();
                    break;

                case "GetInoutDetailInfoById":
                    content = GetInoutDetailInfoById();
                    break;
                //以上有用

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

                case "SavePosDelivery":
                    content = SavePosDelivery();
                    break;
                case "UpdateOrderUnit":
                    content = UpdateOrderUnit();
                    break;
                case "GetOrderTable":
                    content = GetOrderTable();
                    break;

                case "GetVipSummerInfo"://获取VIP信息
                    content = GetVipSummerInfo();
                    break;
                case "SaveDeliveryInfo":
                    content = SaveDeliveryInfo();
                    break;

                case "SaveDefrayType":
                    content = SaveDefrayType();
                    break;

                //jifeng.cao begin
                case "UpdateStatus":
                    content = UpdateStatus(pContext.Request.Form, pContext);
                    break;
                case "GetInoutStatusList":
                    content = GetInoutStatusList(pContext.Request.Form);
                    break;
                case "GetOrderStatusList":
                    content = GetOrderStatusList();
                    break;
                //jifeng.cao end

            }
            pContext.Response.Write(content);
            pContext.Response.End();



        }

        #region GetPosOrderData
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <returns></returns>
        public string GetPosOrderData()
        {
            return GetInOutOrderData(
                "1F0A100C42484454BAEA211D4C14B80F",
                "2F6891A2194A4BBAB6F17B4C99A6C6F5", "1");
        }
        #endregion
        /// <summary>
        /// 获取打印配送单数据
        /// </summary>
        /// <returns></returns>
        public string GetDeliveryData()
        {
            string customerId = this.CurrentUserInfo.ClientID;
            string orderId = Request("orderId");
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("未传入OrderId.");
            }
            var wApplicationInterfaceBLL = new WApplicationInterfaceBLL(CurrentUserInfo);
            var inoutService = new Inout3Service(CurrentUserInfo);

            //ds: 表头信息 标题
            var ds = wApplicationInterfaceBLL.GetWebWApplicationDelivery(CurrentUserInfo.ClientID);
            //订单信息 包含订单明细
            var orderInfo = inoutService.GetInoutInfoByIdDelivery(orderId);
            var detail = inoutService.GetDeliveryDetail(orderId);
            decimal? deliveryAmount = inoutService.GetDeliveryAmountByOrderId(orderId);

            //余额支付、返现支付
            VipAmountDetailBLL VipAmountDetailBll = new VipAmountDetailBLL(CurrentUserInfo);
            //VipAmountDetailEntity AmountDetailEntity = VipAmountDetailBll.QueryByEntity(
            //        new VipAmountDetailEntity()
            //        {
            //            ObjectId = orderId
            //        }
            //        , null
            //    ).OrderByDescending(m => m.CreateTime).FirstOrDefault();
            List<VipAmountDetailEntity> AmountDetailList = VipAmountDetailBll.QueryByEntity(
                 new VipAmountDetailEntity()
                 {
                     ObjectId = orderId
                 }, null).ToList();


            //积分支付
            VipIntegralDetailBLL VipIntegralDetailBll = new VipIntegralDetailBLL(CurrentUserInfo);
            VipIntegralDetailEntity IntegralDetailEntity = VipIntegralDetailBll.QueryByEntity(
                    new VipIntegralDetailEntity()
                    {
                        ObjectId = orderId
                    }
                    , null
                ).OrderByDescending(m => m.CreateTime).FirstOrDefault();



            var r = new InoutDeliveryData();
            if (ds.Tables[0].Rows.Count > 0)
            {
                r.A1 = ds.Tables[0].Rows[0]["customer_name"].ToString() + "微信微商城配送单"
                        + "(" + ds.Tables[0].Rows[0]["WeiXinName"].ToString() + ")";
                r.A2 = "关注 【" + ds.Tables[0].Rows[0]["WeiXinName"].ToString() + "】微信"
                    + ds.Tables[0].Rows[0]["WeiXinTypeName"].ToString() + ":进入微商城 手机购物，送货上门。客服电话:"
                    + ds.Tables[0].Rows[0]["SettingValue"].ToString() + " 制单日期:" + DateTime.Now.ToString("yyyy-MM-dd");
            }
            for (int c = 0; c < 8; c++)
                detail.Tables[0].Rows.RemoveAt(detail.Tables[0].Rows.Count - 1);
            r.C4 = orderInfo.Field14;
            r.E4 = orderInfo.Field6;
            r.H4 = orderInfo.order_no;
            r.J4 = orderInfo.Field2;
            r.C5 = orderInfo.Field4;
            r.H5 = orderInfo.create_time;
            r.J5 = orderInfo.payment_name;
            r.Details = detail.Tables[0];
            r.E9 = deliveryAmount == null ? 0 : deliveryAmount;
            r.actualAmount = orderInfo.actual_amount; //实际支付金额
            r.couponAmount = orderInfo.couponAmount; //优惠券金额

            r.payPoints = IntegralDetailEntity == null ? 0 :
                (IntegralDetailEntity.Integral == null ? 0 : Math.Abs(IntegralDetailEntity.Integral.Value));//积分

            //r.payPointsAmount = IntegralDetailEntity == null ? 0 :
            //    (IntegralDetailEntity.SalesAmount == null ? 0 : IntegralDetailEntity.SalesAmount);//积分抵扣金额

            if (r.payPoints != null)
            {
                VipBLL vipBll = new VipBLL(this.CurrentUserInfo);
                decimal integralAmountPre = vipBll.GetIntegralAmountPre(this.CurrentUserInfo.ClientID);//获取积分金额比例
                r.payPointsAmount = Math.Abs(r.payPoints.Value) * (integralAmountPre > 0 ? integralAmountPre : 0.01M);
            }
            //r.vipEndAmount = orderInfo.Field3 == "1" ? 0 :
            //    (
            //        AmountDetailEntity == null ? 0 :
            //        (AmountDetailEntity.Amount == null ? 0 : AmountDetailEntity.Amount)
            //    ); //余额实付

            //余额支付金额
            var vipAmountEntity = AmountDetailList.Where(t => t.AmountSourceId == "1").FirstOrDefault();
            if (vipAmountEntity != null)
            {
                r.vipEndAmount = vipAmountEntity.Amount;
            }
            else
                r.vipEndAmount = 0;
            //返现支付金额
            var returnAmountEntity = AmountDetailList.Where(t => t.AmountSourceId == "13").FirstOrDefault();
            if (returnAmountEntity != null)
            {
                r.ReturnAmount = returnAmountEntity.Amount;
            }
            else
                r.ReturnAmount = 0;

            //r.ALB = orderInfo.Field3 == "1" ?
            //    (
            //        AmountDetailEntity == null ? 0 :
            //        (AmountDetailEntity.Amount == null ? 0 : AmountDetailEntity.Amount)
            //    )
            //    : 0;

            //r.ALBAmount = orderInfo.Field3 == "1" ?
            //    (
            //        AmountDetailEntity == null ? 0 :
            //        (AmountDetailEntity.Amount == null ? 0 : AmountDetailEntity.Amount / 100)
            //    )
            //    : 0;

            //r.AllDeduction = r.couponAmount + r.payPointsAmount + r.vipEndAmount + r.ALBAmount;

            //会员折扣处理
            if (orderInfo.discount_rate > 0)
            {
                //r.VipDiscountAmount = (orderInfo.actual_amount - deliveryAmount) * ((100 - orderInfo.discount_rate) / 100);
                var tempAmount = orderInfo.actual_amount - deliveryAmount; //应付-运费后的应付金额
                r.VipDiscountAmount = tempAmount / (orderInfo.discount_rate / 100) - tempAmount;// (应付-运费)/折扣率=去除折扣后实付Y；Y-包含折扣的实付=会员折扣

            }
            r.AllDeduction = r.couponAmount + r.payPointsAmount + r.vipEndAmount + r.ReturnAmount + r.VipDiscountAmount;


            if (detail.Tables[0].Rows.Count > 0)
            {
                decimal? sumQty = 0;
                decimal? sumAmount = 0;
                for (var i = 0; i < detail.Tables[0].Rows.Count; i++)
                {
                    sumQty += detail.Tables[0].Rows[i]["qty"] as decimal?;
                    sumAmount += detail.Tables[0].Rows[i]["EnterAmount"] as decimal?;
                }
                r.SumQty = sumQty;
                r.SumAmount = sumAmount;
            }
            return r.ToJSON();
        }
        /// <summary>
        /// 生成配送单
        /// </summary>
        /// <param name="pContext"></param>
        public void ExportDelivery2(HttpContext pContext)
        {
            Aspose.Cells.License lic = new Aspose.Cells.License();
            lic.SetLicense("Aspose.Total.lic");
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Workbook = new Workbook(pContext.Server.MapPath("~/File/ExcelTemplate/DeliveryTemplate.xlsx"));
            string customerId = this.CurrentUserInfo.ClientID;
            string orderId = Request("orderId");
            if (string.IsNullOrEmpty(orderId)) return;
            var wApplicationInterfaceBLL = new WApplicationInterfaceBLL(CurrentUserInfo);
            var inoutService = new Inout3Service(CurrentUserInfo);
            //ds: 表头信息 标题
            var ds = wApplicationInterfaceBLL.GetWebWApplicationDelivery(CurrentUserInfo.ClientID);
            //订单信息 包含订单明细
            var orderInfo = inoutService.GetInoutInfoByIdDelivery(orderId);
            var detail = inoutService.GetDeliveryDetail(orderId);
            var tbCount = detail.Tables[0].Rows.Count;
            decimal? deliveryAmount = inoutService.GetDeliveryAmountByOrderId(orderId);
            if (tbCount > 8 && tbCount < 16)
            {
                for (int i = 0; i < tbCount - 8; i++)
                    detail.Tables[0].Rows.RemoveAt(detail.Tables[0].Rows.Count - 1);
            }
            else if (tbCount >= 16)
            {
                for (int c = 0; c < 8; c++)
                    detail.Tables[0].Rows.RemoveAt(detail.Tables[0].Rows.Count - 1);
            }
            detail.Tables[0].TableName = "P";
            designer.SetDataSource(detail.Tables[0]);
            if (tbCount > 8)
            {
                designer.SetDataSource("A1", ds.Tables[0].Rows[0]["customer_name"].ToString() + "微信微商城配送单"
                    + "(" + ds.Tables[0].Rows[0]["WeiXinName"].ToString() + ")");
                designer.SetDataSource("A2", "关注 【" + ds.Tables[0].Rows[0]["WeiXinName"].ToString() + "】微信"
                    + ds.Tables[0].Rows[0]["WeiXinTypeName"].ToString() + ":进入微商城 手机购物，送货上门。客服电话:"
                    + ds.Tables[0].Rows[0]["SettingValue"].ToString() + " 制单日期:" + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            designer.SetDataSource("C4", orderInfo.Field14);
            designer.SetDataSource("E4", orderInfo.Field6);
            designer.SetDataSource("H4", orderInfo.order_no);
            designer.SetDataSource("J4", orderInfo.Field2);
            designer.SetDataSource("C5", orderInfo.Field4);
            designer.SetDataSource("H5", orderInfo.create_time);
            designer.SetDataSource("J5", orderInfo.payment_name);
            designer.SetDataSource("E9", deliveryAmount);
            designer.Process();
            string savePath = HttpContext.Current.Server.MapPath(@"~/File/Delivery");
            if (!System.IO.Directory.Exists(savePath))
            {
                System.IO.Directory.CreateDirectory(savePath);
            }
            savePath = savePath + "\\" + "配送单" + orderInfo.Field2 + DateTime.Now.ToFileTime() + ".xlsx";
            designer.Workbook.Save(savePath);
            new JIT.CPOS.BS.Web.Base.Excel.ExcelCommon().OutPutExcel(HttpContext.Current, savePath);
            designer = null;
            HttpContext.Current.Response.End();
        }

        #region GetPosOrder3Data jifeng.cao 20140319
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <returns></returns>
        public string GetPosOrder3Data()
        {
            return GetInOutOrder3Data(
                "1F0A100C42484454BAEA211D4C14B80F",
                "2F6891A2194A4BBAB6F17B4C99A6C6F5", "1");
        }

        public string GetPosOrder3Data2()
        {
            return GetInOutOrder3Data2(
                "1F0A100C42484454BAEA211D4C14B80F",
                "2F6891A2194A4BBAB6F17B4C99A6C6F5", "1");
        }
        #endregion

        #region GetInOutOrderData
        /// <summary>
        /// 获取出入库单据
        /// </summary>
        public string GetInOutOrderData(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
            InoutInfo data;
            string content = string.Empty;
            var form = Request("form").DeserializeJSONTo<InoutQueryEntity3>();

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

            string Field9_begin = FormatParamValue(form.Field9_begin);
            string Field9_end = FormatParamValue(form.Field9_end);
            string ModifyTime_begin = FormatParamValue(form.ModifyTime_begin);
            string ModifyTime_end = FormatParamValue(form.ModifyTime_end);

            string purchase_warehouse_id = FormatParamValue(form.purchase_warehouse_id);
            string sales_warehouse_id = FormatParamValue(form.sales_warehouse_id);
            string vip_no = FormatParamValue(form.vip_no);
            string InoutSort = FormatParamValue(form.InoutSort); //排序
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
                , Field7, DeliveryId, DefrayTypeId, Field9_begin, Field9_end, ModifyTime_begin, ModifyTime_end, "", vip_no,
                CurrentUserInfo.CurrentUserRole.UnitId, null, InoutSort);
            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.InoutInfoList.ToJSON(),
               data.ICount);
            return content;
        }
        #endregion


        #region GetInOutOrder3Data jifeng.cao 20140319
        /// <summary>
        /// 获取出入库单据
        /// </summary>
        public string GetInOutOrder3Data(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
            InoutInfo data;
            string content = string.Empty;
            var form = Request("form").DeserializeJSONTo<InoutQueryEntity3>();

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

            string PayStatus = FormatParamValue(form.Field1);

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
            string vip_no = FormatParamValue(form.vip_no);
            string InoutSort = FormatParamValue(form.InoutSort); //排序
            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            data = inoutService.SearchInoutInfo_lj(
                PayStatus,
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
                , Field7, DeliveryId, DefrayTypeId, Field9_begin, Field9_end, ModifyTime_begin, ModifyTime_end, "", vip_no,
                CurrentUserInfo.CurrentUserRole.UnitId, null, InoutSort);
            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.InoutInfoList.ToJSON(),
               data.ICount);
            return content;
        }

        //专为出查询订单获取数据
        /// <summary>
        /// 获取出入库单据
        /// </summary>
        public string GetInOutOrder3Data2(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
            InoutInfo data;
            string content = string.Empty;
            var form = Request("form").DeserializeJSONTo<InoutQueryEntity3>();

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

            string PayStatus = FormatParamValue(form.Field1);

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
            string vip_no = FormatParamValue(form.vip_no);
            string InoutSort = FormatParamValue(form.InoutSort); //排序
            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            data = inoutService.SearchInoutInfo_lj2(
                PayStatus,
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
                , Field7, DeliveryId, DefrayTypeId, Field9_begin, Field9_end, ModifyTime_begin, ModifyTime_end, "", vip_no,
                CurrentUserInfo.CurrentUserRole.UnitId, null, InoutSort);
            //只返回了订单列表和订单数量
            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.InoutInfoList.ToJSON(),
               data.ICount);
            return content;
        }


        #endregion

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

        #region GetPosOrderTotalCount
        /// <summary>
        /// GetPosOrderTotalCount
        /// </summary>
        public string GetPosOrderTotalCount()
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
            InoutInfo data;
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            var form = Request("form").DeserializeJSONTo<InoutQueryEntity3>();

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

            string vip_no = FormatParamValue(form.vip_no);
            string InoutSort = FormatParamValue(form.InoutSort); //排序

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
                , Field7, DeliveryId, DefrayTypeId, "", "", "", "", "", vip_no, CurrentUserInfo.CurrentUserRole.UnitId, null, InoutSort);


            var d = new GetPosOrderTotalCountEntity();
            d.num1 = data.StatusCount1;
            d.num2 = data.StatusCount2;
            d.num3 = data.StatusCount3;
            d.num4 = data.StatusCount4;
            d.num5 = data.StatusCount5;
            d.num6 = data.StatusCount0;
            d.num7 = data.StatusCount99;
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
            public int num5;
            public int num6;
            public int num7;
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
                var inoutService = new Inout3Service(CurrentUserInfo);
                InoutInfo data;
                string content = string.Empty;
                var form = Request("param").DeserializeJSONTo<InoutQueryEntity3>();
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

                string vip_no = FormatParamValue(form.vip_no);
                string InoutSort = FormatParamValue(form.InoutSort); //排序

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
                    , Field7, DeliveryId, DefrayTypeId, Field9_begin, Field9_end, ModifyTime_begin, ModifyTime_end, "", vip_no,
                    CurrentUserInfo.CurrentUserRole.UnitId, null, InoutSort);
                #endregion
                string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense("Aspose.Total.lic");
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
                if (Field7.Equals("100"))
                {
                    cells[0, 0].PutValue("订单集合");//填写内容
                }
                else
                {
                    cells[0, 0].PutValue("订单集合");//填写内容
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
                }
                #endregion
                workbook.Save(MapUrl);

                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #endregion

        #region  导出订单
        #region 导出Excel文件
        /// <summary>
        /// 导出Excel数据功能 
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private void Export_lj(HttpContext pContext)
        {
            try
            {
                #region 获取信息
                string order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                string order_reason_type_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                string red_flag = "1";
                var inoutService = new Inout3Service(CurrentUserInfo);
                InoutInfo data;
                string content = string.Empty;
                var form = Request("param").DeserializeJSONTo<InoutQueryEntity3>();
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
                string ModifyTime_begin = form.ModifyTime_begin;  //修改时间
                string ModifyTime_end = form.ModifyTime_end;
                string PayStatus = FormatParamValue(form.Field1);

                string purchase_warehouse_id = null;//FormatParamValue(form.purchase_warehouse_id);
                string sales_warehouse_id = null;//FormatParamValue(form.sales_warehouse_id);

                string vip_no = FormatParamValue(form.vip_no);
                string InoutSort = FormatParamValue(form.InoutSort); //排序

                int maxRowCount = 10000;
                int startRowIndex = 0;

                string key = string.Empty;
                if (Request("id") != null && Request("id") != string.Empty)
                {
                    key = Request("id").ToString().Trim();
                }

                data = inoutService.SearchInoutInfo_lj(
                    PayStatus,
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
                    , Field7, DeliveryId, DefrayTypeId, Field9_begin, Field9_end, ModifyTime_begin, ModifyTime_end, "", vip_no,
                    CurrentUserInfo.CurrentUserRole.UnitId, null, InoutSort, true);
                #endregion
                string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense("Aspose.Total.lic");
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
                cells.Merge(0, 0, 1, 15);//合并单元格
                if (Field7.Equals("100"))
                {
                    cells[0, 0].PutValue("订单集合");//填写内容
                }
                else
                {
                    cells[0, 0].PutValue("订单集合");//填写内容
                }
                cells[0, 0].SetStyle(styleTitle);
                cells.SetRowHeight(0, 38);

                //生成行2 列名行
                for (int i = 0; i < 15; i++)
                {
                    //cells[1, i].PutValue(dt.Columns[i].ColumnName);
                    //cells[1, i].SetStyle(style2);
                    //cells.SetRowHeight(1, 25);
                    cells.SetColumnWidth(i, 30);
                }
                cells.SetColumnWidth(2, 90);
                #region 列明
                cells[1, 0].PutValue("单据号码");
                cells[1, 0].SetStyle(style2);

                cells[1, 1].PutValue("单据日期");
                cells[1, 1].SetStyle(style2);
                cells[1, 2].PutValue("商品明细");
                cells[1, 2].SetStyle(style2);
                cells[1, 3].PutValue("消费金额");
                cells[1, 3].SetStyle(style2);

                cells[1, 4].PutValue("发票抬头");
                cells[1, 4].SetStyle(style2);


                cells[1, 5].PutValue("消费门店");
                cells[1, 5].SetStyle(style2);
                cells[1, 6].PutValue("会员");
                cells[1, 6].SetStyle(style2);
                cells[1, 7].PutValue("交易时间");
                cells[1, 7].SetStyle(style2);
                cells[1, 8].PutValue("支付方式");
                cells[1, 8].SetStyle(style2);
                cells[1, 9].PutValue("配送方式");
                cells[1, 9].SetStyle(style2);
                cells[1, 10].PutValue("发货时间");
                cells[1, 10].SetStyle(style2);
                cells[1, 11].PutValue("配送地址");
                cells[1, 11].SetStyle(style2);

                cells[1, 12].PutValue("收货人");
                cells[1, 12].SetStyle(style2);
                cells[1, 13].PutValue("电话");
                cells[1, 13].SetStyle(style2);
                cells[1, 14].PutValue("来源");
                cells[1, 14].SetStyle(style2);
                //cells[1, 13].PutValue("操作人");
                //cells[1, 13].SetStyle(style2);
                cells.SetRowHeight(1, 25);

                style3.IsTextWrapped = true;
                #endregion

                #region 生成数据行
                for (int i = 0; i < data.InoutInfoList.Count; i++)
                {

                    cells[2 + i, 0].PutValue(data.InoutInfoList[i].order_no);
                    cells[2 + i, 0].SetStyle(style3);

                    cells[2 + i, 1].PutValue(data.InoutInfoList[i].order_date);
                    cells[2 + i, 1].SetStyle(style3);

                    StringBuilder sbDetail = new StringBuilder();
                    foreach (var item in data.InoutInfoList[i].InoutDetailList)
                    {
                        sbDetail.AppendFormat("{0}*{1}；\n", item.item_name, Convert.ToInt32(item.order_qty));
                    }
                    cells[2 + i, 2].PutValue(sbDetail.ToString());
                    cells[2 + i, 2].SetStyle(style3);

                    cells[2 + i, 3].PutValue(data.InoutInfoList[i].total_amount);
                    cells[2 + i, 3].SetStyle(style3);

                    cells[2 + i, 4].PutValue(data.InoutInfoList[i].Field19);
                    cells[2 + i, 4].SetStyle(style3);

                    cells[2 + i, 5].PutValue(data.InoutInfoList[i].sales_unit_name);
                    cells[2 + i, 5].SetStyle(style3);

                    cells[2 + i, 6].PutValue(data.InoutInfoList[i].vip_name);
                    cells[2 + i, 6].SetStyle(style3);

                    cells[2 + i, 7].PutValue(data.InoutInfoList[i].create_time);
                    cells[2 + i, 7].SetStyle(style3);

                    cells[2 + i, 8].PutValue(data.InoutInfoList[i].DefrayTypeName);
                    cells[2 + i, 8].SetStyle(style3);

                    cells[2 + i, 9].PutValue(data.InoutInfoList[i].DeliveryName);
                    cells[2 + i, 9].SetStyle(style3);

                    cells[2 + i, 10].PutValue(data.InoutInfoList[i].Field9);
                    cells[2 + i, 10].SetStyle(style3);

                    cells[2 + i, 11].PutValue(data.InoutInfoList[i].address);
                    cells[2 + i, 11].SetStyle(style3);

                    cells[2 + i, 12].PutValue(data.InoutInfoList[i].linkMan);
                    cells[2 + i, 12].SetStyle(style3);

                    cells[2 + i, 13].PutValue(data.InoutInfoList[i].linkTel);
                    cells[2 + i, 13].SetStyle(style3);

                    cells[2 + i, 14].PutValue(data.InoutInfoList[i].data_from_name);
                    cells[2 + i, 14].SetStyle(style3);

                    //cells[2 + i, 13].PutValue(data.InoutInfoList[i].create_user_name);
                    //cells[2 + i, 13].SetStyle(style3);

                    cells.SetRowHeight(2 + i, 24);
                }
                #endregion
                workbook.Save(MapUrl);

                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件

            }
            catch (Exception ex)
            {
                throw (ex);
            }
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
            var inoutService = new Inout3Service(CurrentUserInfo);
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

        #region GetInoutInfoById_lj  jifeng.cao 20140320
        /// <summary>
        /// 获取出入库单据信息
        /// </summary>
        public string GetInoutInfoById_lj()
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
            InoutInfo data;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("order_id") != null && Request("order_id") != string.Empty)
            {
                key = Request("order_id").ToString().Trim();
            }

            data = inoutService.GetInoutInfoById_lj(key);

            var jsonData = new JsonData();
            jsonData.totalCount = data == null ? "0" : "1";
            jsonData.data = data;

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
            var inoutService = new Inout3Service(CurrentUserInfo);
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


        #region SaveInoutOrder
        /// <summary>
        /// 保存出入库单据
        /// </summary>
        public string SaveInoutOrder(string order_type_id, string order_reason_type_id, string red_flag)
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
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
            var inoutService = new Inout3Service(CurrentUserInfo);
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
            var inoutService = new Inout3Service(CurrentUserInfo);
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
            var inoutService = new Inout3Service(CurrentUserInfo);
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
            var inoutService = new Inout3Service(CurrentUserInfo);
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
            var inoutService = new Inout3Service(CurrentUserInfo);
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
                    inoutService.UpdateOrderDeliveryStatus(id, Field7, null);
                }
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion



        #region SavePosDelivery
        /// <summary>
        /// 保存出入库单据
        /// </summary>
        public string SavePosDelivery()
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
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
            inoutService.UpdateOrderDeliveryStatus(order.order_id, order.Field7, null);

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
            var inoutService = new Inout3Service(CurrentUserInfo);
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

        #region GetVipSummerInfo
        private string GetVipSummerInfo()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.Params["order_id"]))
            {
                return string.Empty;
            }
            var orderid = HttpContext.Current.Request.Params["order_id"];
            var service = new Inout3Service(CurrentUserInfo);
            var dict = service.GetVipSummerInfo(orderid);
            return dict.ToJSON();
        }
        #endregion

        #region 保存配送信息
        private string SaveDeliveryInfo()
        {
            var dict = new Dictionary<string, string>();
            var parms = HttpContext.Current.Request.Form;
            if (string.IsNullOrEmpty(parms["order_id"]))
            {
                return new ResponseData
                {
                    status = "0",
                    success = false
                }.ToJSON();
            }

            if (parms["ReceiveMan"] != null)
            {
                dict.Add("Field14", parms["ReceiveMan"]);
            }
            if (parms["Addr"] != null)
            {
                dict.Add("Field4", parms["Addr"]);
            }
            if (parms["Phone"] != null)
            {
                dict.Add("Field6", parms["Phone"]);
            }
            if (parms["PostCode"] != null)
            {
                dict.Add("Field5", parms["PostCode"]);
            }
            if (parms["Carrier_id"] != null)
            {
                dict.Add("Carrier_id", parms["Carrier_id"]);
            }
            if (parms["DeliveryCode"] != null)
            {
                dict.Add("Field2", parms["DeliveryCode"]);
            }
            if (parms["SendTime"] != null)
            {
                dict.Add("send_time", parms["SendTime"]);
            }
            if (parms["Field9"] != null)
            {
                dict.Add("Field9", parms["Field9"]);
            }
            if (parms["DeliveryType"] != null)
            {
                dict.Add("Field8", parms["DeliveryType"]);
            }
            new Inout3Service(CurrentUserInfo).SaveDeliveryInfo(dict, parms["order_id"]);
            return new ResponseData
            {
                status = "1",
                success = true
            }.ToJSON();
        }
        #endregion

        #region 保存付款方式
        private string SaveDefrayType()
        {
            var dict = new Dictionary<string, string>();
            var parms = HttpContext.Current.Request.Form;
            if (string.IsNullOrEmpty(parms["order_id"]))
            {
                return new ResponseData
                {
                    status = "0",
                    success = false
                }.ToJSON();
            }

            if (parms["DefrayType"] != null)
            {
                dict.Add("Field11", parms["DefrayType"]);
            }

            new Inout3Service(CurrentUserInfo).SaveDefrayType(parms["DefrayType"], parms["order_id"]);
            return new ResponseData
            {
                status = "1",
                success = true
            }.ToJSON();
        }
        #endregion


        #region jifeng.cao

        #region 保存状态
        /// <summary>
        /// 保存状态
        /// </summary>
        /// <param name="rParams"></param>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private string UpdateStatus(NameValueCollection rParams, HttpContext pContext)
        {
            string res = "{success:false,msg:'保存失败'}";
            string error = "";

            //图片路径
            string filePath = "/File/img/" + CurrentUserInfo.CurrentLoggingManager.Customer_Id + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            string orderId = rParams["order_id"];

            if (!string.IsNullOrEmpty(orderId))
            {
                var inoutService = new Inout3Service(CurrentUserInfo);
                var service = new InoutService(CurrentUserInfo);
                var inoutStatus = new TInoutStatusBLL(CurrentUserInfo);
                var inoutStatusnode = new TInOutStatusNodeBLL(CurrentUserInfo);
                var inoutBLL = new T_InoutBLL(CurrentUserInfo);

                var order = inoutService.GetInoutInfoById(orderId);

                TInoutStatusEntity info = new TInoutStatusEntity();
                info.InoutStatusID = Guid.Parse(Utils.NewGuid());
                info.OrderID = orderId;
                info.CustomerID = CurrentUserInfo.CurrentLoggingManager.Customer_Id;
                info.Remark = rParams["Remark"];

                if (order != null)
                {
                    #region 执行流程
                    //下级订单状态
                    string status = rParams["nextStatus"];

                    if (!string.IsNullOrEmpty(status))
                    {
                        #region 添加日志信息
                        if (!string.IsNullOrEmpty(rParams["PayMethod"]))
                        {
                            info.PayMethod = int.Parse(rParams["PayMethod"]);
                        }
                        if (!string.IsNullOrEmpty(rParams["CheckResult"]))
                        {
                            info.CheckResult = int.Parse(rParams["CheckResult"]);
                        }
                        if (!string.IsNullOrEmpty(rParams["PicUrl"]))
                        {
                            UploadFile(pContext.Request.Files, ref filePath);
                            info.PicUrl = filePath;
                        }
                        if (!string.IsNullOrEmpty(rParams["DeliverOrder"]))
                        {
                            order.Field2 = rParams["DeliverOrder"];
                            info.DeliverOrder = rParams["DeliverOrder"];
                        }
                        if (!string.IsNullOrEmpty(rParams["DeliverCompany"]))
                        {
                            order.carrier_id = rParams["DeliverCompany"];
                            info.DeliverCompanyID = rParams["DeliverCompany"];
                        }
                        if (status == "600" || !string.IsNullOrEmpty(rParams["DeliverOrder"]) || !string.IsNullOrEmpty(rParams["DeliverCompany"]))
                        {
                            order.Field9 = DateTime.Now.ToSQLFormatString();
                            //更新订单配送商及配送单号
                            inoutService.Update(order, out error);
                        }
                        #endregion

                        info.OrderStatus = int.Parse(status);

                        string statusDesc=GetStatusDesc(status);//变更后的状态名称

                        if (info.OrderStatus == 10000)
                        {
                            //付款
                            info.StatusRemark = "订单收款成功[操作人:" + CurrentUserInfo.CurrentUser.User_Name + "]";
                            inoutStatusnode.SetOrderPayment(order.order_id, out error);
                        }
                        else
                        {
                            info.StatusRemark = "订单状态从" + order.status_desc + "变为" + statusDesc + "[操作人:" + CurrentUserInfo.CurrentUser.User_Name + "]";
                            service.UpdateOrderDeliveryStatus(order.order_id, status, Utils.GetNow());
                        }

                        inoutStatus.Create(info);

                        #region 处理积分返现和退款

                        if (statusDesc=="已取消")//取消订单
                        {
                            //执行取消订单业务 reconstruction By Henry 2015-10-20
                            inoutBLL.SetCancelOrder(orderId,0,CurrentUserInfo);
                        }

                        
                        #endregion

                        #region 处理订单发货发送微信模板消息
                        if (statusDesc == "已发货")
                        {
                            //获取会员信息
                            var vipBll = new VipBLL(CurrentUserInfo);
                            var vipInfo = vipBll.GetByID(order.vip_no);
                            //物流公司
                            order.carrier_name = inoutService.GetCompanyName(order.carrier_id);
                            
                            var CommonBLL = new CommonBLL();
                            CommonBLL.SentShipMessage(order, vipInfo.WeiXinUserId, order.vip_no, CurrentUserInfo);
                        }
                        #endregion

                        res = "{success:true,msg:'保存成功'}";
                    }
                    #endregion
                }

            }
            return res.ToJSON();
        }
        
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="pClientID"></param>
        /// <param name="pFileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UploadFile(HttpFileCollection files, ref string filePath)
        {
            bool res = false;

            HttpPostedFile postedFile = files["PicUrl"];
            if (postedFile.ContentLength > 153600 * 1024)
            {
                res = false;
            }
            else
            {
                if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
                {
                    string suffixname = "";
                    if (postedFile.FileName != null)
                    {
                        suffixname = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(".")).ToLower();
                    }
                    if (suffixname != ".jpg" && suffixname != ".gif" && suffixname != ".png") //文件格式判断
                    {
                        res = false;
                    }
                    else
                    {
                        res = true;
                    }
                    filePath = filePath + suffixname;
                    string savepath = HttpContext.Current.Server.MapPath(filePath);

                    if (
                        !Directory.Exists(savepath.Remove(savepath.LastIndexOf(@"\"),
                            savepath.Length - savepath.LastIndexOf(@"\")))) //创建目录
                    {
                        Directory.CreateDirectory(savepath.Remove(savepath.LastIndexOf(@"\"),
                            savepath.Length - savepath.LastIndexOf(@"\")));
                    }
                    postedFile.SaveAs(savepath);
                    res = true;
                }
            }
            return res;
        }
        #endregion

        #region 查询订单日志
        public string GetInoutStatusList(NameValueCollection rParams)
        {

            TInoutStatusEntity entity = new TInoutStatusEntity();
            entity.OrderID = rParams["order_id"];

            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();

            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new TInoutStatusBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region 查询当前订单对应的下级状态列表
        public string GetOrderStatusList()
        {
            var parms = HttpContext.Current.Request.Form;

            var order = new Inout3Service(CurrentUserInfo).GetInoutInfoById(parms["order_id"]);
            if (order == null)
            {
                return new ResponseData
                {
                    success = false
                }.ToJSON();
            }
            string order_id = parms["order_id"].ToString();
            StringBuilder items = new StringBuilder();
            items.Append("[");

            IList<TInOutStatusNodeEntity> list = new TInOutStatusNodeBLL(CurrentUserInfo).GetOrderStatusList(parms["order_id"], null, order.Field8);

            if (list.Count > 0)
            {
                foreach (TInOutStatusNodeEntity item in list)
                {
                    if (!string.IsNullOrEmpty(item.ActionDesc))
                    {
                        items.Append("{xtype: 'jitbutton',text: '" + item.ActionDesc + "',jitIsHighlight: false,jitIsDefaultCSS: true,handler: function (){ fnbtn(" + item.NextValue + ");}},");
                    }

                }
            }

            items.Append("{xtype: 'jitbutton',text: '收款',id: 'btn_2',jitIsHighlight: false,jitIsDefaultCSS: true,handler: function (){ fnbtn(10000);}},");
            items.Append("{xtype: 'jitbutton',text: '日志',id: 'btn_6',jitIsHighlight: false,jitIsDefaultCSS: true,handler: fnShowLog},");
            items.Append("{xtype: 'jitbutton',text: '关闭',jitIsHighlight: false,jitIsDefaultCSS: true,handler: fnClose},");
            items.Append("{xtype: 'jitbutton',text: '打印拣货单',jitIsHighlight: false,jitIsDefaultCSS: true,handler: function (){ fnPrintPicking();}},");
            items.Append("{xtype: 'jitbutton',text: '打印配送单',jitIsHighlight: false,jitIsDefaultCSS: true,handler: function (){ fnPrintDelivery();}},");
            items.Append("{xtype: 'jitbutton',text: '下载配送单',jitIsHighlight: false,jitIsDefaultCSS: true,handler: function (){ fnDownLoadDelivery();}}");
            items.Append("]");

            return new ResponseData
            {
                data = items.ToString(),
                success = true
            }.ToJSON();
        }
        #endregion

        #region 获取订单对应状态描述
        /// <summary>
        /// 获取订单对应状态描述
        /// </summary>
        /// <param name="status">订单状态</param>
        /// <returns>状态描述</returns>
        private string GetStatusDesc(string status)
        {
            string str = "";
            OptionsBLL optionsBll = new OptionsBLL(CurrentUserInfo);
            var optionsList = optionsBll.QueryByEntity(new OptionsEntity
            {
                OptionValue = Convert.ToInt32(status)
                ,
                IsDelete = 0
                ,
                OptionName = "TInOutStatus"
                ,
                CustomerID = CurrentUserInfo.CurrentLoggingManager.Customer_Id
            }, null);
            if (optionsList != null && optionsList.Length > 0)
            {
                str = optionsList[0].OptionText;
            }
            return str;
        }
        #endregion

        #region GetPosOrder3TotalCount
        /// <summary>
        /// GetPosOrder3TotalCount
        /// </summary>
        public string GetPosOrder3TotalCount()
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
            InoutInfo data;
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            var form = Request("form").DeserializeJSONTo<InoutQueryEntity3>();

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
            string order_date_begin = FormatParamValue(form.order_date_begin);//订单成交日期
            string order_date_end = FormatParamValue(form.order_date_end);//订单成交日期
            string complete_date_begin = FormatParamValue(form.complete_date_begin);
            string complete_date_end = FormatParamValue(form.complete_date_end);
            string data_from_id = FormatParamValue(form.data_from_id);
            string ref_order_no = FormatParamValue(form.ref_order_no);

            string Field7 = FormatParamValue(Request("Field7"));
            string DefrayTypeId = FormatParamValue(form.DefrayTypeId);
            string DeliveryId = FormatParamValue(form.DeliveryId);

            string purchase_warehouse_id = FormatParamValue(form.purchase_warehouse_id);
            string sales_warehouse_id = FormatParamValue(form.sales_warehouse_id);

            string vip_no = FormatParamValue(form.vip_no);
            string InoutSort = FormatParamValue(form.InoutSort); //排序

            string PayStatus = FormatParamValue(form.Field1);

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            data = inoutService.SearchInoutInfo_lj3(
                PayStatus,
                order_no,
                order_reason_type_id,
                sales_unit_id,
                warehouse_id,
                purchase_unit_id,
                status,
                order_date_begin,//订单日期
                order_date_end,
                complete_date_begin,  //完成日期
                complete_date_end,
                data_from_id,
                ref_order_no,
                order_type_id,
                red_flag,
                maxRowCount,
                startRowIndex
                , purchase_warehouse_id
                , sales_warehouse_id
                , Field7, DeliveryId, DefrayTypeId, "", "", "", "", "", vip_no, CurrentUserInfo.CurrentUserRole.UnitId, null, InoutSort);


            var d = new GetPosOrder3TotalCountEntity();
            d.StatusTotalCount = 0;
            StringBuilder str = new StringBuilder();

            foreach (StatusManager item in data.StatusManagerList)
            {
                d.StatusTotalCount += item.StatusCount;

                str.AppendFormat("<div id='tab{0}' class='z_posorder_head' onclick=\"fnGridSearch('{0}')\">", item.StatusType);
                str.AppendFormat("<div style='width: 100px; height: 20px;'>{0}</div>", item.StatusTypeName);
                str.AppendFormat("<div style='height: 24px;'>{0}</div>", item.StatusCount);
                str.Append("</div>");
            }
            d.StatusManagerListHTML = str.ToString();

            responseData.data = d;

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }


        /// <summary>
        /// 设置门店
        /// </summary>
        /// <returns></returns>
        public string SetUnit()
        {
            var responseData = new ResponseData();

            string unitID = Request("unitID");
            string orderList = Request("orderList");

            if (string.IsNullOrWhiteSpace(unitID))
            {
                responseData.success = false;
                responseData.msg = "请选择门店";
                return responseData.ToJSON();
            }

            if (string.IsNullOrWhiteSpace(orderList))
            {
                responseData.success = false;
                responseData.msg = "请选择订单";
                return responseData.ToJSON();
            }

            var inoutService = new Inout3Service(CurrentUserInfo);

            int result = inoutService.SetOrderUnit(orderList, unitID);


            if (result > 0)
            {
                responseData.success = true;
                return responseData.ToJSON();
            }
            else
            {
                responseData.success = false;
                return responseData.ToJSON();
            }
        }

        /// <summary>
        /// 查询未审核订单数
        /// </summary>
        /// <returns></returns>
        public string GetPosOrderUnAuditTotalCount()
        {
            var inoutService = new Inout3Service(CurrentUserInfo);
            InoutInfo order = new InoutInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            var order_reason_type_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            var order_type_id = "1F0A100C42484454BAEA211D4C14B80F";


            int count = 0; //返回count

            int countNow = 0; //当前count
            string strCount = FormatParamValue(Request("Count"));
            int.TryParse(strCount, out countNow);

            DateTime beginDate = DateTime.Now;

            //int sleep = 3000;  //5分钟300000    3000

            try
            {
                //while (true)
                {
                    //DateTime endDate = DateTime.Now;

                    //TimeSpan ts = endDate.Subtract(beginDate).Duration();
                    //double tsSeconds = ts.TotalMilliseconds;

                    //if (tsSeconds > 9000) //30分钟1800000   9000
                    //{
                    //    count = countNow;
                    //    break;
                    //}

                    count = inoutService.GetPosOrderUnAuditTotalCount(order_reason_type_id, order_type_id, CurrentUserInfo.CurrentUserRole.UnitId);
                    //if (count != countNow)
                    //{
                    //    break;
                    //}

                    //Thread.Sleep(sleep); //线程睡眠
                }
            }
            catch (Exception)
            {
                responseData.success = false;
            }


            responseData.data = count;

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        private class GetPosOrder3TotalCountEntity
        {
            /// <summary>
            /// 全部订单数量
            /// </summary>
            public int StatusTotalCount;
            /// <summary>
            /// 生成各状态的订单数量页面html
            /// </summary>
            public string StatusManagerListHTML;
        }
        #endregion

        #endregion

    }
    /// <summary>
    /// 打印配送单所需数据
    /// </summary>
    public class InoutDeliveryData
    {
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string C4 { get; set; }
        public string E4 { get; set; }
        public string H4 { get; set; }
        public string J4 { get; set; }
        public string C5 { get; set; }
        public string H5 { get; set; }
        public string J5 { get; set; }
        public DataTable Details { get; set; }
        public decimal? E9 { get; set; }
        public decimal? SumQty { get; set; }
        public decimal? SumAmount { get; set; }
        //实际支付金额
        public decimal? actualAmount { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>
        public decimal? couponAmount { get; set; }
        /// <summary>
        /// 使用积分
        /// </summary>
        public decimal? payPoints { get; set; }
        /// <summary>
        /// 使用积分抵用金额
        /// </summary>
        public decimal? payPointsAmount { get; set; }
        /// <summary>
        /// 余额支付
        /// </summary>
        public decimal? vipEndAmount { get; set; }
        /// <summary>
        /// 阿拉币
        /// </summary>
        public decimal? ALB { get; set; }
        /// <summary>
        /// 阿拉币金额
        /// </summary>
        public decimal? ALBAmount { get; set; }
        /// <summary>
        /// 返现抵扣金额
        /// </summary>
        public decimal? ReturnAmount { get; set; }
        /// <summary>
        /// 会员折扣金额
        /// </summary>
        public decimal? VipDiscountAmount { get; set; }
        /// <summary>
        /// 总抵扣金额
        /// </summary>
        public decimal? AllDeduction { get; set; }


    }
    #region InoutQueryEntity3
    public class InoutQueryEntity3
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
        public string order_date_begin;   //开始时间
        public string order_date_end;//结束时间
        public string complete_date_begin;
        public string complete_date_end;
        public string purchase_warehouse_id;
        public string sales_warehouse_id;
        public string DefrayTypeId;
        public string DeliveryId;
        public string Field9_begin;
        public string Field9_end;
        public string ModifyTime_begin;//修改时间
        public string ModifyTime_end;
        public string InoutSort;
        public string Field1;
    }
    #endregion
}