using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response
{
    public class SearchKeyWordRD : IAPIResponseData
    {
        public int TotalPages { get; set; }
        public SearchKeyWordInfo[] SearchKeyList { get; set; }
    }

    public class SearchKeyWordInfo
    {
        public int DisplayIndex { get; set; }
        public string KeyWord { get; set; }
        public string ReplyId { get; set; }
    }
}
