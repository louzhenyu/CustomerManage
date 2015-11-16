using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Tags.Request
{
    public class GetTagsByTypeNameRP : IAPIRequestParameter
    {
        /// <summary>
        /// 标签类型名称（如“年龄段”）
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 字段验证
        /// </summary>
        public void Validate()
        {

        }
    }
}
