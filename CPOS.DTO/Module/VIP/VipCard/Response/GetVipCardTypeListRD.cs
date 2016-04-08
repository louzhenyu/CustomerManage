using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipCard.Response
{
    public class GetVipCardTypeListRD : IAPIResponseData
    {
        public List<VipCardTypeInfo> VipCardTypeIdList { get; set; }
    }
    public class VipCardTypeInfo
    {
        public string VipCardTypeId{get;set;}

        public string VipCardTypeName { get; set; }
    }
}
