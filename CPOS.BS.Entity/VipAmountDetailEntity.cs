/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 11:40:34
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
    public partial class VipAmountDetailEntity : BaseEntity
    {
        #region 属性集
        public string AmountSourceName { get; set; }
        public string ImageUrl { get; set; }
        /// <summary>
        /// 手工调整时，显示操作人
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// 对应Amount
        /// </summary>
        public decimal UpdateCount { get; set; }
        /// <summary>
        /// 对应CreateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 对应Remark
        /// </summary>
        public string UpdateReason { get; set; }
        #endregion
    }

    public partial class SumVipAmountDetailEntity
    {
        /// <summary>
        /// 当前余额
        /// </summary>
        public decimal? IncomeAmount { get; set; }
        /// <summary>
        /// 提现余额
        /// </summary>
        public decimal? ExpenditureAmount { get; set; }
        /// <summary>
        /// 总收入
        /// </summary>
        public int TotalAmount { get; set; }
    }
}