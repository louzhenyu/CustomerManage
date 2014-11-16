using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Event.Request
{
    public class GetDrawMethodListRP : IAPIRequestParameter
    {
        public  string EventTypeId { get; set; }

        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }

        public void Validate()
        {
        }
    }
}
