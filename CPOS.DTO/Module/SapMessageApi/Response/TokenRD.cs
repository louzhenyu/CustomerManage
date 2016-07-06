using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SapMessageApi.Response
{
    public class SapTokenRD : IAPIResponseData
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public int expires_in { get; set; }

        public string userName { get; set; }

        public string issued { get; set; }

        public string expires { get; set; }
    }
}
