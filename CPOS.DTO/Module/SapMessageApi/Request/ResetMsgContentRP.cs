using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SapMessageApi.Request
{
    public class ResetMsgContentRP : IAPIRequestParameter
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public int SequenceID { get; set; }
        /// <summary>
        /// 新的内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 内容的长度
        /// </summary>
        public int iLength { get; set; }
        public void Validate()
        {
        }
    }
}
