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
    /// 实体：  
    /// </summary>
    public partial class VipCardReRechargeProfitRuleEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardReRechargeProfitRuleEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? ReRechargeProfitRuleId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? CardBuyToProfitRuleId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// Step 阶梯   Superposition 叠加
		/// </summary>
		public String ProfitType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? LimitAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ProfitPct { get; set; }

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