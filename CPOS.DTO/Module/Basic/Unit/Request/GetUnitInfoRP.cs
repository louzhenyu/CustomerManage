using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Unit.Request
{
    public class GetUnitInfoRP : IAPIRequestParameter
    {
        public string UnitID { get; set; }
        public void Validate()
        {

        }
    }
}
