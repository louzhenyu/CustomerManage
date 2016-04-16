using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response
{
    public class LingkinRD : IAPIResponseData
    {
        public int errCode { get; set; }
        public string errMsg { get; set; }
        public string sign { get; set; }
        public string noncestr { get; set; }
        public string timestamp { get; set; }
        public string appid { get; set; }
        public string secret { get; set; }

        public UrlInfo UrlInfo { get; set; }


    } 
    public class UrlInfo
    {
        public string Name{get;set;}
        public string Url{get;set;}
    }
}
