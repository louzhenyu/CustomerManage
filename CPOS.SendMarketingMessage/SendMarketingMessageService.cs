using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;

namespace CPOS.SendMarketingMessage
{
    partial class SendMarketingMessageService : ServiceBase
    {
        string customerID = ConfigurationManager.AppSettings["CustomerID"];
        private bool sendFlag = true;
        private int sleepTime = int.Parse(ConfigurationManager.AppSettings["SleepingTime"]);
        // private const int sleepTime = 300000;
        public SendMarketingMessageService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            sendFlag = true;
            //启动发送线程
            string[] customers = customerID.Split(',');
            foreach (var customer in customers)
            {
                ParameterizedThreadStart parameterizedThreadStart = SendMarketingMessage;
                Thread sendThread = new Thread(parameterizedThreadStart);
                sendThread.Start(customer);
            }
        }

        protected override void OnStop()
        {
            sendFlag = false;
        }
        /// <summary>
        /// 发送营销消息消息
        /// </summary>
        public void SendMarketingMessage(object o)
        {
            while (true)
            {
                try
                {
                    if (sendFlag)
                    {
                        LoggingSessionInfo loggingSessionInfo = GetLoggingSession(o.ToString(), "1");
                        SendMarketingMessageBLL bll = new SendMarketingMessageBLL(loggingSessionInfo);
                        bll.Send();
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
                Thread.Sleep(sleepTime);
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
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
    }
}
