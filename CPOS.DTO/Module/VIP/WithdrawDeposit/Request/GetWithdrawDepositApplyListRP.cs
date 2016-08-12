using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Request
{
    public class GetWithdrawDepositApplyListRP : IAPIRequestParameter
    {
        public int? VipType { get; set; }
        public string WithdrawNo { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ApplyStartDate { get; set; }
        public string ApplyEndDate { get; set; }
        public int? Status { get; set; }
        public string CompleteStartDate { get; set; }
        public string CompleteEndDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        {
            //throw new NotImplementedException();
        }
    }
}
