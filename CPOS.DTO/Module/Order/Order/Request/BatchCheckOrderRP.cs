using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class BatchCheckOrderRP : IAPIRequestParameter
    {
        public List<OrderInfo> OrderList { get; set; }
        public string Remark { get; set; }

        public void Validate()
        {

        }

    }
}
