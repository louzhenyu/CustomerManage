using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;


namespace JIT.CPOS.DTO.Module.Event.Bargain.Request
{
    public class GetPanicBuyingKJItemDetailRP : IAPIRequestParameter
    {
        public string EventId { get; set; }

        public string ItemId { get; set; }

        public string SkuId { get; set; }

        public string VipId { get; set; }

        public string KJEventJoinId { get; set; }

        public int type { get; set; } // 1-抢购商品页面，2-帮砍商品页面
        public void Validate() { }
    }
}
