using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Knowledge.Tag.Request
{
    /// <summary>
    /// 获取标签请求
    /// </summary>
    public class GetHotTagsRP : IAPIRequestParameter
    {
        /// <summary>
        /// 获取标签的数量，默认为3 
        /// </summary>
        public int? Count { get; set; }

        public void Validate()
        {
        }
    }
}
