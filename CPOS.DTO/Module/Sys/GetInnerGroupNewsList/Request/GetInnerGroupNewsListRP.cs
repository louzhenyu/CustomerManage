using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Sys.InnerGroupNews.Request
{
    public class GetInnerGroupNewsListRP : IAPIRequestParameter
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        /// <summary>
        /// 平台编号   1=微信用户 2=APP员工
        /// </summary>
        public int NoticePlatformTypeId { get; set; }
      public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        /// <summary>
        /// 业务类型Id(1=内部公告2=集客行动)
        /// </summary>
        public int? BusType { get; set; }
        public void Validate()
        {
         
        }
    }
}
