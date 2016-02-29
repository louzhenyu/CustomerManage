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
        /// <summary>
        /// 奖励类型(0=按订单；1=按商品)
        /// </summary>
        public int? RewardsType { get; set; }
        /// <summary>
        /// 启用积分(0=不启用；1=启用)
        /// </summary>
        public int? EnableIntegral { get; set; }
        /// <summary>
        /// 启用返现(0=不启用；1=启用)
        /// </summary>
        public int? EnableRewardCash { get; set; }
        /// <summary>
        /// 返积分比例
        /// </summary>
        public double? RewardPointsPer { get; set; }
        /// <summary>
        ///  n积分抵扣一元
        /// </summary>
        public double? IntegralAmountPer { get; set; }
        /// <summary>
        /// 返现比例
        /// </summary>
        public double? RewardCashPer { get; set; }
        /// <summary>
        /// 积分使用上限比例
        /// </summary>
        public double? PointsRedeemUpLimit { get; set; }
        /// <summary>
        /// 返现使用上限比例
        /// </summary>
        public double? CashRedeemUpLimit { get; set; }
        /// <summary>
        /// 积分最低使用限制
        /// </summary>
        public int? PointsRedeemLowestLimit { get; set; }
        /// <summary>
        /// 返现最低使用限制
        /// </summary>
        public double? CashRedeemLowestLimit { get; set; }
        /// <summary>
        /// 每单赠送积分上限
        /// </summary>
        public int? PointsOrderUpLimit { get; set; }
        /// <summary>
        /// 每单返现上限
        /// </summary>
        public double? CashOrderUpLimit { get; set; }
        /// <summary>
        /// 积分有效期
        /// </summary>
        public double? PointsValidPeriod{get;set;}
        /// <summary>
        /// 返现有效期
        /// </summary>
        public double? CashValidPeriod { get; set; }
    }
}
