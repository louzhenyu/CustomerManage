using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.RetailTrader.Request
{
    public class SetTSuperRetailTraderConfigRP : IAPIRequestParameter
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 相关Id
        /// </summary>
        public Guid? RefId { get; set; }
        /// <summary>
        /// 协议内容
        /// </summary>
        public string Agreement { get; set; }
        /// <summary>
        /// 协议名称
        /// </summary>
        public string AgreementName { get; set; }
        /// <summary>
        /// 商品销售佣金比例
        /// </summary>
        public decimal? SkuCommission { get; set; }
        /// <summary>
        /// 分销商分润比例
        /// </summary>
        public decimal? DistributionProfit { get; set; }
        /// <summary>
        /// 商家分润比例
        /// </summary>
        public decimal? CustomerProfit { get; set; }
        /// <summary>
        /// 商品成本比例
        /// </summary>
        public decimal? Cost { get; set; }
        public void Validate()
        {

        }
    }
}
