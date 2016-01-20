using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class BatchInvalidShipRP : IAPIRequestParameter
    {
        public List<OrderInfo> OrderList{get;set;}

        public string Remark { get; set; }
        public void Validate()
        {

        }
    }

    public class OrderInfo{
        public string OrderID{get;set;}
        public string Status { get; set; }
        public string DeliverOrder { get; set; }
        public string DeliverCompanyID { get; set; }
        public string Field9 { get; set; }
        public string StatusDesc { get; set; }
    }
}
