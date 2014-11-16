/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/8 10:10:02
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
    public partial class WXHouseDetailEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXHouseDetailEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? DetailID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? HouseID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeductionMessg { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RealPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DeductionPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PurchCaseNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TotalHoseNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VirtualSaleNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OnlineStatus { get; set; }

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