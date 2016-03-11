using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipCard.Response
{
    public class GetVipCardRD : IAPIResponseData
    {
        public string VipCardCode { get; set; } //卡号
    }
}
