using System.Web;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX.Interface
{
    /// <summary>
    /// 微信工厂
    /// </summary>
    public interface IFactory
    {
        BaseBLL CreateWeiXin(HttpContext httpContext, RequestParams requestParams);
    }
}
