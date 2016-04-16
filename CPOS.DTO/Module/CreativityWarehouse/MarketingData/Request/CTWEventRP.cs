using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Request
{
    public class CTWEventRP : IAPIRequestParameter
    {
        public void Validate()
        { }
        public string CTWEventId { get; set; }
    }
}
