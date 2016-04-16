using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Request
{
    public class GetTCTWLEventListRP : PageQueryRequestParameter
    {
        public string EventName { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string EventStatus { get; set; }
        public string ActivityGroupId { get; set; }
        




    }
}
