using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.PA.Response
{
    /// <summary>
    /// 退款接口返回实体
    /// </summary>
    public class RefuntAmountRD
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
        /// 退款金额
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 状态   00 退款成功 01 退款失败
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 预付单号
        /// </summary>
        public string prepayId { get; set; }
    }
}
