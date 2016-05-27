using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Basic.Customer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Customer
{
    public class SetRewardsSettingAH : BaseActionHandler<SetRewardsSettingRP, EmptyResponseData>
    {

        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetRewardsSettingRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);
            ResponseData res = new ResponseData();
            List<CustomerBasicSettingEntity> list = new List<CustomerBasicSettingEntity>();

            if(!string.IsNullOrEmpty(para.RewardsType.ToString()))
            { 
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "RewardsType",
                    SettingValue = para.RewardsType.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.EnableIntegral.ToString()))
            { 
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "EnableIntegral",
                    SettingValue = para.EnableIntegral.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.EnableRewardCash.ToString()))
            { 
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "EnableRewardCash",
                    SettingValue = para.EnableRewardCash.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.RewardPointsPer.ToString()))
            { 
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "RewardPointsPer",
                    SettingValue = para.RewardPointsPer.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.IntegralAmountPer.ToString()))
            { 
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "IntegralAmountPer",
                    SettingValue = para.IntegralAmountPer.ToString()
                });
            }
            
            if (!string.IsNullOrEmpty(para.RewardCashPer.ToString()))
            { 
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "RewardCashPer",
                    SettingValue = para.RewardCashPer.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.PointsRedeemUpLimit.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "PointsRedeemUpLimit",
                    SettingValue = para.PointsRedeemUpLimit.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.CashRedeemUpLimit.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "CashRedeemUpLimit",
                    SettingValue = para.CashRedeemUpLimit.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.PointsRedeemLowestLimit.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "PointsRedeemLowestLimit",
                    SettingValue = para.PointsRedeemLowestLimit.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.CashRedeemLowestLimit.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "CashRedeemLowestLimit",
                    SettingValue = para.CashRedeemLowestLimit.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.PointsOrderUpLimit.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "PointsOrderUpLimit",
                    SettingValue = para.PointsOrderUpLimit.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.CashOrderUpLimit.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "CashOrderUpLimit",
                    SettingValue = para.CashOrderUpLimit.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.PointsValidPeriod.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "PointsValidPeriod",
                    SettingValue = para.PointsValidPeriod.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.CashValidPeriod.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "CashValidPeriod",
                    SettingValue = para.CashValidPeriod.ToString()
                });
            }
            int i = customerBasicSettingBLL.SaveCustomerBasicInfo(loggingSessionInfo.ClientID,list);

            return rd;
        }
    }
}