using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class AddKJEventJoinDetailRD : IAPIResponseData
    {
        public decimal BargainPrice { get; set; }
    }
}
