using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.SalesReturn.Request
{
    public class GetSalesReturnListRP : IAPIRequestParameter
    {
        public int PageIndex{get;set;}
        public int PageSize { get; set; }

        public string SalesReturnNo { get; set; }
        public int DeliveryType { get; set; }
        public int Status { get; set; }
        public void Validate()
        {

        }
    }
}
