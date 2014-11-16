using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipListNum.Request
{
    public class GetVipListNumRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
        /// <summary>
        /// 门店ID
        /// </summary>
        public string UnitID { get; set; }
    }
}
