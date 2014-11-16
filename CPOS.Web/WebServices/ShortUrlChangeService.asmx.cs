using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.WebServices
{
    /// <summary>
    /// ShortUrlChangeService 的摘要说明 长连接换成短连接
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ShortUrlChangeService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 长连接转换成短连接
        /// </summary>
        /// <param name="OldUrl">长连接（原链接）</param>
        /// <param name="ShortUrl">短连接</param>
        /// <param name="strError">错误提示</param>
        /// <returns>是否成功</returns>
        [WebMethod]
        public bool SetShortUrlChange(string OldUrl, out string ShortUrl, out string strError)
        {
            LoggingSessionInfo loggingSessionInfo = Default.GetAPLoggingSession("");
            ShortUrlChangeBLL server = new ShortUrlChangeBLL(loggingSessionInfo);
            bool bReturn = server.SetShortUrlChange(OldUrl, out ShortUrl, out strError);
            return bReturn;
        }
    }
}
