
namespace JIT.CPOS.BS.Entity.WX
{
    /// <summary>
    /// 通用返回
    /// </summary>
    public class WeiXinCommonEntity
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误内容
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public string expires_in { get; set; }
    }

    /// <summary>
    /// 获取凭证接口
    /// </summary>
    //public class AccessTokenEntity : WeiXinCommonEntity
    //{
    //    /// <summary>
    //    /// 调用接口凭证
    //    /// </summary>
    //    public string access_token { get; set; }
    //}

    /// <summary>
    /// 获取jsapi_ticket接口
    /// </summary>
    public class JsApiTicketEntity : WeiXinCommonEntity
    {
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string ticket { get; set; }
    }
}
