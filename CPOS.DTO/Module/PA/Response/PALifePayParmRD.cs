using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPOS.Common;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.PA.Response
{
    public class PALifePayParmRD : ResBase
    {
        public PALifePayParmRD()
        {
            Result = new PALifePayParm();
        }
        public PALifePayParm Result { get; set; }
    }

    public abstract class ResBase
    {
        /// <summary>
        /// 0表示成功
        /// </summary>
        [IgnoreSignature]
        [SignatureField]
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [IgnoreSignature]
        [SignatureField]
        public string Msg { get; set; }
    }

    public class PALifePayParm
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public int timestamp { get; set; }
        /// <summary>
        /// 商户编码
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 平安用户的ID实际传userid
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 订单详情扩展字符串，旺财支付统一下单接口返回的prepay_id参数值，提交格式如：prepay_id=***
        /// </summary>
        public string package { get; set; }

        [IgnoreSignature]
        [SignatureField]
        public string paySign { get; set; }
    }
}
