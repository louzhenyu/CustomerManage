using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.MessageTemplate.Request
{
    public class SetMessageTemplateRP : IAPIRequestParameter
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        /// 模板标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; }

        public void Validate() { }
    }
}
