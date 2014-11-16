namespace JIT.CPOS.Web.ApplicationInterface.Base
{
    /// <summary>
    /// 处理APP请求的接口
    /// </summary>
    public interface IRequestHandler
    {
        string DoAction(string pRequest);
    }
}
