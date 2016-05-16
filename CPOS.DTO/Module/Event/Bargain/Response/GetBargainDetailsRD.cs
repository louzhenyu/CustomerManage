using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetBargainDetailsRD : IAPIResponseData
    {
        public string EventItemMappingID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }

        public int SinglePurchaseQty { get; set; }

        public decimal BargaingingInterval { get; set; }
        public List<SkuInfos> SkuInfoList { get; set; }
    }

    public class SkuInfos
    {
        public string SkuID { get; set; }
        public string SkuName { get; set; }
        public decimal Price { get; set; }
        public EventSkuInfo EventSkuInfo { get; set; }
    }
}
