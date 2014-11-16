/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/19 15:40:35
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
    public partial class ZCourseEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ZCourseEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String CourseId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouseDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CourseTypeId { get; set; }

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
		public String CourseName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CourseSummary { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CourseFee { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CourseStartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouseCapital { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouseContact { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EmailTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ParentId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CourseLevel { get; set; }


        #endregion

    }
}