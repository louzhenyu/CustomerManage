/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 15:41:40
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
    public partial class ActivityMediaEntity : BaseEntity ,IExtensionable
    {
        #region 属性集
        /// <summary>
        /// 活动标题，关联活动表
        /// </summary>
        public string ActivityTitle { get; set; }
        /// <summary>
        /// 文件名，关联附件表
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径，关联附件表
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 媒体类型文本
        /// </summary>
        public string MediaTypeText { get; set; }
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
        public IList<ActivityMediaEntity> EntityList { get; set; }
        #endregion
    }
}