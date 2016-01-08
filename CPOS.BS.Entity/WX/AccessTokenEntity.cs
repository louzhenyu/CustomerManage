
namespace JIT.CPOS.BS.Entity.WX
{
    /// <summary>
    /// 获取凭证接口
    /// </summary>
    public class AccessTokenEntity
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
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public string expires_in { get; set; }
    }
    /// <summary>
    /// 获取凭证接口
    /// </summary>
    public class OpenAccessTokenEntity
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
        /// 接口凭证
        /// </summary>
        public string authorizer_access_token { get; set; }
        /// <summary>
        /// 刷新凭证
        /// </summary>
        public string authorizer_refresh_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public string expires_in { get; set; }
        public string P2PData { get; set; }
    }
}
