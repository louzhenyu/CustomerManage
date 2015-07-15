using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.SalesReturn.Response
{
    public class GetRefundOrderListRD : IAPIResponseData
    {
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
        public RefundOrderInfo[] RefundOrderList { get; set; }
    }
    public class RefundOrderInfo
    {
        public Guid? RefundID { get; set; }
        public string RefundNo { get; set; }
        public decimal? ActualRefundAmount { get; set; }
        public string VipName { get; set; }
        public int? Status { get; set; }
        public string CreateTime { get; set; }
    }
}
