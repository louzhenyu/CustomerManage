using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SapMessageApi.Response
{
    /// <summary>
    /// 用于大部分API请求的响应结果
    /// </summary>
    public class SapMessageApiRD : IAPIResponseData
    {
        /// <summary>
        /// 当该字段为空的时候表示调用成功
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 当是消费的时候如果还有下一个待消费的消息，该字段返回的就是下一条消息的ID
        /// </summary>
        public string NextSequenceID { get; set; }
        /// <summary>
        /// 生产消息的时候如果调用成功该字段返回对应新增的消息ID
        /// </summary>
        public string SequenceID { get; set; }
    }
}
