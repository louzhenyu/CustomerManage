using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.PA.Request
{
    /// <summary>
    /// 退款接口请求实体
    /// </summary>
    public class RefuntAmountRP
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string merchantId { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [IgnoreSignature]
        [SignatureField]
        public string sign { get; set; }
        /// <summary>
        /// 预付单号
        /// </summary>
        public string prepayId { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public string amount { get; set; }
    }
}
