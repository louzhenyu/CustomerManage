using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Account.Response
{
    public class GetAccountListRD : IAPIResponseData
    {
        public int TotalPages { get; set; }
        public WXApplicationInfo[] AccountList { get; set; }
    }

    public class WXApplicationInfo
    {
        public string ApplicationId { get; set; }
        public string WeiXinName { get; set; }
    }
}
