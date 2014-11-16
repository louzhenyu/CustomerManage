using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Event.Response
{
    public class GetDrawMethodListRD : IAPIResponseData
    {
        public DrawMethodInfo[] DrawMethodList { get; set; }
        public int TotalPages { get; set; }
    }

    public class DrawMethodInfo
    {
        public int DrawMethodId { get; set; }
        public string DrawMethodName { get; set; }
    }
}
