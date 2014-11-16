using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// sku
    /// </summary>
    public class SkuInfo
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
        /// barcode
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// 属性1明细标识
        /// </summary>
        public string  prop_1_detail_id { get; set; }
        /// <summary>
        /// 属性1明细名称
        /// </summary>
        public string  prop_1_detail_name { get; set; }
        /// <summary>
        /// 属性1明细号码
        /// </summary>
        public string prop_1_detail_code { get; set; }
        /// <summary>
        /// 属性2明细标识
        /// </summary>
        public string prop_2_detail_id { get; set; }
        /// <summary>
        /// 属性2明细名称
        /// </summary>
        public string prop_2_detail_name { get; set; }
        /// <summary>
        /// 属性2明细号码
        /// </summary>
        public string prop_2_detail_code { get; set; }
        /// <summary>
        /// 属性3明细标识
        /// </summary>
        public string prop_3_detail_id { get; set; }
        /// <summary>
        /// 属性3明细名称
        /// </summary>
        public string prop_3_detail_name { get; set; }
        /// <summary>
        /// 属性3明细号码
        /// </summary>
        public string prop_3_detail_code { get; set; }
        /// <summary>
        /// 属性4明细标识
        /// </summary>
        public string prop_4_detail_id { get; set; }
        /// <summary>
        /// 属性4明细名称
        /// </summary>
        public string prop_4_detail_name { get; set; }
        /// <summary>
        /// 属性4明细标识
        /// </summary>
        public string prop_4_detail_code { get; set; }
        /// <summary>
        /// 属性5明细标识
        /// </summary>
        public string prop_5_detail_id { get; set; }
        
        /// <summary>
        /// 属性5明细名称
        /// </summary>
        public string prop_5_detail_name { get; set; }
        /// <summary>
        /// 属性5明细号码
        /// </summary>
        public string prop_5_detail_code { get; set; }
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
        /// 商品名称
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string item_code { get; set; }
        /// <summary>
        /// 属性1标识
        /// </summary>
        public string prop_1_id { get; set; }
        /// <summary>
        /// 属性1名称
        /// </summary>
        public string prop_1_name { get; set; }
        /// <summary>
        /// 属性1号码
        /// </summary>
        public string prop_1_code { get; set; }
        /// <summary>
        /// 属性2标识
        /// </summary>
        public string prop_2_id { get; set; }
        /// <summary>
        /// 属性2名称
        /// </summary>
        public string prop_2_name { get; set; }
        /// <summary>
        /// 属性2号码
        /// </summary>
        public string prop_2_code { get; set; }
        /// <summary>
        /// 属性3标识
        /// </summary>
        public string prop_3_id { get; set; }
        /// <summary>
        /// 属性3名称
        /// </summary>
        public string prop_3_name { get; set; }
        /// <summary>
        /// 属性3号码
        /// </summary>
        public string prop_3_code { get; set; }
        /// <summary>
        /// 属性4标识
        /// </summary>
        public string prop_4_id { get; set; }
        /// <summary>
        /// 属性4名称
        /// </summary>
        public string prop_4_name { get; set; }
        /// <summary>
        /// 属性4号码
        /// </summary>
        public string prop_4_code { get; set; }
        /// <summary>
        /// 属性5标识
        /// </summary>
        public string prop_5_id { get; set; }
        /// <summary>
        /// 属性5名称
        /// </summary>
        public string prop_5_name { get; set; }
        /// <summary>
        /// 属性5号码
        /// </summary>
        public string prop_5_code { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 前台显示名称
        /// </summary>
        public string display_name { get; set; }
        /// <summary>
        /// sku集合
        /// </summary>
        public IList<SkuInfo> SkuInfoList { get; set; }
        /// <summary>
        /// sku关联价格
        /// </summary>
        public IList<SkuPriceInfo> sku_price_list = new List<SkuPriceInfo>();
        public string image_url { get; set; }
    }
}
