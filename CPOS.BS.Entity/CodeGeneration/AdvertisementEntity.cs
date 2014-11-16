/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/11 13:28:17
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
    public partial class AdvertisementEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AdvertisementEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
        /// 主键ID
		/// </summary>
		public Guid? AdvertisementId { get; set; }

		/// <summary>
        /// 广告标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
        /// 广告内容
		/// </summary>
		public string Content { get; set; }

		/// <summary>
        /// 内容链接
		/// </summary>
		public string ContentUrl { get; set; }

		/// <summary>
        /// 图片链接
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
        /// 缩略图链接
		/// </summary>
		public string ThumbnailImageUrl { get; set; }

		/// <summary>
        /// 是否置顶
		/// </summary>
		public int? IsTop { get; set; }

		/// <summary>
        /// 作者
		/// </summary>
		public string Author { get; set; }

		/// <summary>
        /// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
        /// 创建人
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
        /// 最后修改人
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
        /// 最后修改时间
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
        /// 是否被删除
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
        /// 客户ID
		/// </summary>
		public string CustomerId { get; set; }


        #endregion

    }
}