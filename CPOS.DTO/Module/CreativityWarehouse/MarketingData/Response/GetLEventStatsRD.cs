using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GetLEventStatsRD : IAPIResponseData
    {
        public LeventsStats LeventsStats { get; set; }
    }
    public class LeventsStats
    {
        /// <summary>
        /// 现金券
        /// </summary>
        public decimal CASHCouponValue { get; set; }
        /// <summary>
        /// 折扣券
        /// </summary>
        public decimal DISCOUNTCouponValue { get; set; }
        /// <summary>
        /// 礼物券
        /// </summary>
        public int GIFTCouponValue { get; set; }
        /// <summary>
        /// 积分总额
        /// </summary>
        public int PointSum { get; set; }
        /// <summary>
        /// 礼品份数
        /// </summary>
        public int PrizeCount { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FocusVipCount { get; set; }
        /// <summary>
        /// 会员数
        /// </summary>
        public int RegVipCount { get; set; }
    }
}
