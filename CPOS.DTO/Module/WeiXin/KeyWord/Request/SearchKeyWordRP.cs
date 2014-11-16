using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request
{
    public class SearchKeyWordRP : IAPIRequestParameter
    {
        public string ApplicationId { get; set; }
        public string KeyWord { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public void Validate()
        {
        }
    }
}
