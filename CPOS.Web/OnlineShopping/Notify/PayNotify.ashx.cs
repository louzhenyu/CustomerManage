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
                        //更新积分和状态
                        var loggingSessionInfo = Default.GetBSLoggingSession(CustomerID, "1");
                        var bll = new TInOutStatusNodeBLL(loggingSessionInfo);
                        string msg;
                        if (!bll.SetOrderPayment(OrderID, out msg, ChannelID, SerialPay))
                        //if (!bll.SetOrderPayment(OrderID, out msg, ChannelID))
                        {
                            throw new Exception(msg);
                        }
                        else
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = "调用SetOrderPayment方法更新订单成功" });
                        }
                        //获取订单信息,根据Field3==1判断,如果是ALD订单,则调用ALD接口更新ALD订单的状态
                        #region 更新ALD状态
                        var orderbll = new InoutService(loggingSessionInfo);
                        var orderInfo = orderbll.GetInoutInfoById(OrderID);
                        if (orderInfo.Field3 == "1")
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新O2OMarketing订单状态成功[OrderID={0}].", OrderID) });
                            //更新阿拉丁的订单状态
                            JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatus aldChangeOrder = new data.DataOnlineShoppingHandler.ALDChangeOrderStatus();
                            if (string.IsNullOrEmpty(orderInfo.vip_no))
                                throw new Exception("会员ID不能为空,OrderID:" + OrderID);
                            aldChangeOrder.MemberID = new Guid(orderInfo.vip_no);
                            aldChangeOrder.SourceOrdersID = OrderID;
                            aldChangeOrder.Status = int.Parse(orderInfo.status);
                            aldChangeOrder.IsPaid = true;
                            JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatusRequest aldRequest = new data.DataOnlineShoppingHandler.ALDChangeOrderStatusRequest();
                            aldRequest.BusinessZoneID = 1;//写死
                            aldRequest.Locale = 1;

                            aldRequest.UserID = new Guid(orderInfo.vip_no);
                            aldRequest.Parameters = aldChangeOrder;
                            var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                            var postContent = string.Format("Action=ChangeOrderStatus&ReqContent={0}", aldRequest.ToJSON());
                            Loggers.Debug(new DebugLogInfo() { Message = "通知ALD更改状态:" + postContent });
                            var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                            var aldRsp = strAldRsp.DeserializeJSONTo<JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                            if (!aldRsp.IsSuccess())
                            {
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新阿拉丁订单状态失败[Request ={0}][Response={1}]", aldRequest.ToJSON(), strAldRsp) });
                            }
                        }
                        #endregion

                        #region 格力推送通知
                        try
                        {
                            GLServiceOrderBLL glsobll = new GLServiceOrderBLL(loggingSessionInfo);
                            if (glsobll.ValidateGree(CustomerID, "cpos_bs_lj"))//先写死
                                glsobll.GreePushPaymentOorder(CustomerID, OrderID, loggingSessionInfo);
                        }
                        catch (Exception ex)
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("付款推送评价师傅链接失败[OrderID={0}].", OrderID) });
                        }
                        #endregion

                        #region ALD生活服务处理
                        var rechargeBll = new RechargeStrategyBLL(loggingSessionInfo);
                        var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
                        var couponBll = new CouponBLL(loggingSessionInfo);
                        var vipCouponMappingBll = new VipCouponMappingBLL(loggingSessionInfo);
                        var unitBll = new UnitBLL(loggingSessionInfo);
                        DataSet dsOrderInfo = rechargeBll.GetInoutOrderItems(OrderID);
                        int itemSort = 0;            //商品业务分类
                        string skuId = string.Empty; //商品SkuId
                        string vipId = string.Empty; //会员ID
                        string itemId = string.Empty;//商品ID
                        string couponId = string.Empty;//优惠券ID
                        if (dsOrderInfo.Tables[0].Rows.Count > 0)
                        {
                            //if (dsOrderInfo.Tables[0].Rows[0]["ItemSort"] != DBNull.Value && Convert.ToString(dsOrderInfo.Tables[0].Rows[0]["ItemSort"]) != "0")
                            itemSort = int.Parse(dsOrderInfo.Tables[0].Rows[0]["ItemSort"].ToString());
                            switch (itemSort)
                            {
                                case 2://充
                                    #region 充值金额处理

                                    VipAmountDetailBLL vipAmountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);

                                    List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                                    complexCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value =OrderID  });

                                    var vipAmountDetal= vipAmountDetailBll.Query(complexCondition.ToArray(),null);
                                    if (vipAmountDetal.Count() == 0)//确认没有多次执行回调
                                    {
                                        ////查询
                                        //RechargeStrategyEntity[] rechargeList = rechargeBll.Query(complexCondition.ToArray(), lstOrder.ToArray());

                                        skuId = dsOrderInfo.Tables[0].Rows[0]["SkuId"].ToString();
                                        //RechargeStrategyEntity rechargeEntity = rechargeBll.GetByID(skuId);
                                        DataSet dsSkuPirce = unitBll.GetSkuPirce(skuId);
                                        decimal salePrice = 0;//购买金额
                                        decimal returnCash = 0;//奖励金额
                                        if (dsSkuPirce.Tables[0].Rows.Count > 0)
                                        {
                                            salePrice = Convert.ToDecimal(dsSkuPirce.Tables[0].Rows[0]["SalesPrice"].ToString());
                                            returnCash = Convert.ToDecimal(dsSkuPirce.Tables[0].Rows[0]["ReturnCash"].ToString());
                                            InoutService server = new InoutService(loggingSessionInfo);
                                            #region 充值金额
                                            var tran = server.GetTran();
                                            using (tran.Connection)//事物
                                            {
                                                try
                                                {
                                                    //充值金额
                                                    vipAmountBll.AddVipEndAmount(UserID, salePrice, tran, "4", OrderID, loggingSessionInfo);//4=充值
                                                    tran.Commit();
                                                }
                                                catch (Exception)
                                                {
                                                    tran.Rollback();
                                                    throw;
                                                }
                                            }
                                            #endregion

                                            #region 奖励金额
                                            var tran2 = server.GetTran();
                                            using (tran2.Connection)//事物
                                            {
                                                try
                                                {
                                                    //奖励金额
                                                    vipAmountBll.AddVipEndAmount(UserID, returnCash, tran2, "6", OrderID, loggingSessionInfo);//6=奖励金额
                                                    tran2.Commit();
                                                }
                                                catch (Exception)
                                                {
                                                    tran2.Rollback();
                                                    throw;
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion
                                    break;
                                case 3://券
                                    #region 券类商品绑定到会员
                                    itemId = dsOrderInfo.Tables[0].Rows[0]["ItemId"].ToString();
                                    vipId = dsOrderInfo.Tables[0].Rows[0]["VipId"].ToString();
                                    couponId = couponBll.GetCouponByItemId(itemId);
                                    if (!string.IsNullOrEmpty(couponId))
                                    {
                                        VipCouponMappingEntity coupon = new VipCouponMappingEntity
                                        {
                                            VIPID = vipId,
                                            CouponID = couponId,
                                        };
                                        vipCouponMappingBll.Create(coupon);
                                    }
                                    #endregion
                                    break;
                                default:
                                    break;
                            }
                        }

                        #endregion
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