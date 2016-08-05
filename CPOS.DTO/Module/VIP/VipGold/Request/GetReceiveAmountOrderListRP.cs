using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetReceiveAmountOrderListRP : IAPIRequestParameter
    {
        public int Status { get; set; }
        
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public void Validate() { }
    }
}
