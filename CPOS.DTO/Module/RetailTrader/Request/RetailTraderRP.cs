using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.RetailTrader.Request
{
    public class RetailTraderRP : IAPIRequestParameter
    {

        public string RetailTraderID { get; set; }//专门是分销商的ID

        public void Validate()
        {
        }
    }
}
