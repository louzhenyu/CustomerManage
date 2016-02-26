using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Reward.Request
{
    public class SetRewardOrderRP : IAPIRequestParameter
    {
        /// <summary>
        /// 打赏金额
        /// </summary>
        public decimal RewardAmount { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeID { get; set; }
        public void Validate()
        {
        }
    }
}
