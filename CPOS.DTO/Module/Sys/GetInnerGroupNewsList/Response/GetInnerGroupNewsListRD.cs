using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Sys.InnerGroupNews.Response
{
    public class GetInnerGroupNewsListRD : IAPIResponseData
    {
        public List<InnerGroupNewsInfo> InnerGroupNewsList { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
    public class InnerGroupNewsInfo
    {
        public string GroupNewsId { get; set; }
        public string Title { get; set; }

        public string MsgTime { get; set; }
        public string Text { get; set; }

        public string IsRead { get; set; }

        public int? BusType { get; set; }
    }
}
