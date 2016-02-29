using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.AP.ZMBA.Response
{
    public class GetZMBACourseRD : IAPIResponseData
    {
        public List<T_ZMBA_CourseEntity> ZMBACourseList{get;set;}
    }
}
