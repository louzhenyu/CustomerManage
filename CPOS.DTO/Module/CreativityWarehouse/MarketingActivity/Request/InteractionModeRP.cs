using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
   public class InteractionModeRP : IAPIRequestParameter
    {
        public void Validate()
        { }
       /// <summary>
       /// 风格ID
       /// </summary>
        public string ThemeId { get; set; }
    }
}
