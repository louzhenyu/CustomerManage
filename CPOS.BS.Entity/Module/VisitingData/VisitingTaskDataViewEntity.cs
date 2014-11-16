/*
 * Author		:Yuangxi
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/11 14:18:56
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
    /// ʵ�壺 �ݷ�ִ����ϸ�鿴 (һ����һ�����е��ߵ���Ϣ��ϸ)
    /// </summary>
    public class VisitingTaskDataViewEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskDataViewEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// �Զ����
        /// </summary>
        public string VisitingTaskDataID { get; set; }

        /// <summary>
        /// �û���ʶ
        /// </summary>
        public string ClientUserID { get; set; }

        /// <summary>
        /// �û���
        /// </summary>
        public string ClientUserName { get; set; }
        
        /// <summary>
        /// �ݷö�����(�����ŵ�/�����̵�)
        /// </summary>
        public string POPID { get; set; }
        /// <summary>
        /// �ն�����
        /// </summary>
        public int POPType { get; set; }
        /// <summary>
        /// �ŵ�����(�ݷö�������)
        /// </summary>
        public string POPName { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? OutTime { get; set; }
        
        /// <summary>
        /// �ߵ�����(dayof(����ʱ��))
        /// </summary>
        public DateTime? ExecutionTime { get; set; }

        /// <summary>
        /// ����ʱ��(����)
        /// </summary>
        public int? WorkingHoursIndoor { get; set; }

        /// <summary>
        /// ·;ʱ��(���ν���-�ϴγ���)
        /// </summary>
        public int? WorkingHoursJourneyTime { get; set; }

        /// <summary>
        /// ��ʱ��(����ʱ��+·;ʱ��)
        /// </summary>
        public int? WorkingHoursTotal { get; set; }

        /// <summary>
        /// ��λ������Ϣ:���궨λ����
        /// </summary>
        public string InCoordinate { get; set; }

        /// <summary>
        /// ��λ������Ϣ:���궨λ����
        /// </summary>
        public string OutCoordinate { get; set; }

        /// <summary>
        /// ������Ƭ
        /// </summary>
        public string InPic { get; set; }

        /// <summary>
        /// ������Ƭ
        /// </summary>
        public string OutPic { get; set; }

        #endregion

    }
}