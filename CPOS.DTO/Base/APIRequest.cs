/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/26 14:57:40
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

using JIT.CPOS.DTO.ValueObject;

namespace JIT.CPOS.DTO.Base
{
    /// <summary>
    /// API请求接口 
    /// </summary>
    public class APIRequest<T>
        where T : IAPIRequestParameter, new()
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public APIRequest()
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
        /// 是否需要将ALD会员同步到商户会员
        /// </summary>
        public int IsAToC { get; set; }

        /// <summary>
        /// 公共参数。如果不为空,则表示接口调用需要支持JSONP。实现方式为返回结果为：JSONP(Response);
        /// 其中JSONP为当前JSONP参数的值。Response为响应的JSON字符
        /// </summary>
        public string JSONP { get; set; }

        /// <summary>
        /// 公共参数。渠道  add by donal 2014-9-24 19:21:24
        /// </summary>
        public string ChannelId { get; set; }
        #endregion

        #region 接口参数
        /// <summary>
        /// 参数对象
        /// </summary>
        public T Parameters { get; set; }
        #endregion
    }
}
