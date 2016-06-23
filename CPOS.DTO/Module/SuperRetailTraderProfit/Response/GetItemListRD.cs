using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetItemListRD : IAPIResponseData
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<BaseItemInfo> ItemList { get; set; }

        public int TotalPageCount { get; set; }

        public int TotalCount { get; set; }
    }

    public class BaseItemInfo
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
        /// 商品编码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// 规格1
        /// </summary>
        public string PropName { get; set; }


        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal SalesPrice { get; set; }

    }


}
