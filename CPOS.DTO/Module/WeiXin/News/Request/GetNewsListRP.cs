using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.News.Request
{
    public class GetNewsListRP : IAPIRequestParameter
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }

        public string NewsTypeId { get; set; }
        public string NewsName { get; set; }

        public void Validate()
        {
            
        }
    }
}
