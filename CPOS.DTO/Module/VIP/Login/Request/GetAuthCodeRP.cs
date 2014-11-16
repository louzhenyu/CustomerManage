using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Request
{
    public class GetAuthCodeRP : IAPIRequestParameter
    {
        public string Mobile { get; set; }

        public void Validate()
        {

        }
    }
}
