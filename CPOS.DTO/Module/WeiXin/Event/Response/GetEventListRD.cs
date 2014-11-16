using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Event.Response
{
    public class GetEventListRD : IAPIResponseData
    {
        public int TotalPages { get; set; }
        public EventInfo[] EventList { get; set; }
    }

    public class EventInfo
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string EventTypeId { get; set; }
        public string EventTypeName { get; set; }

        public string BegTime { get; set; }
        public string EndTime { get; set; }
        public int EventStatus { get; set; }

        public string CityName { get; set; }
        public string EventStatusName { get; set; }
        public string DrawMethod { get; set; }
        //public int DisplayIndex { get; set; }
    }
}
