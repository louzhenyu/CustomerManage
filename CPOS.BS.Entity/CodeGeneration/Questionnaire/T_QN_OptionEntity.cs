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
    public partial class T_QN_OptionEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_QN_OptionEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? OptionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionID { get; set; }

		/// <summary>
		/// 1.单行输入   2.多行输入   3.单选   4.多选   5.下拉框   6.手机号   7.地址   8.日期   9.图片单选   10.图片多选
		/// </summary>
		public Int32? QuestionidType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OptionContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OptionPicSrc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? NoOptionScore { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? YesOptionScore { get; set; }

		/// <summary>
		/// 0,否   1,是
		/// </summary>
		public Int32? IsRightValue { get; set; }

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