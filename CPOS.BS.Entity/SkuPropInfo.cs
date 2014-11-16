using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// sku属性集合
    /// </summary>
    public class SkuPropInfo
    {
        /// <summary>
        /// sku与属性关系标识
        /// </summary>
        public string sku_prop_id { get; set; }
        /// <summary>
        /// 属性标识
        /// </summary>
        public string prop_id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int display_index { get; set; }
        /// <summary>
        /// 属性号码
        /// </summary>
        public string prop_code { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string prop_name { get; set; }
        /// <summary>
        /// 属性明细的控制标识
        /// </summary>
        public string prop_input_flag { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 创建人标识
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 修改人标识
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string CustomerId { get; set; }

    }

    /// <summary>
    /// sku属性定义
    /// </summary>
    public class SkuPropCfgInfo
    {
        /// <summary>
        /// sku属性1
        /// </summary>
        public string sku_prop_1 { get; set; }
        /// <summary>
        /// sku属性1名称
        /// </summary>
        public string sku_prop_1_name { get; set; }
        /// <summary>
        /// sku属性2
        /// </summary>
        public string sku_prop_2 { get; set; }
        /// <summary>
        /// sku属性2名称
        /// </summary>
        public string sku_prop_2_name { get; set; }
        /// <summary>
        /// sku属性3
        /// </summary>
        public string sku_prop_3 { get; set; }
        /// <summary>
        /// sku属性3名称
        /// </summary>
        public string sku_prop_3_name { get; set; }
        /// <summary>
        /// sku属性4
        /// </summary>
        public string sku_prop_4 { get; set; }
        /// <summary>
        /// sku属性4名称
        /// </summary>
        public string sku_prop_4_name { get; set; }
        /// <summary>
        /// sku属性5
        /// </summary>
        public string sku_prop_5 { get; set; }
        /// <summary>
        /// sku属性5名称
        /// </summary>
        public string sku_prop_5_name { get; set; }
    }
}
