/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/2 13:18:37
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
    public partial class WXHouseNewRateEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXHouseNewRateEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? RateID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MerchantDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FundNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FundName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BonusCurrDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BonusCuramt { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BonusCurratio { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BonusBefDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BonusBefamt { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BonusBefratio { get; set; }

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