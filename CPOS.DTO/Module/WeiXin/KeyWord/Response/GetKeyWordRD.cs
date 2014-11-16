using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;

namespace JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response
{
    public class GetKeyWordRD : IAPIResponseData
    {
        public KeyWordInfo KeyWordList { get; set; }
    }

    public class KeyWordInfo
    {
        public string ReplyId { get; set; }
        public string KeyWord { get; set; }
        public int BeLinkedType { get; set; }
        public string ApplicationId { get; set; }
        public int KeywordType { get; set; }
        public int DisplayIndex { get; set; }
        public int ReplyType { get; set; }
        public string Text { get; set; }
        public MaterialTextIdInfo[] MaterialTextIds { get; set; }
    }
}
