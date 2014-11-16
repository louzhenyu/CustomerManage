using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Knowledge.Type.Response
{
    /// <summary>
    /// 获取分类响应
    /// </summary>
    public class GetTypeListRD : IAPIResponseData
    {
        public CategoryInfo[] CategoryList { get; set; }
    }

    public class CategoryInfo
    {
        /// <summary>
        /// 文章标签ID
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
