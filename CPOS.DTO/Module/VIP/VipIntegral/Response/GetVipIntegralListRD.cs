using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    public class GetVipIntegralListRD : IAPIResponseData
    {
        public List<IntegralInfo> IntegralList { get; set; }
        public int Total { get; set; }

        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 收入积分
        /// </summary>
        public decimal? IncomeAmount { get; set; }

        /// <summary>
        /// 支出积分
        /// </summary>
        public decimal? ExpenditureAmount { get; set; }
    }

    /// <summary>
    /// 我的积分
    /// </summary>
    public class IntegralInfo
    {
        public string UpdateReason { get; set; }
        public int UpdateCount { get; set; }
        public string UpdateTime { get; set; }

        /// <summary>
        /// 收入积分
        /// </summary>
        public decimal? IncomeAmount { get; set; }

        /// <summary>
        /// 支出积分
        /// </summary>
        public decimal? ExpenditureAmount { get; set; }
    }
}
