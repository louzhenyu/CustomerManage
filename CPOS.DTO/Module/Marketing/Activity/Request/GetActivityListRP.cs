using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Request
{
    public class GetActivityListRP : IAPIRequestParameter
    {   
        /// <summary>
        /// 活动类型
        /// </summary>
        public string ActivityType { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }

        public void Validate() { }
    }
}
