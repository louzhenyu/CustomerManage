using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.ContactEvent.Request
{
    public class GetContactEventDetailRP : IAPIRequestParameter
    {
        public string ContactEventId { get; set; }
        public void Validate()
        {
        }
    }
}
