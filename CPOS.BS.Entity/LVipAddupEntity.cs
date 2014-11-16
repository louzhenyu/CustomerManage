/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/22 14:59:47
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
    public partial class LVipAddupEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 排序 
        /// </summary>
        public Int64 displayIndex { get; set; }
        /// <summary>
        /// 总数量 
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 集合 
        /// </summary>
        public IList<LVipAddupEntity> EntityList { get; set; }
        #endregion
    }
}