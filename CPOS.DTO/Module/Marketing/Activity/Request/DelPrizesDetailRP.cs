using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Activity.Request
{
    public class DelPrizesDetailRP : IAPIRequestParameter
    {
        /// <summary>
        /// 奖品明细ID
        /// </summary>
        public string PrizesDetailID { get; set; }
        public string ActivityID { get; set; }
        public void Validate() { }
    }
}
