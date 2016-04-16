using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GetPanicbuyingEventStatsRD : IAPIResponseData
    {
        public PanicbuyingEventStatsInfo PanicbuyingEventStatsInfo { get; set; }
    }

    public class PanicbuyingEventStatsInfo
    {
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal OrderActualAmount { get; set; }
        /// <summary>
        /// 订单数
        /// </summary>
        public int OrderCount { get; set; }
        /// <summary>
        /// 客单价
        /// </summary>
        public decimal CustSinglePrice { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FocusVipCount { get; set; }
        /// <summary>
        /// 会员数
        /// </summary>
        public int RegVipCount { get; set; }
    }
}
