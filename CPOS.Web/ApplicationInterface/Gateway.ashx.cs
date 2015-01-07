using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.Web.Module.Log.InterfaceWebLog;
using JIT.Utility.Log;
using log4net.Layout.Pattern;
using log4net.Layout;
using log4net.Core;
using System.Reflection;

namespace JIT.CPOS.Web.ApplicationInterface
{
    /// <summary>
    /// API请求的入口
    /// <remarks>
    /// <para>请求会根据请求的Action转交给负责具体业务的一般处理程序来处理</para>
    /// </remarks>
    /// </summary>
    public class Gateway : IHttpHandler
    {
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //参数解析
                var action = context.Request.QueryString["action"];
           
                //var strVersion = context.Request.QueryString["v"];
                var strAPIType = context.Request.QueryString["type"];
                var reqContent = context.Request["req"];
                var type = APITypes.Product;
                //获取请求参数的公共参数部分
                var commonRequest = reqContent.DeserializeJSONTo<EmptyRequest>();   //将请求反序列化为空接口请求,获得接口的公共参数
                if (commonRequest == null)
                {
                    throw new APIException(ERROR_CODES.INVALID_REQUEST_REQUEST_DESERIALIZATION_FAILED, "缺少请求参数.");
                }
                else
                {
                    this.JSONP = commonRequest.JSONP;
                }
                #region 插入日志信息  Add by changjian.tian 2014-04-24

                log4net.ILog logger = log4net.LogManager.GetLogger("Logger");
                if (reqContent != null && reqContent.Length > 0)
                {
                reqContent = HttpUtility.UrlDecode(reqContent);
                //Default.ReqData reqObj = reqContent.DeserializeJSONTo<Default.ReqData>();
                logger.Info(new LogContent(action, context.Request["req"], commonRequest.UserID, commonRequest.CustomerID, commonRequest.UserID, commonRequest.OpenID,HttpContext.Current.GetClientIP(), "", "", null));
                 }

                #endregion
                //
                if (Enum.TryParse<APITypes>(strAPIType, out type))
                {
                    var handler = ActionHandlerFactory.GetActionHandler(type, action);
                    if (handler == null)
                        throw new APIException(ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER, "根据action找不到指定的接口操作处理类(实现IActionHandler的类).");
                    //var rspData = handler.ProcessAction(version, reqContent);
                    var rspData = handler.ProcessAction(reqContent);
                    if (rspData == null)
                    {
                        throw new APIException(ERROR_CODES.INVALID_RESPONSE_ACTION_NO_RESPONSE, "接口处理没有返回响应.");
                    }
                    var rsp = new SuccessResponse<IAPIResponseData>(rspData);
                    //输出
                    this.DoResponse(context, rsp.ToJSON());
                }
                else
                {
                    throw new APIException(ERROR_CODES.INVALID_REQUEST_LACK_TYPE_IN_QUERYSTRING, "请求的QueryString中缺少type节.");
                }
            }
            catch (APIException ex)
            {
                Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                ErrorResponse rsp = new ErrorResponse(ex.ErrorCode, ex.Message);
                this.DoResponse(context, rsp.ToJSON());
            }
            catch (ThreadAbortException) { }    //Response.End通过抛出ThreadAbortException异常来实现中止执行后续代码的功能
            catch (Exception ex)
            {
                Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                ErrorResponse rsp = new ErrorResponse(ERROR_CODES.DEFAULT_ERROR, ex.Message);
                this.DoResponse(context, rsp.ToJSON());
            }
        }

        #region 工具方法
        /// <summary>
        /// 执行响应输出
        /// </summary>
        /// <param name="pContext">Http上下文</param>
        /// <param name="pRspContent">响应内容</param>
        protected void DoResponse(HttpContext pContext, string pRspContent)
        {
            if (!string.IsNullOrWhiteSpace(this.JSONP))
            {
                pRspContent = string.Format("{0}({1})",this.JSONP,pRspContent);
            }

            pContext.Response.Clear();
            pContext.Response.StatusCode = 200;
            pContext.Response.Write(pRspContent);
            pContext.Response.End();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="pContext"></param>
        protected void InsertLog(HttpContext pContext)
        {

        }
        #endregion

        #region 属性集
        /// <summary>
        /// javascript的跨域支持
        /// </summary>
        protected string JSONP { get; set; }
        #endregion

        #region 基础结构
        /// <summary>
        /// 是否允许并发访问
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

    }
}