/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 11:13:49
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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.CPOS.DTO.Base;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;
using System.Collections;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipIntegralBLL
    {
        #region Jermyn20130916 处理终端消费，计算积分，发送消息
        public bool SetPushIntegral(string orderId, string msgUrl, out string strError)
        {
            try
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPushIntegral--订单标识:{0}", orderId)
                });

                VipBLL vipBLL = new VipBLL(this.CurrentUserInfo);
                IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(this.CurrentUserInfo);
                VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(this.CurrentUserInfo);
                VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(this.CurrentUserInfo);
                InoutInfo orderInfo = new InoutInfo();
                InoutService orderService = new InoutService(this.CurrentUserInfo);
                orderInfo = orderService.GetInoutInfoById(orderId);

                if (orderInfo == null || orderInfo.order_id == null)
                {
                    strError = "订单不存在.";
                    return false;
                }

                if (orderInfo != null)
                {

                    string integralSourceId = "1";
                    int integralValue = 0;
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetPushIntegral --Order--订单集合:{0}", orderInfo.ToJSON())
                    });
                    if (orderInfo.vip_no == null || orderInfo.vip_no.Trim().Length == 0)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetPushIntegral--订单上的:{0}", "vip_no为空")
                        });
                        strError = "vip_no为空";
                        return false;
                    }

                    #region 查询会员ID
                    VipEntity vipInfo = null;
                    var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                    {
                        VIPID = orderInfo.vip_no
                    }, null);
                    if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetPushIntegral:{0}", "数据库不存在对应的vip记录")
                        });
                        strError = "数据库不存在对应的vip记录";
                        return false;
                    }
                    else
                    {
                        vipInfo = vipIdDataList[0];
                    }
                    #endregion

                    #region 计算积分
                    IntegralRuleEntity integralRuleData = null;
                    var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
                    {
                        IntegralSourceID = integralSourceId
                    }, null);
                    if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetVipIntegral:{0}", "未查询到积分规则")
                        });
                        strError = "未查询到积分规则";
                        return false;
                    }
                    else
                    {
                        integralRuleData = integralRuleDataList[0];
                        integralValue = CPOS.Common.Utils.GetParseInt(integralRuleData.Integral) *
                            CPOS.Common.Utils.GetParseInt(orderInfo.total_amount);
                    }
                    #endregion

                    #region 插入积分明细
                    VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                    vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                    vipIntegralDetailEntity.VIPID = vipInfo.VIPID;
                    vipIntegralDetailEntity.FromVipID = vipInfo.VIPID;
                    vipIntegralDetailEntity.SalesAmount = orderInfo.total_amount;
                    vipIntegralDetailEntity.Integral = integralValue;
                    vipIntegralDetailEntity.IntegralSourceID = integralSourceId;
                    vipIntegralDetailEntity.IsAdd = 1;
                    //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                    // 更新积分
                    VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                    var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                        new VipIntegralEntity() { VipID = vipInfo.VIPID }, null);
                    if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                    {
                        vipIntegralEntity.VipID = vipInfo.VIPID;
                        vipIntegralEntity.BeginIntegral = 0; // 期初积分
                        vipIntegralEntity.InIntegral = 0; // 增加积分
                        vipIntegralEntity.OutIntegral = integralValue; //消费积分
                        vipIntegralEntity.EndIntegral = integralValue; //积分余额
                        vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                        vipIntegralEntity.ValidIntegral = integralValue; // 当前有效积分
                        //vipIntegralBLL.Create(vipIntegralEntity);
                    }
                    else
                    {
                        vipIntegralEntity.VipID = vipInfo.VIPID;
                        //vipIntegralEntity.InIntegral = 0; // 增加积分
                        vipIntegralEntity.OutIntegral = vipIntegralDataList[0].OutIntegral + integralValue; //消费积分
                        vipIntegralEntity.EndIntegral = vipIntegralDataList[0].EndIntegral + integralValue; //积分余额
                        //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                        vipIntegralEntity.ValidIntegral = vipIntegralDataList[0].ValidIntegral + integralValue; // 当前有效积分
                        //vipIntegralBLL.Update(vipIntegralEntity, false);
                    }
                    #endregion

                    #region 更新VIP
                    VipEntity vipEntity = new VipEntity();
                    var vipEntityDataList = vipBLL.QueryByEntity(
                        new VipEntity() { VIPID = vipInfo.VIPID }, null);
                    if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                    {
                        vipEntity.VIPID = vipInfo.VIPID;
                        //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                        vipEntity.ClientID = this.CurrentUserInfo.CurrentUser.customer_id;
                        vipEntity.Status = 1;
                        vipBLL.Create(vipEntity);
                    }
                    else
                    {
                        vipEntity.VIPID = vipInfo.VIPID;
                        //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                        vipBLL.Update(vipEntity, false);
                    }
                    #endregion

                    #region 推送消息
                    if (vipInfo != null && vipInfo.WeiXinUserId != null && vipInfo.WeiXinUserId.Length > 15)
                    {
                        var strValidIntegral = string.Empty;
                        if (vipIntegralEntity.ValidIntegral == null)
                        {
                            strValidIntegral = "0";
                        }
                        else
                        {
                            decimal vd = (decimal)vipIntegralEntity.ValidIntegral;
                            strValidIntegral = Convert.ToString(decimal.Truncate(vd));
                        }

                        //string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                        string msgText = string.Format("感谢您来店消费。您刚刚消费共计{1}元，新增积分{0}，积分累计为{2}。欢迎您下次光临。",
                           Convert.ToString(integralValue), Convert.ToString(System.Math.Abs(double.Parse(orderInfo.total_amount.ToString()))), System.Math.Abs(CPOS.Common.Utils.GetParseInt(vipIntegralEntity.ValidIntegral)));
                        string msgData = "<xml><OpenID><![CDATA[" + vipInfo.WeiXinUserId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                        var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                        #region 发送日志
                        MarketSendLogBLL logServer = new MarketSendLogBLL(CurrentUserInfo);
                        MarketSendLogEntity logInfo = new MarketSendLogEntity();
                        logInfo.LogId = BaseService.NewGuidPub();
                        logInfo.IsSuccess = 1;
                        logInfo.MarketEventId = orderInfo.order_id;
                        logInfo.SendTypeId = "2";
                        logInfo.TemplateContent = msgData;
                        logInfo.VipId = vipInfo.VIPID;
                        logInfo.WeiXinUserId = vipInfo.WeiXinUserId;
                        logInfo.CreateTime = System.DateTime.Now;
                        logServer.Create(logInfo);
                        #endregion
                        //推送刮奖消息
                        msgText = "您赢得了一次在线抽奖机会，<a href='http://o2oapi.aladingyidong.com/wap/weixin/luckyDraw.html'>点击参与刮奖</a>";
                        msgData = "<xml><OpenID><![CDATA[" + vipInfo.WeiXinUserId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                        msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                        #region 发送日志
                        logInfo.LogId = BaseService.NewGuidPub();
                        logInfo.IsSuccess = 1;
                        logInfo.MarketEventId = orderInfo.order_id;
                        logInfo.SendTypeId = "2";
                        logInfo.TemplateContent = msgData;
                        logInfo.VipId = vipInfo.VIPID;
                        logInfo.WeiXinUserId = vipInfo.WeiXinUserId;
                        logInfo.CreateTime = System.DateTime.Now;
                        logServer.Create(logInfo);
                        #endregion

                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("PushMsgResult:{0}", msgResult)
                        });
                    }
                    #endregion
                    //GetHightOpenInfo(data); //Jermyn20130517给上家添加积分
                    //respData.Data = vipIntegralEntity.ValidIntegral.ToString();
                }
                strError = "成功.";
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPushIntegral--失败提示:{0}", ex.ToString())
                });
                return false;
            }
        }
        #endregion

        #region 积分处理    by Willie Yan
        /// <summary>
        /// 处理积分
        /// </summary>
        /// <param name="sourceId">积分来源：根据业务设置【必须】</param>
        /// <param name="customerId">客户标识【必须】</param>
        /// <param name="vipId">用户标识【必须】</param>
        /// <param name="objectId">对象标识【必须】</param>
        /// <param name="tran">是否批处理</param>
        /// <param name="fromVipId">来源会员</param>
        /// <param name="point">积分(如果指定积分,则使用此积分更新)</param>
        /// <param name="remark">描述</param>
        /// <param name="updateBy">积分操作人 【必须】</param>
        public void ProcessPoint(int sourceId, string customerId, string vipId, string objectId, System.Data.SqlClient.SqlTransaction tran = null, string fromVipId = null, decimal point = 0, string remark = null, string updateBy = null)
        {
            try
            {
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("soureId: {0}, customerId: {1}, vipId: {2}, objectId: {3}, point: {4}", sourceId, customerId, vipId, objectId, point) });
                string result = "0";
                result = this._currentDAO.ProcessPoint(sourceId, customerId, vipId, objectId, tran, fromVipId, point, remark, updateBy) ?? "0";

                if (result == "0")
                    Loggers.Debug(new DebugLogInfo() { Message = string.Format("积分处理失败： soureId: {0}, customerId: {1}, vipId: {2}, objectId: {3}, point: {4}", sourceId, customerId, vipId, objectId, point) });
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("ProcessPoint--失败提示:{0}", ex.Message)
                });
            }
        }
        #endregion

        /// <summary>
        /// 订单完成时，返积分，返现 Willie Yan
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId">vipId</param>
        /// <param name="tran"></param>
        public void OrderReturnMoneyAndIntegral(string orderId, string userId, SqlTransaction tran)
        {

            #region 返积分
            var vipIntegralBll = new VipIntegralBLL(this.CurrentUserInfo);

            const int sourceId = 21;//返现
            vipIntegralBll.ProcessPoint(sourceId, CurrentUserInfo.ClientID, userId, orderId, tran, null,
                0, null, userId);
            #endregion

            #region 返现
            //1.Get All Order.skuId and Order.Qty 

            var orderDetail = new TInoutDetailBLL(this.CurrentUserInfo);

            var orderDetailList = orderDetail.QueryByEntity(new TInoutDetailEntity()
            {
                order_id = orderId
            }, null);

            if (orderDetailList == null || orderDetailList.Length == 0)
            {
                throw new APIException("该订单没有商品") { ErrorCode = 121 };
            }
            var str = orderDetailList.Aggregate("", (i, j) =>
            {
                i += string.Format("{0},{1};", j.SkuID, Convert.ToInt32(j.OrderQty));
                return i;
            });

            var bll = new VipBLL(CurrentUserInfo);
            //返现总金额
            var totalReturnAmount = bll.GetTotalReturnAmountBySkuId(str, tran);

            if (totalReturnAmount > 0)
            {
                //更新个人账户的可使用余额 

                var vipAmountBll = new VipAmountBLL(CurrentUserInfo);

                var vipAmountEntity = vipAmountBll.GetByID(userId);

                if (vipAmountEntity == null)
                {
                    vipAmountEntity = new VipAmountEntity
                    {
                        VipId = userId,
                        BeginAmount = totalReturnAmount,
                        InAmount = totalReturnAmount,
                        EndAmount = totalReturnAmount,
                        IsLocking = 0
                    };

                    vipAmountBll.Create(vipAmountEntity, tran);


                    // throw new APIException("您尚未开通付款账户") { ErrorCode = 121 };
                }
                else
                {
                    vipAmountEntity.EndAmount = vipAmountEntity.EndAmount + totalReturnAmount;
                    vipAmountEntity.InAmount = vipAmountEntity.InAmount + totalReturnAmount;

                    vipAmountBll.Update(vipAmountEntity, tran);
                }


                //Insert VipAmountDetail

                var vipamountDetailBll = new VipAmountDetailBLL(CurrentUserInfo);

                var vipAmountDetailEntity = new VipAmountDetailEntity
                {
                    AmountSourceId = "2",
                    Amount = totalReturnAmount,
                    VipAmountDetailId = Guid.NewGuid(),
                    VipId = userId,
                    ObjectId = orderId
                };

                vipamountDetailBll.Create(vipAmountDetailEntity, tran);
            }


            #endregion

        }

        /// <summary>
        /// 确认收货时处理积分、返现、佣金
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="tran">事务</param>
        /// <param name="dataFromId">16=会员小店;17=员工小店;3=微商城下单</param>
        public void OrderReward(T_InoutEntity orderInfo, SqlTransaction tran)
        {
            var vipBll = new VipBLL(this.CurrentUserInfo);
            var unitBLL = new t_unitBLL(CurrentUserInfo);
            var basicSettingBll = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            var vipAmountBll = new VipAmountBLL(CurrentUserInfo);

            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardRuleBLL = new VipCardRuleBLL(CurrentUserInfo);

            var userBLL = new T_UserBLL(CurrentUserInfo);

            //获取会员信息
            var vipInfo = vipBll.GetByID(orderInfo.vip_no);
            //获取门店信息
            t_unitEntity unitInfo = null;
            if (!string.IsNullOrEmpty(orderInfo.sales_unit_id))
                unitInfo = unitBLL.GetByID(orderInfo.sales_unit_id);

            //获取社会化销售配置和积分返现配置
            Hashtable htSetting = basicSettingBll.GetSocialSetting();

            //获取积分与金额的兑换比例
            var integralAmountPre = vipBll.GetIntegralAmountPre(this.CurrentUserInfo.ClientID);
            if (integralAmountPre == 0)
                integralAmountPre = (decimal)0.01;

            decimal actualAmount = orderInfo.actual_amount ?? 0;    //实付金额
            decimal deliveryAmount = orderInfo.DeliveryAmount;      //运费

            actualAmount = actualAmount - deliveryAmount;           //实付金额-运费

            //账户余额和返现
            var vipAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();

            #region 获取卡规则

            VipCardRuleEntity vipCardRuleInfo = null;
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipInfo.VIPID }, null).FirstOrDefault();
            if (vipCardMappingInfo != null)
            {
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID }, null).FirstOrDefault();
                if (vipCardInfo != null)
                {
                    vipCardRuleInfo = vipCardRuleBLL.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                }
            }
            #endregion

            #region 返积分 update by Henry 2015-4-17

            if (int.Parse(htSetting["enableIntegral"].ToString()) == 1)
            {
                var vipIntegralBll = new VipIntegralBLL(this.CurrentUserInfo);
                var vipIntegralDetailBll = new VipIntegralDetailBLL(this.CurrentUserInfo);
                if (int.Parse(htSetting["rewardsType"].ToString()) == 1)//按商品[暂不支持]
                {
                    //const int sourceId = 21;//返现
                    //vipIntegralBll.ProcessPoint(sourceId, CurrentUserInfo.ClientID, userId, orderId, tran, null,0, null, userId);
                }
                else//按订单
                {
                    if (vipCardRuleInfo != null)
                    {
                        decimal paidGivePoints = vipCardRuleInfo.PaidGivePoints != null ? vipCardRuleInfo.PaidGivePoints.Value : 0;
                        if (paidGivePoints > 0)
                        {
                            //int points = (int)Math.Round(actualAmount * (decimal.Parse(htSetting["rewardPointsPer"].ToString()) / 100) / integralAmountPre, 1);
                            int points = (int)Math.Round(actualAmount / paidGivePoints, 1);
                            int pointsOrderUpLimit = int.Parse(htSetting["pointsOrderUpLimit"].ToString());
                            if (pointsOrderUpLimit > 0)
                                points = points > pointsOrderUpLimit ? pointsOrderUpLimit : points; //处理每单赠送积分上限
                            if (points > 0)
                            {
                                //积分变更
                                var IntegralDetail = new VipIntegralDetailEntity()
                                {
                                    Integral = points,
                                    IntegralSourceID = "1",
                                    ObjectId = orderInfo.order_id
                                };
                                //变动前积分
                                string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                                //变动积分
                                string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                                String vipIntegralDetailId = this.AddIntegral(ref vipInfo, unitInfo,IntegralDetail, tran, this.CurrentUserInfo);
                                //发送微信积分变动通知模板消息
                                if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                                {
                                    var CommonBLL = new CommonBLL();
                                    CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, this.CurrentUserInfo);
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region 返现

            if (int.Parse(htSetting["enableRewardCash"].ToString()) == 1)
            {
                if (vipCardRuleInfo != null)
                {
                    decimal returnAmountPer = vipCardRuleInfo.ReturnAmountPer != null ? vipCardRuleInfo.ReturnAmountPer.Value : 0;
                    if (returnAmountPer > 0)
                    {
                        var orderDetail = new TInoutDetailBLL(this.CurrentUserInfo);
                        var orderDetailList = orderDetail.QueryByEntity(new TInoutDetailEntity() { order_id = orderInfo.order_id }, null);

                        if (orderDetailList == null || orderDetailList.Length == 0)
                        {
                            throw new APIException("该订单没有商品") { ErrorCode = 121 };
                        }
                        var str = orderDetailList.Aggregate("", (i, j) =>
                        {
                            i += string.Format("{0},{1};", j.SkuID, Convert.ToInt32(j.OrderQty));
                            return i;
                        });

                        decimal totalReturnAmount = 0;//返现总金额

                        if (int.Parse(htSetting["rewardsType"].ToString()) == 1)//按商品[暂不支持]
                            totalReturnAmount = vipBll.GetTotalReturnAmountBySkuId(str, tran);
                        else//按订单
                            totalReturnAmount = actualAmount * (returnAmountPer / 100);
                        //totalReturnAmount = actualAmount * (decimal.Parse(htSetting["rewardCashPer"].ToString()) / 100);

                        decimal cashOrderUpLimit = int.Parse(htSetting["cashOrderUpLimit"].ToString());
                        if (cashOrderUpLimit > 0)
                            totalReturnAmount = totalReturnAmount > cashOrderUpLimit ? cashOrderUpLimit : totalReturnAmount; //处理每单返现上线

                        if (totalReturnAmount > 0)
                        {
                            //更新个人账户的可使用余额 
                            var detailInfo = new VipAmountDetailEntity()
                            {
                                Amount = totalReturnAmount,
                                ObjectId = orderInfo.order_id,
                                AmountSourceId = "2"
                            };
                            var vipAmountDetailId= vipAmountBll.AddReturnAmount(vipInfo, unitInfo, vipAmountEntity,ref detailInfo, tran, CurrentUserInfo);
                            if (!string.IsNullOrWhiteSpace(vipAmountDetailId))
                            {//发送返现到账通知微信模板消息
                                var CommonBLL = new CommonBLL();
                                CommonBLL.CashBackMessage(orderInfo.order_no, detailInfo.Amount, vipInfo.WeiXinUserId, vipInfo.VIPID, CurrentUserInfo);

                            }
                        }
                    }
                }
            }
            #endregion

            #region 佣金处理 add by Henry 2015-6-10

            decimal totalAmount = 0; //订单总佣金
            if (int.Parse(htSetting["socialSalesType"].ToString()) == 1)     //按商品设置计算
            {
                //确认收货时，如果销售者(sales_user)不为空,则将商品佣金*购买的数量保存到余额表中
                if (!string.IsNullOrEmpty(orderInfo.sales_user))
                {
                    var skuPriceBll = new SkuPriceService(this.CurrentUserInfo);              //sku价格
                    var inoutService = new InoutService(this.CurrentUserInfo);
                    List<OrderDetail> orderDetailList = skuPriceBll.GetSkuPrice(orderInfo.order_id);
                    if (orderDetailList.Count > 0)
                    {
                        foreach (var detail in orderDetailList)
                        {
                            totalAmount += decimal.Parse(detail.salesPrice) * decimal.Parse(detail.qty);
                        }
                    }
                }
            }
            else//按订单金额
            {
                if (orderInfo.data_from_id == "16")     //会员小店
                {
                    if (int.Parse(htSetting["enableVipSales"].ToString()) > 0)//启用会员小店
                        totalAmount += actualAmount * (decimal.Parse(htSetting["vOrderCommissionPer"].ToString()) / 100);

                    if (totalAmount > 0)
                    {
                        var detailInfo = new VipAmountDetailEntity()
                        {
                            Amount = totalAmount,
                            AmountSourceId = "10",
                            ObjectId = orderInfo.order_id
                        };

                        var vipSalesVipInfo = vipBll.GetByID(orderInfo.sales_user);
                        //账户余额和返现
                        var vipSalesAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipSalesVipInfo.VIPID, VipCardCode = vipSalesVipInfo.VipCode }, null).FirstOrDefault();
                        var vipAmountDetailId = vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipSalesAmountEntity, detailInfo, tran, this.CurrentUserInfo);
                        if (!string.IsNullOrWhiteSpace(vipAmountDetailId) && orderInfo.data_from_id == "16")
                        {//发送微信账户余额变动模板消息

                            var CommonBLL = new CommonBLL();
                            CommonBLL.BalanceChangedMessage(orderInfo.order_no, vipSalesAmountEntity, detailInfo, vipSalesVipInfo.WeiXinUserId, orderInfo.vip_no, this.CurrentUserInfo);
                        }
                    }

                }
                else if (orderInfo.data_from_id == "17") //员工小店
                {
                    if (int.Parse(htSetting["enableEmployeeSales"].ToString()) > 0)//启用员工小店
                        totalAmount += actualAmount * (decimal.Parse(htSetting["eOrderCommissionPer"].ToString()) / 100);

                    if (totalAmount > 0)
                    {
                        var detailInfo = new VipAmountDetailEntity()
                        {
                            Amount = totalAmount,
                            AmountSourceId = "10",
                            ObjectId = orderInfo.order_id
                        };

                        var employeeSalesUserInfo= userBLL.GetByID(orderInfo.sales_user);
                        vipInfo.VIPID = employeeSalesUserInfo.user_id;
                        vipInfo.VipCode = employeeSalesUserInfo.user_code;
                        //账户余额和返现
                        var vipSalesAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = employeeSalesUserInfo.user_id }, null).FirstOrDefault();
                        vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipSalesAmountEntity, detailInfo, tran, this.CurrentUserInfo);
                    }

                }
            }
            #endregion
        }
        /// <summary>
        /// 退换货-确认收货时退回订单奖励积分、返现和佣金
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="tran"></param>
        public void CancelReward(T_InoutEntity orderInfo, VipEntity vipInfo, SqlTransaction tran)
        {
            //取消奖励积分
            var vipIntegralBll = new VipIntegralBLL(this.CurrentUserInfo);
            var vipIntegralDetailBll = new VipIntegralDetailBLL(this.CurrentUserInfo);

            var integralDetailInfo = vipIntegralDetailBll.QueryByEntity(new VipIntegralDetailEntity() { VIPID = orderInfo.vip_no, ObjectId = orderInfo.order_id, IntegralSourceID = "1" }, null).FirstOrDefault();
            if (integralDetailInfo != null)
            {
                var vipIntegralInfo = vipIntegralBll.QueryByEntity(new VipIntegralEntity() { VipID = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();

                vipIntegralInfo.InIntegral -= integralDetailInfo.Integral;
                vipIntegralInfo.EndIntegral -= integralDetailInfo.Integral;
                vipIntegralInfo.ValidIntegral -= integralDetailInfo.Integral;
                vipIntegralInfo.CumulativeIntegral -= integralDetailInfo.Integral;
                vipIntegralBll.Update(vipIntegralInfo, tran);

                vipIntegralDetailBll.Delete(integralDetailInfo, tran);
            }
            //取消奖励返现
            var vipAmountBll = new VipAmountBLL(this.CurrentUserInfo);
            var vipAmountDetailBll = new VipAmountDetailBLL(this.CurrentUserInfo);

            var vipAmountDetailInfo = vipAmountDetailBll.QueryByEntity(new VipAmountDetailEntity() { VipId = orderInfo.vip_no, ObjectId = orderInfo.order_id, AmountSourceId = "2" }, null).FirstOrDefault();
            if (vipAmountDetailInfo != null)
            {
                var vipAmountInfo = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();

                vipAmountInfo.InReturnAmount -= vipAmountDetailInfo.Amount;
                vipAmountInfo.ReturnAmount -= vipAmountDetailInfo.Amount;
                vipAmountInfo.ValidReturnAmount -= vipAmountDetailInfo.Amount;
                vipAmountInfo.TotalReturnAmount -= vipAmountDetailInfo.Amount;
                vipAmountBll.Update(vipAmountInfo, tran);

                vipAmountDetailBll.Delete(vipAmountDetailInfo, tran);
            }
            //取消奖励佣金
        }
        /// <summary>
        /// 积分变更
        /// </summary>
        /// <param name="vipInfo">会员信息</param>
        /// <param name="unitInfo">门店信息</param>
        /// <param name="detailInfo">变更明细信息</param>
        /// <param name="tran">事物</param>
        /// <param name="loggingSessionInfo">登录信息</param>
        /// <returns></returns>
        public string AddIntegral(ref VipEntity vipInfo, t_unitEntity unitInfo,VipIntegralDetailEntity detailInfo, SqlTransaction tran, LoggingSessionInfo loggingSessionInfo)
        {
            string vipIntegralDetailId = string.Empty;//变更明细ID
            //更新个人账户的可使用余额 
            try
            {
                var vipBLL = new VipBLL(loggingSessionInfo);
                var vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
                var vipIntegralDetailBLL = new VipIntegralDetailBLL(loggingSessionInfo);
                var customerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);

                //获取积分有效期
                int pointsValidPeriod = 2;  //默认为1，业务处理时会减去1
                var pointsValidPeriodInfo = customerBasicSettingBLL.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "PointsValidPeriod", CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                if (pointsValidPeriodInfo != null)
                    pointsValidPeriod = int.Parse(pointsValidPeriodInfo.SettingValue);

                //获取会员积分主表信息
                //var vipIntegralInfo = vipIntegralBLL.GetByID(vipId);
                var vipIntegralInfo = vipIntegralBLL.QueryByEntity(new VipIntegralEntity() { VipID = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                //修改会员信息剩余积分
                if (vipInfo != null)
                {
                    vipInfo.Integration = (vipInfo.Integration == null ? 0 : vipInfo.Integration.Value) + detailInfo.Integral;
                    vipBLL.Update(vipInfo, tran);
                }
                //创建会员积分记录信息
                if (vipIntegralInfo == null)
                {
                    vipIntegralInfo = new VipIntegralEntity
                    {
                        VipID = vipInfo.VIPID,
                        VipCardCode = vipInfo.VipCode,
                        BeginIntegral = 0,
                        InIntegral = detailInfo.Integral,
                        OutIntegral = 0,
                        EndIntegral = detailInfo.Integral,
                        ImminentInvalidIntegral = 0,
                        InvalidIntegral = 0,
                        ValidIntegral = detailInfo.Integral,
                        CumulativeIntegral = detailInfo.Integral,
                        CustomerID = loggingSessionInfo.ClientID
                    };
                    vipIntegralBLL.Create(vipIntegralInfo, tran);
                }
                else//修改会员积分记录信息
                {
                    if (detailInfo.Integral > 0)
                    {
                        vipIntegralInfo.InIntegral = (vipIntegralInfo.InIntegral == null ? 0 : vipIntegralInfo.InIntegral.Value) + detailInfo.Integral;
                        vipIntegralInfo.CumulativeIntegral = (vipIntegralInfo.CumulativeIntegral == null ? 0 : vipIntegralInfo.CumulativeIntegral.Value) + detailInfo.Integral;
                    }
                    else
                        vipIntegralInfo.OutIntegral = (vipIntegralInfo.OutIntegral == null ? 0 : vipIntegralInfo.OutIntegral.Value) + Math.Abs(detailInfo.Integral.Value);
                    vipIntegralInfo.EndIntegral = (vipIntegralInfo.EndIntegral == null ? 0 : vipIntegralInfo.EndIntegral.Value) + detailInfo.Integral;
                    vipIntegralInfo.ValidIntegral = (vipIntegralInfo.ValidIntegral == null ? 0 : vipIntegralInfo.ValidIntegral.Value) + detailInfo.Integral;

                    vipIntegralBLL.Update(vipIntegralInfo, tran);
                }
                //增加记录
                VipIntegralDetailEntity detail = new VipIntegralDetailEntity() { };
                detail.VipIntegralDetailID = Guid.NewGuid().ToString();
                detail.VIPID = vipInfo.VIPID;
                detail.VipCardCode = vipInfo.VipCode;
                detail.UnitID = unitInfo != null ? unitInfo.unit_id : "";
                detail.UnitName = unitInfo != null ? unitInfo.unit_name : "";
                detail.ObjectId = detailInfo.ObjectId;
                detail.Integral = detailInfo.Integral;
                detail.UsedIntegral = 0;
                detail.IntegralSourceID = detailInfo.IntegralSourceID;
                detail.Reason = detailInfo.Reason;
                detail.Remark = detailInfo.Remark;
                detail.EffectiveDate = DateTime.Now;
                detail.DeadlineDate = Convert.ToDateTime((DateTime.Now.Year + pointsValidPeriod - 1) + "-12-31 23:59:59 ");//失效时间
                detail.CustomerID = loggingSessionInfo.ClientID;
                vipIntegralDetailBLL.Create(detail, tran);

                vipIntegralDetailId = detail.VipIntegralDetailID;
            }
            catch (Exception ex)
            {
                throw new APIException(ex.ToString()) { ErrorCode = 121 };
            }
            return vipIntegralDetailId;
        }
    }
}