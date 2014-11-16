/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
    /// ʵ�壺 �ݷò���ѡ��ֵ 
    /// </summary>
    public partial class VisitingParameterOptionsEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingParameterOptionsEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? VisitingParameterOptionsID { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public string OptionName { get; set; }

		/// <summary>
		/// ����ֵ
		/// </summary>
		public int? OptionValue { get; set; }

		/// <summary>
		/// �����ı���ʾֵ
		/// </summary>
		public string OptionText { get; set; }

		/// <summary>
		/// ��ʾֵ(Ӣ��)
		/// </summary>
		public string OptionTextEn { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public int? Sequence { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? ClientDistributorID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete { get; set; }


        #endregion

    }
}