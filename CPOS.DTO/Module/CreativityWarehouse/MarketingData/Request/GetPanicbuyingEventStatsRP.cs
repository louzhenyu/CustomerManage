using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Request
{
    public class GetPanicbuyingEventStatsRP : IAPIRequestParameter
    {
        public string CTWEventId { get; set; }
        public void Validate()
        { }
    }
}
