using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// sku 价格
    /// </summary>
    public class SkuPrice
    {
        /// <summary>
        /// sku标识
        /// </summary>
        public string sku_id { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// 人人销售价
        /// </summary>
        public decimal EveryoneSalesPrice{get;set;}

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal ReturnCash { get; set; }

        /// <summary>
        /// 团购价
        /// </summary>
        public decimal groupPrice { get; set; }
    }
}
