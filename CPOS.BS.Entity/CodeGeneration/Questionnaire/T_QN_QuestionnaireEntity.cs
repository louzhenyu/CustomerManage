/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/23 15:32:57
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
    public partial class T_QN_QuestionnaireEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_QN_QuestionnaireEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? QuestionnaireType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QRegular { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsShowQRegular { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ButtonName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BGImageSrc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StartPageBtnBGColor { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StartPageBtnTextColor { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QResultTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QResultBGImg { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QResultImg { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QResultBGColor { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QResultBtnTextColor { get; set; }

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
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ModelType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionnaireName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? QuestionnaireID { get; set; }


        #endregion

    }
}