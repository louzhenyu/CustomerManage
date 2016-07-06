using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.PA.Request
{
    public class GetAddressRP
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string merchantCode { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [IgnoreSignature]
        [SignatureField]
        public string securitySign { get; set; }
    }
}
