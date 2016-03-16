using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Dealer.Response
{
    public class GetVipDealerSpreadCodeRD : IAPIResponseData
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string imageUrl { get; set; }
        public string VipName { get; set; }
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 二维码ID
        /// </summary>
        public string QRCodeId { get; set; }
    }
}
