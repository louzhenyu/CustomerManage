using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 商品价格类型
    /// </summary>
    public class ItemPriceTypeInfo
    {
        /// <summary>
        /// 商品价格类型标识【保存必须】
        /// </summary>
        public string item_price_type_id {get;set;}
        /// <summary>
        /// 类型号码【保存必须】
        /// </summary>
        public string  item_price_type_code {get;set;}
        /// <summary>
        /// 类型名称【保存必须】
        /// </summary>
        public string  item_price_type_name {get;set;}
        /// <summary>
        /// 状态
        /// </summary>
        public string status {get;set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time {get;set;}
        /// <summary>
        /// 创建人
        /// </summary>
        public string  create_user_id {get;set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time {get;set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
    }
}
