using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetSuperRetailTraderItemDetailRD : IAPIResponseData
    {
        public string ItemId{get;set;}
        public string ItemName { get; set; }

        public List<ImageInfo> ImageList { get; set; }

        public decimal DistributerPrice { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public string Prop1 { get; set; }

        public string Prop2 { get; set; }

        public string DeliveryDesc { get; set; }

        public string ItemIntroduce { get; set; }

        public List<APISkuInfo> SkuList { get; set; }
    }

    public class ImageInfo
    {
        public string imageURL { get; set; }
    }

    public class APISkuInfo
    {
        public string SkuID{get;set;}

        public string PropName1{get;set;}

        public string PropName2{get;set;}

        public decimal DistributerPrice{get;set;}

        public int DistributerStock{get;set;}

        public int SoldQty{get;set;} 

    }
}
