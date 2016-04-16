using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
   public class LingkinRP : IAPIRequestParameter
    {
       public void Validate()
       { }
       public string Url { get; set; }
       public string DrawMethodCode { get; set; }
       public string CTWEventId { get; set; }
       public string TemplateId { get; set; }
    }
}
