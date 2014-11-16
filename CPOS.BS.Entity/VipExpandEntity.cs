/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:29
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
    public partial class VipExpandEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// CarBarndName
        /// </summary>
        public string CarBarndName { get; set; }
        /// <summary>
        /// 车品牌
        /// </summary>
        public string CarBrandName { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string CarModelsName { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// 批量车集合
        /// </summary>
        public IList<VipExpandEntity> VipExpandInfoList { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 页面数量
        /// </summary>
        public int maxRowCount { get; set; }
        /// <summary>
        /// 开始行号
        /// </summary>
        public int startRowIndex { get; set; }

        #endregion
    }
}