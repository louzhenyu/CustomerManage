/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
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
    public partial class VipCardEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String VipCardID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCardName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardStatusId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MembershipTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BeginDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BalanceAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardGradeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PurchaseTotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PurchaseTotalCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastSalesTime { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }


        #endregion

    }
}