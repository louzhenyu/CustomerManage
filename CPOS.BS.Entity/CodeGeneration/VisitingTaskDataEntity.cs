/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:49
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
    /// ʵ�壺 �ݷ����ݱ� 
    /// </summary>
    public partial class VisitingTaskDataEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskDataEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? VisitingTaskDataID { get; set; }

		/// <summary>
		/// ��Ա���(����ClientUser��)
		/// </summary>
		public int? ClientUserID { get; set; }

		/// <summary>
		/// �ݷö�����(�����ŵ�/�����̵�)
		/// </summary>
		public string POPID { get; set; }

		/// <summary>
		/// �ݷ�������(����VisitionTask��)
		/// </summary>
		public Guid? VisitingTaskID { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? InTime { get; set; }

		/// <summary>
		/// ������Ƭ
		/// </summary>
		public string InPic { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public string InCoordinate { get; set; }

		/// <summary>
		/// ���궨λ����
		/// </summary>
		public int? InGPSType { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? OutTime { get; set; }

		/// <summary>
		/// ������Ƭ
		/// </summary>
		public string OutPic { get; set; }

		/// <summary>
		/// ���궨λ
		/// </summary>
		public string OutCoordinate { get; set; }

		/// <summary>
		/// ���궨λ����
		/// </summary>
		public int? OutGPSType { get; set; }

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