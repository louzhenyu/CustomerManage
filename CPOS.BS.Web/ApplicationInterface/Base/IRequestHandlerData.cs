using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Base
{
    /// <summary>
    /// 处理APP请求的元数据接口，无需实现
    /// </summary>
    public interface IRequestHandlerData
    {
        string Action { get; }
    }
}