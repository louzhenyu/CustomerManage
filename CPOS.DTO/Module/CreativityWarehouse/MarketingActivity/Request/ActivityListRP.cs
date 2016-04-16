using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
   public class ActivityListRP : IAPIRequestParameter
    {
       public void Validate()
        { }
       public string Status { get; set; }
       public string ActivityGroupCode { get; set; }
       public string EventName { get; set; }

    }
}
