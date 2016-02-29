using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.AP.ZMBA.Request
{
    public class GetZMBACourseDetailListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 课程标识
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
        public void Validate()
        {

        }
    }
}
