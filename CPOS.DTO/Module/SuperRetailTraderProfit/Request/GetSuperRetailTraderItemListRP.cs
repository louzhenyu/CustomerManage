using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetSuperRetailTraderItemListRP : IAPIRequestParameter
    {
        public string ItemName { get; set; }

        public int Status { get; set; }
        
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
        
        public void Validate() { }
    }

}
