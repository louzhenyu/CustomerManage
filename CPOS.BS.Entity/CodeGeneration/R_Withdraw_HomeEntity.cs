/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/22 17:09:07
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
    public partial class R_Withdraw_HomeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_Withdraw_HomeEntity()
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
		/// 1：会员   2：员工   3：旧分销商   4：超级分销商
		/// </summary>
		public Int32? VipType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BalanceTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CanWithdrawTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PreAuditWithdrawTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PreAuditWithdrawNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CurrYearFinishWithdrawTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CurrYearFinishWithdrawNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }


        #endregion

    }
}