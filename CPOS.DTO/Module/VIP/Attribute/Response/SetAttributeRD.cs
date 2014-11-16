using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Attribute.Response
{
    /// <summary>
    /// 增加评论请求
    /// </summary>
    public class SetAttributeRD : IAPIResponseData
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Msg { get; set; }
    }
}