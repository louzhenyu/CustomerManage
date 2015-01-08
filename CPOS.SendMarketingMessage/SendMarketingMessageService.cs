using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using System.Diagnostics;
using JIT.Utility.ExtensionMethod;

namespace CPOS.SendMarketingMessage
{
    partial class SendMarketingMessageService : ServiceBase
    {
        string customerID = ConfigurationManager.AppSettings["CustomerID"];
        private bool sendFlag = true;
        private int sleepTime = int.Parse(ConfigurationManager.AppSettings["SleepingTime"]);
        // private const int sleepTime = 300000;

        EventLog log = new EventLog("SendMarketingMessage Windows Service");

        public SendMarketingMessageService()
        {
            if (!EventLog.SourceExists("SendMarketingMessage"))
                EventLog.CreateEventSource("SendMarketingMessage", "SendMarketingMessage Windows Service");

            log.Source = "SendMarketingMessage";

            InitializeComponent();

            //OnStart(null);              //for debug
        }

        protected override void OnStart(string[] args)
        {
            log.WriteEntry("服务已启动......", EventLogEntryType.Information);

            sendFlag = true;

            log.WriteEntry("要推送的客户ID有：" + customerID, EventLogEntryType.Information);
            //启动发送线程
            string[] customers = customerID.Split(',');
            foreach (var customer in customers)
            {
                log.WriteEntry("为" + customer + "创建进程......", EventLogEntryType.Information);
                ParameterizedThreadStart parameterizedThreadStart = SendMarketingMessage;
                Thread sendThread = new Thread(parameterizedThreadStart);
                sendThread.IsBackground = true;
                sendThread.Start(customer);
                log.WriteEntry( customer + "进程已启动......", EventLogEntryType.Information);
            }
        }

        protected override void OnStop()
        {
            sendFlag = false;
            log.WriteEntry("服务已停止.", EventLogEntryType.Information);
        }
        /// <summary>
        /// 发送营销消息消息
        /// </summary>
        public void SendMarketingMessage(object o)
        {
            TimeSpan timeSpan = new TimeSpan();
            while (true)
            {
                try
                {
                    if (sendFlag)
                    {
                        LoggingSessionInfo loggingSessionInfo = GetLoggingSession(o.ToString(), "1");
                        SendMarketingMessageBLL bll = new SendMarketingMessageBLL(loggingSessionInfo);
                        timeSpan = bll.Send();
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    log.WriteEntry(ex.ToJSON(), EventLogEntryType.Error);
                }
                log.WriteEntry(o.ToString() + "将在" + timeSpan.TotalSeconds + "秒后再次启动。", EventLogEntryType.Information);

                //timeSpan = new TimeSpan(0, 0, 3);   //for debug
                Thread.Sleep(timeSpan);
            }
        }
        public static LoggingSessionInfo GetLoggingSession(string customerId, string userId)
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new JIT.CPOS.BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = GetCustomerConn(customerId);

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }
        public static string GetCustomerConn(string customerId)
        {
            string sql = string.Format(
                "select 'user id='+a.db_user+';password='+a.db_pwd+';data source='+a.db_server+';database='+a.db_name+';' conn " +
                " from t_customer_connect a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["APConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
    }
}
