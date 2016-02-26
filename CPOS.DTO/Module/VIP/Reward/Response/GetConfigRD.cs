using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Reward.Response
{
    public class GetConfigRD : IAPIResponseData
    {
        /// <summary>
        /// 打赏类型：0=两者，1=打赏，2=评分
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 员工信息
        /// </summary>
        public IList<UserInfo> UserInfo { get; set; }
        /// <summary>
        /// 打赏金额列表
        /// </summary>
        public IList<RewardAmountInfo> AmountList { get; set; }        
    }
    /// <summary>
    /// 打赏金额
    /// </summary>
    public class RewardAmountInfo
    {
        public decimal? Amount { get; set; }        
    }
    /// <summary>
    /// 员工信息
    /// </summary>
    public class UserInfo
    {
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
        /// 被打赏数
        /// </summary>
        public int RewardCount { get; set; }
    }
}
