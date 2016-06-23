using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class SetuperRetailTraderProfitConfigRP : IAPIRequestParameter
    {
        public List<SetSuperRetailTraderProfitConfigInfoRP> ProfitConfigList { get; set; }
        public void Validate()
        {
            decimal? ProfitMoney = ProfitConfigList.Where(m => m.Level != 1 && m.Status == "10").Sum(m => m.Profit); //分销体系百分比
            if (ProfitMoney == 0)
            {
                throw new APIException("分润体系百分比不能设置为0") { ErrorCode = 135 };
            }

            foreach (var item in ProfitConfigList)
            {
                if (item.Status == "10")
                {
                    if (item.Profit <= 0 || item.Profit >= 100)
                    {
                        throw new APIException("单个分润体系百分比不能设置为负数或者超过100") { ErrorCode = 135 };
                    }
                }
            }
        }
    }
    public class SetSuperRetailTraderProfitConfigInfoRP
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
