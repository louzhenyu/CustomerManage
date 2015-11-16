using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.DayReport.Response
{
    public class DayVendingRD : IAPIResponseData
    {
        /// <summary>
        /// 售卡数量
        /// </summary>
        public int VendingCount { get; set; }
        /// <summary>
        /// 赠送卡数量
        /// </summary>
        public int GiftCardCount { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal VendingAmountCount { get; set; }
        /// <summary>
        /// 卡列表信息
        /// </summary>
        public List<DayVendingInfo> DayVendingInfoList { get; set;}
    }

    public class DayVendingInfo {
        /// <summary>
        /// 售卡日期
        /// </summary>
        public string MembershipTime { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 赠卡数量
        /// </summary>
        public int GiftCardCount { get; set; }
        /// <summary>
        /// 售卡数量
        /// </summary>
        public int SaleCardCount { get; set; }
        /// <summary>
        /// 售卡金额
        /// </summary>
        public decimal SalesAmount { get; set; }
    }
}
