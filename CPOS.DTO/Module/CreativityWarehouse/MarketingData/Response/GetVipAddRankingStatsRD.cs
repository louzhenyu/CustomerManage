using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GetVipAddRankingStatsRD : IAPIResponseData
    {
        /// <summary>
        /// 游戏会员增长排行
        /// </summary>
        public List<GameVipAddRankingInfo> GameVipAddRankingList { get; set; }
        /// <summary>
        /// 促销会员增长排行
        /// </summary>
        public List<PromotionVipAddRankingInfo> PromotionVipAddRankingList { get; set; }
    }
    /// <summary>
    /// 游戏会员增长排行
    /// </summary>
    public class GameVipAddRankingInfo
    {
        public string DateStr { get; set; }
        public int FocusVipCount { get; set; }
    }
    /// <summary>
    /// 促销会员增长排行
    /// </summary>
    public class PromotionVipAddRankingInfo
    {
        public string DateStr { get; set; }
        public int RegVipCount { get; set; }
    }

}
