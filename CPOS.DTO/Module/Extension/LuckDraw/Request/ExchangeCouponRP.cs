using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Extension.LuckDraw.Request
{
    public class ExchangeCouponRP : IAPIRequestParameter
    {
        public string PrizesID { get; set; }
        public void Validate()
        {

        }
    }
}
