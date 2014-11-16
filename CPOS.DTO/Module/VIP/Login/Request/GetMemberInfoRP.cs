using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Request
{
    public class GetMemberInfoRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员Id
        /// </summary>
        public string MemberID { get; set; }
        public void Validate()
        {

        }
    }
}
