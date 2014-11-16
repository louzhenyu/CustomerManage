using System.Web;
using JIT.CPOS.BS.BLL.WX.Interface;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX.Factory
{
    /// <summary>
    /// 逸马顾问
    /// </summary>
    public class YmgwFactory : IFactory
    {
        public BaseBLL CreateWeiXin(HttpContext httpContext, RequestParams requestParams)
        {
            return new YmgwBLL(httpContext, requestParams);
        }
    }
}