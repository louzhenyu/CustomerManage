using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.ContactEvent.Request
{
   public class SetContactEventRP : IAPIRequestParameter
    {
       public string ContactEventId { get; set; }
        public String ContactTypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContactEventName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Point：积分，Coupon：优惠券，Chance：参加活动次数
        /// </summary>
        public String PrizeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Integral { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String[] CouponTypeID { get; set; }
       /// <summary>
       /// 奖品数量
       /// </summary>
        public Int32? PrizeCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ChanceCount { get; set; }
       /// <summary>
       /// 触点为分享时的分享活动Id
       /// </summary>
        public string ShareEventId { get; set; }
       /// <summary>
        /// 奖励次数OnlyOne,Once a day，unlimited
       /// </summary>
        public string RewardNumber { get; set; }
       /// <summary>
        /// Add,Append
       /// </summary>
        public string Method { get; set; }
        public int UnLimited { get; set; }
        public void Validate()
        {
        }
    }
}
