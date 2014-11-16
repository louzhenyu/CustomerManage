/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/22 18:54:19
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
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LNewsEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String NewsId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NewsType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NewsTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NewsSubTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Intro { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PublishTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContentUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ThumbnailImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String APPId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? NewsLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ParentNewsId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDefault { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsTop { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Author { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? BrowseCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PraiseCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CollCount { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }


        #endregion

    }
}