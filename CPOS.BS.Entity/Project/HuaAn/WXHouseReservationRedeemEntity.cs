/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 10:16:11
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
    public partial class WXHouseReservationRedeemEntity : BaseEntity
    {
        #region 属性集
        /// <summary>
        /// 客户协议号
        /// </summary>
        public string Assignbuyer { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// 委托金额
        /// </summary>
        public decimal RealPay { get; set; }

        /// <summary>
        /// 第三方订单号
        /// </summary>
        public string ThirdOrderNo { get; set; }
        #endregion
    }
}