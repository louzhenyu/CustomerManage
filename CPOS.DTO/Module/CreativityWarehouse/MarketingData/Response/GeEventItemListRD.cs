using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GeEventItemListRD : IAPIResponseData
    {
        public List<EventItemInfo> EventItemList { get; set; }

        public int TotalCount { get; set; }
        public int TotalPage { get; set; }

    }

    public class EventItemInfo
    {

        public string item_id { get; set; }
        public string SkuID { get; set; }
        public string item_name { get; set; }
        public string SkuName { get; set; }
        public decimal price { get; set; }
        public decimal SalesPrice { get; set; }
           public int Qty { get; set; }
    public int KeepQty { get; set; }
          public int SoldQty { get; set; }
       public int InverTory { get; set; }
        public int TotalSales { get; set; }
        public int TurnoverRate { get; set; }


    }
}
