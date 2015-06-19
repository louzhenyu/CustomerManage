using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Extension.LuckDraw.Response
{
    public class LuckDrawRD : IAPIResponseData
    {
        public int Flag { get; set; }
        public Guid? PrizesID { get; set; }
        public string PrizesName { get; set; }
        public int? DisplayIndex { get; set; }

    }
}
