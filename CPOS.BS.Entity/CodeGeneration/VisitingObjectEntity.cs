/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/18 16:28:29
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
    /// ʵ�壺 �Զ���ݷö��� 
    /// </summary>
    public partial class VisitingObjectEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingObjectEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? VisitingObjectID { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public string ObjectName { get; set; }

		/// <summary>
		/// ״̬(0-δ����,1-����)
		/// </summary>
		public int? Status { get; set; }

		/// <summary>
		/// �������(0-�ŵ�ݷö���,1-�ŵ��˶��� ����Options��)
		/// </summary>
		public int? ObjectGroup { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public int? Sequence { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ParentID { get; set; }

		/// <summary>
		/// �Ű淽ʽ
		/// </summary>
		public int? LayoutType { get; set; }

		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark { get; set; }

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