/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/15 19:16:25
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
    public partial class T_QN_QuestionnaireAnswerRecordEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_QN_QuestionnaireAnswerRecordEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? QuestionnaireAnswerRecordID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionnaireID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionnaireName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionID { get; set; }

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
		/// 1.单行输入   2.多行输入   3.单选   4.多选   5.下拉框   6.手机号   7.地址   8.日期   9.图片单选   10.图片多选
		/// </summary>
		public Int32? QuestionidType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerOptionId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerOption { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerProvince { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerCity { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerCounty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? QuestionScore { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }


        #endregion

    }
}