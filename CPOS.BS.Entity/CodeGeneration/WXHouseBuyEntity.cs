/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/13 18:00:57
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
    public partial class WXHouseBuyEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXHouseBuyEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? BuyingID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? MappingID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DeductPayment { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DeductPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ProfitPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RealPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? OrderDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PayDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BillNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? State { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

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