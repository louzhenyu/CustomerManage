using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Web;

namespace JIT.CPOS.ZODataTransfer
{
    class Program
    {
        static int cycleTime = 10000;

        static void Main(string[] args)
        {
            try
            {
                string batId = string.Empty;

                while (true)
                {
                    Console.WriteLine(string.Format("[{0}]任务开始...", Utils.GetNow()));
                    
                    // to do...

                    Console.WriteLine(string.Format("[{0}]任务结束", Utils.GetNow()));
                    
                    Console.WriteLine(string.Format("".PadLeft(50, '=')));
                    Thread.Sleep(cycleTime);
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("Exception:", ex.ToString())
                });
                Console.Write(ex.ToString());
                Console.Read();
            }
        }

        //public static bool SMSSend(string mobileNO, string SMSContent)
        //{
        //    Encoding gb2312 = Encoding.GetEncoding("gb2312");
        //    string smsContentEncode = HttpUtility.UrlEncode(SMSContent, gb2312);
        //    string uri = string.Format("http://www.jitmarketing.cn/SendSMS.asp?code=jit2010sms&mobilelist={0}&content={1}",
        //        mobileNO, smsContentEncode);
        //    string method = "GET";
        //    string cotent = "";
        //    string data = CPOS.Common.Utils.GetRemoteData(uri, method, cotent);
        //    return true;
        //}

        static LoggingSessionInfo GetLoginUser(string name)
        {
            var obj = new LoggingSessionInfo();
            obj.CurrentLoggingManager = new LoggingManager();
            obj.CurrentLoggingManager.Connection_String = GetConn(name);
            obj.CurrentUser = new BS.Entity.User.UserInfo();
            obj.CurrentUser.User_Id = "1";
            return obj;
        }

        static string GetConn(string name)
        {
            switch (name)
            { 
                case "bs":
                default:
                    return ConfigurationManager.AppSettings["Conn"];
            }
        }
    }
}
