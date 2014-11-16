using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request
{
    public class GetDefaultKeyWordRP : IAPIRequestParameter
    {
        public string ApplicationId { get; set; }
        public int KeywordType { get; set; }
        public void Validate()
        {
            
        }
    }
}
