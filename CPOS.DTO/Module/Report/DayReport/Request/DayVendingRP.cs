using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.DayReport.Request
{
    public class DayVendingRP : IAPIRequestParameter
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StareDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate { get; set; }

        public void Validate() { }
    }
}
