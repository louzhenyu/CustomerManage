using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Dealer.Response
{
    public class GetVipDealerAccountDetailRD : IAPIResponseData
    {

        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 当日收入
        /// </summary>
        public string DayIncome { get; set; }
        /// <summary>
        /// 当日收入粉丝
        /// </summary>
        public string DayFans { get; set; }
        /// <summary>
        /// 当月收入
        /// </summary>
        public string MounthIncome { get; set; }
        /// <summary>
        /// 累计总收入
        /// </summary>
        public string SumIncome { get; set; }
        /// <summary>
        /// 本月收入粉丝
        /// </summary>
        public string MounthFans { get; set; }
        /// <summary>
        /// 累计收入粉丝
        /// </summary>
        public string SumFans { get; set; }
        /// <summary>
        /// 可提现总金额
        /// </summary>
        public string WithdrawSumMoney { get; set; }

    }
}
