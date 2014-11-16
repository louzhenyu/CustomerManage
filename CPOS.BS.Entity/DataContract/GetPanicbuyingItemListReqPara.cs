using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Entity
{
    public class GetPanicbuyingItemListReqPara
    {
        public string itemName { get; set; }//      模糊查询商品名称
        public string itemTypeId { get; set; }//    商品类别标识
        public Guid? eventId { get; set; }//      活动标识
        public string eventTypeId { get; set; }//    商品类别 1=团购 2=抢购
        public string page { get; set; }//      页码
        public string pageSize { get; set; }//      页面数量	

        public bool IsValid(out string msg)
        {
            msg = string.Empty;
            return true;
        }
    }
}