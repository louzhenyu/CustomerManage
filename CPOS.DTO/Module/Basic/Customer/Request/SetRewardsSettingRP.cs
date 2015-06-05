using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Customer.Request
{
    public class SetRewardsSettingRP : IAPIRequestParameter
    {
        public void Validate()
        {

        }
        public int RewardsType { get; set; }
        public int EnableIntegral { get; set; }
        public int EnableRewardCash { get; set; }
        public double RewardPointsPer { get; set; }
        public double RewardCashPer { get; set; }
        public double PointsRedeemUpLimit { get; set; }
        public double CashRedeemUpLimit { get; set; }
        public int PointsRedeemLowestLimit { get; set; }
        public double CashRedeemLowestLimit { get; set; }
        public int PointsOrderUpLimit { get; set; }
        public double CashOrderUpLimit { get; set; }

    }
}
