using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Request
{
    public class GetKJEventJoinListRP : IAPIRequestParameter
    {
        public void Validate()
        {
            if (this.PageIndex < 0)
            {
                PageIndex = 1;
            }
            if (this.PageSize < 1)
            {
                PageSize = 10;
            }
        }
        /// <summary>
        /// 页码。为空则默认为0
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数。为空则为10
        /// </summary>
        public int PageSize { get; set; }
    }
}
