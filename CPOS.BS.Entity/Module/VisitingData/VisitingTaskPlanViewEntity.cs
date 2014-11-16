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
    public class VisitingTaskPlanViewEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskPlanViewEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// �����ʶ
        /// </summary>
        public string VisitingTaskID { get; set; }
        
        /// <summary>
        /// ��������
        /// </summary>
        public string VisitingTaskName { get; set; }

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
        /// �ŵ�����
        /// </summary>
        public string Coordinate { get; set; }

        #endregion

    }
}