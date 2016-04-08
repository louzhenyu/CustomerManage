using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipCard.Resquest
{
    public class GetVipCardRP : IAPIRequestParameter
    {
        public string VipCardCode { get; set; } //卡号
        public int ObjectTypeId { get; set; } //卡类型
        public void Validate() { }
    }
}
