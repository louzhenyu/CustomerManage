using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.AP.ZMBA.Response
{
    public class GetZMBACourseDetailListRD : IAPIResponseData
    {
        /// <summary>
        /// 总叶数
        /// </summary>
        public int TotalPageCount { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 课程内容列表
        /// </summary>
        public List<T_ZMBA_CourseDetailEntity> ZMBACourseDetailList { get; set; }
    }

    
}
