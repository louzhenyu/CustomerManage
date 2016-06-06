using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetVipGoldHomeListRD : IAPIResponseData
    {
        /// <summary>
        /// 微信粉丝
        /// </summary>
        public int? OnlineFansCount { get; set; }
        /// <summary>
        /// 已完成微信注册
        /// </summary>
        public int? OnlineOnlyFansCount { get; set; }
        /// <summary>
        /// 会员总数
        /// </summary>
        public int? VipCount { get; set; }
        /// <summary>
        /// 已完成注册人数
        /// </summary>
        public int? OnlineVipCount { get; set; }
        /// <summary>
        /// 未完成注册人数
        /// </summary>
        public int? OfflineVipCount { get; set; }
        /// <summary>
        /// 近30天活跃会员人数
        /// </summary>

        public int? OnlineVipCountFor30DayOrder { get; set; }
        /// <summary>
        /// 近30天活跃会员占比
        /// </summary>
        public decimal? OnlineVipCountFor30DayOrderM2M { get; set; }
        /// <summary>
        /// 近30天活跃会员占比占注册会员总数
        /// </summary>
        public decimal? OnlineVipCountPerFor30DayOrder { get; set; }
        /// <summary>
        /// 注册会员近30天销量贡献
        /// </summary>
        public decimal? OnlineVipSalesFor30Day { get; set; }
        /// <summary>
        /// 注册会员近30天销量贡献增减比
        /// </summary>
        public decimal? OnlineVipSalesPerFor30Day { get; set; }

        public decimal? OnlineVipSalesFor30DayM2M { get; set; }

        public decimal? OnlineVipSalesPerFor30DayM2M { get; set; }

        public decimal? OnlineVipCountPerFor30DayOrderM2M { get; set; }
    }
}