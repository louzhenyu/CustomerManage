using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Event.Response
{
    public class GetEventTypeListRD : IAPIResponseData
    {
        public int TotalPages { get; set; }
        public EventTypeInfo[] EventTypeList { get; set; }
    }

    public class EventTypeInfo
    {
        public string EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        //public int DisplayIndex { get; set; }
    }
  

}
