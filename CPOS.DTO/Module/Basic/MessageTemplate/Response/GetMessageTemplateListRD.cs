using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.MessageTemplate.Response
{
    public class GetMessageTemplateListRD : IAPIResponseData
    {
        /// <summary>
        /// 模板集合
        /// </summary>
        public List<MessageTemplateInfo> MessageTemplateInfoList { get; set; }
    }

    public class MessageTemplateInfo {
        /// <summary>
        /// 消息模板ID
        /// </summary>
        public string TemplateID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string LastUpdateTime { get; set; }
    }
}
