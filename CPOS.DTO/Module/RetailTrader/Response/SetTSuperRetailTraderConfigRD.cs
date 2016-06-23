using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.RetailTrader.Response
{
   public class SetTSuperRetailTraderConfigRD : IAPIResponseData
    {
        /// <summary>
        /// 协议名称
        /// </summary>
        public string Agreement { get; set; }
        /// <summary>
        /// 协议内容
        /// </summary>
        public string AgreementName { get; set; }
        /// <summary>
        /// 商家分润比例
        /// </summary>
        public string CustomerProfit { get; set; }
        /// <summary>
        /// 分销商分润比例
        /// </summary>
        public string DistributionProfit { get; set; }
        public string Id { get; set; }
        public string MustBuyAmount { get; set; }
        /// <summary>
        /// 商品销售佣金比例
        /// </summary>
        public string SkuCommission { get; set; }
        /// <summary>
        /// 商品成本比例
        /// </summary>
        public string Cost { get; set; }
    }
}