using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Register.Request
{
    public class GetRegisterFormItemsRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
        public string EventCode { get; set; }
    }
}
