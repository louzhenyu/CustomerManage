using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 市场活动分析
    /// </summary>
    public class MarketEventAnalysisEntity
    {
        /// <summary>
        /// 活动主标识
        /// </summary>
        public string MarketEventID { get; set; }

        /// <summary>
        /// 执行活动开始时间
        /// </summary>
        public string BeginDate { get; set; }
        /// <summary>
        /// //执行活动结束时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 参与门店
        /// </summary>
        public int? StoreCount { get; set; }
        /// <summary>
        /// 响应门店
        /// </summary>
        public int? ResponseStoreCount { get; set; }
        /// <summary>
        /// 门店响应率
        /// </summary>
        public string ResponseStoreRate { get; set; }
        /// <summary>
        /// 邀约人数
        /// </summary>
        public int? PersonCount { get; set; }
        /// <summary>
        /// 响应人数
        /// </summary>
        public int? ResponsePersonCount { get; set; }
        /// <summary>
        /// 会员响应率
        /// </summary>
        public string ResponsePersonRate { get; set; }
        /// <summary>
        /// 预算总费用
        /// </summary>
        public decimal? BudgetTotal { get; set; }
        /// <summary>
        /// 当前消费额
        /// </summary>
        public decimal? CurrentSales { get; set; }
        /// <summary>
        /// 活动毛利
        /// </summary>
        public decimal? EventMaori { get; set; }
        /// <summary>
        /// 活动净利润
        /// </summary>
        public decimal? EventNetProfit { get; set; }
    }
}
