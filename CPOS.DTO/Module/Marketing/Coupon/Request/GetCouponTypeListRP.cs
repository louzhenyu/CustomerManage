using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Coupon.Request
{
    public class GetCouponTypeListRP : IAPIRequestParameter
    {
        public string CouponTypeName { get; set; }
        public string ParValue { get; set; }
        /// <summary>
        /// 是否获取有效券
        /// </summary>
        public bool? IsEffective { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
		/// <summary>
		/// 是否设置了相对时间
		/// </summary>
		public int IsServiceLife { get; set; }

		public int? IsNotLimitQty { get; set; }
        /// <summary>
        /// SurplusCount==1 查询剩余张数大于0 的优惠券 否则不做处理
        /// </summary>
        public int? SurplusCount { get; set; }
        public void Validate()
        {

        }
    }
}
