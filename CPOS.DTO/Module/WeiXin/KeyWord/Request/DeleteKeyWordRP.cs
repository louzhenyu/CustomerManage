using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request
{
    public class DeleteKeyWordRP : IAPIRequestParameter
    {
        public string ReplyId { get; set; }

        public void Validate()
        {
        }
    }
}
