using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.Lottery.Request
{
    public class GetPrizeLocationListRP : IAPIRequestParameter
    {
        public string EventID { get; set; }
        public void Validate()
        {
        }
    }
}
