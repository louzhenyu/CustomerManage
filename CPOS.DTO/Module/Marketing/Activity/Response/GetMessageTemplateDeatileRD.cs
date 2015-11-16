using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Activity.Response
{
    public class GetMessageTemplateDeatileRD : IAPIResponseData
    {
        /// <summary>
        /// 模板ID
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
    }
}
