/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:37
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
    public partial class T_QN_QuestionnaireOptionCountEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_QN_QuestionnaireOptionCountEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? QuestionnaireOptionCountID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionnaireName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionnaireID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ActivityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ActivityName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OptionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OptionName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SelectedCount { get; set; }

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
		/// 0,否   1,是
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}