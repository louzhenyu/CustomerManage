using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.ContactEvent.Request
{
    public class GetContactEventRP : IAPIRequestParameter
    {
        public void Validate()
        {
            if (this.PageIndex < 0)
            {
                PageIndex = 0;
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
    }
}
