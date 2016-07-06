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
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class T_InoutBLL
    {
        /// <summary>
        /// 获取指定订单佣金信息
        /// </summary>
        /// <param name="pOrderId">订单id</param>
        /// <returns></returns>
        public DataTable GetCommissionList(string pOrderId)
        {
            return _currentDAO.GetCommissionList(pOrderId);
        }

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
            PanicbuyingEventBLL panicbuyingEventBLL = new PanicbuyingEventBLL(loggingSessionInfo); //活动订单业务实例化
            T_SuperRetailTraderItemMappingBLL superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(loggingSessionInfo); //分销商业务实例化

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

            //普通订单库存处理
            if (inoutInfo.order_reason_id == "2F6891A2194A4BBAB6F17B4C99A6C6F5")
            {
                inoutBll.SetStock(orderId, inoutDetailList, 2, loggingSessionInfo);
            }
            //团购抢购订单库存处理
            if (inoutInfo.order_reason_id == "CB43DD7DD1C94853BE98C4396738E00C" || inoutInfo.order_reason_id == "671E724C85B847BDA1E96E0E5A62055A")
            {
                panicbuyingEventBLL.SetEventStock(orderId, inoutDetailList.ToList());
            }
            if (inoutInfo.order_reason_id == "096419BFDF394F7FABFE0DFCA909537F")
            {
                panicbuyingEventBLL.SetKJEventStock(orderId, inoutDetailList.ToList());
            }
            if (inoutInfo.data_from_id == "35" || inoutInfo.data_from_id == "36")
            {
                superRetailTraderItemMappingBll.DeleteSuperRetailTraderItemStock(inoutDetailList.ToList());
            }
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
                    var VipBLL=new VipBLL(loggingSessionInfo);
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
                    //将Col48至为1
                    var VipData = VipBLL.GetByID(inoutInfo.vip_no);
                    if (VipData != null)
                    {
                        VipData.Col48 = "1";
                        VipBLL.Update(VipData);
                    }
                    // 判断客户是否是符合潜在经销商条件
                    var isCan = VipBLL.IsSetVipDealer(inoutInfo.vip_no);

                    if (isCan)
                    {
                        new RetailTraderBLL(loggingSessionInfo).CreatePrepRetailTrader(loggingSessionInfo, inoutInfo.vip_no); // 创建潜在经销商
                    }
                }
            }
        }
   
        /// <summary>
        /// 计算超级分销商佣金分润
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderInfo"></param>
        public void CalculateSuperRetailTraderOrder(LoggingSessionInfo loggingSessionInfo,T_InoutEntity orderInfo)
        {
            
            if (orderInfo != null)
            {
                if (orderInfo.data_from_id == "35" || orderInfo.data_from_id == "36")
                {
                   


                        T_SuperRetailTraderBLL bllSuperRetailTrader = new T_SuperRetailTraderBLL(loggingSessionInfo);
                        DataSet dsAllFather = bllSuperRetailTrader.GetAllFather(orderInfo.sales_user);
                        if (dsAllFather != null && dsAllFather.Tables.Count > 0 && dsAllFather.Tables[0].Rows.Count > 0)
                        {

                            T_SuperRetailTraderProfitConfigBLL bllSuperRetailTraderProfitConfig = new T_SuperRetailTraderProfitConfigBLL(loggingSessionInfo);
                            T_SuperRetailTraderConfigBLL bllSuperRetailTraderConfig = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);
                            T_SuperRetailTraderProfitDetailBLL bllSuperRetailTraderProfitDetail = new T_SuperRetailTraderProfitDetailBLL(loggingSessionInfo);

                            VipAmountBLL bllVipAmount = new VipAmountBLL(loggingSessionInfo);
                            VipAmountDetailBLL bllVipAmountDetail = new VipAmountDetailBLL(loggingSessionInfo);

                            var entitySuperRetailTraderProfitConfig = bllSuperRetailTraderProfitConfig.QueryByEntity(new T_SuperRetailTraderProfitConfigEntity() { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0, Status = "10" }, null);
                            var entityConfig = bllSuperRetailTraderConfig.QueryByEntity(new T_SuperRetailTraderConfigEntity() { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0 }, null).SingleOrDefault();
                            if (entityConfig != null && entitySuperRetailTraderProfitConfig != null)
                            {

                                //佣金比列
                                decimal SkuCommission = Convert.ToDecimal(entityConfig.SkuCommission) * Convert.ToDecimal(0.01);
                                //商品分润比列
                                decimal DistributionProfit = Convert.ToDecimal(entityConfig.DistributionProfit) * Convert.ToDecimal(0.01);


                                foreach (DataRow dr in dsAllFather.Tables[0].Rows)
                                {
                                    decimal amount = 0;
                                    string strAmountSourceId = string.Empty;
                                    T_SuperRetailTraderProfitConfigEntity singlProfitConfig = new T_SuperRetailTraderProfitConfigEntity();
                                    if (dr["level"].ToString() == "1")//佣金
                                    {
                                        strAmountSourceId = "34";
                                        singlProfitConfig = entitySuperRetailTraderProfitConfig.Where(a => a.Level == Convert.ToInt16(dr["level"].ToString())).SingleOrDefault();
                                        if (singlProfitConfig != null)
                                        {
                                            if (singlProfitConfig.ProfitType == "Percent")
                                            {
                                                amount = Convert.ToDecimal(orderInfo.actual_amount) * SkuCommission;
                                            }
                                        }
                                    }
                                    else//分润
                                    {
                                        strAmountSourceId = "33";
                                        singlProfitConfig = entitySuperRetailTraderProfitConfig.Where(a => a.Level == Convert.ToInt16(dr["level"].ToString())).SingleOrDefault();
                                        if (singlProfitConfig != null)
                                        {
                                            if (singlProfitConfig.ProfitType == "Percent")
                                            {
                                                amount = Convert.ToDecimal(orderInfo.actual_amount) * DistributionProfit * Convert.ToDecimal(singlProfitConfig.Profit) * Convert.ToDecimal(0.01);
                                            }
                                        }
                                    }
                                    if (amount > 0)
                                    {
                                        IDbTransaction tran = new JIT.CPOS.BS.DataAccess.Base.TransactionHelper(loggingSessionInfo).CreateTransaction();
                                        try
                                        {
                                            T_SuperRetailTraderProfitDetailEntity entitySuperRetailTraderProfitDetail = new T_SuperRetailTraderProfitDetailEntity()
                                            {
                                                SuperRetailTraderProfitConfigId = singlProfitConfig.SuperRetailTraderProfitConfigId,
                                                SuperRetailTraderID = new Guid(dr["SuperRetailTraderID"].ToString()),
                                                Level = Convert.ToInt16(dr["level"].ToString()),
                                                ProfitType = "Cash",
                                                Profit = amount,
                                                OrderType = "Order",
                                                OrderId = orderInfo.order_id,
                                                OrderDate = Convert.ToDateTime(orderInfo.order_date),
                                                VipId = orderInfo.vip_no,
                                                OrderActualAmount = orderInfo.actual_amount,
                                                SalesId = new Guid(orderInfo.sales_user),
                                                OrderNo = orderInfo.order_no,
                                                CustomerId = loggingSessionInfo.ClientID
                                            };


                                            bllSuperRetailTraderProfitDetail.Create(entitySuperRetailTraderProfitDetail, (SqlTransaction)tran);


                                            VipAmountDetailEntity entityVipAmountDetail = new VipAmountDetailEntity();
                                            VipAmountEntity entityVipAmount = new VipAmountEntity();
                                            entityVipAmountDetail = new VipAmountDetailEntity
                                            {
                                                VipAmountDetailId = Guid.NewGuid(),
                                                VipId = dr["SuperRetailTraderID"].ToString(),
                                                Amount = amount,
                                                UsedReturnAmount = 0,
                                                EffectiveDate = DateTime.Now,
                                                DeadlineDate = Convert.ToDateTime("9999-12-31 23:59:59"),
                                                AmountSourceId = strAmountSourceId,
                                                ObjectId = orderInfo.order_id,
                                                CustomerID = loggingSessionInfo.ClientID,
                                                Reason = "超级分销商"
                                            };
                                            bllVipAmountDetail.Create(entityVipAmountDetail, (SqlTransaction)tran);

                                            entityVipAmount = bllVipAmount.QueryByEntity(new VipAmountEntity() { VipId = dr["SuperRetailTraderID"].ToString(), IsDelete = 0, CustomerID = loggingSessionInfo.ClientID }, null).SingleOrDefault();
                                            if (entityVipAmount == null)
                                            {
                                                entityVipAmount = new VipAmountEntity
                                                {
                                                    VipId = dr["SuperRetailTraderID"].ToString(),
                                                    BeginAmount = 0,
                                                    InAmount = amount,
                                                    OutAmount = 0,
                                                    EndAmount = amount,
                                                    TotalAmount = amount,
                                                    BeginReturnAmount = 0,
                                                    InReturnAmount = 0,
                                                    OutReturnAmount = 0,
                                                    ReturnAmount = 0,
                                                    ImminentInvalidRAmount = 0,
                                                    InvalidReturnAmount = 0,
                                                    ValidReturnAmount = 0,
                                                    TotalReturnAmount = 0,
                                                    IsLocking = 0,
                                                    CustomerID = loggingSessionInfo.ClientID,
                                                    VipCardCode = ""

                                                };
                                                bllVipAmount.Create(entityVipAmount, tran);
                                            }
                                            else
                                            {

                                                entityVipAmount.InReturnAmount = (entityVipAmount.InReturnAmount == null ? 0 : entityVipAmount.InReturnAmount.Value) + amount;
                                                entityVipAmount.TotalReturnAmount = (entityVipAmount.TotalReturnAmount == null ? 0 : entityVipAmount.TotalReturnAmount.Value) + amount;

                                                entityVipAmount.ValidReturnAmount = (entityVipAmount.ValidReturnAmount == null ? 0 : entityVipAmount.ValidReturnAmount.Value) + amount;
                                                entityVipAmount.ReturnAmount = (entityVipAmount.ReturnAmount == null ? 0 : entityVipAmount.ReturnAmount.Value) + amount;

                                                bllVipAmount.Update(entityVipAmount);
                                            }
                                            tran.Commit();
                                        }
                                        catch (Exception)
                                        {
                                            tran.Rollback();
                                            throw;
                                        }
                                    }

                                }

                            }
                        }
                }
            }
        }
    }
}