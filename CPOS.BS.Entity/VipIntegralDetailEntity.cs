/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 9:45:29
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
    /// ʵ�壺  
    /// </summary>
    public partial class VipIntegralDetailEntity : BaseEntity
    {
        #region ���Լ�

        /// <summary>
        /// ��Դ
        /// </summary>
        public string IntegralSourceName { get; set; }

        /// <summary>
        /// ��Դ��Ա����
        /// </summary>
        public string FromVipName { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Create_Time { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        public string UpdateReason { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int UpdateCount { get; set; }
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime UpdateTime { get; set; }
        #endregion
    }
}