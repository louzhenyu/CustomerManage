using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Request
{
    public class SetVipWithDrawRuleRP : IAPIRequestParameter
    {
        /// <summary>
        /// 可提现天数
        /// </summary>
        public int BeforeWithDrawDays { get; set; }
        /// <summary>
        /// 最低提现条件
        /// </summary>
        public decimal MinAmountCondition { get; set; }
        /// <summary>
        /// 每次最多提现额度
        /// </summary>
        public decimal WithDrawMaxAmount { get; set; }
        /// <summary>
        /// 提现次数限制数量
        /// </summary>
        public int WithDrawNum { get; set; }
        public void Validate()
        {
            //throw new NotImplementedException();
        }
    }
}
