using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Response
{
    public class DeleteFormRD : IAPIResponseData
    {
       public bool IsSuccess { get; set; }
       public string Msg { get; set; }
    }
}
