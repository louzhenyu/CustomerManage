using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Basic.Holiday.Request
{
    public class GetHolidayDetailRP
    {
        /// <summary>
        /// 假日ID
        /// </summary>
        public string HolidayId { get; set; }

        public void Validate() { }
    }
}
