using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Request
{
    public class MultiCompleteRP : IAPIRequestParameter
    {
        public string[] Ids { get; set; }
        public void Validate()
        {
            //throw new NotImplementedException();
        }
    }
}
