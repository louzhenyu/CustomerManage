using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Coupon.Request
{
    public class GrantCouponRP : IAPIRequestParameter
    {
        /// <summary>
        /// 赠送者
        /// </summary>
        public string Giver { get; set; }
        /// <summary>
        /// 优惠券
        /// </summary>
        public string CouponId { get; set; }
        public void Validate()
        {

        }
    }
}
