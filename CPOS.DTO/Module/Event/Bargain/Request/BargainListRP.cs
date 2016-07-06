using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Request
{
    public class BargainListRP : IAPIRequestParameter
    {
        public void Validate()
        {
            if (this.PageIndex < 0)
            {
                PageIndex = 1;
            }
            if (this.PageSize < 1)
            {
                PageSize = 15;
            }
        }
        /// <summary>
        /// 页码。为空则默认为0
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数。为空则为15
        /// </summary>
        public int PageSize { get; set; }

        public string EventName { get; set; }
        public int EventStatus { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
    }
  
}
