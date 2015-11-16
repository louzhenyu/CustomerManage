/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/30 15:25:27
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
    public partial class VipAmountEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipAmountEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BeginAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? InAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OutAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? EndAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BeginReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? InReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OutReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ImminentInvalidRAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? InvalidReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ValidReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsLocking { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}