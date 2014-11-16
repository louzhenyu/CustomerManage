using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.LoginOut.Request
{
    public class RemoveIOSMessageBindRP : IAPIRequestParameter
    {
        public string UserId { get; set; }

        public void Validate()
        {
           
        }
    }
}
