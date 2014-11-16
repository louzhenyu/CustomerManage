using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Event.Response
{
    public class GetRecommendRD : IAPIResponseData
    {
        public string EventId { get; set; }
    }
}
