using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Request
{
    public class DeleteFormRP : IAPIRequestParameter
    {
        public string MobileModuleID { get; set; }

        public void Validate()
        {

        }
    }
}
