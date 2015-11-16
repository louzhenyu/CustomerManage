using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipCardTransLog.Request
{
    public class GetVipCardTransLogListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string VipCardCode { get; set; }

        public void Validate() { }
    }
}
