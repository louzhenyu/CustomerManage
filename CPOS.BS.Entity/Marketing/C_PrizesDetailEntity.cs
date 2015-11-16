/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/9 15:07:16
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
    public partial class C_PrizesDetailEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 券名称
        /// </summary>
        public string CouponTypeName { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 发型数量
        /// </summary>
        public int? IssuedQty { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public int? IsVoucher { get; set; }
        /// <summary>
        /// 券描述
        /// </summary>
        public string CouponTypeDesc { get; set; }
        #endregion
    }
}