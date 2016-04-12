using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response
{
    public class UnitSalesBoardRD : IAPIResponseData
    {
        /// <summary>
        /// 门店当日销售金额
        /// </summary>
        public Decimal? UnitCurrentDaySalesAmount { get; set; }

        /// <summary>
        /// 当月预定总销售额
        /// </summary>
        public Decimal? UnitCurrentMonthSalesAmountPlan { get; set; }

        /// <summary>
        /// 当月业绩达成率
        /// </summary>
        public Decimal? UnitCurrentMonthSalesAchievementRate { get; set; }

        /// <summary>
        /// 当月已完成销售额
        /// </summary>
        public Decimal? UnitCurrentMonthCompleteSalesAmount { get; set; }

        /// <summary>
        /// 当月未完成销售额
        /// </summary>
        public Decimal? UnitCurrentMonthNoCompleteSalesAmount { get; set; }

        /// <summary>
        /// 当月仅剩天数
        /// </summary>
        public Int32? UnitCurrentMonthDaysRemaining { get; set; }

        /// <summary>
        /// 当月剩余每天目标销售额
        /// </summary>
        public Decimal? UnitCurrentMonthDayTargetSalesAmount { get; set; }


        /// <summary>
        /// 门店七天销售额
        /// </summary>
        public List<UnitDaySalesAmount> Unit7DaysSalesAmountList { get; set; }
    }

    /// <summary>
    /// 门店日销售额
    /// </summary>
    public class UnitDaySalesAmount
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// 门店当日销售金额
        /// </summary>
        public decimal? SalesAmount { get; set; }
    }
}
