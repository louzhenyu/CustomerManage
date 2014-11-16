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
    /// ʵ�壺 �ݷ���ϸ���� 
    /// </summary>
    public partial class VisitingTaskDetailDataEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskDetailDataEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? VisitingTaskDetailDataID { get; set; }

		/// <summary>
		/// �ݷ����ݱ��(����VisitingTaskData��)
		/// </summary>
		public Guid? VisitingTaskDataID { get; set; }

		/// <summary>
		/// �ݷò�����(����VisitingTaskStep��)
		/// </summary>
		public Guid? VisitingTaskStepID { get; set; }

		/// <summary>
		/// �ݷö�����(����VisitingTaskStepObject��)
		/// </summary>
		public Guid? ObjectID { get; set; }

		/// <summary>
		/// �ݷö�����(�����ŵ�/��Ʒ/Ʒ��/���ȱ�,�ݷö���Ϊһ��ʱֵ���ڸ��ֶ�)
		/// </summary>
		public string Target1ID { get; set; }

		/// <summary>
		/// �ݷö�����(������Ϊ����ʱ,��ŵڶ�������)
		/// </summary>
		public string Target2ID { get; set; }

		/// <summary>
		/// �ݷò������(����VisitingParameter��)
		/// </summary>
		public Guid? VisitingParameterID { get; set; }

		/// <summary>
		/// �ݷò���ֵ
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// �����ύʱ��
		/// </summary>
		public DateTime? SubmitTime { get; set; }

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