/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 17:54:09
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

namespace JIT.CPOS.Entity
{
    /// <summary>
    /// ʵ�壺  
    /// </summary>
    public partial class ObjectEvaluationEntity : BaseEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ObjectEvaluationEntity()
        {
        }
        #endregion

        #region ���Լ�
        /// <summary>
        /// 
        /// </summary>
        public String ItemEvaluationID { get; set; }

        /// <summary>
        /// ��ƷID�����ŵ�ID
        /// </summary>
        public String ObjectID { get; set; }

        /// <summary>
        /// ��ԱID
        /// </summary>
        public String MemberID { get; set; }

        /// <summary>
        /// �ͻ�ID
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StarLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// ����ƽ̨
        /// </summary>
        public String Platform { get; set; }


        #endregion

    }
}