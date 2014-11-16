using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request
{
    /// <summary>
    /// 文章搜索请求
    /// </summary>
    public class SearchRP : IAPIRequestParameter
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 每页记录数。默认为15条
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 当前页码。默认为0
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// 文章分类ID。
        /// </summary>
        public Guid? Type { get; set; }

        public void Validate()
        {
            
        }
    }
}
