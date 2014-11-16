using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    public class LEventsAlbumViewEntity : LEventsAlbumEntity
    {
        #region 构造函数
        public LEventsAlbumViewEntity() { }
        #endregion

        #region 属性集

        /// <summary>
        /// 视频被赞数量
        /// </summary>
        public int? PraiseCount { get; set; }

        /// <summary>
        /// 视频浏览数量
        /// </summary>
        public int? BrowseCount { get; set; }

        /// <summary>
        /// 视频收藏数量
        /// </summary>
        public int? KeepCount { get; set; }

        /// <summary>
        /// 是否赞
        /// </summary>
        public int isPraise { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public int isKeep { get; set; }

        #endregion
    }
}
