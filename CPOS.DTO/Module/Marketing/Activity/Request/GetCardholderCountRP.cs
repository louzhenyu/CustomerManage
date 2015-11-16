using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Request
{
    public class GetCardholderCountRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员卡类型ID
        /// </summary>
        public string VipCardTypeID { get; set; }

        public void Validate() { }
    }
}
