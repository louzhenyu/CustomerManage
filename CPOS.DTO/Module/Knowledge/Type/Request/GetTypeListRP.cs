using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Knowledge.Type.Request
{
    /// <summary>
    /// 获取分类请求
    /// </summary>
    public class GetTypeListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 分类标识
        /// </summary>
        public Guid? ID { get; set; }

        public void Validate()
        {
           
        }
    }
}
