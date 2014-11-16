using System;
using System.Web;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.RateLetterInterface
{
    public class OfflineMessageHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = "Offline message callback received"
            });
            
            //保存离线消息到数据库
            //保存离线消息到离线消息中心
            //string toUser = string.Empty, fromUser = string.Empty, group = string.Empty, content = string.Empty;
            //OfflineMessageManager.Instance.StoreMessage(toUser, fromUser, group, content);
        }

        #endregion
    }
}
