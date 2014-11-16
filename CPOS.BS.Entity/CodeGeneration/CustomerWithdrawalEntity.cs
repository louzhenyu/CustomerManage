/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:21:46
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
    public partial class CustomerWithdrawalEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CustomerWithdrawalEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? WithdrawalId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? CustomerBackId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SerialNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? WithdrawalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WithdrawalBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? WithdrawalTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WithdrawalStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PlayMoneyTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FailureReason { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? PlotId { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}