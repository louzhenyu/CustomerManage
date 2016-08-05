using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Response
{
    public class GetWithdrawDepositApplyListRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public List<WithdrawDepositApplyInfo> List { get; set; }
        
    }
    public class WithdrawDepositApplyInfo
    {
        public string ApplyID { get; set; }
        public string WithdrawNo { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int VipType { get; set; }
        public decimal Amount { get; set; }
        public string BankName { get; set; }
        public string CardNo { get; set; }
        public string AccountName { get; set; }
        /// <summary>
        /// 1：待审核2：已审核3：已完成4：审核不通过
        /// </summary>
        public int Status { get; set; }
        public string ApplyDate { get; set; }
        public string CompleteDate { get; set; }

    }
}
