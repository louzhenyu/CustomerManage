using System.Web;
using JIT.CPOS.BS.BLL.WX.Interface;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX.Factory
{
    /// <summary>
    /// 中欧国际工商学院(正式用户)
    /// </summary>
    public class CeibsFactory : IFactory
    {
        public BaseBLL CreateWeiXin(HttpContext httpContext, RequestParams requestParams)
        {
            return new CeibsBLL(httpContext, requestParams);
        }
    }
}
