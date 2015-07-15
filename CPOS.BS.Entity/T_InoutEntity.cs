/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:14
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
    public partial class T_InoutEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 余额抵扣
        /// </summary>
        public decimal VipEndAmount { get; set; }
        /// <summary>
        /// 返现抵扣
        /// </summary>
        public decimal ReturnAmount { get; set; }
        /// <summary>
        /// 积分抵扣
        /// </summary>
        public decimal IntegralAmount { get; set; }
        /// <summary>
        /// 优惠券抵扣
        /// </summary>
        public decimal CouponAmount { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal DeliveryAmount { get; set; }
        #endregion
    }
}