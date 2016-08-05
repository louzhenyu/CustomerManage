using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Order.Response
{
    public class OrderRewardRD : IAPIResponseData
    {
        public bool IsSuccess { get; set; }

        public string Msg { get; set; }
    }
}
