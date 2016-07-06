using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.PA.Request
{
    public class GetOrderInfoRP
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public string merchantCode { get; set; }
        /// <summary>
        /// 商户号ID
        /// </summary>
        public string merchantId { get; set; }
        /// <summary>
        /// 用户唯一ID
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string orderStatus { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [IgnoreSignature]
        [SignatureField]
        public string securitySign { get; set; }
    }
}
