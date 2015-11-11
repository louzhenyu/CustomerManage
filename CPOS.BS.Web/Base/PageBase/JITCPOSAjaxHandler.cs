/*
 * Author		:roy.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:19/2/2012 10:03:10 AM
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
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Collections.Specialized;

using JIT.Utility;
using JIT.Utility.Log;
using JIT.Utility.Web;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.PageBase
{
    public abstract class JITCPOSAjaxHandler : JITAjaxHandler<LoggingSessionInfo>
    {
        protected int PageSize = 15;

        ///// <summary>
        ///// use for utility log
        ///// </summary>
        //protected override LoggingSessionInfo BasicUserInfo
        //{
        //    get { return new SessionManager().CurrentUserLoginInfo; }
        //}

        protected override LoggingSessionInfo CurrentUserInfo
        {
            get { return new SessionManager().CurrentUserLoginInfo; }
        }

        #region 页面入口
        protected override void ProcessAjaxRequest(HttpContext pContext)
        {
            //验证是否登录
            this.Authenticate();
            //验证是否有方法权限
            //this.CheckMethodRight(pContext.Request["mid"], pContext.Request["btncode"]);
            //执行方法
            this.BTNCode = pContext.Request["btncode"];
            //this.MID = pContext.Request["mid"];
            this.Method = pContext.Request["method"];
            this.AjaxRequest(pContext);
           
            //this.RunMethod(this.GetType(), pContext.Request["method"], pContext);
        }
        #endregion

        #region 方法
        protected string Request(string key)
        {
            if (HttpContext.Current.Request[key] == null) return string.Empty;
            return HttpContext.Current.Request[key];
        }

        protected string FormatParamValue(string value)
        {
            if (value == null || value == "null" || value == "undefined" || value == "")
            {
                return string.Empty;
            }
            return value.Trim();
        }

        /// <summary>
        /// 认证(内部调用)
        /// </summary>
        protected override void Authenticate()
        {
            if (!new JITPage().CheckUserLogin())
            {
                HttpContext.Current.Response.Write("{success:false,msg:'未登录，请先登录'}");
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 请求Action
        /// </summary>
        public string BTNCode { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method
        {
            get;
            set;
        }

        /// <summary>
        /// 验证是否有方法权限
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="buttonCode"></param>
        protected void CheckMethodRight(string menuId, string buttonCode)
        {
            //string result = "";
            ////菜单Code，ButtonCode
            //if (string.IsNullOrEmpty(menuId))
            //{
            //    result = "{success:false,msg:'未传入菜单标识MenuId(mid)'}";
            //    HttpContext.Current.Response.Write(result);
            //    HttpContext.Current.Response.End();
            //    return;
            //}
            //if (string.IsNullOrEmpty(buttonCode))
            //{
            //    result = "{success:false,msg:'未传入按钮标识ButtonCode(btncode)'}";
            //    HttpContext.Current.Response.Write(result);
            //    HttpContext.Current.Response.End();
            //    return;
            //}
            
            ////验证menu btncode 权限
            //int rightCount = new SessionManager().CurrentUserLoginInfo.UserOPRight.Tables[0].Select(
            //          "ClientMenuID='" + menuId + "'"
            //          + " and ButtonCode='" + buttonCode + "'").Length;
            //if (rightCount == 0)
            //{
            //    result = "{success:false,msg:'没有权限'}";
            //    HttpContext.Current.Response.Write(result);
            //    HttpContext.Current.Response.End();
            //}
        }

        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected abstract void AjaxRequest(HttpContext pContext);

        /// <summary>
        /// 用反射执行调用Handler方法
        /// 所有Handler方法的参数类型只有3种： 1.空  2.NameValueCollection 3.string
        /// 其中NameValueCollection 类型的参数名有3种： rParams form queryString  , string 类型的参数名无限制
        /// </summary>
        /// <param name="handler">执行方法的handler页</param>
        /// <param name="methodName">方法名，需与JS传递的方法名一致</param>
        /// <param name="context"></param>
        protected void RunMethod(Type handler, string methodName, HttpContext context)
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
            MethodInfo method = handler.GetMethod(methodName);
            if (method == null)
            {
                result = string.Format("{success:false,msg:'未找到方法{0}'}", methodName);
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
            context.Response.End();
        }

        #endregion
    }
}