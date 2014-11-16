using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.News.Response
{
    public class GetNewsTypeListRD : IAPIResponseData
    {
        public NewsTypeInfo[] NewsTypeList { get; set; }
    }

    public class NewsTypeInfo
    {
        public string NewsTypeId { get; set; }
        public string NewsTypeName { get; set; }
    }
}
