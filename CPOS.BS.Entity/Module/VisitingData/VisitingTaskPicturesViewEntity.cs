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
    /// ʵ�壺 �ݷ�ִ����ϸ�鿴 (ĳ��ĳ����ĳ����Ĳ�Ʒ�����ϸ)
    /// </summary>
    public class VisitingTaskPicturesViewEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskPicturesViewEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// ���ű�ʶ
        /// </summary>
        public string ClientStructureID { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string StructureName { get; set; }

        /// <summary>
        /// ְλ��ʶ
        /// </summary>
        public string ClientPositionID { get; set; }

        /// <summary>
        /// ְλ����
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// �ŵ�/�����̱�ʶ
        /// </summary>
        public string POPID { get; set; }

        /// <summary>
        /// �ŵ�/����������
        /// </summary>
        public string POPName { get; set; }

        /// <summary>
        /// ��Ա��ʶ
        /// </summary>
        public int? ClientUserID { get; set; }

        /// <summary>
        /// ��Ա����
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string InCoordinate { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string OutCoordinate { get; set; }

        /// <summary>
        /// �ݷò����ʶ
        /// </summary>
        public string VisitingTaskStepID { get; set; }

        /// <summary>
        /// �ݷò�������
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// �ݷ������ʶ
        /// </summary>
        public string VisitingTaskID { get; set; }

        /// <summary>
        /// �ݷ���������
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// ��Ƭ����
        /// </summary>
        public string PhotoName { get; set; }

        ///// <summary>
        ///// ��Ƭ����(Ӣ��)
        ///// </summary>
        //public string PhotoNameEn { get; set; }

        /// <summary>
        /// ��Ƭ�洢�ļ���
        /// </summary>
        public string Value { get; set; }

        #endregion

    }
}