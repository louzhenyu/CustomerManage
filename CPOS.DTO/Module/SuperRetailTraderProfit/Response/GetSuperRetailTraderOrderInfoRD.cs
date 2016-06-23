using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetSuperRetailTraderOrderInfoRD : IAPIResponseData
    {
        public string SuperRetailTraderId { get; set; }
        public List<SuperRetailTraderOrderInfo> OrderInfo { get; set; }
    }
    public class SuperRetailTraderOrderInfo
    {
        public string OrderNo { get; set; }
        public string Name { get; set; }
        public string OrderActualAmount { get; set; }
        public string OrderDate { get; set; }
        public decimal Commission { get; set; }
    }
}
