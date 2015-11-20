using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.ContactEvent.Response
{
    public class GetContactEventListRD : IAPIResponseData
    {

        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        /// 会员信息集合
        /// </summary>
        public IList<ContactEvent> ContactEventList { get; set; }
    }
    public class ContactEvent
    {
        public Guid ContactEventId { get; set; }
        public string ContactTypeCode { get; set; }
        public string ContactEventName { get; set; }
        public string ContactTypeName { get; set; }
        /// <summary>
        /// 奖励名称
        /// </summary>
        public string Reward { get; set; }
        /// <summary>
        /// 奖励类型    Point,Coupon,Chance
        /// </summary>
        public string PrizeType { get; set; }
        public int PrizeCount { get; set; }

        public string RewardNumber { get; set; }

        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string Date { get; set; }

        public int? Integral { get; set; }

        public string CouponTypeID { get; set; }
        public string EventId { get; set; }
        public string ChanceCount { get; set; }
        /// <summary>
        /// 状态类型
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        public string ShareEventId { get; set; }

        public int JoinCount { get; set; }
    }
}
