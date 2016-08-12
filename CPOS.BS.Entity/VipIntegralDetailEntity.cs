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
    /// 实体：  
    /// </summary>
    public partial class VipIntegralDetailEntity : BaseEntity
    {
        #region 属性集

        /// <summary>
        /// 来源
        /// </summary>
        public string IntegralSourceName { get; set; }

        /// <summary>
        /// 来源会员名称
        /// </summary>
        public string FromVipName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string Create_Time { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string UpdateReason { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public int UpdateCount { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        #endregion
    }
}