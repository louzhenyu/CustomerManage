using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetSuperRetailTraderProfitConfigRP : IAPIRequestParameter
    {
        public List<GetSuperRetailTraderProfitConfigInfoRP> lst { get; set; }
        public GetSuperRetailTraderProfitConfigRP()
        {
            lst = new List<GetSuperRetailTraderProfitConfigInfoRP>();
        }
        public void Validate()
        {

        }
    }

    public class GetSuperRetailTraderProfitConfigInfoRP
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