using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Response
{
    public class GetMemberBenefitsRD : IAPIResponseData
    {
        public string[] MemberBenefits { get; set; }
    }
}
