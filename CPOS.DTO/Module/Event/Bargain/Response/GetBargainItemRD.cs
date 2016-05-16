using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetBargainItemRD : IAPIResponseData
    {
        public string EventName { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }

        public List<ItemMappingInfo> ItemMappingInfoList { get; set; }
    }

    public class ItemMappingInfo {
        public string EventItemMappingID { get; set; }
        public string ItemName { get; set; }
        /// <summary>
        /// 单人限购数量
        /// </summary>
        public int SinglePurchaseQty { get; set; }

        public string ItemId { get; set; }
    }
}
