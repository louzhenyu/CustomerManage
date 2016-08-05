using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetRechargeActivityListRD : IAPIResponseData
    {
        /// <summary>
        /// 会员头像
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 会员级别名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 当前剩余积分
        /// </summary>
        public int Integration { get; set; }
        /// <summary>
        /// 当前会员余额
        /// </summary>
        public decimal? VipAmount { get; set; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 获取充值信息列表
        /// </summary>
        public List<RechargeStrategyInfo> RechargeStrategyList { get; set; }
    }

    public class RechargeStrategyInfo
    {
        /// <summary>
        /// 活动主标识
        /// </summary>
        public string ActivityID { get; set; }
        /// <summary>
        /// 满多少金额
        /// </summary>
        public decimal RechargeAmount { get; set; }
        /// <summary>
        /// 送多少金额
        /// </summary>
        public decimal GiftAmount { get; set; }
        /// <summary>
        /// 活动类型 Step=阶梯;Superposition=叠加;
        /// </summary>
        public string RuleType { get; set; }
    }
}
