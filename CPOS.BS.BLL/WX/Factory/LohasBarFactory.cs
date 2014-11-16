using System.Web;
using JIT.CPOS.BS.BLL.WX.Interface;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX.Factory
{
    /// <summary>
    /// 乐活吧
    /// </summary>
    public class LohasBarFactory : IFactory
    {
        public BaseBLL CreateWeiXin(HttpContext httpContext, RequestParams requestParams)
        {
            return new LohasBarBLL(httpContext, requestParams);
        }
    }
}
