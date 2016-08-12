using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Response;

namespace JIT.CPOS.DTO.Module.Marketing.Activity.Request
{
    public class SetRechargeStrategyRP : IAPIRequestParameter
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public string ActivityID { get; set; }
        /// <summary>
        /// 充值策略集合
        /// </summary>
        public List<RechargeStrategyInfo> RechargeStrategyInfoList { get; set; }
        public void Validate() { }
    }
}
