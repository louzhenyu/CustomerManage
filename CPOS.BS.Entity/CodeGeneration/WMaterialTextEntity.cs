/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/7 11:18:03
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
    public partial class WMaterialTextEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WMaterialTextEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String TextId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ParentTextId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Author { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CoverImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Text { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OriginalUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ApplicationId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TypeId { get; set; }

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
		public Guid? PageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PageUrlJson { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PageParamJson { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAuth { get; set; }

        public string Abstract { get; set; }
        #endregion

    }
}