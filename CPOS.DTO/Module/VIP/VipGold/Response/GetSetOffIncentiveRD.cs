using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetSetOffIncentiveRD : IAPIResponseData
    {
        /// <summary>
        /// 激励类型(1=现金，2=积分)
        /// </summary>
        public int SetoffRegAwardType { get; set; }
        /// <summary>
        /// 激励金额或激励积分
        /// </summary>
        public string SetoffRegPrize { get; set; }
        /// <summary>
        /// 集客销售成功提成比例
        /// </summary>
        public decimal? SetoffOrderPer { get; set; }
    }
}
