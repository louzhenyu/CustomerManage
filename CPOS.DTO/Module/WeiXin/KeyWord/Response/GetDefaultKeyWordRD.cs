using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response
{
   public class GetDefaultKeyWordRD : IAPIResponseData
    {
       public KeyWordInfo KeyWordList { get; set; }
    }
}
