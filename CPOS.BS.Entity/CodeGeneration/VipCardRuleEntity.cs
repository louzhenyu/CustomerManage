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
    /// 实体：  
    /// </summary>
    public partial class VipCardRuleEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardRuleEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? RuleID { get; set; }

		/// <summary>
		/// 卡类型标识
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// 品牌标识
		/// </summary>
		public String BrandID { get; set; }

		/// <summary>
		/// 卡折扣
		/// </summary>
		public Decimal? CardDiscount { get; set; }

		/// <summary>
		/// 消费n元赠送1积分
		/// </summary>
		public Decimal? PaidGivePoints { get; set; }

		/// <summary>
		/// 积分倍数
		/// </summary>
		public Int32? PointsMultiple { get; set; }

		/// <summary>
		/// 充值满n
		/// </summary>
		public Decimal? ChargeFull { get; set; }

		/// <summary>
		/// 充值送n
		/// </summary>
		public Decimal? ChargeGive { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReturnAmountPer { get; set; }

		/// <summary>
		/// 客户标识
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