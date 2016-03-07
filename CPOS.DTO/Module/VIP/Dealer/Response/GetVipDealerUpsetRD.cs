using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Dealer.Response
{
    public class GetVipDealerUpsetRD : IAPIResponseData
    {
        public string Prices { get; set; }
    }
}
