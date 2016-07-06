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
using System.Threading.Tasks;
using JIT.CPOS.BS.BLL.SapMessage;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess;



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
            var isTonyCard = context.Request["isTonyCard"];

            try
            {
                LogConsole.PrintLog("收到更改订单状态请求：" + context.Request.Url.AbsoluteUri);
                if (string.IsNullOrEmpty(OrderID) || string.IsNullOrEmpty(OrderStatus) || string.IsNullOrEmpty(CustomerID) || string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(ChannelID))
                {
                    throw new Exception("参数不全:OrderID,OrderStatus,CustomerID,UserID");
                }
                else
                {
                    if (OrderStatus == "2")
                    {
                        var loggingSessionInfo = Default.GetBSLoggingSession(CustomerID, UserID);
                        var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
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
                            {
                                context.Response.Write("SUCCESS");
                            }
                            else
                            {
                                context.Response.Write("FAIL");
                            }
                            return;
                        }

                        var inoutInfo = inoutBll.GetByID(OrderID);
                        if (inoutInfo != null)
                        {
                            var bll = new TInOutStatusNodeBLL(loggingSessionInfo);
                            string msg;
                            // string field7 = inoutInfo.Field7 == "-99" ? "700" : inoutInfo.Field7;
                            if (!bll.SetOrderPayment(OrderID, out msg, ChannelID, SerialPay, isTonyCard))
                            {
                                throw new Exception(msg);
                            }
                            else
                            {
                                #region 发送订单支付成功微信模板消息
                                //获取会员信息
                                var vipBll = new VipBLL(loggingSessionInfo);
                                var vipInfo = vipBll.GetByID(loggingSessionInfo.UserID);
                                if (vipInfo != null)
                                {
                                    var SuccessCommonBLL = new CommonBLL();
                                    SuccessCommonBLL.SentPaymentMessage(inoutInfo, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);
                                }
                                #endregion
                                Loggers.Debug(new DebugLogInfo() { Message = "调用SetOrderPayment方法更新订单成功" });
                            }

                            CustomerBasicSettingBLL customerBasicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
                            string AfterPaySetStock = customerBasicSettingBll.GetSettingValueByCode("AfterPaySetStock");
                            if (AfterPaySetStock == "1")
                            {

                                var inoutServiceBLL = new InoutService(loggingSessionInfo);
                                var inoutDetailList = inoutServiceBLL.GetInoutDetailInfoByOrderId(OrderID);
                                inoutBll.SetStock(OrderID, inoutDetailList, 1, loggingSessionInfo);
                            }
                            ///超级分销商订单入队列
                            if(inoutInfo.data_from_id=="35" || inoutInfo.data_from_id=="36" )
                            {
                                BS.BLL.RedisOperationBLL.Order.SuperRetailTraderOrderBLL bllSuperRetailTraderOrder=new BS.BLL.RedisOperationBLL.Order.SuperRetailTraderOrderBLL();
                                bllSuperRetailTraderOrder.SetRedisToSuperRetailTraderOrder(loggingSessionInfo,inoutInfo);
                            }
                        }
                        else//充值订单
                        {
                            var rechargeOrderBll = new RechargeOrderBLL(loggingSessionInfo);
                            var vipamountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);
                            var rechargeOrderInfo = rechargeOrderBll.GetByID(OrderID);
                            if (rechargeOrderInfo != null)
                            {
                                rechargeOrderInfo.Status = 1;//已支付
                                rechargeOrderBll.Update(rechargeOrderInfo);

                                var vipBll = new VipBLL(loggingSessionInfo);

                                var unitBLL = new t_unitBLL(loggingSessionInfo);

                                //获取会员信息
                                var vipInfo = vipBll.GetByID(loggingSessionInfo.UserID);
                                //获取门店信息
                                t_unitEntity unitInfo = null;
                                if (!string.IsNullOrEmpty(inoutInfo.sales_unit_id))
                                    unitInfo = unitBLL.GetByID(inoutInfo.sales_unit_id);
                                var vipAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                                //充值
                                var amountDetailInfo = new VipAmountDetailEntity()
                                {
                                    Amount = rechargeOrderInfo.TotalAmount.Value,
                                    AmountSourceId = "4",
                                    ObjectId = rechargeOrderInfo.OrderID.ToString()
                                };
                                var vipAmountDetailId = vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipAmountEntity, amountDetailInfo, null, loggingSessionInfo);
                                if (!string.IsNullOrWhiteSpace(vipAmountDetailId))
                                {//发送账户余额变动微信模板消息
                                    var CommonBLL = new CommonBLL();
                                    CommonBLL.BalanceChangedMessage(inoutInfo.order_no, vipAmountEntity, amountDetailInfo, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);
                                }
                                //充值返现
                                var returnAmountdetailInfo = new VipAmountDetailEntity()
                                {
                                    Amount = rechargeOrderInfo.ReturnAmount.Value,
                                    ObjectId = rechargeOrderInfo.OrderID.ToString(),
                                    AmountSourceId = "6"
                                };
                                var vipReturnAmountDetailId = vipAmountBll.AddReturnAmount(vipInfo, unitInfo, vipAmountEntity, ref returnAmountdetailInfo, null, loggingSessionInfo);
                                if (!string.IsNullOrWhiteSpace(vipReturnAmountDetailId))
                                {//发送返现到账通知微信模板消息
                                    var CommonBLL = new CommonBLL();
                                    CommonBLL.CashBackMessage(inoutInfo.order_no, returnAmountdetailInfo.Amount, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);
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