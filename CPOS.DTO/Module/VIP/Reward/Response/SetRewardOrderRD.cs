using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Reward.Response
{
    public class SetRewardOrderRD : IAPIResponseData
    {
        /// <summary>
        /// 打赏单号（格式: REWARD|*********）
        /// </summary>
        public string RewardOrderID { get; set; }
        /// <summary>
        /// 付款方式ID
        /// </summary>
        public string paymentId { get; set; }        
    }
}
