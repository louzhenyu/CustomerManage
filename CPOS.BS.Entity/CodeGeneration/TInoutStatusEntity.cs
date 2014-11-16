/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 13:31:25
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
    /// ����״̬��Ϣ��¼  
    /// </summary>
    public partial class TInoutStatusEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TInoutStatusEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
        /// ID
		/// </summary>
		public Guid? InoutStatusID { get; set; }

		/// <summary>
        /// ����ID
		/// </summary>
		public string OrderID { get; set; }

		/// <summary>
        /// ����״̬
		/// </summary>
		public int? OrderStatus { get; set; }

		/// <summary>
        /// δ�������
		/// </summary>
		public int? CheckResult { get; set; }

		/// <summary>
        /// ֧����ʽ
		/// </summary>
		public int? PayMethod { get; set; }

		/// <summary>
        /// ���͹�˾
		/// </summary>
		public string DeliverCompanyID { get; set; }

		/// <summary>
        /// ���͵���
		/// </summary>
		public string DeliverOrder { get; set; }

		/// <summary>
        /// ͼƬ
		/// </summary>
		public string PicUrl { get; set; }

		/// <summary>
        /// ��ע
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
        /// �ͻ�ID
		/// </summary>
		public string CustomerID { get; set; }

		/// <summary>
        /// ������
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
        /// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
        /// ����޸���
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
        /// ����޸�ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
        /// �Ƿ�ɾ��
		/// </summary>
		public int? IsDelete { get; set; }

        /// <summary>
        /// ����״̬����
        /// </summary>
        public string StatusRemark { get; set; }

        #endregion

    }
}