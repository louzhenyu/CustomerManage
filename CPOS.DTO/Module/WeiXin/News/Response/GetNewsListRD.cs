using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.News.Response
{
    public class GetNewsListRD : IAPIResponseData
    {
        public NewsInfo[] NewsList { get; set; }
        public int TotalPages { get; set; }
    }

    public class NewsInfo
    {
        public string NewsId { get; set; }
        public string NewsName { get; set; }
        public string NewsTypeName { get; set; }
        public string PublishTime { get; set; }
    }
}
