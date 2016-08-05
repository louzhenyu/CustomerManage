using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    public class GetSysVipCardTypeByLevelRD : IAPIResponseData
    {
        /// <summary>
        /// 开卡礼集合信息
        /// </summary>
        public List<WXVipCardUpgradeRewardInfo> VipCardUpgradeRewardInfoList { get; set; }

        public GetSysVipCardTypeByLevelRD()
        {
            VipCardUpgradeRewardInfoList = new List<WXVipCardUpgradeRewardInfo>();
        }
    }

    public class WXVipCardUpgradeRewardInfo
    {
        /// <summary>
        /// 标题{基本权益 特殊化处理}
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 基本权益图片路径{特殊化处理}
        /// </summary>
        public string ImagesUrl { get; set; }
        /// <summary>
        /// 开卡礼主标识
        /// </summary>
        public Guid? CardUpgradeRewardId { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 券类型名称
        /// </summary>
        public string CouponTypeName { get; set; }
        /// <summary>
        /// 类别 1=基本权益;2=开卡礼
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 优惠券数量
        /// </summary>
        public int CouponNum { get; set; }
    }
}