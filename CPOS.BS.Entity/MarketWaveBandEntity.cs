/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    public partial class MarketWaveBandEntity : BaseEntity 
    {
        #region 属性集
        //总数量
        public int ICount { get; set; }
        /// <summary>
        /// 波段集合
        /// </summary>
        public IList<MarketWaveBandEntity> MarketWaveBandInfoList { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        #endregion
    }
}