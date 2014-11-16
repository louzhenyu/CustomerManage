using System.Web;
using JIT.CPOS.BS.BLL.WX.Interface;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX.Factory
{
    /// <summary>
    /// 杰亦特-企业移动应用
    /// </summary>
    public class JitmarketingFactory : IFactory
    {
        public BaseBLL CreateWeiXin(HttpContext httpContext, RequestParams requestParams)
        {
            return new JitmarketingBLL(httpContext, requestParams);
        }
    }
}
