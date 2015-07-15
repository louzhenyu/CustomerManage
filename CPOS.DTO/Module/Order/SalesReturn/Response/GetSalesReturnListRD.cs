using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.SalesReturn.Response
{
    public class GetSalesReturnListRD : IAPIResponseData
    {
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
        public SalesReturnInfo[] SalesReturnList { get; set; }
    }
    public class SalesReturnInfo
    {
        public string SalesReturnID { get; set; }
        public string SalesReturnNo { get; set; }
        public string ItemName { get; set; }
        public string ImageUrl { get; set; }
        public decimal SalesPrice { get; set; }
        public int? Qty { get; set; }
        public SkuDetailInfo SkuDetail { get; set; }
        public int? Status { get; set; }
        public string VipName { get; set; }
        public int? DeliveryType { get; set; }
        public string CreateTime { get; set; }
        public int? ServicesType { get; set; }

    }
    public class SkuDetailInfo
    {
        public string PropName1 { get; set; }
        public string PropDetailName1 { get; set; }
        public string PropName2 { get; set; }
        public string PropDetailName2 { get; set; }
        public string PropName3 { get; set; }
        public string PropDetailName3 { get; set; }

    }
}
