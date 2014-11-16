using System;
using System.Threading;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 处理华安接口的RetUrl回调
    /// </summary>
    public class HuaAnRetCallbackHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "华安RetUrl回调Handler."});
            try
            {
                HttpContext httpContext = context;
                //参数解析
                var action = httpContext.Request.QueryString["action"];
                if (string.IsNullOrEmpty(action))
                {
                    Loggers.DEFAULT.Debug(new DebugLogInfo { Message = "Action is empty, skip process." });
                    return;
                }

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
#if(DEBUG)
                Loggers.DEFAULT.Debug(new DebugLogInfo { Message = "RetUrl表单交易代码：" + message.txcode});
                Loggers.DEFAULT.Debug(new DebugLogInfo { Message = "RetUrl表单内容：" + message.content });
#endif

                //AES解密
                string content = Utility.AESDecrypt(message.content, HuaAnConfigurationAppSitting.AesKey);
                message.content = content;

                #region 插入日志信息

                Loggers.DEFAULT.Debug(new DebugLogInfo { Message = "RetUrl message：" + message.ToJSON() });
                #endregion

                // 启动工作线程
                string[] data = {action, content};
                ThreadPool.QueueUserWorkItem(o =>
                {
                    var d = o as string[];
                    if (d == null)
                    {
                        Loggers.DEFAULT.Debug(new DebugLogInfo { Message = "RetUrl callback: 无效的参数" });
                        return;
                    }

                    var act = d[0];
                    var con = d[1];
#if(DEBUG)
                    Loggers.DEFAULT.Debug(new DebugLogInfo { Message = "启动RetUrl callback线程." });
#endif
                    try
                    {
                        // 等待30秒，确保前端的PageUrl回调已经结束。
                        Thread.Sleep(30000);
                        // 验证回调是否执行过
                        if (Common.Verify(act, con))
                            return;

                        switch (action)
                        {
                            case "BuyCallBack": //基金购买。
                                Common.BuyCallBack(content);
                                break;
                            case "RansomCallBack": //赎回。
                                Common.RansomCallBack(content);
                                break;
                            case "TransferCallBack": //用号
                                Common.TransferCallBack(content);
                                break;
                            default:
                                Loggers.DEFAULT.Debug(new DebugLogInfo { Message = string.Format("未实现Action名为{0}的处理.", action) });
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Loggers.DEFAULT.Exception(new ExceptionLogInfo{ErrorMessage = "RetCallback action process异常：" + ex.Message});
                    }
                }, data);

                httpContext.Response.Write(DateTime.Now + ": RetCallbackHandler ok.");
                httpContext.Response.End();
            }
            catch (Exception ex)
            {
                Loggers.DEFAULT.Exception(new ExceptionLogInfo {ErrorMessage = "RetCallbackHandler异常：" + ex.Message});
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}