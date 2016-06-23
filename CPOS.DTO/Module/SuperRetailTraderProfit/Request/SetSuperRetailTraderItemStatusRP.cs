using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class SetSuperRetailTraderItemStatusRP : IAPIRequestParameter
    {
        public List<SuperRetailTraderItem> ItemIdList { get; set; }

        public int Status { get; set; }
        public void Validate() { }
    }
    public class SuperRetailTraderItem
    {
        public string ItemId { get; set; }

        public string SkuId { get; set; }

        public int DistributerStock { get; set; }

        public decimal DistributerCostPrice { get; set; }

        public int IsAllSelected { get; set; }
    }
}
