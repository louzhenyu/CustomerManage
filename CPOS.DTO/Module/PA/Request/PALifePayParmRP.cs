using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.PA.Request
{
    public class PALifePayParmRP
    {
        public ReqCommonData common;
        public PALifePayParmDetail special;
    }

    public class PALifePayParmDetail
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 平安用户的ID实际传userid
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 支付方式ID
        /// </summary>
        public string PaymentId { get; set; }
    }
}
