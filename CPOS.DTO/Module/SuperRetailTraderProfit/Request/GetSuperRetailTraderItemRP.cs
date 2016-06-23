using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetSuperRetailTraderItemRP : IAPIRequestParameter
    {
        public string ItemName { get; set; }

        public string ItemCategoryId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public void Validate() { }
    }
}
