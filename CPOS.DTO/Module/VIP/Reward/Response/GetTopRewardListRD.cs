using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Reward.Response
{
    public class GetTopRewardListRD : IAPIResponseData
    {
        /// <summary>
        /// 我的打赏信息
        /// </summary>
        public RewardInfo MyReward { get; set; }
        /// <summary>
        /// 打赏列表
        /// </summary>
        public IList<RewardInfo> RewardList { get; set; }        
    }
    /// <summary>
    /// 打赏信息
    /// </summary>
    public class RewardInfo
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 员工头像
        /// </summary>
        public string UserPhoto { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public int StarLevel { get; set; }
        /// <summary>
        /// 排名
        /// </summary>
        public int Rank { get; set; }
        /// <summary>
        /// 打赏收入
        /// </summary>
        public decimal? RewardIncome { get; set; }        
    }
}
