using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetBuyerListRD : IAPIResponseData
    {
        public int TotalCount { get; set; }

        public int TotalPage { get; set; }

        public List<Buyer> BuyerList { get; set; }
    }

    public class Buyer
    {
        public string BuyerId { get; set; }

        public string BuyerName { get; set; }

        public string HeadImgUrl { get; set; }

        public string CreateTime { get; set; }

        public string SalesPrice { get; set; }
    }
}
