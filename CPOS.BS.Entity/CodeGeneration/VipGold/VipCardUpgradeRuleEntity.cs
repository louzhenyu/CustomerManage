/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 15:10:56
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
    public partial class VipCardUpgradeRuleEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardUpgradeRuleEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? VipCardUpgradeRuleId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsFormVerify { get; set; }

		/// <summary>
		/// 1=��   0=��
		/// </summary>
		public Int32? IsPurchaseUpgrade { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsExchangeIntegral { get; set; }

		/// <summary>
		/// 1=��   0=��
		/// </summary>
		public Int32? IsRecharge { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnceRechargeAmount { get; set; }

		/// <summary>
		/// 1=��   0=��
		/// </summary>
		public Int32? IsBuyUpgrade { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BuyAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OnceBuyAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsPointUpgrade { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalPoint { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsMustDeductPoint { get; set; }

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