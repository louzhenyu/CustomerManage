using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Entity;
using System.Configuration;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.Web
{
    public partial class Default : JITPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region RespData
        public class RespData
        {
            public string Code = "200";
            public string Description = "操作成功";
            public string Exception = null;
            public string Data;
        }
        public class LowerRespData
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public string data;
            public int searchCount;
        }
        #endregion

        #region ReqData
        public class ReqData
        {
            public ReqCommonData common;
        }
        public class ReqCommonData
        {
            public string weiXinId;
            public string openId;
            public string locale;
            public string userId;
            public string sessionId;
            public string version;
            public string plat;
            public string deviceToken;
            public string osInfo;
            public string channel;
            public string baiduPushAppId;
            public string baiduPushChannelId;
            public string baiduPushUserId;
            public string customerId;
            public string webPage;
            public string isALD;//Jermyn20140314 ;1=是
        }
        public class BaseEntity
        {
            public string code
            {
                get;
                set;
            }

            public string description
            {
                get;
                set;
            }

            public string exception
            {
                get;
                set;
            }
        }
        #endregion
        #region 获取登录用户信息

        #region 

        ///// <summary>
        ///// 获取登录用户信息
        ///// </summary>
        ///// <param name="customerId"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public static LoggingSessionInfo GetLjLoggingSession(string userId, string customerId)
        //{
        //    if (userId == null || userId.Trim().Length == 0) userId = "1";
        //    LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        //    //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
        //    loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
        //    loggingSessionInfo.CurrentUser.User_Id = userId;
        //    loggingSessionInfo.CurrentUser.customer_id = customerId;

        //    loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
        //    loggingSessionInfo.ClientID = customerId;
        //    loggingSessionInfo.Conn = ConfigurationManager.AppSettings["Conn_lj"].Trim();

        //    loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
        //    loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
        //    loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
        //    loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
        //    loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
        //    loggingSessionInfo.CurrentLoggingManager.User_Name = "";
        //    return loggingSessionInfo;
        //}
        //public static LoggingSessionInfo GetLjLoggingSession()
        //{
        //    return GetLjLoggingSession("", "");
        //}
        //public static LoggingSessionInfo GetLjLoggingSession(string userId)
        //{
        //    return GetLjLoggingSession(userId, "");
        //}

        /// <summary>
        /// 获取BS用户登录信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoggingSessionInfo GetBSLoggingSession(string customerId, string userId)
        {
            if (userId == null || userId == string.Empty) userId = "1";
            string conn = GetCustomerConn(customerId);
            string name = GetCustomerName(customerId);

            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = conn;

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = name;
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";

            loggingSessionInfo.ClientName = name;
            return loggingSessionInfo;
        }
        #endregion



        #region GetCustomerConn
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
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

        public static string GetCustomerName(string customerId)
        {
            string sql = string.Format(
                "select a.customer_name from t_customer a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion

        #region GetCustomerId
        /// <summary>
        /// 获取客户ID
        /// </summary>
        public static string GetCustomerId(string customerCode)
        {
            string sql = string.Format(
                "select top 1 customer_id " +
                " from t_customer a where a.customer_code='{0}' ",
                customerCode);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion
        #endregion
    }
}