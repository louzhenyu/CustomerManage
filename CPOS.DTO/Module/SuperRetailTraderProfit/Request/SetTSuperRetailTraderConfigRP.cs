using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
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
        public bool? IsFlag { get; set; }
        public void Validate()
        {

            #region 修改协议
            if (IsFlag == true)
            {
                //修改协议
                if (String.IsNullOrEmpty(Id))
                {
                    throw new APIException("参数[id " + Id + "]无效");
                }
                if (String.IsNullOrEmpty(Agreement))
                {
                    throw new APIException("参数[Agreement " + Agreement + "]无效");
                }
                if (String.IsNullOrEmpty(AgreementName))
                {
                    throw new APIException("参数[AgreementName " + AgreementName + "]无效");
                }

            }
            else
            {
                decimal? sum = Cost + CustomerProfit + DistributionProfit + SkuCommission;

                if (sum == null || sum > 100 || sum < 0)
                {
                    throw new APIException("规则不能超过100%。") { ErrorCode = 135 };
                }
                if (Cost <= 0 || CustomerProfit <= 0 || DistributionProfit <= 0 || SkuCommission <= 0 || Cost > 100 || CustomerProfit > 100 || DistributionProfit > 100 || SkuCommission > 100)
                {
                    throw new APIException("规则不能超过100%，或规则不能小于0") { ErrorCode = 135 };
                }
            }
            #endregion

        }
    }
}
