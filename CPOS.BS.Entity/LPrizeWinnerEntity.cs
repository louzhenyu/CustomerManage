/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:46
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
    public partial class LPrizeWinnerEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 中奖人名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string PrizeName { get; set; }
        /// <summary>
        /// 奖品描述
        /// </summary>
        public string PrizeDesc { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int ICount { get; set; }

        public IList<LPrizeWinnerEntity> PrizeWinnerList { get; set; }
        #endregion
    }
}