using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Response
{
    public class ApplyRD : IAPIResponseData
    {
        public int errCode { get; set; }
        public string errMsg { get; set; }
    }
}
