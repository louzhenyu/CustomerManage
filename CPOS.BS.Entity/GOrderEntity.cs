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
    /// 实体：  
    /// </summary>
    public partial class GOrderEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 待处理
        /// </summary>
        public int status1Count { get; set; }
        /// <summary>
        /// 计划中
        /// </summary>
        public int status2Count { get; set; }
        /// <summary>
        /// 已完成
        /// </summary>
        public int status3Count { get; set; }
        /// <summary>
        /// 店号/工号 【显示】
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 店号/工号 【显示】
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 店号/工号标识【显示】
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 吻合度
        /// </summary>
        public string GoodnessFit { get; set; }
        /// <summary>
        /// 距离订单地址
        /// </summary>
        public double Distance { get; set; }

        public decimal TotalAmount { get; set; }
        #endregion
    }
}