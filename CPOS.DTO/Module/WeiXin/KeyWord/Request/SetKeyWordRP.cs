using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;

namespace JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request
{
    public class SetKeyWordRP : IAPIRequestParameter
    {
        public KeyWordInfo KeyWordList { get; set; }
        public void Validate()
        {
        }
    }
}
