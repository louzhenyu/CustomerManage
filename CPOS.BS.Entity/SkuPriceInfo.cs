using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// sku 价格
    /// </summary>
    public class SkuPriceInfo
    {
        /// <summary>
        /// sku标识
        /// </summary>
        public string sku_id { get; set; }
        /// <summary>
        /// 商品标识
        /// </summary>
        public string item_id { get; set; }
        /// <summary>
        /// 组织标识，组织为空，则为所有
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 价格类型
        /// </summary>
        public string item_price_type_id { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// sku价格集合
        /// </summary>
        public IList<SkuPriceInfo> SkuPriceInfoList { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }


        //jifeng.cao(20140221)
        /// <summary>
        /// sku价格ID
        /// </summary>
        public string sku_price_id { get; set; }
        /// <summary>
        /// sku价格
        /// </summary>
        public decimal sku_price { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 标记
        /// </summary>
        public string if_flag { get; set; }
        /// <summary>
        /// 价格类型名称
        /// </summary>
        public string item_price_type_name { get; set; }


    }
}
