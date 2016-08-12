using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Response
{
    public class RechargeStrategyInfo
    {
        /// <summary>
        /// 充值策略ID
        /// </summary>
        public string RechargeStrategyId { get; set; }
        /// <summary>
        /// 策略类型：Step 阶梯，Superposition 叠加
        /// </summary>
        public string RuleType { get; set; }
        /// <summary>
        /// 满/每满充值金额
        /// </summary>
        public decimal RechargeAmount { get; set; }
        /// <summary>
        /// 送金额
        /// </summary>
        public decimal GiftAmount { get; set; }
        /// <summary>
        /// 是否启用（1：启用，0不启用）
        /// </summary>
        public int IsEnable { get; set; }
    }
}
