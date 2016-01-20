using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Unit.Response
{
    public class GetUnitInfoRD : IAPIResponseData
    {
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitAddress { get; set; }

        public string UnitPhone { get; set; }
    }
}
