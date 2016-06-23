/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 14:12:49
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
    public partial class R_SRT_RTProductHomeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_SRT_RTProductHomeEntity()
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
		public Int32? Day30SharedRTProductCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30NoSharedRTProductCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30SalesRTProductCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30ShareSalesRTProductCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30F2FSalesRTProductCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Day7RTProductCRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? LastDay7RTProductCRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Last2Day7RTProductCRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Last3Day7RTProductCRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }


        #endregion

    }
}