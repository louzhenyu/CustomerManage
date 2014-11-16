using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response;

namespace JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request
{
    public class SaveDefaultKeyWordRP : IAPIRequestParameter
    {
        public KeyWordInfo KeyWordList { get; set; }

        public void Validate()
        {
            
        }
    }
}
