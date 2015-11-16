using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.Basic.Holiday.Request
{
    public class GetHolidayListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 叶大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 叶索引
        /// </summary>
        public int PageIndex { get; set; }

        public void Validate() { }
    }
}
