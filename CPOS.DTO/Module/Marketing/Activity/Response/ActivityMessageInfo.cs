using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Response
{
    public class ActivityMessageInfo
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessageID { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 提前发送天数
        /// </summary>
        public int? AdvanceDays { get; set; }
        /// <summary>
        /// 发送小时点
        /// </summary>
        public int? SendAtHour { get; set; }
    }
}
