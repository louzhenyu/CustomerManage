using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Request
{
    public class GetActivityDeatilRP : IAPIRequestParameter
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public string ActivityID { get; set; }

        public void Validate() { }
    }
}
