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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipIntegralBLL
    {
        #region Jermyn20130916 处理终端消费，计算积分，发送消息
        public bool SetPushIntegral(string orderId,string msgUrl, out string strError)
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
                        vipIntegralEntity.OutIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].OutIntegral) + integralValue; //消费积分
                        vipIntegralEntity.EndIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].EndIntegral) + integralValue; //积分余额
                        //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                        vipIntegralEntity.ValidIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].ValidIntegral) + integralValue; // 当前有效积分
                        //vipIntegralBLL.Update(vipIntegralEntity, false);
                    }
                    #endregion

                    #region 更新VIP
                    VipEntity vipEntity = new VipEntity();
                    var vipEntityDataList = vipBLL.QueryByEntity(
                        new VipEntity() { VIPID = vipInfo.VIPID}, null);
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
            catch (Exception ex) {
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
        public void ProcessPoint(int sourceId, string customerId, string vipId, string objectId,System.Data.SqlClient.SqlTransaction tran=null, string fromVipId = null, decimal point = 0, string remark = null, string updateBy = null)
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
        /// 订单完成时，返积分，返现
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
            var totalReturnAmount = bll.GetTotalReturnAmountBySkuId(str,tran);

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
        /// 积分变更
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="points"></param>
        /// <param name="tran"></param>
        /// <param name="type"></param>
        /// <param name="objectId"></param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public bool AddIntegral(string vipId,int points, System.Data.SqlClient.SqlTransaction tran, string type, string objectId, LoggingSessionInfo loggingSessionInfo)
        {
            bool b = false;
            //更新个人账户的可使用余额 
            try
            {
                var vipBLL = new VipBLL(loggingSessionInfo);
                var vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
                var vipIntegralDetailBLL = new VipIntegralDetailBLL(loggingSessionInfo);
                var vipInfo = vipBLL.GetByID(vipId);
                var vipIntegralInfo = vipIntegralBLL.GetByID(vipId);
                //修改会员信息剩余积分
                if (vipInfo != null)
                {
                    vipInfo.Integration=vipInfo.Integration==null?0:(vipInfo.Integration.Value+points);
                    vipBLL.Update(vipInfo, tran);
                }
                //创建会员积分记录信息
                if (vipIntegralInfo == null)
                {
                    vipIntegralInfo = new VipIntegralEntity
                    {
                        VipID=vipId,
                        BeginIntegral=points,
                        InIntegral=points,
                        EndIntegral=points,
                        ValidIntegral=points
                    };
                    vipIntegralBLL.Create(vipIntegralInfo, tran);
                }
                else//修改会员积分记录信息
                {
                    vipIntegralInfo.EndIntegral = (vipIntegralInfo.EndIntegral == null ? 0 : vipIntegralInfo.EndIntegral.Value) + points;
                    vipIntegralInfo.ValidIntegral = (vipIntegralInfo.ValidIntegral == null ? 0 : vipIntegralInfo.ValidIntegral.Value) + points;
                    if (points > 0)
                        vipIntegralInfo.InIntegral = (vipIntegralInfo.InIntegral == null ? 0 : vipIntegralInfo.InIntegral.Value) + points;
                    else
                        vipIntegralInfo.OutIntegral = (vipIntegralInfo.OutIntegral == null ? 0 : vipIntegralInfo.OutIntegral.Value) + points;
                    vipIntegralBLL.Update(vipIntegralInfo, tran);
                }
                //增加记录
                VipIntegralDetailEntity detail = new VipIntegralDetailEntity() { };
                detail.VipIntegralDetailID = Guid.NewGuid().ToString();
                detail.VIPID = vipId;
                detail.ObjectId = objectId;
                detail.Integral = points;
                detail.IntegralSourceID = type;
                vipIntegralDetailBLL.Create(detail,tran);
            }
            catch (Exception ex)
            {
                throw new APIException(ex.ToString()) { ErrorCode = 121 };
            }
            finally
            {
                b = true;
            }

            return b;
        }
    }
}