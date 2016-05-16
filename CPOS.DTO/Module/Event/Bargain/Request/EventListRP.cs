using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Request
{
    public class EventListRP : IAPIRequestParameter
    {
        public int EventTypeId { get; set; } // 活动类型 4-砍价

        public void Validate() { }
    }
}
