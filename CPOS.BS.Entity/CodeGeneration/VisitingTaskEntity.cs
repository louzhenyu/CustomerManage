/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/22 16:57:28
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
    /// ʵ�壺 �ݷ����� 
    /// </summary>
    public partial class VisitingTaskEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? VisitingTaskID { get; set; }

		/// <summary>
		/// �ݷ�������
		/// </summary>
		public string VisitingTaskNo { get; set; }

		/// <summary>
		/// �ݷ���������
		/// </summary>
		public string VisitingTaskName { get; set; }

		/// <summary>
		/// �ݷ���������(Ӣ��)
		/// </summary>
		public string VisitingTaskNameEn { get; set; }

		/// <summary>
		/// �ݷ���������(1-�ճ��ݷ�,2-Э��,3-���)
		/// </summary>
		public int? VisitingTaskType { get; set; }

		/// <summary>
		/// ִ����Աְλ���(����ClientPosition��)
		/// </summary>
		public string ClientPositionID { get; set; }

		/// <summary>
		/// �ݷö���(1-�ն�,2-������)
		/// </summary>
		public int? POPType { get; set; }

		/// <summary>
		/// �ն˷��鶨����(����POPGroup��)
		/// </summary>
		public int? POPGroupID { get; set; }

		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// ��ʼ��Ҫִ�еĶ�λ����(1-��վ��λ,2-GPS��λ,3-��϶�λ)
		/// </summary>
		public int? StartGPSType { get; set; }

		/// <summary>
		/// ������Ҫִ�еĶ�λ����(1-��վ��λ,2-GPS��λ,3-��϶�λ)
		/// </summary>
		public int? EndGPSType { get; set; }

		/// <summary>
		/// ��ʼ��Ҫ����(0-��,1-��)
		/// </summary>
		public int? StartPic { get; set; }

		/// <summary>
		/// ������Ҫ����(0-��,1-��)
		/// </summary>
		public int? EndPic { get; set; }

		/// <summary>
		/// �������ȼ�
		/// </summary>
		public int? TaskPriority { get; set; }

		/// <summary>
		/// �Ƿ�ϲ�����(0-��,1-��)
		/// </summary>
		public int? IsCombin { get; set; }

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