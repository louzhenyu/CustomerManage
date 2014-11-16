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
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// ʵ�壺 �ն�/�����̷��鶨�� 
    /// </summary>
    public partial class POPGroupEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public POPGroupEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public int? POPGroupID { get; set; }

		/// <summary>
		/// �ն����(0-�ŵ�,1-������)
		/// </summary>
		public int? POPType { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public string GroupNo { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public string GroupName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string GroupNameEn { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public string GroupCondition { get; set; }

		/// <summary>
		/// sql���
		/// </summary>
		public string SqlTemplate { get; set; }

		/// <summary>
		/// �Ƿ���Ҫ���������Զ����
		/// </summary>
		public int? IsAutoFill { get; set; }

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