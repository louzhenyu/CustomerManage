using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.PA.Request
{
    /// <summary>
    /// 更新订单到PA的时候订单详情实体
    /// </summary>
    public class UpdateOrderDetailRP
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
        /// 订单号
        /// </summary>
        public string orderNo { get; set; }
        ///// <summary>
        ///// 商品图片URL
        ///// </summary>
        //public string commodityImageUrl { get; set; }
        ///// <summary>
        ///// 商品摘要
        ///// </summary>
        //public string commoditySubject { get; set; }
    }
}
