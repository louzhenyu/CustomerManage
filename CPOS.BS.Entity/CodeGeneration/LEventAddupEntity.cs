/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/22 14:59:47
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
    public partial class LEventAddupEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LEventAddupEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String AddupId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String YearMonth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AddupSalesTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? MonthSalesTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? MonthSalesMoM { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AddupEventTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MonthEventTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? MonthEventMoM { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AddupPutinTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? MonthPutinTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? MonthPutinMom { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}