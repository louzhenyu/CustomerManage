using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using JIT.Utility.Notification;

namespace JIT.CPOS.BS.BLL.NoticeEmail
{
    public class NoticeMailSender<T> where T : class, IMailTemplate
    {
        private static T _instance;
        private static readonly object _SyncObject = new object();
        protected static IMailTemplate Instance 
        {
            get
            {
                if (_instance == null)
                {
                    lock (_SyncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = Activator.CreateInstance(typeof(T), true) as T;
                        }
                    }
                }
                return _instance;
            }
        }

        public static bool SendMail(string mailto, string mailsubject, object data)
        {
            //var mailbody = Instance.Render(data);
            return Mail.SendMail(mailto, mailsubject, data.ToString());
        }
    }
}