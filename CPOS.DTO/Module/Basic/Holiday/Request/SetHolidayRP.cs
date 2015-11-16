using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Holiday.Request
{
    public class SetHolidayRP : IAPIRequestParameter
    {
        /// <summary>
        /// 假日ID
        /// </summary>
        public string HolidayId { get; set; }
        /// <summary>
        /// 假日名城
        /// </summary>
        public string HolidayName { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desciption { get; set; }
        public void Validate() { }
    }
}
