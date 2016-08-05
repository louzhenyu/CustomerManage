using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Response
{
    public class GetActivityListRD : IAPIResponseData
    {
        /// <summary>
        /// 总叶数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 营销活动集合
        /// </summary>
        public List<ActivityInfo> ActivityList { get; set; }
    }

    public class ActivityInfo
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public string ActivityID { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int ActivityType { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 目标群体
        /// </summary>
        public string TargetGroups { get; set; }
        /// <summary>
        /// 起止时间
        /// </summary>
        public string BeginEndData { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 实发人群数
        /// </summary>
        public int SendCouponQty { get; set; }
        /// <summary>
        /// 是否长期
        /// </summary>
        public int IsLongTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 是否适用所有目标群体
        /// </summary>
        public int IsAllCardType { get; set; }
        /// <summary>
        /// 参与人数
        /// </summary>
        public int TargetCount { get; set; }
    }
}
