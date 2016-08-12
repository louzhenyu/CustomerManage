using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetReceiveAmountOrderListRD : IAPIResponseData
    {
        public List<ReceiveAmountOrderDetail> OrderList { get; set; }

        public int TotalCount { get;set;}

        public int TotalPage { get; set; }

        /// <summary>
        /// 订单总数
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 总应付金额
        /// </summary>
        public decimal SumTotalAmount { get; set; }

        /// <summary>
        /// 总实付金额
        /// </summary>
        public decimal SumTransAmount { get; set; }

    }
    public class ReceiveAmountOrderDetail
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public string PayStatus { get; set; }

        /// <summary>
        /// 成交时间
        /// </summary>
        public string PayDateTime { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal TransAmount { get; set; }
    }
}
