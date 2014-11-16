using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Event.Request
{
    public class GetEventListRP : IAPIRequestParameter
    {
        public string EventTypeId { get; set; }
        public string EventName { get; set; }
        public bool? BeginFlag { get; set; }
        public bool? EndFlag { get; set; }

        public  int DrowMethodId { get; set; }
        
        public int? EventStatus { get; set; }
        
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public void Validate()
        {
           
        }
    }
}
