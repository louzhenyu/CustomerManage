using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetRSetoffHomeListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 类型 1 会员集客 2 员工集客
        /// </summary>
        public int SetoffTypeId { get; set; }
        public void Validate()
        {

        }
    }
}

