namespace JIT.CPOS.Web.ApplicationInterface.Base
{
    /// <summary>
    /// 处理APP请求的元数据接口，无需实现
    /// </summary>
    public interface IRequestHandlerData
    {
        string Action { get; }
    }
}
