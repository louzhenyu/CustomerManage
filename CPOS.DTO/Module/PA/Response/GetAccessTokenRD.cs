using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.PA.Response
{
    public class GetAccessTokenRD
    {
        /// <summary>
        /// 0表示成功
        /// </summary>
        public string ret { get; set; }
        public AccessTokenData data { get; set; }
        public string msg { get; set; }
    }

    public class AccessTokenData
    {
        /// <summary>
        /// 有效期
        /// </summary>
        public string expires_in { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
    }

}
