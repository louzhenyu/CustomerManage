using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetKJEventJoinListRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }

        public List<KJEventJoinInfo> KJEventJoinInfoList { get; set; }
    }

    public class KJEventJoinInfo {
        public string KJEventJoinId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal BasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 活动状态(1:进行中，2：可看时间已到,3:已结束,4:暂停)
        /// </summary>
        public int Status { get; set; }

        public string ItemImageURL { get; set; }

        public string ItemId { get; set; }

        public int Qty { get; set; }

        public string EventId { get; set; }

        public string SkuID { get; set; }

        public string VipId { get; set; }
    }
}
