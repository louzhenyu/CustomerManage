/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 14:22:26
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

using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Util.ExtensionMethods;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;
using System.Web.SessionState;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Base
{
    /// <summary>
    /// 接口请求处理基类 
    /// </summary>
    public abstract class BaseActionHandler<TRequestPara, TResponseData> : IActionHandler, IRequiresSessionState
        where TRequestPara : IAPIRequestParameter, new()
        where TResponseData : IAPIResponseData
    {
        /// <summary>
        /// 处理接口请求
        /// </summary>
        /// <param name="pVersion">接口版本号</param>
        /// <param name="pRequest">接口请求</param>
        /// <returns>处理结果</returns>
        protected abstract TResponseData ProcessRequest(APIRequest<TRequestPara> pRequest);

        /// <summary>
        /// 验证公共参数
        /// </summary>
        /// <param name="pRequest"></param>
        protected virtual void ValidateCommonParameters(APIRequest<TRequestPara> pRequest)
        {

        }

        #region 属性集

        #region 请求参数的JSON字符串
        /// <summary>
        /// 请求参数的JSON字符串
        /// </summary>
        protected string RequestParametersJSON { get; set; }
        #endregion

        #region 接口请求
        /// <summary>
        /// 接口请求
        /// </summary>
        protected APIRequest<TRequestPara> APIRequest { get; set; }
        #endregion

        #region 当前的用户信息
        private static LoggingSessionInfo _currentUserInfo = null;
        /// <summary>
        /// 当前的用户信息
        /// </summary>
        protected LoggingSessionInfo CurrentUserInfo
        {
            get
            {
                if (_currentUserInfo == null)
                    if (!string.IsNullOrEmpty(this.APIRequest.CustomerID))
                        return Default.GetBSLoggingSession(this.APIRequest.CustomerID, "");
                    else
                        return new SessionManager().CurrentUserLoginInfo;
                else
                {
                    return _currentUserInfo;
                }
            }
        }
        #endregion

        #endregion

        #region IActionHandler 成员
        /// <summary>
        /// 处理接口请求
        /// </summary>
        /// <param name="pVersion">接口版本号</param>
        /// <param name="pRequestJSON">请求JSON</param>
        /// <returns>返回响应结果</returns>
        IAPIResponseData IActionHandler.ProcessAction(string pRequestJSON)
        {
            //参数检查
            if (string.IsNullOrWhiteSpace(pRequestJSON))
            {
                throw new APIException(ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER, "缺少请求参数[req].");
            }
            this.RequestParametersJSON = pRequestJSON;
            //反序列化
            var request = pRequestJSON.DeserializeJSONToAPIRequest<TRequestPara>();
            if (request == null)
                throw new APIException(ERROR_CODES.INVALID_REQUEST_REQUEST_DESERIALIZATION_FAILED, "请求反序列化失败.JSON反序列化后获得的请求对象为null.");
            if (request.Parameters == null)
                throw new APIException(ERROR_CODES.INVALID_REQUEST_REQUEST_DESERIALIZATION_FAILED, "请求反序列化失败.JSON反序列化后获得的接口参数对象为null.");
            this.APIRequest = request;
            //公共参数处理
            this.ValidateCommonParameters(request);
            //参数验证
            request.Parameters.Validate();
            //执行处理
            return this.ProcessRequest(request);
        }
        #endregion
    }
}
