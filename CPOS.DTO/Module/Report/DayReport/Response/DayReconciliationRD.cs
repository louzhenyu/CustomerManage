using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.DayReport.Response
{
    public class DayReconciliationRD : IAPIResponseData
    {
        /// <summary>
        /// 列表信息
        /// </summary>
        public List<DayReconciliationInfo> DayReconciliationInfoList { get; set; }
    }

    public class DayReconciliationInfo
    {
        /// <summary>
        /// 售卡日期
        /// </summary>
        public string MembershipTime { get; set; }
        /// <summary>
        /// 售卡统计
        /// </summary>
        public int SaleCardCount { get; set; }
        /// <summary>
        /// 售卡总额
        /// </summary>
        public decimal SalesToAmount { get; set; }
        /// <summary>
        /// 充值总额
        /// </summary>
        public decimal RechargeTotalAmount { get; set; }
        /// <summary>
        /// 储值消费总额
        /// </summary>
        public decimal StoredSalesTotalAmount { get; set; }
        /// <summary>
        /// 积分抵扣总数
        /// </summary>
        public decimal IntegratDeductibleCount { get; set; }
        /// <summary>
        /// 积分抵扣金额
        /// </summary>
        public decimal IntegratDeductibleToAmount { get; set; }
    }
}
