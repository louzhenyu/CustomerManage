/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 14:47:15
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
    public partial class VipCardProfitRuleEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardProfitRuleEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? CardBuyToProfitRuleId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// Employee  Ա��   Unit   �ŵ�   
		/// </summary>
		public String ProfitOwner { get; set; }

		/// <summary>
		/// �ٷֱ�
		/// </summary>
		public Decimal? FirstCardSalesProfitPct { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? FirstRechargeProfitPct { get; set; }

		/// <summary>
		/// 1= ��   0= ��
		/// </summary>
		public Int32? IsApplyAllUnits { get; set; }

		/// <summary>
		/// 1=��   0=��
		/// </summary>
		public Int32? IsConsumeRule { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CardSalesProfitPct { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RechargeProfitPct { get; set; }

		/// <summary>
		/// ֻӦ�����ŵ�
		/// </summary>
		public Decimal? UnitCostRebateProfitPct { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? RefId { get; set; }

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