/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/18 14:40:25
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
    public partial class R_VipGoldHomeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_VipGoldHomeEntity()
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
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OnlineOnlyFansCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OnlineVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OnlineFansCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OfflineVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OnlineVipCountFor30DayOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OnlineVipCountForLast30DayOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipCountFor30DayOrderM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipCountPerFor30DayOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipCountPerForLast30DayOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipCountPerFor30DayOrderM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipSalesFor30Day { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipSalesForLast30Day { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipSalesFor30DayM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? VipSalesFor30Day { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipSalesPerFor30Day { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? VipSalesForLast30Day { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipSalesPerForLast30Day { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnlineVipSalesPerFor30DayM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesFor30Day { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesForLast30Day { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }


        #endregion

    }
}