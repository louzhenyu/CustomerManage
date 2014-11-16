using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using JIT.Utility.Log;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Util.SMS
{
    public static class SMSHelper
    {
        public static bool Send(string pPhone, string pContent, string pSign, out string msg)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["SMSURL"];
                if (string.IsNullOrEmpty(url))
                    throw new Exception("未配置短信服务URL");
                var para = new { MobileNO = pPhone, SMSContent = string.Format(@"您的验证码是：【{0}】", pContent), Sign = pSign };
                var request = new { Action = "SendMessage", Parameters = para };
                string str = string.Format("request={0}", request.ToJSON());
                Loggers.Debug(new DebugLogInfo() { Message = "发送短信:" + str });
                var res = HttpClient.PostQueryString(url, str);
                var response = res.DeserializeJSONTo<Response>();
                Loggers.Debug(new DebugLogInfo() { Message = "收到返回信息:" + response.ToJSON() });
                if (response.ResultCode < 100)
                {
                    msg = "短信发送成功";
                    return true;
                }
                else
                {
                    msg = "短信发送失败:" + response.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                msg = ex.Message;
                return false;
            }
        }
    }

    public class Response
    {
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public object ResData { get; set; }
    }

    public static class CharsFactory
    {
        public static string Create6Char()
        {
            Random r = new Random();
            var i = r.Next(100000, 1000000);
            return i.ToString();
        }
    }
}