using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
    public class CTWEventShareLogRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
        public string CTWEventId { get; set; }
        /// <summary>
        /// 分享者id
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 分享者微信id
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 被分享者微信id
        /// </summary>
        public string BeSharedOpenId { get; set; }
        /// <summary>
        /// 被分享者id
        /// </summary>
        public string BEsharedUserId { get; set; }
        /// <summary>
        /// 分享的地址
        /// </summary>
        public string ShareURL { get; set; }

    }
}
