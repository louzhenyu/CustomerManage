using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request
{
    /// <summary>
    /// 制卡请求对象
    /// </summary>
    public class MakeVipCardRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡介质
        /// </summary>
        public string CardMedium { get; set; }
        /// <summary>
        /// 地区编号
        /// </summary>
        public string RegionNumber { get; set; }
        /// <summary>
        /// 卡类型编号
        /// </summary>
        public string VipCardTypeCode { get; set; }
        /// <summary>
        /// 卡前缀
        /// </summary>
        public string CardPrefix { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public int BatchNo { get; set; }
        public void Validate()
        {

        }
    }
}
