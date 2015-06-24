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
    /// 实体：  
    /// </summary>
    public partial class VipWithdrawDepositApplyEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 会员/店员名称
        /// </summary>
        public string VipName { get; set; }

        #endregion
    }
}