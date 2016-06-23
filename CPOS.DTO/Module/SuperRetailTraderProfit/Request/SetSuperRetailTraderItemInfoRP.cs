using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class SetSuperRetailTraderItemInfoRP : IAPIRequestParameter
    {
        public List<SuperRetailTraderItem> ItemList { get; set; }
        public void Validate() { }
    }
}
