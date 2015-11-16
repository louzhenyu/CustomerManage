using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Response
{
    public class GetActivityDeatilRD : IAPIResponseData
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public string ActivityID { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 是否长期
        /// </summary>
        public string IsLongTime { get; set; }
        /// <summary>
        /// 多倍积分
        /// </summary>
        public decimal PointsMultiple { get; set; }
        /// <summary>
        /// 持卡人数
        /// </summary>
        public int holderCardCount { get; set; }
        /// <summary>
        /// 卡类型ID
        /// </summary>
        public string VipCardTypeID { get; set; }
        /// <summary>
        /// 会员分组ID
        /// </summary>
        public string VipGroupingID { get; set; } 
        /// <summary>
        /// 奖品集合
        /// </summary>
        public List<PrizesInfo> PrizesInfoList { get; set; }
        /// <summary>
        /// 消息集合
        /// </summary>
        public List<ActivityMessageInfo> ActivityMessageInfoList { get; set; }
    }
}
