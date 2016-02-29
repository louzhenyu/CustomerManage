using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Request
{
    public class ApplyRP : IAPIRequestParameter
    {
        public void Validate()
        { }
        public string ThemeId { get; set; }
    }
}
