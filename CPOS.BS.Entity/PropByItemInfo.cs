using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 商品的属性集合
    /// </summary>
    public class PropByItemInfo
    {   
        /// <summary>
        /// 属性标识
        /// </summary>
        public string prop_id { get; set; }
        /// <summary>
        /// 属性号码
        /// </summary>
        public string prop_code { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string prop_name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int display_index { get; set; }
    }
}
