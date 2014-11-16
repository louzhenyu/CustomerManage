using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 调价单模板
    /// </summary>
    public class AdjustmentOrderDetailSkuInfo
    {
        /// <summary>
        /// 调价单SKU明细标识[保存必须]
        /// </summary>
        public string order_detail_sku_id { get; set; }
        /// <summary>
        /// 订单标识[保存必须]
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// sku标识[保存必须]
        /// </summary>
        public string sku_id { get; set; }
        /// <summary>
        /// 商品价格[保存必须]
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 商品名称(显示用)
        /// </summary>
        public string item_name { get; set; }

        /// <summary>
        /// 属性1的值(显示用)
        /// </summary>
        public string prop_1_detail_name { get; set; }

        /// <summary>
        /// 属性2的值(显示用)
        /// </summary>
        public string prop_2_detail_name { get; set; }

        /// <summary>
        /// 属性3的值(显示用)
        /// </summary>
        public string prop_3_detail_name { get; set; }

        /// <summary>
        /// 属性4的值(显示用)
        /// </summary>
        public string prop_4_detail_name { get; set; }

        /// <summary>
        /// 属性5的值(显示用)
        /// </summary>
        public string prop_5_detail_name { get; set; }
    }
}
