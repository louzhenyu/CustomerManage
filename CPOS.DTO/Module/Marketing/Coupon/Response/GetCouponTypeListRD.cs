using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Coupon.Response
{
    public class GetCouponTypeListRD : IAPIResponseData
    {
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
        public CouponTypeInfo[] CouponTypeList { get; set; }
    }

    public class CouponTypeInfo
    {
        public Guid? CouponTypeID { get; set; }
        public string CouponTypeName { get; set; }

        public string ValidityPeriod { get; set; }
        public decimal? ParValue { get; set; }
        public int? IssuedQty { get; set; }
        public int? SurplusQty { get; set; }

        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 年月日格式
        /// </summary>
        public string BeginTimeDate { get; set; }
        /// <summary>
        /// 年月日格式
        /// </summary>
        public string EndTimeDate { get; set; }
        public int? ServiceLife { get; set; }
        public int IsNotLimitQty { get; set; }

    }
}
