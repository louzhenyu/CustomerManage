using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Request
{
    public class AddKJEventJoinDetailRP : IAPIRequestParameter
    {
        public string ItemId { get; set; }
        public string SkuId { get; set; }
        public string EventId { get; set; }

        public string EventSKUMappingId { get; set; }

        public string KJEventJoinId { get; set; }
        public void Validate() { }
    }
}
