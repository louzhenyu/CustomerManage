/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 13:31:25
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
    public partial class TInoutStatusEntity : BaseEntity 
    {
        #region 属性集

        /// <summary>
        /// 订单状态描述
        /// </summary>
        public string OrderStatusName { get; set; }
        /// <summary>
        /// 审核未通过理由
        /// </summary>
        public string CheckResultName { get; set; }
        /// <summary>
        /// 支付方式描述
        /// </summary>
        public string PayMethodName { get; set; }
        /// <summary>
        /// 配送公司名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 日期格式化
        /// </summary>
        public string LastUpdateTimeFormat {
            get {
                return LastUpdateTime == null ? "" : DateTime.Parse(LastUpdateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        #endregion
    }
}