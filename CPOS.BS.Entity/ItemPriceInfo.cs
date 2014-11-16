using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 商品价格类
    /// </summary>
    public class ItemPriceInfo
    {
        /// <summary>
        /// 商品价格标识【保存必须】
        /// </summary>
        public string item_price_id { get; set; }
        /// <summary>
        /// 商品标识【保存必须】
        /// </summary>
        public string item_id { get; set; }
        /// <summary>
        /// 商品价格类型标识【保存必须】
        /// </summary>
        public string item_price_type_id { get; set; }
        /// <summary>
        /// 价格【保存必须】
        /// </summary>
        public decimal item_price { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }

        /// <summary>
        /// 商品价格类型名称
        /// </summary>
        public string item_price_type_name { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 商品价格集合
        /// </summary>
        public IList<ItemPriceInfo> ItemPriceInfoList { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
    }
}
