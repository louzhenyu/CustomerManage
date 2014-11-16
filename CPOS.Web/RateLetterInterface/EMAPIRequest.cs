using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.RateLetterInterface
{
    public class EMAPIRequest<T>
        where T : IAPIRequestParameter, new()
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EMAPIRequest()
        {
            this.Parameters = new T();
        }
        #endregion

        #region 公共参数
        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 语言
        /// <remarks>
        /// <para>预留作为扩展</para>
        /// </remarks>
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// 访问票据
        /// <remarks>
        /// <para>预留</para>
        /// </remarks>
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 微信平台的用户ID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 公共参数。如果不为空,则表示接口调用需要支持JSONP。实现方式为返回结果为：JSONP(Response);
        /// 其中JSONP为当前JSONP参数的值。Response为响应的JSON字符
        /// </summary>
        public string JSONP { get; set; }

        /// <summary>
        /// 平台
        /// </summary>
        public string Plat { set; get; }

        /// <summary>
        /// 子账号
        /// </summary>
        public string SubAccountSid { get; set; }

        /// <summary>
        /// 子账号令牌
        /// </summary>
        public string SubToken { get; set; }

        /// <summary>
        /// 主账号
        /// </summary>
        public string MainAccount { get; set; }

        /// <summary>
        /// 主账号令牌
        /// </summary>
        public string MainToken { get; set; }

        /// <summary>
        /// 应用程序版本号
        /// </summary>
        public string Version { get; set; }

        #endregion

        #region 接口参数
        /// <summary>
        /// 参数对象
        /// </summary>
        public T Parameters { get; set; }
        #endregion
    }

    public enum Plat
    {
        android,
        iPhone
    }
}
