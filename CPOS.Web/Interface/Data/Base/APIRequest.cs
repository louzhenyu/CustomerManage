using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.Interface.Data.Base
{
    public class APIRequest
    {
        public CommonReqPara common { get; set; }
        public object special { get; set; }
        public T GetParameters<T>()
        {
            if (special == null)
                return default(T);
            else
                return special.ToJSON().DeserializeJSONTo<T>();
        }

        public LoggingSessionInfo GetUserInfo()
        {
            return Default.GetBSLoggingSession(common.customerId, "1");
        }
    }
}