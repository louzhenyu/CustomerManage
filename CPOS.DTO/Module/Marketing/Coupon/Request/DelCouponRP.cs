using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Coupon.Request
{
    public class DelCouponRP : IAPIRequestParameter
    {
        public Guid CouponTypeID { get; set; }
        public void Validate()
        {

        }
    }
}
