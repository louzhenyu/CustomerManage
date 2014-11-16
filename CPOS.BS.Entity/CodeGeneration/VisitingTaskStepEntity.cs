/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:50
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
    /// ʵ�壺 �ݷò�����Ϣ��ϸ 
    /// </summary>
    public partial class VisitingTaskStepEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskStepEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? VisitingTaskStepID { get; set; }

		/// <summary>
		/// ������(����VisitingTask��)
		/// </summary>
		public Guid? VisitingTaskID { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public string StepNo { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public string StepName { get; set; }

		/// <summary>
		/// ��������(Ӣ��)
		/// </summary>
		public string StepNameEn { get; set; }

		/// <summary>
		/// ��������(��������: 1-��Ʒ���,2-Ʒ�����,3-Ʒ�����)
		/// </summary>
		public int? StepType { get; set; }

		/// <summary>
		/// �Ƿ����(0-��,1-��)
		/// </summary>
		public int? IsMustDo { get; set; }

		/// <summary>
		/// �������ȼ�
		/// </summary>
		public int? StepPriority { get; set; }

		/// <summary>
		/// �Ƿ�һҳ����(0-��,1-��)
		/// </summary>
		public int? IsOnePage { get; set; }

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