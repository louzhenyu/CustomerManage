/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/11 18:15:53
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
    public partial class EclubMicroEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EclubMicroEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? MicroID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MicroTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MicroNumberID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MicroTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContentUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PublishTime { get; set; }

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
		public String Source { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SourceUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Intro { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Sequence { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Clicks { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Goods { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Shares { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

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