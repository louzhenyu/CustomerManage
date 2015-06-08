using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Extension.PointMark.Response
{
    public class GetVipPointMarkRD : IAPIResponseData
    {
        public string PointMarkID { get; set; }
        public int? Count { get; set; }
        public int? TotalCount { get; set; }
        public int? WeekCount { get; set;}
    }
}
