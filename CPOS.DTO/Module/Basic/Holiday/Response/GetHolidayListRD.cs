using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Holiday.Response
{
    public class GetHolidayListRD : IAPIResponseData
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 假日列表信心集合
        /// </summary>
        public List<HolidayInfo> HolidayList { get; set; }
    }

    public class HolidayInfo {
        /// <summary>
        /// 假日ID
        /// </summary>
        public string HolidayId { get; set; }
        /// <summary>
        /// 假日名城
        /// </summary>
        public string HolidayName { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desciption { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}
