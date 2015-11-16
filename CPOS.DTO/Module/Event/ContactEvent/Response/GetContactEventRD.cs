using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.ContactEvent.Response
{
    public class GetContactEventRD : IAPIResponseData
    {
        public Guid ContactEventId { get; set; }
        public string ContactEventName { get; set; }
        public string ContactTypeName { get; set; }
        /// <summary>
        /// 奖励名称
        /// </summary>
        public string Reward { get; set; }
        /// <summary>
        /// 奖励类型
        /// </summary>
        public int RewardType { get; set; }
        /// <summary>
        /// 状态类型
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }
    }
}
