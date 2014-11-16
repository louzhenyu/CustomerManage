using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Base
{
    /// <summary>
    /// 处理APP请求的接口
    /// </summary>
    public interface IRequestHandler
    {
        string DoAction(string pRequest);
    }
}