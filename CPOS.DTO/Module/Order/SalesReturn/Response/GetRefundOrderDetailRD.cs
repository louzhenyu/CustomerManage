using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.SalesReturn.Response
{
    public class GetRefundOrderDetailRD : IAPIResponseData
    {
        public Guid? RefundID { get; set; }
        public string RefundNo { get; set; }
        public int? Status { get; set; }
        public string OrderID { get; set; }
        public string OrderNo { get; set; }
        public string ItemID { get; set; }
        public string Contacts { get; set; }
        public string Phone { get; set; }
        public decimal? ConfirmAmount { get; set; }
        public decimal? ActualRefundAmount { get; set; }
        public int? Points { get; set; }
        public decimal? PointsAmount { get; set; }
        public decimal? ReturnAmount { get; set; }
        public decimal? Amount { get; set; }
        public string PayTypeName { get; set; }
        public string PayOrderID { get; set; }
        public OrderInfoDetail OrderDetail { get; set; }
    }
}
