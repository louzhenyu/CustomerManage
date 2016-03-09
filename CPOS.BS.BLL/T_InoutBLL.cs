/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:26
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Reflection;
using System.Web;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.DTO.Base;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.Order.Response;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.Utility.Log;
using JIT.CPOS.Common;
using JIT.CPOS.BS.DataAccess;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class T_InoutBLL
    {
        /// <summary>
        /// 取消订单(Api和后台通用)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userType">0=会员；1=后台用户</param>
        /// <param name="loggingSessionInfo"></param>
        public void SetCancelOrder(string orderId, int userType, LoggingSessionInfo loggingSessionInfo)
        {
            var vipBll = new VipBLL(loggingSessionInfo);                    //会员业务实例化
            var inoutDetailBLL = new Inout3Service(loggingSessionInfo);     //订单业务实例化
            var refundOrderBll = new T_RefundOrderBLL(loggingSessionInfo);  //退货业务实例化
            var inoutBll = new T_InoutBLL(loggingSessionInfo);              //订单业务实例化

            //获取订单详情
            var inoutInfo = inoutBll.GetInoutInfo(orderId, loggingSessionInfo);

            //处理积分、余额、返现和优惠券
            vipBll.ProcSetCancelOrder(loggingSessionInfo.ClientID, inoutInfo.order_id, inoutInfo.vip_no);
            //获取订单明细
            var inoutDetailList = inoutDetailBLL.GetInoutDetailInfoByOrderId(inoutInfo.order_id, loggingSessionInfo.ClientID);

            #region 处理退款业务
            if (inoutInfo != null)
            {
                //if (inoutInfo.Field1 == "1" && (inoutInfo.actual_amount - inoutInfo.DeliveryAmount) > 0)//已付款,并且实付款-运费>0
                if (inoutInfo.Field1 == "1" && inoutInfo.actual_amount > 0)//已付款,并且实付款>0,未发货所以不用减运费
                {

                    #region 新增退货单(默认状态为确认收货)
                    var salesReturnBLL = new T_SalesReturnBLL(loggingSessionInfo);
                    var historyBLL = new T_SalesReturnHistoryBLL(loggingSessionInfo);
                    T_SalesReturnEntity salesReturnEntity = null;
                    T_SalesReturnHistoryEntity historyEntity = null;

                    var userBll = new T_UserBLL(loggingSessionInfo);    //店员BLL实例化
                    VipEntity vipEntity = null;                         //会员信息

                    salesReturnEntity = new T_SalesReturnEntity();
                    salesReturnEntity.SalesReturnNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    salesReturnEntity.VipID = loggingSessionInfo.UserID;
                    salesReturnEntity.ServicesType = 1;//退货
                    salesReturnEntity.DeliveryType = 1;//快递送回;
                    salesReturnEntity.OrderID = inoutInfo.order_id;
                    var inoutDetailInfo = inoutDetailList.FirstOrDefault();
                    if (inoutDetailInfo != null)
                    {
                        salesReturnEntity.ItemID = inoutDetailInfo.item_id;
                        salesReturnEntity.SkuID = inoutDetailInfo.sku_id;
                    }
                    salesReturnEntity.Qty = 0;
                    salesReturnEntity.ActualQty = 0;
                    if (inoutInfo != null)
                    {
                        salesReturnEntity.UnitID = inoutInfo.sales_unit_id;
                        //salesReturnEntity.UnitName = para.UnitName;
                        //salesReturnEntity.UnitTel = para.UnitTel;
                        salesReturnEntity.Address = inoutInfo.Field4;
                        salesReturnEntity.Contacts = inoutInfo.Field14 != null ? inoutInfo.Field14 : "";
                        salesReturnEntity.Phone = inoutInfo.Field6 != null ? inoutInfo.Field6 : "";
                    }
                    salesReturnEntity.Reason = "取消订单";
                    //if (inoutInfo.actual_amount - inoutInfo.DeliveryAmount > 0)
                    if (inoutInfo.actual_amount > 0)
                        salesReturnEntity.Status = 6; //已完成（待退款）
                    else
                        salesReturnEntity.Status = 7; //已完成（已退款）
                    salesReturnEntity.CustomerID = loggingSessionInfo.ClientID;
                    salesReturnBLL.Create(salesReturnEntity);

                    string userName = string.Empty;//操作人姓名
                    if (userType == 0)//会员操作
                    {
                        vipEntity = vipBll.GetByID(loggingSessionInfo.UserID);
                        userName = vipEntity != null ? vipEntity.VipName : "";
                    }
                    else//后台用户操作
                        userName = loggingSessionInfo.CurrentUser.User_Name;
                    historyEntity = new T_SalesReturnHistoryEntity()
                    {
                        SalesReturnID = salesReturnEntity.SalesReturnID,
                        OperationType = 14,
                        OperationDesc = "取消订单",
                        OperatorID = loggingSessionInfo.UserID,
                        HisRemark = "取消订单",
                        OperatorName = userName,
                        OperatorType = userType   //0=会员;1=管理用户
                    };
                    historyBLL.Create(historyEntity);
                    #endregion

                    #region 新增退款单
                    //if (inoutInfo.actual_amount - inoutInfo.DeliveryAmount > 0)
                    if (inoutInfo.actual_amount > 0)
                    {
                        T_RefundOrderEntity refundOrderEntity = new T_RefundOrderEntity()
                        {
                            RefundNo = DateTime.Now.ToString("yyyyMMddhhmmfff"),
                            VipID = inoutInfo.vip_no,
                            SalesReturnID = salesReturnEntity.SalesReturnID,
                            //ServicesType = 1,//退货
                            DeliveryType = 1,//快递送回
                            ItemID = inoutDetailInfo.item_id,
                            SkuID = inoutDetailInfo.sku_id,
                            Qty = 0,
                            ActualQty = 0,
                            UnitID = inoutInfo.sales_unit_id,
                            //salesReturnEntity.UnitName = para.UnitName;
                            //salesReturnEntity.UnitTel = para.UnitTel;
                            Address = inoutInfo.Field4,
                            Contacts = inoutInfo.Field14,
                            Phone = inoutInfo.Field6,
                            OrderID = inoutInfo.order_id,
                            PayOrderID = inoutInfo.paymentcenter_id,
                            RefundAmount = inoutInfo.total_amount,
                            ConfirmAmount = inoutInfo.total_amount,
                            //ActualRefundAmount = inoutInfo.actual_amount - inoutInfo.DeliveryAmount,//实退金额=实付款-运费
                            ActualRefundAmount = inoutInfo.actual_amount,//实退金额=实付款
                            Points = inoutInfo.pay_points == null ? 0 : Convert.ToInt32(inoutInfo.pay_points),
                            ReturnAmount = inoutInfo.ReturnAmount,
                            Amount = inoutInfo.VipEndAmount,
                            Status = 1,//待退款
                            CustomerID = loggingSessionInfo.ClientID
                        };
                        refundOrderBll.Create(refundOrderEntity);
                    }
                    #endregion

                }
            }
            #endregion

            //处理销量库存业务
            inoutBll.SetStock(orderId, inoutDetailList, 2, loggingSessionInfo);
        }
        /// <summary>
        /// 处理商品销量库存
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="inoutDetailList">订单明细</param>
        /// <param name="actionType">操作类型 1=提交订单；2=取消订单</param>
        /// <param name="loggingSessionInfo"></param>
        public void SetStock(string orderId, IList<InoutDetailInfo> inoutDetailList, int actionType, LoggingSessionInfo loggingSessionInfo)
        {
            var itemPropertyBLL = new T_Item_PropertyBLL(loggingSessionInfo);
            var skuPriceBLL = new T_Sku_PriceBLL(loggingSessionInfo);

            var inoutService = new InoutService(loggingSessionInfo);
            foreach (var item in inoutDetailList)
            {
                //商品总库存
                var stockInfo = itemPropertyBLL.QueryByEntity(new T_Item_PropertyEntity() { item_id = item.item_id, prop_id = "34FF4445D39F49AD8174954D18BC1346" }, null).FirstOrDefault();
                if (stockInfo != null)
                {
                    decimal stock = decimal.Parse(stockInfo.prop_value);
                    if (actionType == 1)
                        stock -= item.enter_qty;
                    else if (actionType == 2)
                        stock += item.enter_qty;
                    stockInfo.prop_value = stock.ToString();
                    itemPropertyBLL.Update(stockInfo);
                }
                //商品总销量
                var salesCountInfo = itemPropertyBLL.QueryByEntity(new T_Item_PropertyEntity() { item_id = item.item_id, prop_id = "34FF4445D39F49AD8174954D18BC1347" }, null).FirstOrDefault();
                if (salesCountInfo != null)
                {
                    decimal salesCount = decimal.Parse(salesCountInfo.prop_value);
                    if (actionType == 1)
                        salesCount += item.enter_qty;
                    else if (actionType == 2)
                        salesCount -= item.enter_qty;

                    salesCountInfo.prop_value = salesCount.ToString();
                    itemPropertyBLL.Update(salesCountInfo);
                }
                //sku库存
                var skuStockInfo = skuPriceBLL.QueryByEntity(new T_Sku_PriceEntity() { sku_id = item.sku_id, item_price_type_id = "77850286E3F24CD2AC84F80BC625859E", status = "1" }, null).FirstOrDefault();
                if (skuStockInfo != null)
                {
                    if (actionType == 1)
                        skuStockInfo.sku_price -= item.enter_qty;
                    else if (actionType == 2)
                        skuStockInfo.sku_price += item.enter_qty;

                    skuPriceBLL.Update(skuStockInfo);
                }
                //sku销量
                var skuSalesCountInfo = skuPriceBLL.QueryByEntity(new T_Sku_PriceEntity() { sku_id = item.sku_id, item_price_type_id = "77850286E3F24CD2AC84F80BC625859f", status = "1" }, null).FirstOrDefault();
                if (skuSalesCountInfo != null)
                {
                    if (actionType == 1)
                        skuSalesCountInfo.sku_price += item.enter_qty;
                    else
                        skuSalesCountInfo.sku_price -= item.enter_qty;

                    skuPriceBLL.Update(skuSalesCountInfo);
                }
            }
        }
        /// <summary>
        /// 支付回调/收款处理虚拟商品订单
        /// </summary>
        public void SetVirtualItem(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            var inoutBLL = new T_InoutBLL(loggingSessionInfo);
            var inoutInfo = this._currentDAO.GetByID(orderId);
            if (inoutInfo != null)
            {
                //如果是经销商订单，付款完成后，订单状态修改成完成状态
                if (inoutInfo.data_from_id == "21")
                {
                    inoutInfo.Field7 = "700";
                    inoutInfo.status = "700";
                    inoutBLL.Update(inoutInfo);
                    InoutService inoutService = new InoutService(loggingSessionInfo);
                    T_VirtualItemTypeSettingBLL virtualItemTypeSettingBLL = new T_VirtualItemTypeSettingBLL(loggingSessionInfo);
                    VipCardVipMappingBLL vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
                    T_Inout_DetailBLL inoutDetailBLL = new T_Inout_DetailBLL(loggingSessionInfo);

                    var inoutDetail = inoutService.GetInoutDetailInfoByOrderId(orderId).FirstOrDefault();
                    string itemId = inoutDetail.item_id;

                    var virtualItemTypeSettingInfo = virtualItemTypeSettingBLL.QueryByEntity(new T_VirtualItemTypeSettingEntity() { ItemId = itemId }, null).FirstOrDefault();
                    if (virtualItemTypeSettingInfo != null)
                    {
                        int objectTypeId = int.Parse(virtualItemTypeSettingInfo.ObjecetTypeId);
                        string objectNo = vipCardVipMappingBLL.BindVirtualItem(inoutInfo.vip_no, inoutInfo.VipCardCode, "", objectTypeId);
                        //将卡/券的编号保存到订单明细
                        T_Inout_DetailEntity inoutDetailEntity = inoutDetailBLL.GetByID(inoutDetail.order_detail_id);
                        if (inoutDetailEntity != null)
                        {
                            inoutDetailEntity.Field10 = objectNo;
                            inoutDetailBLL.Update(inoutDetailEntity);
                        }
                    }
                }
            }
        }
    }
}