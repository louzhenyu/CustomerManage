using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Request
{
    public class SetBargainDetailsRP : IAPIRequestParameter
    {
        public string EventItemMappingID { get; set; }
        public string EventId { get; set; }
        /// <summary>
        /// 单人限购数量
        /// </summary>
        public int SinglePurchaseQty { get; set; }
        public int BargaingingInterval { get; set; }
        public string ItemId { get; set; }

        public List<EventSkuInfo> EventSkuInfoList { get; set; }
        public void Validate()
        {
            
        }
    }

    public class EventSkuInfo{
        public string EventSKUMappingId { get; set; }
        public string EventItemMappingID { get; set; }
        public string SkuID { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal BasePrice { get; set; }
        public decimal BargainStartPrice { get; set; }
        public decimal BargainEndPrice { get; set; }
        
        public int IsDelete { get; set; }
    }
}
