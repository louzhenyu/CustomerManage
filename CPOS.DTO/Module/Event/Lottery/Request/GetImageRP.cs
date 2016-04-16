using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;


namespace JIT.CPOS.DTO.Module.Event.Lottery.Request
{
   public class GetImageRP : IAPIRequestParameter
    {
        public string EventId { get; set; }
        public string CTWEventId { get; set; }
        public void Validate()
        {
        }
    }
}
