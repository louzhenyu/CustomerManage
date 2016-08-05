/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:27
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
    public partial class SysVipCardTypeEntity : BaseEntity 
    {
        #region 属性集
        public Int32? RuleID { get; set; }

        /// <summary>
        /// 品牌标识
        /// </summary>
        public String BrandID { get; set; }

        /// <summary>
        /// 卡折扣
        /// </summary>
        public Decimal? CardDiscount { get; set; }

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
        /// 升级条件（1=购卡升级、0=充值升级）
        /// </summary>
        public int RefillCondition { get; set; }
        #endregion
    }
}