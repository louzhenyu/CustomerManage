using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SapMessageApi.Request
{
    public class SetMsgHandResultRP : IAPIRequestParameter
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public int SequenceID { get; set; }
        /// <summary>
        /// 如果处理失败回调通知的时候可以把错误信息写在该字段
        /// </summary>
        public string ErrorMSG { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 目标系统
        /// </summary>
        public string TargetSystem { get; set; }
        /// <summary>
        /// 目标数据库
        /// </summary>
        public string TargetDB { get; set; }
        /// <summary>
        /// 目标对象
        /// </summary>
        public string TargetType { get; set; }
        /// <summary>
        /// 目标主键
        /// </summary>
        public string TargetValue { get; set; }
        /// <summary>
        /// 消费方
        /// </summary>
        public string FromSystem { get; set; }

        /// <summary>
        /// 消费方
        /// </summary>
        public int Status { get; set; }
        public void Validate()
        {
        }
    }
}
