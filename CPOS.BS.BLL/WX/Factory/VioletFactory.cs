using System.Web;
using JIT.CPOS.BS.BLL.WX.Interface;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX.Factory
{
    /// <summary>
    /// 紫罗兰(正式用户)
    /// </summary>
    public class VioletFactory : IFactory
    {
        public BaseBLL CreateWeiXin(HttpContext httpContext, RequestParams requestParams)
        {
            return new VioletBLL(httpContext, requestParams);
        }
    }
}
