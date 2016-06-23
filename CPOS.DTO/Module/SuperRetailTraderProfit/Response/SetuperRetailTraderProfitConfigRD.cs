using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class SetuperRetailTraderProfitConfigRD : IAPIResponseData
    {
        public List<SetSuperRetailTraderProfitConfigInfo> lst { get; set; }
        public SetuperRetailTraderProfitConfigRD()
        {
            lst = new List<SetSuperRetailTraderProfitConfigInfo>();
        }
    }

    public class SetSuperRetailTraderProfitConfigInfo
    {
        public int Level { get; set; }
        public decimal? Profit { get; set; }
        public string Status { get; set; }
        public Guid? SuperRetailTraderProfitConfigId { get; set; }
    }
}