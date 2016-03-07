using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Dealer.Request
{
    public class GetVipFansListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 成交编码 Y：已成交 N：未成交 空、null代表所有
        /// </summary>
        public string Code { get; set; }

        public string VipName { get; set; }
        public void Validate()
        {

        }
    }
}
