using System.Web;
using JIT.CPOS.BS.BLL.WX.BaseClass;
using JIT.CPOS.BS.BLL.WX.Interface;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX.Factory
{
    /// <summary>
    /// 微信认证服务号基类
    /// </summary>
    public class CertificationFactory : IFactory
    {
        public BaseBLL CreateWeiXin(HttpContext httpContext, RequestParams requestParams)
        {
            return new CertificationBLL(httpContext, requestParams);
        }
    }
}
