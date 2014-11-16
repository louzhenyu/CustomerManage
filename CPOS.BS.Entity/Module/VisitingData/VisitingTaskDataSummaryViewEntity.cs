/*
 * Author		:Yuangxi
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/11 14:18:56
 * Description	:
 * 1st Modified On	:2013/6/7
 * 1st Modified By	:tianjun
 * 1st Modified Desc:ɾ���ֶ�GPSFailureTime�������ֶ� ��Ч�ݷá����������
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
    /// ʵ�壺 �ݷüƻ�ִ�в鿴 (һ����һ�����еĵ����Ϣ�ϼ�)
    /// </summary>
    public class VisitingTaskDataSummaryViewEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskDataSummaryViewEntity()
        {
        }
        #endregion     

        #region ���Լ�
        ///// <summary>
        ///// �Զ����
        ///// </summary>
        //public string VisitingTaskDataID { get; set; }

        /// <summary>
        /// ��Ա��ʶ
        /// </summary>
        public string ClientUserID { get; set; }
        /// <summary>
        /// �ƻ��ݷ�����
        /// </summary>
        public DateTime? CallDate { get; set; }

		/// <summary>
        /// ��Ա����((VisitingTaskData.ClientUserID->ClientUser.Name))
		/// </summary>
		public string ClientUserName { get; set; }

        /// <summary>
        /// ��Աְλ��ʶ
        /// </summary>
        public string ClientPositionID { get; set; }

		/// <summary>
        /// ��Աְλ
		/// </summary>
		public string UserPositionName { get; set; }

        /// <summary>
        /// ��ԱְλӢ��
        /// </summary>
        public string UserPositionNameEn { get; set; }

        /// <summary>
        /// ���ű�ʶ
        /// </summary>
        public string ClientStructureID { get; set; }

		/// <summary>
        /// ��������(VisitingTaskData.ClientUserID->ClientStructureUserMapping.ClientStructureID->ClientStructure.StructureName)
		/// </summary>
        public string DepartmentName{ get; set; }

		/// <summary>
        /// �״ν���ʱ��(Min(VisitingTaskData.InTime))
		/// </summary>
		public DateTime? FirstInTime { get; set; }           

		/// <summary>
        /// ������ʱ��(Max(VisitingTaskData.OutTime))
		/// </summary>
		public DateTime? LastOutTime { get; set; }

		/// <summary>
        /// ���칤��ʱ�䳤��(����)
		/// </summary>
        public double? WorkingHoursTotal { get; set; }

		/// <summary>
        /// ���ڹ���ʱ�䳤��(����)
		/// </summary>
        public double? WorkingHoursIndoor { get; set; }

		/// <summary>
        /// ·;����ʱ�䳤��(����)
		/// </summary>
        public double? WorkingHoursJourneyTime { get; set; }

		/// <summary>
        /// ��Ч�ٷֱ�(�����ܹ���ʱ��/���칤��ʱ�䳤��)
		/// </summary>
        public string WorkingHoursEfficiency { get; set; }

		/// <summary>
        /// �ƻ��ݷ�������
		/// </summary>
        public int? VisitingTaskPlanCount { get; set; }

		/// <summary>
        /// ʵ�ʰݷ���
		/// </summary>
        public int? VisitingTaskExecutionCount { get; set; }

		/// <summary>
        /// �ݷ�������ɰٷֱ�(ʵ�ʰݷ���/�ƻ��ݷ�������)��50%=��0.5��
		/// </summary>
        public string VisitingTaskExecutionEfficiency { get; set; }

        /// <summary>
        /// ��λʧ�ܴ���
        /// </summary>
        //public int? GPSFailureTime { get; set; }

        /// <summary>
        /// ��Ч�ݷ�(������)
        /// </summary>
        public int? EffectiveVisit { get; set; }

        /// <summary>
        /// ���������(������/�ݷ���*100%)
        /// </summary>
        public string OrderSuccessRate { get; set; }

        #endregion

    }
}