/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/27 14:14:27
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
    /// 实体： 10：支付成功   90：支付失败 
    /// </summary>
    public partial class ReceiveAmountOrderEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 订单总数
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 应付总金额
        /// </summary>
        public int SumTotalAmount { get; set; }

        /// <summary>
        /// 实付总金额
        /// </summary>
        public int SumTransAmount { get; set; }
        #endregion
    }
}