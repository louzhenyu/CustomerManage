using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 调价单组织明细
    /// </summary>
    public class AdjustmentOrderDetailUnitInfo
    {
        /// <summary>
        /// 调价单组织明细标识[保存必须]
        /// </summary>
        public string order_detail_unit_id { get; set; }
        /// <summary>
        /// 订单标识[保存必须]
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 组织明细[保存必须]
        /// </summary>
        public string unit_id { get; set; }
    }
}
