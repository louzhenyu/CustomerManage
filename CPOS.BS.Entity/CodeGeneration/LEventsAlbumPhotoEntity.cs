/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-18 17:58
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
    public partial class LEventsAlbumPhotoEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LEventsAlbumPhotoEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 主标识
		/// </summary>
		public String PhotoId { get; set; }

		/// <summary>
		/// 相册标识
		/// </summary>
		public String AlbumId { get; set; }

		/// <summary>
		/// 链接地址
		/// </summary>
		public String LinkUrl { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 序号
		/// </summary>
		public Int32? SortOrder { get; set; }

		/// <summary>
		/// 阅读数量
		/// </summary>
		public Int32? ReaderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}