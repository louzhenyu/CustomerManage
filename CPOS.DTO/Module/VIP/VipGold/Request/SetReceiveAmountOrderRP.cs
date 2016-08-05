using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;


namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class SetReceiveAmountOrderRP : IAPIRequestParameter
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeID{ get; set; }

        /// <summary>
        /// 是否使用优惠券标识
        /// </summary>
        public int CouponFlag { get; set; }

        /// <summary>
        /// 优惠券ID
        /// </summary>
        public string CouponId { get; set; }

        /// <summary>
        /// 是否使用积分标识
        /// </summary>
        public int IntegralFlag { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public decimal Integral { get; set; }

        /// <summary>
        /// 积分抵金额
        /// </summary>
        public decimal IntegralAmount { get; set; }

        /// <summary>
        /// 是否使用余额标识
        /// </summary>
        public int VipEndAmountFlag { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal VipEndAmount { get; set; }

        /// <summary>
        /// 会员折扣
        /// </summary>
        public decimal VipDiscount { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        public void Validate() { }
    }
}
