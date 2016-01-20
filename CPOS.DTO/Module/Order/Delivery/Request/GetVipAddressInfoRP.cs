using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Delivery.Request
{
    public class GetVipAddressInfoRP : IAPIRequestParameter
    {
        public string VipAddressID { get; set; }
        public void Validate()
        {

        }
    }
}
