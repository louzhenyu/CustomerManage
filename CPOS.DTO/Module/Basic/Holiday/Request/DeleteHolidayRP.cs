using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Holiday.Request
{
    public class DeleteHolidayRP : IAPIRequestParameter
    {
        /// <summary>
        /// 假日ID
        /// </summary>
        public string HolidayId { get; set; }

        public void Validate() { }
    }
}
