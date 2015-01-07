/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-12-28 11:40:41
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
    /// ʵ�壺  
    /// </summary>
    public partial class VipWithdrawDepositEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipWithdrawDepositEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? WithdrawDepositID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VIPID { get; set; }

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
		public Decimal? TransitAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAmount { get; set; }

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