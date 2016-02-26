/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016-1-18 17:23:57
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
    public partial class T_RewardRecordEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_RewardRecordEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? RewardId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RewardCode { get; set; }

		/// <summary>
		/// 1.��Ա   2.Ա��
		/// </summary>
		public Int32? RewardOPType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RewardOP { get; set; }

		/// <summary>
		/// 1.��Ա   2.Ա��
		/// </summary>
		public Int32? RewardedOPType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RewardedOP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? RewardType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RewardAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 0.δ֧��   1.��֧��   2.֧��ʧ��
		/// </summary>
		public Int32? PayStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PayDatetime { get; set; }

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
		public String CustomerId { get; set; }


        #endregion

    }
}