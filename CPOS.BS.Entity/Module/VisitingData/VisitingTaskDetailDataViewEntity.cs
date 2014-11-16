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
    public class VisitingTaskDetailDataViewEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskDetailDataViewEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// �Զ����
        /// </summary>
        public string VisitingTaskDetailDataID { get; set; }

        /// <summary>
        /// ��Ա��ʶ
        /// </summary>
        public int? ClientUserID { get; set; } 

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// �ݷò����ʶ
        /// </summary>
        public string VisitingTaskStepID { get; set; }

        /// <summary>
        /// �ݷò������
        /// </summary>
        public int? StepType { get; set; }

        /// <summary>
        /// �ݷò�������
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// �ݷò�����ʶ
        /// </summary>
        public string VisitingParameterID { get; set; }

        /// <summary>
        /// �ݷò�������(1-��Ʒ���,2-Ʒ����ص�)
        /// </summary>
        public int? ParameterType { get; set; }

        /// <summary>
        /// �ݷò�������
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// �ݷò���Ӣ������
        /// </summary>
        public string ParameterNameEn { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public int? ParameterOrder { get; set; }

        /// <summary>
        /// �ؼ�����
        /// </summary>
        public int? ControlType { get; set; }

        /// <summary>
        /// �ؼ�����
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// �������������ʶ
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// �ݷö�����(�����ŵ�/��Ʒ/Ʒ��/���ȱ�)
        /// </summary>
        public string Target1ID { get; set; }

        /// <summary>
        /// �ݷö�����(���ڰݷö���ΪƷ��Ĳ���:Ʒ��/���ȱ�)
        /// </summary>
        public string Target2ID { get; set; }

        /// <summary>
        /// �����ɼ�����ֵ
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// �����������������
        /// ȡ����StepType��
        /// 1��SKUName
        /// 2��BrandName
        /// 3��CategoryName
        /// 4��ClientPositionName
        /// </summary>
        public string ObjectName1 { get; set; }

        /// <summary>
        /// �����������������2
        /// ȡ����StepType��
        /// 1������ֵ��
        /// 2��CategoryName���ߡ���ֵ��
        /// 3��BrandName���ߡ���ֵ��
        /// 4��ClientUserName
        /// </summary>
        public string ObjectName2 { get; set; }
        #endregion

    }
}