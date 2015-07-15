using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.SalesReturn.Request
{
    public class GetRefundOrderDetailRP : IAPIRequestParameter
    {
        public string RefundID { get; set; }
        public void Validate()
        {

        }
    }
}
