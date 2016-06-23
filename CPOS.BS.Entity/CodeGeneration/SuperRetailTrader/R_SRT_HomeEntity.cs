/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 14:12:48
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
    public partial class R_SRT_HomeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_SRT_HomeEntity()
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
		public Decimal? RTTotalSalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DayUserRTSalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DayVipRTSalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RTDay7SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RTLastDay7SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RTDay30SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RTLastDay30SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? RTTotalCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DayAddUserRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DayAddVipRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day7RTOrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDay7RTOrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day7RTOrderCountW2W { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30RTOrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDay30RTOrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day30RTOrderCountM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day7RTAC { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? LastDay7RTAC { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day7RTACW2W { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day30RTAC { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? LastDay30RTAC { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day30RTACM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day7ActiveRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDay7ActiveRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day7ActiveRTCountW2W { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30ActiveRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDay30ActiveRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day30ActiveRTCountM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day7RTShareCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDay7RTShareCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day7RTShareCountW2W { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30RTShareCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDay30RTShareCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day30RTShareCountM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day7AddRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDay7AddRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day7AddRTCountW2W { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30AddRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastDay30AddRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day30AddRTCountM2M { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }


        #endregion

    }
}