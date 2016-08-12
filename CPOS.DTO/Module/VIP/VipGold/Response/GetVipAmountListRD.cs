using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    public class GetVipAmountListRD : IAPIResponseData
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 收入余额
        /// </summary>
        public decimal InComeBalance { get; set; }
        /// <summary>
        /// 支出余额
        /// </summary>
        public decimal ExpenditureBalance { get; set; }
        /// <summary>
        /// 余额信息集合
        /// </summary>
        public List<AmountInfo> AmountInfoList { get; set; }
    }

    public class AmountInfo
    {
        /// <summary>
        /// 消费备注
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 消费日期
        /// </summary>
        public string ConsumptionDate { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}