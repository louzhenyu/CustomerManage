/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016-2-25 13:54:09
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class R_HomePageStatsEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_HomePageStatsEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StatsDateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CurrentDay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ViewUnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ViewUnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ViewUnitCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDayNewVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? NewVipDToD { get; set; }

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
		public Decimal? CurrentMonthSingleUnitAvgTranCount { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public Int32? PreAuditOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PreSendOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PreTakeOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PreRefund { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PreReturnCash { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? SuperRetailTraderCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? CurrentDaySuperRetailTraderOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? LastDaySuperRetailTraderOrderAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? CurrentDaySuperRetailTraderOrderAmountDToD { get; set; }


        #endregion

    }
}