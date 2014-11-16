using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 调价单商品明细类
    /// </summary>
    public class AdjustmentOrderDetailItemInfo
    {
        /// <summary>
        /// 调价单商品明细标识[保存必须]
        /// </summary>
        public string order_detail_item_id { get; set; }
        /// <summary>
        /// 订单标识[保存必须]
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 商品标识（保存用）
        /// </summary>
        public string item_id { get; set; }
        /// <summary>
        /// 商品价格[保存必须]
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 商品名称（显示用）
        /// </summary>
        public string item_name { get; private set; }
    }
}
