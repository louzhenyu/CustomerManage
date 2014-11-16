using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.Module
{
    /// <summary>
    /// CheckIn 的摘要说明
    /// </summary>
    public class BrowserRecord : IHttpHandler
    {
        string customerId = "";
        string reqContent = "";

        public void ProcessRequest(HttpContext context)
        {
            reqContent = context.Request["ReqContent"];
            string action = context.Request["action"].ToString().Trim();
            string content = string.Empty;

            JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(context, context.Request, action);

            switch (action)
            {
                //case "browserRecord":
                //    content = RecordBrowserLog();
                //    break;
                default:
                    content = RecordBrowserLog();   //参数判断
                    break;
            }

            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write(content);
            context.Response.End();
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 注册
        public string RecordBrowserLog()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("BrowserRecord: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<ReqData>();
                #endregion

                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                respData.code = "200";
                respData.description = "记录完成";
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();

                Loggers.Exception(new ExceptionLogInfo() { ErrorMessage = ex.ToJSON() });
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion
    }
}