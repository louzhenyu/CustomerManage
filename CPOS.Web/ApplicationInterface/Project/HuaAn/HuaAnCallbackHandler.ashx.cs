using System;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 华安回调Handler.
    /// </summary>
    public class HuaAnCallbackHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext pContext)
        {
            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "华安PageUrl回调Handler."});
            try
            {
                HttpContext httpContext = pContext;
                //参数解析
                var action = httpContext.Request.QueryString["action"];
                if (string.IsNullOrEmpty(action)) return;
                //获取华安响应表单对象
                var message = new HanAnMessage
                {
                    verNum = httpContext.Request.Form["verNum"],
                    sysdate = httpContext.Request.Form["sysdate"],
                    systime = httpContext.Request.Form["systime"],
                    txcode = httpContext.Request.Form["txcode"],
                    seqNO = httpContext.Request.Form["seqNO"],
                    maccode = httpContext.Request.Form["maccode"],
                    content = httpContext.Request.Form["content"]
                };
                //AES解密
                string content = Utility.AESDecrypt(message.content, HuaAnConfigurationAppSitting.AesKey);
                message.content = content;

                #region 插入日志信息

                Loggers.DEFAULT.Debug(new DebugLogInfo {Message = message.ToJSON()});
                #endregion
                    
                //验证回调是否执行过
                if (Common.Verify(action, content)) return;
                switch (action)
                {
                    case "BuyCallBack":  //基金购买。
                        Common.BuyCallBack(content);
                        break;
                    case "RansomCallBack":  //赎回。
                        Common.RansomCallBack(content);
                        break;
                    case "TransferCallBack":  //用号
                        Common.TransferCallBack(content);
                        break;
                    default:
                        throw new APIException(string.Format("未实现Action名为{0}的处理.", action)) { ErrorCode = 201 };
                }
            }
            catch (Exception ex)
            {
                Loggers.DEFAULT.Exception(new ExceptionLogInfo {ErrorMessage = ex.Message});
            }
        }

        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion
    }
}