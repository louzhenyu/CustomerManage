/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 11:39:44
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
    public partial class T_CTW_LEventTemplateEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_CTW_LEventTemplateEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? TemplateId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TemplateName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TemplateDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityGroupId { get; set; }

		/// <summary>
		/// 10=���ϼ�   20=������   30=�ѷ���   40=���¼�
		/// </summary>
		public Int32? TemplateStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 0.����   1.��
		/// </summary>
		public Int32? IsLongTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

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