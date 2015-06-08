/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-6-6 16:15:13
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
    public partial class X_QuestionEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public X_QuestionEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? QuestionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NameUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Option1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Option4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Option3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Option2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Option1ImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Option2ImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Option3ImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Option4ImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Answer { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsMultiple { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}