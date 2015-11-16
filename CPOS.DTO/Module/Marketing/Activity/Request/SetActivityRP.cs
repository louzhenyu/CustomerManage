using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Activity.Request
{
    public class SetActivityRP : IAPIRequestParameter
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
        /// 活动开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 是否长期
        /// </summary>
        public int IsLongTime { get; set; }
        /// <summary>
        /// 会员卡关联类型ID
        /// </summary>
        public string VipCardTypeID { get; set; }
        /// <summary>
        ///  会员分组ID
        /// </summary>
        public string VIPID { get; set; }
        public void Validate() { }
    }
}
