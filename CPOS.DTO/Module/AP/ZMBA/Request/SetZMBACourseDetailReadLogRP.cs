using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.AP.ZMBA.Request
{
    public class SetZMBACourseDetailReadLogRP : IAPIRequestParameter
    {
        /// <summary>
        /// 课程Id
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 课程内容Id
        /// </summary>
        public string CourseDetailId { get; set; }
        /// <summary>
        /// 课程内容名称
        /// </summary>
        public string Name { get; set; }
        public void Validate()
        {

        }
    }
}
