using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GeEventItemDetailListRD : IAPIResponseData
    {
        public List<EventItemDetailInfo> EventItemDetailList { get; set; }

        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
    }
    public class EventItemDetailInfo
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


        public string order_no { get; set; }
        public string create_time { get; set; }
        public string vipname { get; set; }
        public string viprealname { get; set; }
        public string DeliveryName { get; set; }


    }
}
