using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetSuperRetailTraderProfitConfigRD : IAPIResponseData
    {
        public List<GetSuperRetailTraderProfitConfigInfoRD> ProfitConfigList { get; set; }
        public GetSuperRetailTraderProfitConfigRD()
        {
            ProfitConfigList = new List<GetSuperRetailTraderProfitConfigInfoRD>();
        }
    }

    public class GetSuperRetailTraderProfitConfigInfoRD
    {
        /// <summary>
        /// 主标识	首次添加信息时不用传递
        /// </summary>
        public Guid? SuperRetailTraderProfitConfigId { get; set; }
        /// <summary>
        /// 分润比例
        /// </summary>
        public decimal? Profit { get; set; }
        /// <summary>
        /// 等级	佣金的level=1
        /// </summary>
        public int? Level { get; set; }
        /// <summary>
        /// 状态	10=有效90=失效
        /// </summary>
        public string Status { get; set; }
    }
}