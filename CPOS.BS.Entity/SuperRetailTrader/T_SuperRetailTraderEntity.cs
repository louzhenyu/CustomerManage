/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 9:08:39
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
    public partial class T_SuperRetailTraderEntity : BaseEntity
    {
        #region 属性集
        /// <summary>
        /// 订单总数
        /// </summary>
        public int? OrderCount { get; set; }
        /// <summary>
        /// 下线人数
        /// </summary>
        public int? NumberOffline { get; set; }
        /// <summary>
        /// 体现次数
        /// </summary>
        public int? WithdrawCount { get; set; }
        /// <summary>
        /// 体现总金额
        /// </summary>
        public decimal? WithdrawTotalMoney { get; set; }
        #endregion
    }
}