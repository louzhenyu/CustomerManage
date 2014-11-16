/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/29 16:01:06
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
    public partial class TPaymentTypeEntity : BaseEntity
    {
        #region 属性集
        /// <summary>
        /// 是否默认
        /// </summary>
        public string IsDefault { set; get; }
        /// <summary>
        /// 是否已开启支付通道
        /// </summary>
        public string IsOpen { set; get; }
        /// <summary>
        /// 是否自定义
        /// </summary>
        public string IsCustom { set; get; }
        #endregion
    }
}