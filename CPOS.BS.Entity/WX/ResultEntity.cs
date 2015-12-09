using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.WX
{
    /// <summary>
    /// 微信公众平台返回消息
    /// </summary>
    public class ResultEntity
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误内容
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }
    }
}
