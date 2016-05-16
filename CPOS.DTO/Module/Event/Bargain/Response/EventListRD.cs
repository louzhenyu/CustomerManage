using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class EventListRD : IAPIResponseData
    {
        public List<EventInfo> EventList { get; set; }

        public int IsAllEnd { get; set; } //1-全都为已结束的活动
    }

    public class EventInfo
    {
        public string EventId { get; set; } // 活动Id

        public string BeginTime { get; set; } //开始时间

        public string EndTime { get; set; } //结束时间

        public long Seconds { get; set; } //现在到结束时间
        public int Status { get; set; } //状态 0-进行中 1-即将开始 2-已结束

        public List<EventItem> EventItemList { get; set; }
    }

    public class EventItem
    {

        public string ItemId { get; set; }

        public string ItemName { get; set; }

        public string ImageUrl { get; set; }

        public string ImageUrlThumb { get; set; }

        public string ImageUrlMiddle { get; set; }

        public string ImageUrlBig { get; set; }

        public decimal Price { get; set; }

        public decimal BasePrice { get; set; }

        public int Qty { get; set; }

        public int PromotePersonCount { get; set; }
    }
}
