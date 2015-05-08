/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/21 14::00
 * Description	:订单执行操作
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
using System.Web;
using JIT.CPOS.Common;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using System.Data;
using JIT.CPOS.BS.DataAccess.Base;
namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    public class ProcessActionAH : BaseActionHandler<ProcessActionRP, ProcessActionRD>
    {
        protected override ProcessActionRD ProcessRequest(APIRequest<ProcessActionRP> pRequest)
        {
            #region 错误码
            const int ERROR_ORDER_NOTEXISTS = 301;
            #endregion
            ProcessActionRD rd = new ProcessActionRD();
            T_InoutBLL _TInoutbll = new T_InoutBLL(this.CurrentUserInfo);  //订单表
            TInoutStatusBLL _TInoutStatusBLL = new TInoutStatusBLL(this.CurrentUserInfo);//日志表

            string OrderID = pRequest.Parameters.OrderID; //订单ID
            string ActionCode = pRequest.Parameters.ActionCode;//订单操作码(当前订单状态码作为操作码)
            string ActionParameter = pRequest.Parameters.ActionParameter;//订单操作参数，可为空

            var tran = _TInoutbll.GetTran();
            using (tran.Connection)//事物
            {
                try
                {
                    #region 1.根据订单ID，订单操作码更新订单表中订单状态和状态描述
                    var entity = _TInoutbll.GetByID(OrderID);       //根据标识获取新的实例
                    if (entity == null)
                    {
                        throw new APIException(string.Format("未找到OrderID：{0}订单", pRequest.Parameters.OrderID)) { ErrorCode = ERROR_ORDER_NOTEXISTS };
                    }

                    #region

                    if (entity.status == ActionCode)
                    {
                        return rd;
                    }

                    #endregion


                    string Updatebeforestatus = entity.status_desc; //更新之前的订单状态描述
                    entity.status = ActionCode;                     //输入的状态码
                    entity.Field7 = ActionCode;
                    entity.status_desc = GetStatusDesc(ActionCode);  //输入的状态码对应的状态描述
                    entity.Field10 = GetStatusDesc(ActionCode);     //Field10=status_desc状态描述
                    entity.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  //修改时间
                    entity.modify_user_id = CurrentUserInfo.UserID; //修改人
                    _TInoutbll.Update(entity, tran);                      //用事物更新订单表(T_Inout)
                    #endregion
                    #region 2.根据订单ID更新订单日志表中数据
                    var _TInoutStatusEntity = new TInoutStatusEntity()
                    {
                        InoutStatusID = Guid.NewGuid(),
                        OrderID = OrderID,                         //订单ID
                        OrderStatus = Convert.ToInt32(ActionCode),   //状态码
                        StatusRemark = "订单状态从" + Updatebeforestatus + "变为" + GetStatusDesc(ActionCode) + "[操作人:" + CurrentUserInfo.CurrentUser.User_Name + "]",               //状态更新描述
                        CustomerID = CurrentUserInfo.ClientID        //客户ID
                    };
                    _TInoutStatusBLL.Create(_TInoutStatusEntity, tran);  //用事物更新，向日志表(TInoutStatus)中插入一条数据
                    #endregion


                    #region 当状态为完成时，返现，返积分

                    if (ActionCode == "700")
                    {
                        OrderReturnMoneyAndIntegral(OrderID, pRequest.UserID, tran, entity.actual_amount.Value);
                    }

                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new APIException(ex.Message) { ErrorCode = ERROR_ORDER_NOTEXISTS };
                }
            }

            #region 佣金处理 add by Henry 2014-11-26
            //确认收货时，如果销售者(sales_user)不为空,则将商品佣金*购买的数量保存到余额表中
            var inoutService = new InoutService(this.CurrentUserInfo);
            var orderInfo = inoutService.GetInoutInfoById(OrderID);
            if (ActionCode == "700" && !string.IsNullOrEmpty(orderInfo.sales_user))
            {
                var skuPriceBll = new SkuPriceService(this.CurrentUserInfo);              //sku价格
                var vipAmountBll = new VipAmountBLL(this.CurrentUserInfo);                //账户余额 
                decimal totalAmount = 0;                                                  //订单总佣金
                List<OrderDetail> orderDetailList = skuPriceBll.GetSkuPrice(OrderID);
                if (orderDetailList.Count > 0)
                {
                    foreach (var detail in orderDetailList)
                    {
                        totalAmount += decimal.Parse(detail.salesPrice) * decimal.Parse(detail.qty);
                    }
                    if (totalAmount > 0)
                    {
                        IDbTransaction tran1 = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
                        using (tran.Connection)
                        {
                            try
                            {
                                vipAmountBll.AddVipEndAmount(orderInfo.sales_user, totalAmount, tran1, "10", OrderID, this.CurrentUserInfo);  //变更余额和余额记录
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                throw new APIException(ex.Message);
                            }
                        }
                    }
                }
            }
            #endregion

            return rd;
        }
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


        public void OrderReturnMoneyAndIntegral(string orderId, string userId, SqlTransaction tran, decimal actualAmount)
        {
            #region 获取奖励配置 add by Henry 2015-4-17

            var customerBasicSettingBll = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            //所有商户配置
            var settingList = customerBasicSettingBll.QueryByEntity(new CustomerBasicSettingEntity() { CustomerID = this.CurrentUserInfo.ClientID }, null);
            //奖励类型 0=按订单；1=按商品
            var rewardsType = settingList.Where(t => t.SettingCode == "RewardsType").FirstOrDefault().SettingValue;
            //积分启用配置
            var enableIntegral = settingList.Where(t => t.SettingCode == "EnableIntegral").FirstOrDefault().SettingValue;
            //返现启用配置
            var enableRewardCash = settingList.Where(t => t.SettingCode == "EnableRewardCash").FirstOrDefault().SettingValue;
            //返回积分比例
            var rewardPointsPer = settingList.Where(t => t.SettingCode == "RewardPointsPer").FirstOrDefault().SettingValue;
            //返现比例
            var rewardCashPer = settingList.Where(t => t.SettingCode == "RewardCashPer").FirstOrDefault().SettingValue;

            //3.获取积分与金额的兑换比例
            var vipBll = new VipBLL(this.CurrentUserInfo);
            var integralAmountPre = vipBll.GetIntegralAmountPre(this.CurrentUserInfo.ClientID);
            if (integralAmountPre == 0)
                integralAmountPre = (decimal)0.01;
            #endregion

            #region 返积分 update by Henry 2015-4-17
            if (enableIntegral == "1")
            {
                var vipIntegralBll = new VipIntegralBLL(this.CurrentUserInfo);
                var vipIntegralDetailBll = new VipIntegralDetailBLL(this.CurrentUserInfo);
                if (rewardsType == "1")//按商品
                {
                    const int sourceId = 21;//返现
                    vipIntegralBll.ProcessPoint(sourceId, CurrentUserInfo.ClientID, userId, orderId, tran, null,
                        0, null, userId);
                }
                else//按订单
                {
                    decimal points = actualAmount * (decimal.Parse(rewardPointsPer) / 100) / integralAmountPre;
                    var vipIntegral = vipIntegralBll.GetByID(userId);
                    if (vipIntegral == null)
                    {
                        VipIntegralEntity vipIntegralEntity = new VipIntegralEntity() { };
                        vipIntegralEntity.VipID = userId;
                        vipIntegralEntity.InIntegral = points;
                        vipIntegralEntity.EndIntegral = points;
                        vipIntegralEntity.ValidIntegral = points;
                        vipIntegralBll.Create(vipIntegralEntity);
                    }
                    else
                    {
                        vipIntegral.InIntegral = vipIntegral.InIntegral + points;
                        vipIntegral.EndIntegral = vipIntegral.EndIntegral + points;
                        vipIntegral.ValidIntegral = vipIntegral.ValidIntegral + points;
                        vipIntegralBll.Update(vipIntegral);
                    }
                    VipIntegralDetailEntity detail = new VipIntegralDetailEntity() { };
                    detail.VipIntegralDetailID=Guid.NewGuid().ToString();
                    detail.VIPID = userId;
                    detail.ObjectId = orderId;
                    detail.Integral = points;
                    detail.IntegralSourceID = "1";//消费奖励
                    detail.IsAdd = 1;
                    vipIntegralDetailBll.Create(detail);
                }
            }
            #endregion

            #region 返现

            if (enableRewardCash == "1")
            {
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
                decimal totalReturnAmount = 0;//返现总金额
                if (rewardsType == "1")//按商品
                {
                    totalReturnAmount = bll.GetTotalReturnAmountBySkuId(str, tran);
                }
                else//按订单
                {
                    totalReturnAmount = actualAmount * (decimal.Parse(rewardCashPer)/100);
                }
                if (totalReturnAmount > 0)
                {
                    //更新个人账户的可使用余额 

                    var vipAmountBll = new VipAmountBLL(CurrentUserInfo);

                    vipAmountBll.AddReturnAmount(userId, totalReturnAmount, orderId, "2", this.CurrentUserInfo);

                    //var vipAmountEntity = vipAmountBll.GetByID(userId);

                    //if (vipAmountEntity == null)
                    //{
                    //    //vipAmountEntity = new VipAmountEntity
                    //    //{
                    //    //    VipId = userId,
                    //    //    BeginAmount = totalReturnAmount,
                    //    //    InAmount = totalReturnAmount,
                    //    //    EndAmount = totalReturnAmount,
                    //    //    IsLocking = 0
                    //    //};
                    //    vipAmountEntity = new VipAmountEntity
                    //    {
                    //        VipId = userId,
                    //        ReturnAmount=totalReturnAmount,
                    //        IsLocking = 0
                    //    };
                    //    vipAmountBll.Create(vipAmountEntity, tran);
                    //}
                    //else
                    //{
                    //    vipAmountEntity.ReturnAmount = vipAmountEntity.ReturnAmount==null?0:vipAmountEntity.ReturnAmount.Value + totalReturnAmount;
                    //    //vipAmountEntity.EndAmount = vipAmountEntity.EndAmount + totalReturnAmount;
                    //    //vipAmountEntity.InAmount = vipAmountEntity.InAmount + totalReturnAmount;
                    //    vipAmountBll.Update(vipAmountEntity, tran);
                    //}


                    ////Insert VipAmountDetail

                    //var vipamountDetailBll = new VipAmountDetailBLL(CurrentUserInfo);

                    //var vipAmountDetailEntity = new VipAmountDetailEntity
                    //{
                    //    AmountSourceId = "2",
                    //    Amount = totalReturnAmount,
                    //    VipAmountDetailId = Guid.NewGuid(),
                    //    VipId = userId,
                    //    ObjectId = orderId
                    //};

                    //vipamountDetailBll.Create(vipAmountDetailEntity, tran);
                }

            }
            #endregion

        }
    }
}