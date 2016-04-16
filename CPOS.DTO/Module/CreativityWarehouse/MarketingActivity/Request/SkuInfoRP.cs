using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
   public class SkuInfoRP : IAPIRequestParameter
    {
       public void Validate()
       { }

       public string CategoryId{get;set;}
       public string ItemName { get; set; }
       public string BatId { get; set; }
    }
}
