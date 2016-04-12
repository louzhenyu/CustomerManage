/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 14:10:41
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
    public partial class Agg_UnitYearlyEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Agg_UnitYearlyEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StoreType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? VipSalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipOrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SetoffVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SetoffCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? NewVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OldVipBackCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ActiveVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? HighValueVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? NewPotentialVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? New3MonthNoBackVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? BirthdayVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? BirthdayNoBackVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UseCouponCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }


        #endregion

    }
}