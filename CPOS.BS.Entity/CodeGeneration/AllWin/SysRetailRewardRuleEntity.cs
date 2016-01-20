/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/6/1 16:12:03
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
    /// 实体： 分为单向和双向奖励 
    /// </summary>
    public partial class SysRetailRewardRuleEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SysRetailRewardRuleEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String RetailRewardRuleID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CooperateType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RewardTypeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RewardTypeCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SellUserReward { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsTemplate { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public Decimal? RetailTraderReward { get; set; }
        public Decimal? ItemSalesPriceRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AmountOrPercent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

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
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public String RetailTraderID { get; set; }


        #endregion

    }
}