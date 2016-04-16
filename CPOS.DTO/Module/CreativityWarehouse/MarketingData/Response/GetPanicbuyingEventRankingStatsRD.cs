using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GetPanicbuyingEventRankingStatsRD : IAPIResponseData
    {
        /// <summary>
        /// 订单金额5天排行
        /// </summary>
        public List<OrderMoneyRank> OrderMoneyRankList { get; set; }
        /// <summary>
        /// 订单数量5天排行
        /// </summary>
        public List<OrderCountRank> OrderCountRankList { get; set; }
    }
    /// <summary>
    /// 订单金额5天排行
    /// </summary>
    public class OrderMoneyRank
    {
        public string DateStr { get; set; }
        public decimal OrderActualAmount { get; set; }
    }
    /// <summary>
    /// 订单数量5天排行
    /// </summary>
    public class OrderCountRank
    {
        public string DateStr { get; set; }
        public int OrderCount { get; set; }
    }
}
