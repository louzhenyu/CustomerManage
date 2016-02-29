using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.HomePageStats.Response
{
    public class GetHomePageStatsRD : IAPIResponseData
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32? UnitCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UnitCurrentDayOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UnitLastDayOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UnitCurrentDayOrderAmountDToD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? UnitMangerCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UnitCurrentDayAvgOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UnitLastDayAvgOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UnitCurrentDayAvgOrderAmountDToD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? UnitUserCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UserCurrentDayAvgOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UserLastDayAvgOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? UserCurrentDayAvgOrderAmountDToD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? RetailTraderCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? CurrentDayRetailTraderOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? LastDayRetailTraderOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? CurrentDayRetailTraderOrderAmountDToD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? VipCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? NewVipCount { get; set; }

        public decimal? NewVipDToD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? EventsCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? EventJoinCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CurrentMonthSingleUnitAvgTranCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? CurrentMonthUnitAvgCustPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? CurrentMonthSingleUnitAvgTranAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? CurrentMonthTranAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? TranAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? VipTranAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? VipContributePect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? MonthArchivePect { get; set; }
        public int? PreAuditOrder { get; set; }
        public int? PreSendOrder { get; set; }
        public int? PreTakeOrder { get; set; }
        public int? PreRefund { get; set; }
        public int? PreReturnCash { get; set; }
        public List<PerformanceInfo> PerformanceTop { get; set; }
        public List<PerformanceInfo> PerformanceLower { get; set; }


    }
    public class PerformanceInfo
    {
        public string UnitName { get; set; }
        public decimal? OrderPeopleTranAmount { get; set; }
    }
}
