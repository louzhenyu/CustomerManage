/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/27 10:35:51
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
    public partial class GOrderEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// ������
        /// </summary>
        public int status1Count { get; set; }
        /// <summary>
        /// �ƻ���
        /// </summary>
        public int status2Count { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        public int status3Count { get; set; }
        /// <summary>
        /// ���/���� ����ʾ��
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// ���/���� ����ʾ��
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ���/���ű�ʶ����ʾ��
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// �Ǻ϶�
        /// </summary>
        public string GoodnessFit { get; set; }
        /// <summary>
        /// ���붩����ַ
        /// </summary>
        public double Distance { get; set; }

        public decimal TotalAmount { get; set; }
        #endregion
    }
}