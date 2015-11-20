using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.ContactEvent.Request
{
    public class ChangeContactEventStatusRP : IAPIRequestParameter
    {
        public string ContactEventId { get; set; }
        /// <summary>
        /// 1:未开始2：运行中3：暂停：4结束
        /// </summary>
        public int Status { get; set; }
        public void Validate()
        {
        }
    }
}
