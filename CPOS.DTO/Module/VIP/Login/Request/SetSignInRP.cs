using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Request
{
    public class SetSignInRP : IAPIRequestParameter
    {
        public string CustomerCode { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            
        }
    }
}
