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
            
            var tptcmBll = new TPaymentTypeCustomerMappingBLL(CurrentUserInfo);
            
            var employeeId = pRequest.Parameters.EmployeeID;//员工ID
            var rewardAmount = pRequest.Parameters.RewardAmount;
            if (string.IsNullOrEmpty(employeeId))
            {
                throw new APIException("员工ID无效") { ErrorCode = 121 };
            }

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
            trrBll.Create(trrEntity);//创建打赏单

            rd.RewardOrderID = "REWARD|" + trrEntity.RewardId;

            var tptcmEntity = tptcmBll.QueryByEntity(new TPaymentTypeCustomerMappingEntity() { PaymentTypeID = "DFD3E26D5C784BBC86B075090617F44B", CustomerId = customerId }, null).FirstOrDefault();
            rd.paymentId = tptcmEntity != null ? tptcmEntity.PaymentTypeID : string.Empty;

            return rd;
        }
    }
}