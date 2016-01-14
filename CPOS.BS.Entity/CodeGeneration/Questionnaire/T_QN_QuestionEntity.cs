/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:36
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
    public partial class T_QN_QuestionEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_QN_QuestionEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? Questionid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// 1.单行输入   2.多行输入   3.单选   4.多选   5.下拉框   6.手机号   7.地址   8.日期   9.图片单选   10.图片多选
		/// </summary>
		public Int32? QuestionidType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DefaultValue { get; set; }

		/// <summary>
		/// 1,选中选项获得选项分数   2.答对几项的几分，答错不得分   3.全部答对才得分
		/// </summary>
		public Int32? ScoreStyle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MinScore { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MaxScore { get; set; }

		/// <summary>
		/// 1,必填   0，非必填
		/// </summary>
		public Int32? IsRequired { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsValidateMinChar { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MinChar { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsValidateMaxChar { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MaxChar { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsShowProvince { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsShowCity { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsShowCounty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsShowAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? NoRepeat { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsValidateStartDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsValidateEndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Isphone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Sort { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

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