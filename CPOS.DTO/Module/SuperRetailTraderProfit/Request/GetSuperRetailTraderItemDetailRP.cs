using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetSuperRetailTraderItemDetailRP : IAPIRequestParameter
    {
        public string ItemId { get; set; }
        public void Validate() { }
    }
}
