using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Coupon.Request
{
    public class GetCouponTypeListRP : IAPIRequestParameter
    {
        public string CouponTypeName{get;set;}
        public string ParValue { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        {

        }
    }
}
