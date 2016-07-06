using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.PA.Request
{
    /// <summary>
    ///第一次同步订单到PA使用的订单详情实体
    /// </summary>
    public class PAOrderDetailRP
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public string commodityID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string commodity { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public string commodityCount { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public string commodityPrice { get; set; }
        /// <summary>
        /// 商品URL
        /// </summary>
        public string commodityUrl { get; set; }
        /// <summary>
        /// 商品图片URL
        /// </summary>
        public string commodityImageUrl { get; set; }
        /// <summary>
        /// 商品摘要
        /// </summary>
        public string commoditySubject { get; set; }
    }
}
