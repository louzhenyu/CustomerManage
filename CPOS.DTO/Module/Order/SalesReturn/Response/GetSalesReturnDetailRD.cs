using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.SalesReturn.Response
{
    public class GetSalesReturnDetailRD:IAPIResponseData
    {
        public string SalesReturnID { get; set; }
        public string SalesReturnNo { get; set; }
        public string OrderID { get; set; }
        public string OrderNo { get; set; }
        public int? Status { get; set; }
        public int? DeliveryType { get; set; }
        public string Reason { get; set; }
        public int? ActualQty { get; set; }
        public int? Qty { get; set; }
        public string Contacts { get; set; }
        public string UnitName { get; set; }
        public string UnitTel { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int? ServicesType { get; set; }
        public OrderInfoDetail OrderDetail { get; set; }
        public HistoryInfo[] HistoryList { get; set; } 

    }
    public class HistoryInfo
    {
        public string HistoryID { get; set; }
        //public int OperationType { get; set; }
        public string OperationDesc { get; set; }
        public string HisRemark { get; set; }
        public string OperatorName { get; set; }
        public string CreateTime { get; set; }
    }
    public class OrderInfoDetail
    {
        public string OrderID { get; set; }
        public int Qty { get; set; }
        public string ItemName { get; set; }
        public string ImageUrl { get; set; }
        public decimal SalesPrice { get; set; }
        public SkuDetailInfo SkuDetail { get; set; }
        public string PayTypeName { get; set; }
        /// <summary>
        /// 应退金额
        /// </summary>
        public decimal RefundAmount { get; set; }
        /// <summary>
        /// 确定退款金额
        /// </summary>
        public decimal ConfirmAmount { get; set; }
        //public decimal IntegralDeduction { get; set; }
        //public decimal ReturnAmountDeduction { get; set; }
        //public decimal AmountDeduction { get; set; }
        //public decimal CouponDeduction { get; set; }

    }
   
}
