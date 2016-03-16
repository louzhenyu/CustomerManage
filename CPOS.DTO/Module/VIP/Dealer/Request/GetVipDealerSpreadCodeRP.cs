using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Dealer.Request
{
    public class GetVipDealerSpreadCodeRP : IAPIRequestParameter
    {
        public void Validate()
        {

        }
        /// <summary>
        /// 经销商会员分享者ID
        /// </summary>
        public string ShareUserId { get; set; }
        /// <summary>
        /// 二维码ID
        /// </summary>
        public string QRCodeId { get; set; }
    }
}
