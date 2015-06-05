using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Coupon.Request
{
    public class SetCouponRP : IAPIRequestParameter
    {
        public string CouponTypeID { get; set; }
        public string CouponTypeName { get; set; }
        public string CouponTypeDesc { get; set; }
        public decimal ParValue { get; set; }
        public int IssuedQty { get; set; }
        public decimal ConditionValue { get; set; }
        public int UsableRange { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServiceLife { get; set; }
        public int SuitableForStore { get; set; }
        public ObjectInfo[] ObjectIDList { get; set; }
        public void Validate()
        {

        }
    }
    public class ObjectInfo
    {
        public string ObjectID { get; set; }
    }
}
