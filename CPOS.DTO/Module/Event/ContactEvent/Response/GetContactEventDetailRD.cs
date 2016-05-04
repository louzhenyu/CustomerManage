using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.ContactEvent.Response
{
    public class GetContactEventDetailRD : IAPIResponseData
    {
        public Guid? ContactEventId { get; set; }
        public string ContactTypeCode { get; set; }
        public string ContactEventName { get; set; }
        public string PrizeType { get; set; }
        public int? PrizeCount { get; set; }

        public string BeginDate { get; set; }
        public string EndDate { get; set; }

        public int? Integral{get;set;}

        public string CouponTypeID{get;set;}
        public string EventId{get;set;}
        public int? ChanceCount{get;set;}

        public string ShareEventId { get; set; }
        public string ShareEventName { get; set; }

        public string EventName { get; set; }

        public string CouponTypeName { get; set; }
        public int UnLimited { get; set; }


    }
}
