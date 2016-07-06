using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.PA.Request
{
    /// <summary>
    /// 第一次同步订单到PA使用实体
    /// </summary>
    public class SaveOrderInfoRP
    {
        /// <summary>
        /// 用户唯一ID
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 商户号ID
        /// </summary>
        public string merchantCode { get; set; }
        /// <summary>
        /// 旺财支付分配商户号
        /// </summary>
        public string merchantId { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [IgnoreSignature]
        [SignatureField]
        public string securitySign { get; set; }
        /// <summary>
        /// 订单摘要
        /// </summary>
        public string orderSubject { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string merOrderNo { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string orderAmount { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string orderPrepayTime { get; set; }
        /// <summary>
        /// 下单失效时间
        /// </summary>
        public string orderPrepayExpireTime { get; set; }
        /// <summary>
        /// 是否允许信用卡支付 Y/N
        /// </summary>
        public string creditFlag { get; set; }
        /// <summary>
        /// 交易渠道
        /// </summary>
        public string tradeType { get; set; }
        /// <summary>
        /// 优惠券数量
        /// </summary>
        public string couponCount { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>
        public string couponAmount { get; set; }
        /// <summary>
        /// 优惠券ID
        /// </summary>
        public string couponId { get; set; }
        /// <summary>
        /// 订单详情链接
        /// </summary>
        public string detailUrl { get; set; }
        /// <summary>
        /// 订单状态    订单状态 01:待付款02:已付款03:已发货04:确认收货05:退款06:交易关闭07:交易成功08:付款失败
        /// </summary>
        public string orderStatus { get; set; }
        /// <summary>
        /// 订单类型  订单类型，00:虚拟类 01:实物类
        /// </summary>
        public string orderCategory { get; set; }
        /// <summary>
        /// 代理人编号
        /// </summary>
        public string agentNo { get; set; }
        ///// <summary>
        ///// 支付成功通知地址
        ///// </summary>
        //public string notityUrl { get; set; }
        /// <summary>
        /// 订单详情信息
        /// </summary>
        public string orderDetail { get; set; }
    }
}
