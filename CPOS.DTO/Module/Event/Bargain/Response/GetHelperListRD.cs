using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetHelperListRD : IAPIResponseData
    {
        public int TotalCount { get; set; }

        public int TotalPage { get; set; }

        public List<Helper> HelperList { get; set; }
    }

    public class Helper
    {
        public string HelperId { get; set; }

        public string HelperName { get; set; }

        public string HeadImgUrl { get; set; }

        public string CreateTime { get; set; } //参与时间

        public string BargainPrice { get; set; }
    }
}
