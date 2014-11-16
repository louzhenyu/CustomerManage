using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Menu.Request
{
    public class GetMenuListRP : IAPIRequestParameter
    {
        public string ApplicationId { get; set; }

        public void Validate()
        {
        }
    }
}
