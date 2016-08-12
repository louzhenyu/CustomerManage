using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipAmount.Response
{
    public class GetVipAmountDetailRD : IAPIResponseData
    {
        /// <summary>
        /// 总个数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页面数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 当前余额/返现
        /// </summary>
        public decimal CurrentAmount { get; set; }

        /// <summary>
        /// 支出 余额
        /// </summary>
        public decimal OutAmount { get; set; }

        /// <summary>
        /// 总收入金额 {兼容 微信端 会员中心 红利明细}
        /// </summary>
        public decimal? InAmount { get; set; }
        /// <summary>
        /// 提现总金额
        /// </summary>
        public decimal? WithdrawAmount { get; set; }

        public VipAmountDetailInfo[] VipAmountDetailList { get; set; }

    }
    //变更明细
    public class VipAmountDetailInfo
    {
        public string UnitName { get; set; }
        public string AmountSourceName { get; set; }
        public decimal Amount { get; set; }
        public string CreateTime { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public string CreateByName { get; set; }
        public string ImageUrl { get; set; }
    }
}
