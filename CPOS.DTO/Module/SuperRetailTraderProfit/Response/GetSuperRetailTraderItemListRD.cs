using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetSuperRetailTraderItemListRD : IAPIResponseData
    {
        public List<SuperRetailTraderItemInfo> SuperRetailTraderItemList { get; set; }

        public int TotalCount { get; set; }

        public int TotalPageCount { get; set; }
    }
    public class SuperRetailTraderItemInfo 
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public string ItemId { get; set; }
        
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string PropName { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 分销库存
        /// </summary>
        public int DistributerStock { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public decimal DistributerCostPrice { get; set; }

        /// <summary>
        /// 分销价
        /// </summary>
        public decimal DistributePirce { get; set; }

        /// <summary>
        /// 商家利润
        /// </summary>
        public decimal CustomerProgit { get; set; }

        /// <summary>
        /// 状态 10 -上架 90 -下架
        /// </summary>
        public int Status { get; set; }
    }
}
