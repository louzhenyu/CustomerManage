using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class BargainListRD : IAPIResponseData
    {

        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public IList<BargainEvent> BargainList { get; set; }
    }
    public class BargainEvent
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ItemCount { get; set; }
        /// <summary>
        /// 发起人数
        /// </summary>
        public int SponsorCount { get; set; }
        /// <summary>
        /// 参与人数
        /// </summary>
        public int JoinCount { get; set; }

        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string EventStatus { get; set; }
        public string CreateTime { get; set; }

    }
}
