/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/15 15:26:36
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
    public partial class TItemTagEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// TreeGrid商品分类必要属性(jifeng.cao)
        /// </summary>

        /// <summary>
        /// 节点是否展开
        /// </summary>
        public bool? expanded { get; set; }
        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public bool leaf { get; set; }
        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool? @checked { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int? IsFirstVisit { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<TItemTagEntity> children = new List<TItemTagEntity>();
        #endregion
    }
}