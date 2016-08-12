using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;
using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL;
using System.Configuration;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;
using System.Data;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess;
using JIT.CPOS.BS.BLL.RedisOperationBLL.Order;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderReward;



namespace JIT.CPOS.Web.OnlineShopping.Notify
{
    /// <summary>
    /// PayNotify 的摘要说明
    /// </summary>
    public class PayNotify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var OrderID = context.Request["OrderID"];
            var OrderStatus = context.Request["OrderStatus"];
            var CustomerID = context.Request["CustomerID"];
            var UserID = context.Request["UserID"];
            var ChannelID = context.Request["ChannelID"];
            var SerialPay = context.Request["SerialPay"];
            
            try
            {
                if (string.IsNullOrEmpty(OrderID) || string.IsNullOrEmpty(OrderStatus) || string.IsNullOrEmpty(CustomerID) || string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(ChannelID))
                {
                    throw new Exception("参数不全:OrderID,OrderStatus,CustomerID,UserID");
                }
                else
                {
                    if (OrderStatus == "2")
                    {
                        //支付成功，更新卡的支付状态
                        //OrderID就是VIPCardID
                        //
                        //var rp = pRequest.DeserializeJSONTo<APIRequest<SetVipCardRP>>();

                        //if (string.IsNullOrEmpty(rp.Parameters.PayID))
                        //{
                        //    throw new APIException("缺少参数【PayID】或参数值为空") { ErrorCode = 135 };
                        //}
                        var loggingSessionInfo = Default.GetBSLoggingSession(CustomerID, UserID);
                        //会员
                        var vipBll = new VipBLL(loggingSessionInfo);
                        var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
                        var vipCardVipMappingBll = new VipCardVipMappingBLL(loggingSessionInfo);
                        //支付
                        var tPaymentTypeCustomerMappingBll = new TPaymentTypeCustomerMappingBLL(loggingSessionInfo);
                        var tPaymentTypeBll = new T_Payment_TypeBLL(loggingSessionInfo);
                        var paymentDetailBll = new T_Payment_detailBLL(loggingSessionInfo);
                        //门店
                        var unitBLL = new t_unitBLL(loggingSessionInfo);
                        //商品订单支付
                        //更新积分和状态
                        //var loggingSessionInfo = Default.GetBSLoggingSession(CustomerID, "1");
                        var inoutBll = new T_InoutBLL(loggingSessionInfo);//订单业务对象实例化

                        var trrBll = new T_RewardRecordBLL(loggingSessionInfo);
                        //辨别打赏订单
                        var rewardOrderPrefix = "REWARD|";
                        if (OrderID.Contains(rewardOrderPrefix))
                        {
                            OrderID = OrderID.Substring(rewardOrderPrefix.Length, OrderID.Length - rewardOrderPrefix.Length);
                            var trrEntity = trrBll.GetByID(OrderID);
                            trrEntity.PayStatus = 2;
                            trrEntity.LastUpdateTime = DateTime.Now;
                            trrEntity.LastUpdateBy = loggingSessionInfo.UserID;
                            trrBll.Update(trrEntity);

                            #region 员工余额变更--需要独立出来处理                            
                            var userAmountBll = new VipAmountBLL(loggingSessionInfo);//作为员工余额使用
                            var employeeId = trrEntity.RewardedOP;
                            var rewardAmount = trrEntity.RewardAmount == null ? 0 : (decimal)trrEntity.RewardAmount;//转为非null的decimal类型
                            //门店
                            var unitService = new UnitService(loggingSessionInfo);
                            var unitInfo = unitService.GetUnitByUser(CustomerID, employeeId).FirstOrDefault();//获取员工所属门店

                            var tran = userAmountBll.GetTran();
                            using (tran.Connection)//事务
                            {
                                try
                                {
                                    var userAmountEntity = userAmountBll.GetByID(trrEntity.RewardedOP);
                                    if (userAmountEntity == null)
                                    {
                                        //创建
                                        userAmountEntity = new VipAmountEntity
                                        {
                                            VipId = employeeId,//员工ID
                                            VipCardCode = string.Empty,
                                            BeginAmount = 0,
                                            InAmount = rewardAmount,
                                            OutAmount = 0,
                                            EndAmount = rewardAmount,
                                            TotalAmount = rewardAmount,
                                            BeginReturnAmount = 0,
                                            InReturnAmount = 0,
                                            OutReturnAmount = 0,
                                            ReturnAmount = 0,
                                            ImminentInvalidRAmount = 0,
                                            InvalidReturnAmount = 0,
                                            ValidReturnAmount = 0,
                                            TotalReturnAmount = 0,
                                            IsLocking = 0,
                                            CustomerID = CustomerID
                                        };
                                        userAmountBll.Create(userAmountEntity, tran);//创建员工余额表
                                    }
                                    else
                                    {
                                        //修改
                                        if (rewardAmount > 0)
                                        {
                                            userAmountEntity.InAmount = (userAmountEntity.InAmount == null ? 0 : userAmountEntity.InAmount.Value) + rewardAmount;
                                            userAmountEntity.TotalAmount = (userAmountEntity.TotalAmount == null ? 0 : userAmountEntity.TotalAmount.Value) + rewardAmount;
                                        }
                                        else
                                            userAmountEntity.OutAmount = (userAmountEntity.OutAmount == null ? 0 : userAmountEntity.OutAmount.Value) + Math.Abs(rewardAmount);
                                        userAmountEntity.EndAmount = (userAmountEntity.EndAmount == null ? 0 : userAmountEntity.EndAmount.Value) + rewardAmount;

                                        userAmountBll.Update(userAmountEntity, tran);//更新余额
                                    }

                                    var vipamountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);
                                    var vipAmountDetailEntity = new VipAmountDetailEntity
                                    {
                                        VipAmountDetailId = Guid.NewGuid(),
                                        VipCardCode = string.Empty,
                                        VipId = employeeId,//员工ID
                                        UnitID = unitInfo != null ? unitInfo.unit_id : string.Empty,
                                        UnitName = unitInfo != null ? unitInfo.Name : string.Empty,
                                        Amount = rewardAmount,
                                        UsedReturnAmount = 0,
                                        EffectiveDate = DateTime.Now,
                                        DeadlineDate = Convert.ToDateTime("9999-12-31 23:59:59"),
                                        AmountSourceId = "26",
                                        Reason = "Reward",
                                        CustomerID = CustomerID
                                    };
                                    vipamountDetailBll.Create(vipAmountDetailEntity, tran);//创建余额详情

                                    tran.Commit();//提交事务
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                    Loggers.Debug(new DebugLogInfo()
                                    {
                                        Message = "异常-->支付成功回调时更新会员打赏金额出错(PayNotify.ashx)：" + ex
                                    });
                                }
                            }                            
                            #endregion

                            if (trrEntity != null)
                                context.Response.Write("SUCCESS");
                            else
                                context.Response.Write("FAIL");
                            return;
                        }
                        //获取会员信息
                        var vipInfo = vipBll.GetByID(loggingSessionInfo.UserID);
                        //支付信息
                        var tPaymentTypeCustomerMappingEntity = tPaymentTypeCustomerMappingBll.QueryByEntity(new TPaymentTypeCustomerMappingEntity(){ChannelId = ChannelID,CustomerId = loggingSessionInfo.ClientID },null).FirstOrDefault();
                        var tPaymentType = tPaymentTypeBll.GetByID(tPaymentTypeCustomerMappingEntity.PaymentTypeID);
                        //获取订单信息
                        var inoutInfo = inoutBll.GetByID(OrderID);
                        if (inoutInfo != null)
                        {
                            var bll = new TInOutStatusNodeBLL(loggingSessionInfo);
                            string msg;
                            if (!bll.SetOrderPayment(OrderID, out msg, ChannelID, SerialPay))
                            //if (!bll.SetOrderPayment(OrderID, out msg, ChannelID))
                            {
                                throw new Exception(msg);
                            }
                            else if (string.IsNullOrEmpty(inoutInfo.Field17) && string.IsNullOrEmpty(inoutInfo.Field18))
                            {
                                #region 发送订单支付成功微信模板消息
                                var SuccessCommonBLL = new CommonBLL();
                                //SuccessCommonBLL.SentPaymentMessage(inoutInfo, vipInfo.WeiXinUserId,vipInfo.VIPID, loggingSessionInfo);
                                new SendOrderPaySuccessMsgBLL().SentPaymentMessage(inoutInfo, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);
                                #endregion
                                Loggers.Debug(new DebugLogInfo() { Message = "调用SetOrderPayment方法更新订单成功" });
                            }
                            //获取订单信息,根据Field3==1判断,如果是ALD订单,则调用ALD接口更新ALD订单的状态
                            #region 更新ALD状态
                            //var orderbll = new InoutService(loggingSessionInfo);
                            //var orderInfo = orderbll.GetInoutInfoById(OrderID);
                            //if (orderInfo.Field3 == "1")
                            //{
                            //    Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新O2OMarketing订单状态成功[OrderID={0}].", OrderID) });
                            //    //更新阿拉丁的订单状态
                            //    JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatus aldChangeOrder = new data.DataOnlineShoppingHandler.ALDChangeOrderStatus();
                            //    if (string.IsNullOrEmpty(orderInfo.vip_no))
                            //        throw new Exception("会员ID不能为空,OrderID:" + OrderID);
                            //    aldChangeOrder.MemberID = new Guid(orderInfo.vip_no);
                            //    aldChangeOrder.SourceOrdersID = OrderID;
                            //    aldChangeOrder.Status = int.Parse(orderInfo.status);
                            //    aldChangeOrder.IsPaid = true;
                            //    JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatusRequest aldRequest = new data.DataOnlineShoppingHandler.ALDChangeOrderStatusRequest();
                            //    aldRequest.BusinessZoneID = 1;//写死
                            //    aldRequest.Locale = 1;

                            //    aldRequest.UserID = new Guid(orderInfo.vip_no);
                            //    aldRequest.Parameters = aldChangeOrder;
                            //    var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                            //    var postContent = string.Format("Action=ChangeOrderStatus&ReqContent={0}", aldRequest.ToJSON());
                            //    Loggers.Debug(new DebugLogInfo() { Message = "通知ALD更改状态:" + postContent });
                            //    var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                            //    var aldRsp = strAldRsp.DeserializeJSONTo<JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                            //    if (!aldRsp.IsSuccess())
                            //    {
                            //        Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新阿拉丁订单状态失败[Request ={0}][Response={1}]", aldRequest.ToJSON(), strAldRsp) });
                            //    }
                            //}
                            #endregion

                            #region 格力推送通知
                            //try
                            //{
                            //    GLServiceOrderBLL glsobll = new GLServiceOrderBLL(loggingSessionInfo);
                            //    if (glsobll.ValidateGree(CustomerID, "cpos_bs_lj"))//先写死
                            //        glsobll.GreePushPaymentOorder(CustomerID, OrderID, loggingSessionInfo);
                            //}
                            //catch (Exception ex)
                            //{
                            //    Loggers.Debug(new DebugLogInfo() { Message = string.Format("付款推送评价师傅链接失败[OrderID={0}].", OrderID) });
                            //}
                            #endregion

                            #region ALD生活服务处理
                            //var rechargeBll = new RechargeStrategyBLL(loggingSessionInfo);
                            //var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
                            //var couponBll = new CouponBLL(loggingSessionInfo);
                            //var vipCouponMappingBll = new VipCouponMappingBLL(loggingSessionInfo);
                            //var unitBll = new UnitBLL(loggingSessionInfo);
                            //DataSet dsOrderInfo = rechargeBll.GetInoutOrderItems(OrderID);
                            //int itemSort = 0;            //商品业务分类
                            //string skuId = string.Empty; //商品SkuId
                            //string vipId = string.Empty; //会员ID
                            //string itemId = string.Empty;//商品ID
                            //string couponId = string.Empty;//优惠券ID
                            //if (dsOrderInfo.Tables[0].Rows.Count > 0)
                            //{
                            //    //if (dsOrderInfo.Tables[0].Rows[0]["ItemSort"] != DBNull.Value && Convert.ToString(dsOrderInfo.Tables[0].Rows[0]["ItemSort"]) != "0")
                            //    itemSort = int.Parse(dsOrderInfo.Tables[0].Rows[0]["ItemSort"].ToString());
                            //    switch (itemSort)
                            //    {
                            //        case 2://充
                            //            #region 充值金额处理

                            //            VipAmountDetailBLL vipAmountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);

                            //            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                            //            complexCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value = OrderID });

                            //            var vipAmountDetal = vipAmountDetailBll.Query(complexCondition.ToArray(), null);
                            //            if (vipAmountDetal.Count() == 0)//确认没有多次执行回调
                            //            {
                            //                ////查询
                            //                //RechargeStrategyEntity[] rechargeList = rechargeBll.Query(complexCondition.ToArray(), lstOrder.ToArray());

                            //                skuId = dsOrderInfo.Tables[0].Rows[0]["SkuId"].ToString();
                            //                //RechargeStrategyEntity rechargeEntity = rechargeBll.GetByID(skuId);
                            //                DataSet dsSkuPirce = unitBll.GetSkuPirce(skuId);
                            //                decimal salePrice = 0;//购买金额
                            //                decimal returnCash = 0;//奖励金额
                            //                if (dsSkuPirce.Tables[0].Rows.Count > 0)
                            //                {
                            //                    salePrice = Convert.ToDecimal(dsSkuPirce.Tables[0].Rows[0]["SalesPrice"].ToString());
                            //                    returnCash = Convert.ToDecimal(dsSkuPirce.Tables[0].Rows[0]["ReturnCash"].ToString());
                            //                    InoutService server = new InoutService(loggingSessionInfo);
                            //                    #region 充值金额
                            //                    var tran = server.GetTran();
                            //                    using (tran.Connection)//事物
                            //                    {
                            //                        try
                            //                        {
                            //                            //充值金额
                            //                            vipAmountBll.AddVipEndAmount(UserID, salePrice, tran, "4", OrderID, loggingSessionInfo);//4=充值
                            //                            tran.Commit();
                            //                        }
                            //                        catch (Exception)
                            //                        {
                            //                            tran.Rollback();
                            //                            throw;
                            //                        }
                            //                    }
                            //                    #endregion

                            //                    #region 奖励金额
                            //                    var tran2 = server.GetTran();
                            //                    using (tran2.Connection)//事物
                            //                    {
                            //                        try
                            //                        {
                            //                            //奖励金额
                            //                            vipAmountBll.AddVipEndAmount(UserID, returnCash, tran2, "6", OrderID, loggingSessionInfo);//6=奖励金额
                            //                            tran2.Commit();
                            //                        }
                            //                        catch (Exception)
                            //                        {
                            //                            tran2.Rollback();
                            //                            throw;
                            //                        }
                            //                    }
                            //                    #endregion
                            //                }
                            //            }
                            //            #endregion
                            //            break;
                            //        case 3://券
                            //            #region 券类商品绑定到会员
                            //            itemId = dsOrderInfo.Tables[0].Rows[0]["ItemId"].ToString();
                            //            vipId = dsOrderInfo.Tables[0].Rows[0]["VipId"].ToString();
                            //            couponId = couponBll.GetCouponByItemId(itemId);
                            //            if (!string.IsNullOrEmpty(couponId))
                            //            {
                            //                VipCouponMappingEntity coupon = new VipCouponMappingEntity
                            //                {
                            //                    VIPID = vipId,
                            //                    CouponID = couponId,
                            //                };
                            //                vipCouponMappingBll.Create(coupon);
                            //            }
                            //            #endregion
                            //            break;
                            //        default:
                            //            break;
                            //    }
                            //}

                            #endregion

                            #region 订单与分润关系处理 add by Henry 2014-10-10
                            //var orderSubBll = new OrderOrderSubRunObjectMappingBLL(loggingSessionInfo);
                            //dynamic o = orderSubBll.SetOrderSub(CustomerID, OrderID);
                            //Type t = o.GetType();
                            //var Desc = t.GetProperty("Desc").GetValue(o, null).ToString();
                            //var IsSuccess = t.GetProperty("IsSuccess").GetValue(o, null).ToString();
                            //if (int.Parse(IsSuccess.ToString()) == 0) //失败
                            //    Loggers.Debug(new DebugLogInfo() { Message = string.Format("订单与分润关系处理失败:{0}", Desc) });
                            #endregion

                            CustomerBasicSettingBLL customerBasicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
                            string AfterPaySetStock = customerBasicSettingBll.GetSettingValueByCode("AfterPaySetStock");
                            if (AfterPaySetStock == "1")
                            {
                                var inoutServiceBLL = new InoutService(loggingSessionInfo);
                                var inoutDetailList = inoutServiceBLL.GetInoutDetailInfoByOrderId(OrderID);
                                inoutBll.SetStock(OrderID, inoutDetailList, 1, loggingSessionInfo);   
                            }
                            ///超级分销商订单入队列
                            if(inoutInfo.data_from_id == "35" || inoutInfo.data_from_id == "36" )
                            {
                                BS.BLL.RedisOperationBLL.Order.SuperRetailTraderOrderBLL bllSuperRetailTraderOrder=new BS.BLL.RedisOperationBLL.Order.SuperRetailTraderOrderBLL();
                                bllSuperRetailTraderOrder.SetRedisToSuperRetailTraderOrder(loggingSessionInfo,inoutInfo);
                            }

                            //购卡
                            if (!string.IsNullOrEmpty(inoutInfo.Field17) && !string.IsNullOrEmpty(inoutInfo.Field18))
                            {
                                //更新订单状态
                                inoutInfo = inoutBll.GetByID(OrderID);
                                inoutInfo.Field7 = "700";
                                inoutInfo.status = "700";
                                inoutInfo.status_desc = "已完成";
                                inoutInfo.Field10 = "已完成";
                                inoutBll.Update(inoutInfo);
                                //会员卡升级
                                vipCardVipMappingBll.BindVirtualItem(vipInfo.VIPID, vipInfo.VipCode, inoutInfo.sales_unit_id, Convert.ToInt32(inoutInfo.Field18), orderId: inoutInfo.order_id);

                                //分润计算
                                RedisSalesVipCardOrderBLL redisSalesVipCardOrderBll = new RedisSalesVipCardOrderBLL();
                                redisSalesVipCardOrderBll.SetRedisSalesVipCardOrder(loggingSessionInfo, inoutInfo);

                                //售卡处理积分、返现、佣金[完成订单]
                                new SendOrderRewardMsgBLL().OrderReward(inoutInfo, loggingSessionInfo, null);//存入到缓存
                            }
                            else
                            {
                                //订单入队列
                                RedisCalculateVipConsumeForUpgrade redisCalculateVipConsumeForUpgrade = new RedisCalculateVipConsumeForUpgrade();
                                redisCalculateVipConsumeForUpgrade.SetVipConsumeForUpgradeList(loggingSessionInfo, inoutInfo);
                            }

                            //获取门店信息
                            t_unitEntity unitInfo = null;
                            if (!string.IsNullOrEmpty(inoutInfo.sales_unit_id))
                            {
                                unitInfo = unitBLL.GetByID(inoutInfo.sales_unit_id);
                            }
                            //入支付明细表
                            var paymentDetail = new T_Payment_detailEntity()
                            {
                                Payment_Id = Guid.NewGuid().ToString(),
                                Inout_Id = inoutInfo.order_id,
                                UnitCode = unitInfo == null ? "" : unitInfo.unit_code,
                                Payment_Type_Id = tPaymentType.Payment_Type_Id,
                                Payment_Type_Code = tPaymentType.Payment_Type_Code,
                                Payment_Type_Name = tPaymentType.Payment_Type_Name,
                                Price = inoutInfo.actual_amount,
                                Total_Amount = inoutInfo.total_amount,
                                Pay_Points = inoutInfo.pay_points,
                                CustomerId = loggingSessionInfo.ClientID
                            };
                            paymentDetailBll.Create(paymentDetail);
                        }
                        else//充值订单
                        {
                            var rechargeOrderBll = new RechargeOrderBLL(loggingSessionInfo);
                            var vipamountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);
                            var rechargeOrderInfo = rechargeOrderBll.GetByID(OrderID);
                            if (rechargeOrderInfo != null)
                            {
                                //更新订单状态
                                rechargeOrderInfo.Status = 1;//已支付
                                rechargeOrderInfo.PayID = tPaymentTypeCustomerMappingEntity.PaymentTypeID;
                                rechargeOrderInfo.PayDateTime = DateTime.Now;
                                rechargeOrderBll.Update(rechargeOrderInfo);

                                //获取会员卡绑卡信息
                                var vipCardVipMappingList = vipCardVipMappingBll.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipInfo.VIPID }, null);
                                //会员无卡并且是续费充值的
                                if (vipCardVipMappingList.Count() == 0 && rechargeOrderInfo.OrderDesc == "ReRecharge")
                                {
                                    vipCardVipMappingBll.BindVipCard(vipInfo.VIPID, vipInfo.VipCardCode, rechargeOrderInfo.UnitId);
                                }

                                //获取门店信息
                                t_unitEntity unitInfo = null;
                                if (!string.IsNullOrEmpty(rechargeOrderInfo.UnitId))
                                    unitInfo = unitBLL.GetByID(rechargeOrderInfo.UnitId);

                                var vipAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                                //充值
                                var amountDetailInfo = new VipAmountDetailEntity()
                                {
                                    Amount = rechargeOrderInfo.TotalAmount.Value, 
                                    AmountSourceId = "4",
                                    ObjectId = rechargeOrderInfo.OrderID.ToString()
                                };
                      
                                var vipAmountDetailId = vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipAmountEntity, amountDetailInfo, null, loggingSessionInfo);
                                if (!string.IsNullOrWhiteSpace(vipAmountDetailId) && rechargeOrderInfo.TotalAmount.Value != 0)
                                {
                                    //发送账户余额变动微信模板消息
                                    var CommonBLL = new CommonBLL();
                                    CommonBLL.BalanceChangedMessage(rechargeOrderInfo.OrderNo, vipAmountEntity, amountDetailInfo, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);
                                }

                                //赠送
                                if (rechargeOrderInfo.ReturnAmount.Value != 0)
                                {
                                    var returnAmountInfo = new VipAmountDetailEntity()
                                    {
                                        Amount = rechargeOrderInfo.ReturnAmount.Value,
                                        AmountSourceId = "6",
                                        ObjectId = rechargeOrderInfo.OrderID.ToString()
                                    };
                                    var vipReturnDetailId = vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipAmountEntity, returnAmountInfo, null, loggingSessionInfo);

                                    if (!string.IsNullOrWhiteSpace(vipReturnDetailId) && rechargeOrderInfo.ReturnAmount.Value != 0)
                                    {
                                        //发送账户余额变动微信模板消息
                                        var CommonBLL = new CommonBLL();
                                        CommonBLL.BalanceChangedMessage(rechargeOrderInfo.OrderNo, vipAmountEntity, returnAmountInfo, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);
                                    }
                                }
                                if (rechargeOrderInfo.OrderDesc == "Upgrade")
                                {
                                    //会员卡升级
                                    vipCardVipMappingBll.BindVirtualItem(vipInfo.VIPID, vipInfo.VipCode, rechargeOrderInfo.UnitId, rechargeOrderInfo.VipCardTypeId ?? 0, "Recharge", orderId: rechargeOrderInfo.OrderID.ToString());

                                }

                                //充值分润
                                RedisRechargeOrderBLL redisRechargeOrderBll = new RedisRechargeOrderBLL();
                                redisRechargeOrderBll.SetRedisToRechargeOrder(loggingSessionInfo, rechargeOrderInfo);

                                //入支付明细表
                                var paymentDetail = new T_Payment_detailEntity()
                                {
                                    Payment_Id = Guid.NewGuid().ToString(),
                                    Inout_Id = rechargeOrderInfo.OrderID.ToString(),
                                    UnitCode = unitInfo == null ? "" : unitInfo.unit_code,
                                    Payment_Type_Id = tPaymentType.Payment_Type_Id,
                                    Payment_Type_Code = tPaymentType.Payment_Type_Code,
                                    Payment_Type_Name = tPaymentType.Payment_Type_Name,
                                    Price = rechargeOrderInfo.ActuallyPaid,
                                    Total_Amount = rechargeOrderInfo.TotalAmount,
                                    Pay_Points = rechargeOrderInfo.PayPoints,
                                    CustomerId = loggingSessionInfo.ClientID
                                };
                                paymentDetailBll.Create(paymentDetail);
                            }
                            else
                            {
                                var receiveAmountOrderBll = new ReceiveAmountOrderBLL(loggingSessionInfo);
                                var receiveAmountOrderEntity = receiveAmountOrderBll.GetByID(OrderID);



                                //更新订单状态
                                if (receiveAmountOrderEntity != null)
                                {
                                    VipIntegralBLL vipIntegralBll = new VipIntegralBLL(loggingSessionInfo);
                                    //更新订单
                                    receiveAmountOrderEntity.PayStatus = "10";
                                    receiveAmountOrderEntity.PayTypeId = tPaymentTypeCustomerMappingEntity.PaymentTypeID;
                                    receiveAmountOrderEntity.PayDatetTime = DateTime.Now;
                                    receiveAmountOrderBll.Update(receiveAmountOrderEntity);

                                    //获取门店信息
                                    t_unitEntity unitInfo = null;
                                    if (!string.IsNullOrEmpty(receiveAmountOrderEntity.ServiceUnitId))
                                    {
                                        unitInfo = unitBLL.GetByID(receiveAmountOrderEntity.ServiceUnitId);
                                    }


                                    //使用过积分，处理积分
                                    if (receiveAmountOrderEntity.PayPoints != 0 && receiveAmountOrderEntity.PayPoints != null)
                                    {

                                        string sourceId = "20"; //积分抵扣
                                        var IntegralDetail = new VipIntegralDetailEntity()
                                        {
                                            Integral = -Convert.ToInt32(receiveAmountOrderEntity.PayPoints),
                                            IntegralSourceID = sourceId,
                                            ObjectId = receiveAmountOrderEntity.OrderId.ToString()
                                        };
                                        if (IntegralDetail.Integral != 0)
                                        {
                                            //变动前积分
                                            string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                                            //变动积分
                                            string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                                            var vipIntegralDetailId = vipIntegralBll.AddIntegral(ref vipInfo, unitInfo, IntegralDetail, loggingSessionInfo);
                                            //发送微信积分变动通知模板消息
                                            if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                                            {
                                                var CommonBLL = new CommonBLL();
                                                CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, loggingSessionInfo);
                                            }
                                        }
                                    }
                                    //if (receiveAmountOrderEntity.CouponUsePay != 0 || receiveAmountOrderEntity.CouponUsePay != null)
                                    //{
                                    //    //更新使用记录
                                    //    var couponUseBll = new CouponUseBLL(loggingSessionInfo);
                                    //    var couponUseEntity = new CouponUseEntity()
                                    //    {
                                    //        CouponUseID = Guid.NewGuid(),
                                    //        CouponID = rp.CouponId,
                                    //        VipID = vipInfo.VIPID,
                                    //        UnitID = rp.UnitId,
                                    //        OrderID = orderId.ToString(),
                                    //        Comment = "商城使用电子券",
                                    //        CustomerID = CurrentUserInfo.ClientID,
                                    //        CreateBy = CurrentUserInfo.UserID,
                                    //        CreateTime = DateTime.Now,
                                    //        LastUpdateBy = CurrentUserInfo.UserID,
                                    //        LastUpdateTime = DateTime.Now,
                                    //        IsDelete = 0
                                    //    };
                                    //    couponUseBll.Create(couponUseEntity);

                                    //    var couponBll = new CouponBLL(CurrentUserInfo);
                                    //    var couponEntity = couponBll.GetByID(rp.CouponId);

                                    //    //更新CouponType数量
                                    //    var conponTypeBll = new CouponTypeBLL(CurrentUserInfo);
                                    //    var conponTypeEntity = conponTypeBll.QueryByEntity(new CouponTypeEntity() { CouponTypeID = new Guid(couponEntity.CouponTypeID), CustomerId = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                                    //    conponTypeEntity.IsVoucher += 1;
                                    //    conponTypeBll.Update(conponTypeEntity);

                                    //    //停用该优惠券
                                    //    couponEntity.Status = 1;
                                    //    couponBll.Update(couponEntity);
                                    //}

                                    //处理余额
                                    if (receiveAmountOrderEntity.AmountAcctPay != null && receiveAmountOrderEntity.AmountAcctPay != 0)
                                    {

                                        var vipAmountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);

                                        var vipAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                                        if (vipAmountEntity != null)
                                        {
                                            var detailInfo = new VipAmountDetailEntity()
                                            {
                                                Amount = -receiveAmountOrderEntity.AmountAcctPay,
                                                AmountSourceId = "1",
                                                ObjectId = receiveAmountOrderEntity.OrderId.ToString()
                                            };
                                            var vipAmountDetailId = vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipAmountEntity, detailInfo, loggingSessionInfo);
                                            if (!string.IsNullOrWhiteSpace(vipAmountDetailId))
                                            {//发送微信账户余额变动模板消息
                                                var CommonBLL = new CommonBLL();
                                                CommonBLL.BalanceChangedMessage(receiveAmountOrderEntity.OrderNo, vipAmountEntity, detailInfo, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);
                                            }
                                        }
                                    }
                                    

                                    //收款订单积分奖励

                                    vipIntegralBll.OrderReward(receiveAmountOrderEntity, null);


                                    var paymentDetail = new T_Payment_detailEntity()
                                    {
                                        Payment_Id = Guid.NewGuid().ToString(),
                                        Inout_Id = receiveAmountOrderEntity.OrderId.ToString(),
                                        UnitCode = unitInfo == null ? "" : unitInfo.unit_code,
                                        Payment_Type_Id = tPaymentType.Payment_Type_Id,
                                        Payment_Type_Code = tPaymentType.Payment_Type_Code,
                                        Payment_Type_Name = tPaymentType.Payment_Type_Name,
                                        Price = receiveAmountOrderEntity.TransAmount,
                                        Total_Amount = receiveAmountOrderEntity.TotalAmount,
                                        Pay_Points = receiveAmountOrderEntity.PayPoints,
                                        CustomerId = loggingSessionInfo.ClientID
                                    };
                                    paymentDetailBll.Create(paymentDetail);
                                }
                            }

                        }
                        context.Response.Write("SUCCESS");
                    }
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                context.Response.Write("ERROR:" + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}