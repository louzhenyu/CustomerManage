using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Entity
{
    public class GetPanicbuyingItemDetailReqPara
    {
        /// <summary>
        /// 商品标识
        /// </summary>
        public string itemId { get; set; }

        /// <summary>
        /// 活动ID
        /// </summary>
        public Guid eventId { get; set; }

        public bool IsValid(out string msg)
        {
            msg = string.Empty;
            return true;
        }
    }
}