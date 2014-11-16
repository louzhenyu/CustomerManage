using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Request
{
    public class FormSaveRP : IAPIRequestParameter
    {
        public string MobileModuleID { get; set; }
        public MobileBunessDefinedSubInfo[] Items { get; set; }

        public void Validate()
        {
           
        }
    }
}
