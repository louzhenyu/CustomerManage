/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/27 22:13:12
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
    public partial class VipCardRuleEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardRuleEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Int32? RuleID { get; set; }

		/// <summary>
		/// �����ͱ�ʶ
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// Ʒ�Ʊ�ʶ
		/// </summary>
		public String BrandID { get; set; }

		/// <summary>
		/// ���ۿ�
		/// </summary>
		public Decimal? CardDiscount { get; set; }

		/// <summary>
		/// ����nԪ����1����
		/// </summary>
		public Decimal? PaidGivePoints { get; set; }

		/// <summary>
		/// ���ֱ���
		/// </summary>
		public Int32? PointsMultiple { get; set; }

		/// <summary>
		/// ��ֵ��n
		/// </summary>
		public Decimal? ChargeFull { get; set; }

		/// <summary>
		/// ��ֵ��n
		/// </summary>
		public Decimal? ChargeGive { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReturnAmountPer { get; set; }

		/// <summary>
		/// �ͻ���ʶ
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
		public Decimal? PaidGivePercetPoints { get; set; }

        #endregion

    }
}