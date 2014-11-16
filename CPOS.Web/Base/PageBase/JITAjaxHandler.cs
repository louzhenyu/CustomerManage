using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.Base.PageBase
{
    /// <summary>
    /// Ajax处理基类
    /// <remarks>
    /// <para>1、AJAX处理程序需继承此类</para>
    /// <para>2、子类需实现ProcessAjaxRequest方法，对请求做响应</para>
    /// </remarks>
    /// </summary>
    public abstract class JITAjaxHandler : IHttpHandler, IRequiresSessionState
    {
        #region ProcessRequest
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                this.CurrentContext = context;

                RunMethod(this.GetType(), context.Request["action"], context);
            }
            catch (ThreadAbortException)//Response.End会通过ThreadAbortException来结束响应
            {
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                //context.Response.Write("{success:false}");
            }
        }
        #endregion
        
        /// <summary>
        /// HTTP请求的上下文
        /// </summary>
        protected HttpContext CurrentContext { get; private set; }

        #region 用反射执行调用方法
        /// <summary>
        /// 用反射执行调用Handler方法
        /// 所有Handler方法的参数类型只有3种： 1.空  2.NameValueCollection 3.string
        /// 其中NameValueCollection 类型的参数名有3种： rParams form queryString  , string 类型的参数名无限制
        /// 备注NameValueCollection 类型的参数名 最好使用对应的类型，最好使用form 或 queryString ，rParams是所有参数会降低执行效率
        /// </summary>
        /// <param name="t">Handler Type</param>
        /// <param name="methodName">Handler MethodName</param>
        public void RunMethod(Type handler, string methodName, HttpContext context)
        {
            var result = new object();
            if (string.IsNullOrEmpty(methodName))
            {
                result = "{success:false,msg:'未传入方法'}";
                context.Response.Write(result);
                context.Response.End();
                return;
            }
            object obj = null;
            obj = Activator.CreateInstance(handler);
            MethodInfo method = handler.GetMethod(methodName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public);
            if (method == null)
            {
                result = string.Format("{{success:false,msg:'未找到方法{0}'}}", methodName);
                context.Response.Write(result);
                context.Response.End();
                return;
            }
            var parms = method.GetParameters();
            var objs = new object[parms.Length];
            for (var i = 0; i < parms.Length; i++)
            {
                var parm = parms[i];

                if (parm.ParameterType == typeof(NameValueCollection))
                {
                    if (parm.Name.Equals("rParams"))
                        objs[i] = context.Request.Params;
                    else if (parm.Name.Equals("form"))
                        objs[i] = context.Request.Form;
                    else if (parm.Name.Equals("queryString"))
                        objs[i] = context.Request.QueryString;
                }
                else if (parm.ParameterType == typeof(string))
                {
                    object objReq = context.Request[parm.Name];
                    if (objReq != null)
                    {
                        objs[i] = objReq.ToString();
                    }
                    else
                    {
                        objs[i] = "";
                    }
                }
                else if (parm.ParameterType == typeof(HttpFileCollection))
                {
                    objs[i] = context.Request.Files;
                }
            }

            result = method.Invoke(obj, objs);
            context.Response.Write(result);
            //context.Response.End();
        }
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
        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="pErrorMessage"></param>
        private void ErrorHandling(HttpContext pContext, string pErrorMessage)
        {
            pContext.Response.Clear();
            pContext.Response.StatusCode = 500;
            pContext.Response.Write(pErrorMessage);
            pContext.Response.End();
        }
        #endregion
    }   
}