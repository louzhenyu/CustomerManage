using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class GetOrderDetailRP : IAPIRequestParameter
    {
        public string OrderId { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(OrderId))
            {
                throw new APIException("订单号不能为空") { ErrorCode = 103 };
            }
        }
    }
}
