using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Response
{
    public class GetInnerGroupNewsByIdRD : IAPIResponseData
    {
        public InnerGroupNewsInfo NewsInfo { get; set; }
        public int PageIndex { get; set; }
        public int TotalPageCount { get; set; }
    }

    public class InnerGroupNewsInfo
    {
        public string GroupNewsId { get; set; }
        public string Title { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Text { get; set; }
        public string IsRead { get; set; }
    }
}
