using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Menu.Request
{
    public class GetMenuDetailRP : IAPIRequestParameter
    {
        public string MenuId { get; set; }

        public void Validate()
        {
        }
    }
}
