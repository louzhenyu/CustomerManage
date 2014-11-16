/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:35
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
    public partial class QuesQuestionsEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public QuesQuestionsEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String QuestionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionnaireID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? QuestionType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MinSelected { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MaxSelected { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? QuestionValueCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsRequired { get; set; }

		/// <summary>
		/// 当前问题是否是否当前开放
		/// </summary>
		public Int32? IsOpen { get; set; }

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
		public Int32? IsSaveOutEvent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CookieName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndexNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionMedia { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MediaType { get; set; }


        #endregion

    }
}