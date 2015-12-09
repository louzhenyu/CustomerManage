using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JIT.CPOS.DTO.Module.Marketing.Coupon.Response
{
    public class GrantCouponRD : IAPIResponseData
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string FollowUrl { get; set; }
        /// <summary>
        /// 是否被领券
        /// </summary>
        public int IsAccept { get; set; }

    }
}
