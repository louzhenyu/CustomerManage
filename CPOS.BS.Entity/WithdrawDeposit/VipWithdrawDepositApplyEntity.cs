/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-12-28 11:40:41
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
    public partial class VipWithdrawDepositApplyEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// ��������
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// ���п���
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// ��Ա/��Ա����
        /// </summary>
        public string VipName { get; set; }

        #endregion
    }
}