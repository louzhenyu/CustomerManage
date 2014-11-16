using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.RateLetterInterface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class APIRequestRateLetter<T> where T : APIRequest<T>, IAPIRequestParameter ,new ()
    {
        public APIRequestRateLetter()
        {
           
        }
        
        #region  公共参数

        /// <summary>
        /// 子账号
        /// </summary>
        public string SubAccountSid { get; set; }

        /// <summary>
        /// 子账号令牌
        /// </summary>
        public string SubToken { get; set; }

        /// <summary>
        /// 主账号
        /// </summary>
        public string MainAccount { get; set; }

        /// <summary>
        /// 主账号令牌
        /// </summary>
        public string MainToken { get; set; }
        #endregion
      
    }
}