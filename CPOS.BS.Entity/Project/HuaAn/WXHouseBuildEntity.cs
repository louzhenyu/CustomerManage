using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 楼盘扩展类定义。
    /// </summary>
    public partial class WXHouseBuildEntity
    {
        /// <summary>
        ///     实付金额
        /// </summary>
        public decimal RealPay { get; set; }

        /// <summary>
        /// 抵扣金额
        /// </summary>
        public decimal DeductionPay { get; set; }

        /// <summary>
        /// 已预订人数。
        /// </summary>
        public int ReservedNum { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyHouseDate { get; set; }

        /// <summary>
        /// 房产详细URL
        /// </summary>
        public string HouseDetailURL { get; set; }
    }
}
