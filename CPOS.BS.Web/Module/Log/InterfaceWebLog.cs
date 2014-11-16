using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Text;
using log4net.Layout.Pattern;
using log4net.Layout;
using log4net.Core;
using System.Reflection;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Module.Log.InterfaceWebLog
{
    public static class Logger
    {
        public static void Log(HttpContext context, HttpRequest request, string method)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("Logger");
            string ip;
            var reqContent = request["ReqContent"];

            //JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo() { Message = "记录日志：" + reqContent.ToJSON() });

            if (reqContent != null && reqContent.Length > 0)
            {
                reqContent = HttpUtility.UrlDecode(reqContent);
                Default.ReqData reqObj = reqContent.DeserializeJSONTo<Default.ReqData>();

                if (context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy
                {
                    ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
                }
                else // not using proxy or can't get the Client IP
                {
                    ip = context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
                }

                logger.Info(new LogContent(method, request["ReqContent"], reqObj.common.userId, reqObj.common.customerId, reqObj.common.userId, reqObj.common.openId, ip, "", "", reqObj.common.webPage));
            }
        }
    }
    /// <summary>
    /// LogContent 的摘要说明
    /// </summary>
    public class LogContent
    {
        public LogContent(string interfaceName, string reqContent, string createBy, string customerId, string userId, string openId, string requestIP, string deviceNumber, string versionNumber, string webPage)
        {
            InterfaceName = interfaceName;
            ReqContent = reqContent;
            CreateBy = createBy;
            CustomerId = customerId;
            UserId = userId;
            OpenId = openId;
            RequestIP = requestIP;
            DeviceNumber = deviceNumber;
            VersionNumber = versionNumber;
            WebPage = webPage;
        }

        //接口方法名称
        public string InterfaceName { get; set; }
        //请求参数
        public string ReqContent { get; set; }

        public string CreateBy { get; set; }

        public string CustomerId { get; set; }

        public string UserId { get; set; }

        public string OpenId { get; set; }

        public string RequestIP { get; set; }

        //设备号
        public string DeviceNumber { get; set; }

        //程序版本号
        public string VersionNumber { get; set; }

        public string WebPage { get; set; }
    }

    public class LogLayout : PatternLayout
    {
        public LogLayout()
        {
            this.AddConverter("property", typeof(PatternConverter));
        }
    }

    public class PatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                // Write the value for the specified key
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }

        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;

            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);

            return propertyValue;
        }

    }
}