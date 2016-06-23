using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class SetSuperRetailTraderItemRP : IAPIRequestParameter
    {
        public List<ItemIdInfo> ItemIdList { get; set; }
        public void Validate(){}
    }

    public class ItemIdInfo
    {
        public string ItemId { get; set; }

        public string SkuId { get; set; }
    }

    public class SkuIdInfo 
    {
        public string SkuId { get; set; }
    }


}
