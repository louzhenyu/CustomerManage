using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SapMessageApi.Request;

namespace JIT.CPOS.DTO.Module.PA.Response
{
    /// <summary>
    /// 保存订单到平安后平安的响应实体
    /// </summary>
    public class SaveOrderRD : IAPIResponseData
    {
        /// <summary>
        /// 商户号ID
        /// </summary>
        public string merchantCode { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string merchantId { get; set; }
        /// <summary>
        /// 预付单号
        /// </summary>
        public string prepayId { get; set; }
        /// <summary>
        /// 交易渠道
        /// </summary>
        public string tradeType { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [IgnoreSignature]
        [SignatureField]
        public string securitySign { get; set; }
    }
}
