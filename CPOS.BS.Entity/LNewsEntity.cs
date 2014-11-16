/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/22 15:46:07
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
    public partial class LNewsEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 排序 
        /// </summary>
        public Int64 displayIndex { get; set; }

        public Decimal DisplayindexOrder { get; set; }

        public Int64 DisplayIndexRow { get; set; }
        /// <summary>
        /// 总数量 
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 集合 
        /// </summary>
        public IList<LNewsEntity> EntityList { get; set; }
        /// <summary>
        /// 创建人 
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 点击数量
        /// </summary>
        public Int32? ClickCount { get; set; }
        /// <summary>
        /// 新闻类型
        /// </summary>
        public string NewsTypeName { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string StrPublishTime { get; set; }
        /// <summary>
        /// 回覆数量
        /// </summary>
        public int ReplyCount { get; set; }
        /// <summary>
        /// 标签字符窜
        /// </summary>
        public string StrTags { get; set; }
      
     

        /// <summary>
        /// 浏览数
        /// </summary>
        public int? BrowseNum { get; set; }
        /// <summary>
        /// 收藏数
        /// </summary>
        public int? BookmarkNum { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int? PraiseNum { get; set; }
        /// <summary>
        /// 分享数
        /// </summary>
        public int? ShareNum { get; set; }
        #endregion
    }
}