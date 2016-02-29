using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.VIP.Reward.Response;
using JIT.CPOS.DTO.Module.VIP.Reward.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System.Data;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Reward
{
    public class SetRewardOrderAH : BaseActionHandler<SetRewardOrderRP, SetRewardOrderRD>
    {
        protected override SetRewardOrderRD ProcessRequest(DTO.Base.APIRequest<SetRewardOrderRP> pRequest)
        {
            var rd = new SetRewardOrderRD();
            string customerId = CurrentUserInfo.ClientID;
            var trrBll = new T_RewardRecordBLL(CurrentUserInfo);
            var userAmountBll = new VipAmountBLL(CurrentUserInfo);//作为员工余额使用
            var tptcmBll = new TPaymentTypeCustomerMappingBLL(CurrentUserInfo);
            
            var employeeId = pRequest.Parameters.EmployeeID;//员工ID
            var rewardAmount = pRequest.Parameters.RewardAmount;
            if (string.IsNullOrEmpty(employeeId))
            {
                throw new APIException("员工ID无效") { ErrorCode = 121 };
            }
            //门店
            var unitService = new UnitService(CurrentUserInfo);
            var unitInfo = unitService.GetUnitByUser(customerId, employeeId).FirstOrDefault();//获取员工所属门店

            //控制评论次数
            
            var tran = userAmountBll.GetTran();
            using (tran.Connection)//事务
            {
                try
                {
                    //生成打赏单
                    var trrEntity = new T_RewardRecordEntity()
                    {
                        RewardId = Guid.NewGuid(),
                        //RewardCode = "哪来的?",
                        RewardOPType = 1,//会员（打赏人）
                        RewardOP = pRequest.UserID,//会员ID
                        RewardedOPType = 2,//员工（被打赏人）
                        RewardedOP = employeeId,
                        RewardAmount = rewardAmount,
                        //Remark = string.Empty,
                        PayStatus = 0,//默认未支付，等待支付回调接口来修改
                        RewardType = 1, //1=现金，2=积分
                        CustomerId = customerId
                    };
                    trrBll.Create(trrEntity,tran);//创建打赏单

                    #region 员工余额变更
                    var userAmountEntity = userAmountBll.GetByID(employeeId);
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
                            CustomerID = customerId
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

                    var vipamountDetailBll = new VipAmountDetailBLL(CurrentUserInfo);
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
                        CustomerID = customerId
                    };
                    vipamountDetailBll.Create(vipAmountDetailEntity, tran);//创建余额详情
                    #endregion
                    tran.Commit();//提交事务
                    rd.RewardOrderID = "REWARD|" + trrEntity.RewardId;

                    var tptcmEntity = tptcmBll.QueryByEntity(new TPaymentTypeCustomerMappingEntity() { PaymentTypeID = "DFD3E26D5C784BBC86B075090617F44B", CustomerId = customerId }, null).FirstOrDefault();
                    rd.paymentId = tptcmEntity != null ? tptcmEntity.PaymentTypeID : string.Empty;

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "异常-->生成打赏单(SetRewardOrder)：" + ex
                    });
                }

            }
                

            return rd;
        }
    }
}