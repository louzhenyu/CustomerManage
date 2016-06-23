using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;


namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetSuperRetailTraderItemRD : IAPIResponseData
    {
       public  List<APISuperRetailTraderItemInfo> ItemList { get; set; }

       public int TotalCount { get; set; }

       public int TotalPageCount { get; set; }
    }

    public class APISuperRetailTraderItemInfo
    {
        public string ItemId { get; set; }

        public string ItemName { get; set; }

        public string ImageUrl { get; set; }

        public int SoldQty { get; set; }

        public decimal DistributerPrice { get; set; }

        public decimal SkuCommission { get; set; }
    }
}
