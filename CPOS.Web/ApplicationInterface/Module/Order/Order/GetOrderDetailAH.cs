using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Data;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    public class GetOrderDetailAH : BaseActionHandler<GetOrderDetailRP, GetOrderDetailRD>
    {
        protected override GetOrderDetailRD ProcessRequest(DTO.Base.APIRequest<GetOrderDetailRP> pRequest)
        {
            GetOrderDetailRD rd = new GetOrderDetailRD();
            string orderId = pRequest.Parameters.OrderId;

            rd.OrderListInfo = new OrderListInfo();

            #region 获取订单列表

            T_InoutBLL orderBll = new T_InoutBLL(this.CurrentUserInfo);
            var orderList = orderBll.QueryByEntity(new T_InoutEntity()
            {
                order_id = orderId
            }, null);

            #endregion

            #region 获取会员信息

            string vipNo = orderList[0].vip_no;
            VipBLL vipBll = new VipBLL(this.CurrentUserInfo);

            var vipList = vipBll.QueryByEntity(new VipEntity()
            {
                VIPID = vipNo
            }, null);

            #endregion

            #region 获取配方式

            string deliveryId = orderList[0].Field8;
            DeliveryBLL deliverBll = new DeliveryBLL(this.CurrentUserInfo);

            var deliverList = deliverBll.QueryByEntity(new DeliveryEntity()
            {
                DeliveryId = deliveryId
            }, null);

            #endregion

            #region 获取门店信息

            string storeId = orderList[0].sales_unit_id;
            if (!string.IsNullOrEmpty(orderList[0].purchase_unit_id))//如果有发货门店，则显示发货门店信息
                storeId = orderList[0].purchase_unit_id;
            TInoutBLL tInoutBll = new TInoutBLL(this.CurrentUserInfo);
            //string storeName = tInoutBll.GetStoreName(storeId);
            DataSet storeDs = tInoutBll.GetStoreInfo(storeId);
            rd.OrderListInfo.StoreID = storeId;

            #endregion

            //配送商
            string carrierId = orderList[0].carrier_id;

            //DataSet carrierDs = tInoutBll.GetStoreInfo(carrierId);
            //if (carrierDs.Tables[0].Rows.Count > 0)
            //{
            //    rd.OrderListInfo.CarrierID = carrierId;
            //    rd.OrderListInfo.CarrierName = carrierDs.Tables[0].Rows[0]["unit_name"].ToString();
            //}
            if (!string.IsNullOrEmpty(carrierId))
            {
                //配送方式 1.送货到家;2.到店提货
                if (deliveryId == "1")
                {
                    var logisticsCompanyBLL = new T_LogisticsCompanyBLL(this.CurrentUserInfo);
                    Guid m_carrierId = Guid.Parse(carrierId);
                    var logCompInfo = logisticsCompanyBLL.GetByID(m_carrierId);
                    if (logCompInfo != null)
                    {
                        rd.OrderListInfo.CarrierID = carrierId;
                        rd.OrderListInfo.CarrierName = logCompInfo.LogisticsName;
                    }
                }
                else if (deliveryId == "2")
                {
                    var unitBLL = new t_unitBLL(this.CurrentUserInfo);
                    var unitInfo = unitBLL.GetByID(carrierId);
                    if (unitInfo != null)
                    {
                        rd.OrderListInfo.CarrierID = carrierId;
                        rd.OrderListInfo.CarrierName = unitInfo.unit_name;
                    }
                }
            }
            rd.OrderListInfo.CourierNumber = orderList[0].Field2;//配送单号
            rd.OrderListInfo.Invoice = orderList[0].Field19 == null ? "" : orderList[0].Field19;     //发票信息
            if (vipList.Count() > 0)
            {
                rd.OrderListInfo.VipID = vipList[0].VIPID;
                rd.OrderListInfo.Phone = vipList[0].Phone;
                rd.OrderListInfo.UserName = vipList[0].VipName;
                rd.OrderListInfo.VipRealName = vipList[0].VipRealName;
                rd.OrderListInfo.VipLevelDesc = vipList[0].VipLevelDesc;
                rd.OrderListInfo.VipCode = vipList[0].VipCode;
                rd.OrderListInfo.Email = vipList[0].Email;
                rd.OrderListInfo.VipLevel = Convert.ToInt32(vipList[0].VipLevel);
            }

            if (storeDs.Tables[0].Rows.Count > 0)
            {
                rd.OrderListInfo.StoreName = storeDs.Tables[0].Rows[0]["unit_name"].ToString();
                rd.OrderListInfo.StoreAddress = storeDs.Tables[0].Rows[0]["unit_address"].ToString();
                rd.OrderListInfo.StoreTel = storeDs.Tables[0].Rows[0]["unit_tel"].ToString();
            }

            if (orderList.Count() > 0)
            {
                rd.OrderListInfo.discount_rate = orderList[0].discount_rate ?? 100;//订单折扣
                rd.OrderListInfo.OrderID = orderList[0].order_id;
                rd.OrderListInfo.OrderCode = orderList[0].order_no;
                rd.OrderListInfo.OrderDate = orderList[0].order_date;
                rd.OrderListInfo.ReceiverName = orderList[0].Field14; //收件人             
                rd.OrderListInfo.TotalQty = Convert.ToDecimal(orderList[0].total_qty);

                string TotalAmount = String.Format("{0:F}",orderList[0].total_amount ?? 0);

                rd.OrderListInfo.TotalAmount = TotalAmount;

                rd.OrderListInfo.Total_Retail = Convert.ToDecimal(orderList[0].total_retail);
                rd.OrderListInfo.Remark = orderList[0].remark;
                rd.OrderListInfo.Status = orderList[0].status;
                rd.OrderListInfo.OrderStatus = int.Parse(orderList[0].Field7);
                rd.OrderListInfo.StatusDesc = orderList[0].status_desc;
                rd.OrderListInfo.DeliveryAddress = orderList[0].Field4;
                rd.OrderListInfo.DeliveryTime = orderList[0].Field9;
                rd.OrderListInfo.ClinchTime = orderList[0].create_time;
                rd.OrderListInfo.ReceiptTime = orderList[0].accpect_time;
                rd.OrderListInfo.CouponsPrompt = orderList[0].Field16;
                rd.OrderListInfo.DeliveryID = orderList[0].Field8;
                rd.OrderListInfo.IsPayment = orderList[0].Field1;
                rd.OrderListInfo.ReceivePoints = orderList[0].receive_points;
                rd.OrderListInfo.PaymentTime = orderList[0].Field1 == "1" ? orderList[0].complete_date : null;
                rd.OrderListInfo.OrderReasonTypeId = orderList[0].order_reason_id;
                rd.OrderListInfo.ActualDecimal = orderList[0].actual_amount ?? 0;

                 

                rd.OrderListInfo.PaymentTypeCode = orderList[0].Payment_Type_Code;
                rd.OrderListInfo.PaymentTypeName = orderList[0].Payment_Type_Name;

                rd.OrderListInfo.ReserveTime = orderList[0].reserveDay + " " + orderList[0].reserveQuantum;

                var deliveryBll = new TOrderCustomerDeliveryStrategyMappingBLL(this.CurrentUserInfo);
                rd.OrderListInfo.DeliveryAmount = deliveryBll.GetDeliverAmount(orderId);//配送费 add by henry***

                if (!string.IsNullOrEmpty(orderList[0].Field15) && orderList[0].Field15 != "0") //是否是团购商品 add by Henry 2014-12-22
                    rd.OrderListInfo.IsEvent = 1;   //团购商品
                else
                    rd.OrderListInfo.IsEvent = 0;   //普通商品

                #region update by changjian.tian

                rd.OrderListInfo.Mobile = orderList[0].Field6; //配送联系电话 
                rd.OrderListInfo.DeliveryRemark = orderList[0].remark;

                rd.OrderListInfo.IsEvaluation = orderList[0].IsEvaluation == null ? 0 : orderList[0].IsEvaluation.Value;//评论
                #endregion
            }

            if (deliverList.Count() > 0)
            {
                rd.OrderListInfo.DeliveryName = deliverList[0].DeliveryName;
            }


            T_Inout_DetailBLL orderDetailBll = new T_Inout_DetailBLL(this.CurrentUserInfo);
            //退换货Bll实例化
            T_SalesReturnBLL salesReturnBll = new T_SalesReturnBLL(this.CurrentUserInfo);

            var orderDetailList = orderDetailBll.QueryByEntity(new T_Inout_DetailEntity()
            {
                order_id = orderId
            }, null);

            var inoutService = new InoutService(this.CurrentUserInfo);

            #region 根据订单ID获取订单明细

            var ds = inoutService.GetOrderDetailByOrderId(orderId);

            #endregion

            #region 获取订单详细列表中的商品规格

            var ggDs = inoutService.GetInoutDetailGgByOrderId(orderId);

            #endregion

            if (ds.Tables[0].Rows.Count > 0)
            {
                string ItemIdList =
                    ds.Tables[0].AsEnumerable().Aggregate("", (x, j) =>
                    {
                        x += string.Format("'{0}',", j["item_id"].ToString());
                        return x;
                    }).Trim(',');

                TInoutDetailBLL tInoutDetailBll = new TInoutDetailBLL(this.CurrentUserInfo);
                //获取商品的图片
                DataSet imageDs = tInoutDetailBll.GetOrderDetailImageList(ItemIdList);


                var tmp = ds.Tables[0].AsEnumerable().Select(t => new OrderDetailEntity()
                {
                    ItemID = t["item_id"].ToString(),
                    ItemName = t["item_name"].ToString(),
                    SkuID = t["sku_id"].ToString(),
                    SalesReturnFlag = salesReturnBll.CheckSalesReturn(orderId, t["sku_id"].ToString()),//是否可申请退换货
                    //GG = t["prop_1_detail_name"].ToString()+t["prop_2_detail_name"].ToString()+t["prop_3_detail_name"].ToString()
                    //+t["prop_4_detail_name"].ToString()+t["prop_5_detail_name"].ToString(),
                    Field9 = t["Field9"].ToString(),
                    isGB = Convert.ToInt32(t["isGB"]),
                    GG =
                        ggDs.Tables[0].AsEnumerable()
                            .Where(tt => tt["sku_id"].ToString() == t["sku_id"].ToString())
                            .Select(tt => new GuiGeInfo
                            {
                                PropName1 = tt["prop_1_name"].ToString(),
                                PropDetailName1 = tt["prop_1_detail_name"].ToString(),
                                PropName2 = tt["prop_2_name"].ToString(),
                                PropDetailName2 = tt["prop_2_detail_name"].ToString(),
                                PropName3 = tt["prop_3_name"].ToString(),
                                PropDetailName3 = tt["prop_3_detail_name"].ToString(),
                                PropName4 = tt["prop_4_name"].ToString(),
                                PropDetailName4 = tt["prop_4_detail_name"].ToString(),
                                PropName5 = tt["prop_5_name"].ToString(),
                                PropDetailName5 = tt["prop_5_detail_name"].ToString()
                            }).FirstOrDefault(),
                    SalesPrice = Convert.ToDecimal(t["enter_price"]),
                    //DiscountRate = Convert.ToDecimal(t["discount_rate"]),
                    DiscountRate = Convert.ToDecimal(t["order_discount_rate"]),
                    ItemCategoryName = t["itemCategoryName"].ToString(),
                    BeginDate = t["Field1"].ToString(),
                    EndDate = t["Field2"].ToString(),
                    DayCount = Convert.ToInt32(t["DayCount"]),
                    Qty = Convert.ToDecimal(t["enter_qty"]),
                    ImageInfo =
                        imageDs.Tables[0].AsEnumerable()
                            .Where(c => c["ObjectId"].ToString() == t["item_id"].ToString())
                            .OrderBy(c => c["displayIndex"])
                            .Select(c => new OrderDetailImage
                            {
                                ImageID = c["imageId"].ToString(),
                                ImageUrl = ImagePathUtil.GetImagePathStr(c["imageUrl"].ToString(), "240")
                            }).ToArray(),
                    IfService = Convert.ToInt32(t["IfService"])
                });

                int tempCount = 0;
                foreach(var i in tmp)
                {
                    if (i.IfService == 0)
                    {
                        tempCount++; 
                    }
                }
                if (tempCount == 0)
                {
                    rd.OrderListInfo.IsAllService = 3; // 3-全部为虚拟商品
                }
                else if (tempCount == tmp.Count())
                {
                    rd.OrderListInfo.IsAllService = 1; // 1-全部为实物商品
                }
                else
                {
                    rd.OrderListInfo.IsAllService = 2; // 2-包含实物商品和虚拟商品
                }
                

                rd.OrderListInfo.OrderDetailInfo = tmp.ToArray();
            }


            var vipIntegralDetailBll = new VipIntegralDetailBLL(this.CurrentUserInfo);
            // var integral = vipIntegralDetailBll.GetVipIntegralByOrder(orderId, pRequest.UserID);
            //使用积分
            rd.OrderListInfo.OrderIntegral = Math.Abs(vipIntegralDetailBll.GetVipIntegralByOrder(orderId, pRequest.UserID));
            //积分抵扣金额 add by Henry 2014-10-8
            //decimal integralAmountPre = vipBll.GetIntegralAmountPre(this.CurrentUserInfo.ClientID);//获取积分金额比例
            //rd.OrderListInfo.UseIntegralToAmount =rd.OrderListInfo.OrderIntegral*(integralAmountPre>0?integralAmountPre:0.01M);
            rd.OrderListInfo.UseIntegralToAmount = vipBll.GetAmountByIntegralPer(CurrentUserInfo.ClientID, rd.OrderListInfo.OrderIntegral);

            var couponUseBll = new CouponUseBLL(this.CurrentUserInfo);

            var couponParValue = couponUseBll.GetCouponParValue(orderId);
            rd.OrderListInfo.CouponAmount = couponParValue;


            var vipAmountDetailBll = new VipAmountDetailBLL(this.CurrentUserInfo);
            //使用的账户余额
            rd.OrderListInfo.VipEndAmount = Math.Abs(vipAmountDetailBll.GetVipAmountByOrderId(orderId, pRequest.UserID, 1));
            //使用的返现金额
            rd.OrderListInfo.ReturnAmount = Math.Abs(vipAmountDetailBll.GetVipAmountByOrderId(orderId, pRequest.UserID, 13));
            //使用阿拉币和阿拉币抵扣 add by Henry 2014-10-13
            if (pRequest.ChannelId == "4")//阿拉丁APP调用
            {
                decimal aldAmount = Math.Abs(vipAmountDetailBll.GetVipAmountByOrderId(orderId, pRequest.UserID, 11));
                rd.OrderListInfo.ALDAmount = aldAmount;
                rd.OrderListInfo.ALDAmountMoney = aldAmount * 0.01M;
            }
            #region 获取订单积分，优惠券金额，使用余额


            //var vipIntegralDetailBll = new VipIntegralDetailBLL(this.CurrentUserInfo);

            //var vipIntegralList = vipIntegralDetailBll.QueryByEntity(new VipIntegralDetailEntity()
            //{
            //    VIPID = pRequest.UserID,
            //    ObjectId = orderId
            //}, null);
            //if (vipIntegralList != null && vipIntegralList.Length > 0)
            //{
            //    rd.OrderListInfo.OrderIntegral = Math.Abs(vipIntegralList[0].Integral??0);
            //}

            //var tOrderCouponMappingBll = new TOrderCouponMappingBLL(this.CurrentUserInfo);

            //var tOrderCouponMappingList = tOrderCouponMappingBll.QueryByEntity(new TOrderCouponMappingEntity()
            //{
            //    OrderId = orderId
            //}, null);
            //if (tOrderCouponMappingList != null && tOrderCouponMappingList.Length > 0)
            //{
            //    var couponId = tOrderCouponMappingList[0].CouponId;

            //    var couponBll = new CouponBLL(this.CurrentUserInfo);
            //    var couponEntity = couponBll.GetByID(couponId);

            //    if (couponEntity != null)
            //    {
            //        var couponTypeId = couponEntity.CouponTypeID;

            //        var couponTypeBll = new CouponTypeBLL(this.CurrentUserInfo);
            //        var couponTypeEntity = couponTypeBll.GetByID(couponTypeId);
            //        if (couponTypeEntity != null)
            //        {
            //            rd.OrderListInfo.CouponAmount = couponTypeEntity.ParValue ?? 0;
            //        }
            //    }
            //}
            //var vipAmountDetailBll = new VipAmountDetailBLL(this.CurrentUserInfo);
            //var vipAmountDetailList = vipAmountDetailBll.QueryByEntity(new VipAmountDetailEntity()
            //{
            //    VipId = pRequest.UserID,
            //    ObjectId = orderId
            //}, null);

            //if (vipAmountDetailList != null && vipAmountDetailList.Length > 0)
            //{
            //    rd.OrderListInfo.VipEndAmount = Math.Abs(vipAmountDetailList[0].Amount ?? 0);
            //}

            #endregion
            return rd;
        }
    }
}