using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetSuperRetailTraderIncomeAndUnderlingInfoRP : IAPIRequestParameter
    {
        public string SuperRetailTraderId { get; set; }
        public string Date { get; set; }
        public void Validate() { }
    }
}
