using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.PA.Request
{
    public class CommissionRP
    {
        /// <summary>
        /// 分润详情
        /// </summary>
        public string orderCommissionList { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string merchantCode { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string openId { get; set; }
        [CPOS.BS.Entity.IgnoreSignature]
        [CPOS.BS.Entity.SignatureField]
        public string securitySign { get; set; }
    }

    public class CommissionList
    {
        /// <summary>
        /// 代理人佣金(以分为单位)
        /// </summary>
        public string agentCommission { get; set; }
        /// <summary>
        /// 人寿APP订单号(就是预付单号)
        /// </summary>
        public string orderNo { get; set; }
    }
}
