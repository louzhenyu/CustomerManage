using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetReceiveAmountOrderRD : IAPIResponseData
    {
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public string VipId { get;set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 优惠券抵用金额
        /// </summary>
        public decimal CouponAmount { get; set; }

        /// <summary>
        /// 积分使用
        /// </summary>
        public decimal PayPoints { get; set; }

        /// <summary>
        /// 余额使用
        /// </summary>
        public decimal AmountAcctPay { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayTypeName { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public string PayStatus { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal TransAmount { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal ReturnAmount { get; set; }

        /// <summary>
        /// 会员折扣
        /// </summary>
        public decimal vipDiscount { get; set; }
    }
}
