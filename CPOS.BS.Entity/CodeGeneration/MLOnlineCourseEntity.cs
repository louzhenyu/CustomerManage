/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/6 16:48:56
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
    /// 实体： 课目表 
    /// </summary>
    public partial class MLOnlineCourseEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MLOnlineCourseEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String OnlineCourseId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Topic { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Introduction { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Icon { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CourseType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AverageStar { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AccessCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String Author { get; set; }


        #endregion

    }
}