using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetItemListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 商品品类ID
        /// </summary>
        public string ItemCategoryId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 当前页条数
        /// </summary>
        public int PageSize { get; set; }
        public void Validate(){}
    }
}
