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
    /// ʵ�壺  
    /// </summary>
    public partial class QuesOptionEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public QuesOptionEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String OptionsID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OptionsText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSelect { get; set; }

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
		/// ����
		/// </summary>
		public Int32? DisplayIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OptionIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAnswer { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OptionMedia { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MediaType { get; set; }


        #endregion

    }
}