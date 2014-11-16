/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 14:01:56
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

namespace JIT.CPOS.DTO.Base
{
    /// <summary>
    /// 错误码
    /// <remarks>
    /// <para>0       OK 正确</para>
    /// <para>100-199 请求不合乎规范,非法请求</para>
    /// <para>200-299 请求的参数不正确</para>
    /// <para>300-499 业务处理失败</para>
    /// <para>500     未归类的错误</para>
    /// </remarks>
    /// </summary>
    public static class ERROR_CODES
    {
        #region 0 - 99  正确
        /// <summary>
        /// OK 正确
        /// </summary>
        public static int SUCCESS = 0;
        #endregion

        #region 100 - 199 请求不合乎规范,非法请求

        /// <summary>
        /// 请求不合乎规范
        /// </summary>
        public static int INVALID_REQUEST = 100;

        /// <summary>
        /// QueryString中缺少type节
        /// </summary>
        public static int INVALID_REQUEST_LACK_TYPE_IN_QUERYSTRING = 101;

        /// <summary>
        /// QueryString中缺少action节
        /// </summary>
        public static int INVALID_REQUEST_LACK_ACTION_IN_QUERYSTRING = 102;

        /// <summary>
        /// 缺少请求参数
        /// </summary>
        public static int INVALID_REQUEST_LACK_REQUEST_PARAMETER = 103;

        /// <summary>
        /// 请求反序列化失败
        /// </summary>
        public static int INVALID_REQUEST_REQUEST_DESERIALIZATION_FAILED = 104;

        /// <summary>
        /// action的格式错误
        /// </summary>
        public static int INVALID_REQUEST_INVALID_ACTION_FORMAT = 105;

        /// <summary>
        /// 根据action找不到指定的ActionHandler类
        /// </summary>
        public static int INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER = 106;
        #endregion

        #region 500 - 599 未归类错误
        /// <summary>
        /// 默认的错误码
        /// </summary>
        public static int DEFAULT_ERROR = 500;

        /// <summary>
        /// 判断可执行操作 ，用于判断来源操作来源。1.客户APP消费者（预留，待实现）2.来自客服APP
        /// </summary>
        public static int DEFAULTSOURCE_ERROE = 501;

        #endregion

        #region 600 - 699 响应不合乎规范
        /// <summary>
        /// 响应不合法
        /// </summary>
        public static int INVALID_RESPONSE = 600;

        /// <summary>
        /// 响应不合法 - 接口处理没有返回响应
        /// </summary>
        public static int INVALID_RESPONSE_ACTION_NO_RESPONSE = 601;
        #endregion
    }
}
