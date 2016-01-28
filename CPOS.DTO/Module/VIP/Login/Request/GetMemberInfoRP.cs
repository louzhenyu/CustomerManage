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
        //查询条件，手机号、会员名或者会员昵称
        public string SearchFlag { get; set; }
        /// <summary>
        /// 店主vipID
        /// </summary>
        public string OwnerVipID { get; set; }
        public void Validate()
        {

        }
    }
}
