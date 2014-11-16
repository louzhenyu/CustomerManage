using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX.BaseClass
{
    /// <summary>
    /// 微信服务号基类
    /// </summary>
    public class ServiceBLL : BaseBLL
    {
        #region 构造函数

        public ServiceBLL(HttpContext httpContext, RequestParams requestParams)
            : base(httpContext, requestParams)
        {

        }

        #endregion
    }
}
