/*
 * Author		:roy.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:19/2/2012 10:03:10 AM
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using JIT.Utility;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using System.Configuration;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.Web.Session
{
    /// <summary>
    /// 系统Session相关读取操作 
    /// </summary>
    public class SessionManager
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SessionManager()
        {
        }
        #endregion

        ///// <summary>
        ///// 当前的用户登录信息
        ///// </summary>
        //public LoggingSessionInfo CurrentUserLoginInfo
        //{
        //    get
        //    {
        //        var cache = HttpContext.Current.Session[SessionKeyConst.CURRENT_USER_LOGIN_INFO];
        //        return cache as LoggingSessionInfo;
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[SessionKeyConst.CURRENT_USER_LOGIN_INFO] = value;
        //    }
        //}


        /// <summary>
        /// 当前的用户登录信息
        /// </summary>
        public LoggingSessionInfo CurrentUserLoginInfo
        {
            get
            {
                var cache = HttpContext.Current.Session[SessionKeyConst.CURRENT_USER_LOGIN_INFO];
                return cache as LoggingSessionInfo;
            }
        }

        public void SetCurrentUserLoginInfo(LoggingSessionInfo pUserInfo)
        {
            HttpContext.Current.Session[SessionKeyConst.CURRENT_USER_LOGIN_INFO] = pUserInfo;
        }


        public static string KEY_CUSTOMER_ID = "customer_id";
        /// <summary>
        /// 获取或设置登录用户的数据库连接
        /// </summary>
        public string CurrentCustomerId
        {
            get
            {
                return HttpContext.Current.Session[KEY_CUSTOMER_ID] == null ? "" :
                    (string)HttpContext.Current.Session[KEY_CUSTOMER_ID];
            }
            set { HttpContext.Current.Session[KEY_CUSTOMER_ID] = value; }
        }

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public LoggingSessionInfo loggingSessionInfo
        {
            get
            {
                return HttpContext.Current.Session["loggingSessionInfo"] == null ? null :
                    (LoggingSessionInfo)HttpContext.Current.Session["loggingSessionInfo"];
            }
            set { HttpContext.Current.Session["loggingSessionInfo"] = value; }
        }

        /// <summary>
        /// 获取或设置当前用户角色信息
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                return HttpContext.Current.Session["UserInfo"] == null ? null :
                    (UserInfo)HttpContext.Current.Session["UserInfo"];
            }
            set { HttpContext.Current.Session["UserInfo"] = value; }
        }

        /// <summary>
        /// 获取登录管理平台信息
        /// </summary>
        public LoggingManager LoggingManager
        {
            get
            {
                return HttpContext.Current.Session["LoggingManager"] == null ? null :
                    (LoggingManager)HttpContext.Current.Session["LoggingManager"];
            }
            set { HttpContext.Current.Session["LoggingManager"] = value; }
        }

        /// <summary>
        /// 获取或设置当前用户角色信息
        /// </summary>
        public UserRoleInfo UserRoleInfo
        {
            get
            {
                return HttpContext.Current.Session["UserRoleInfo"] == null ? null :
                    (UserRoleInfo)HttpContext.Current.Session["UserRoleInfo"];
            }
            set { HttpContext.Current.Session["UserRoleInfo"] = value; }
        }


        #region Jermyn20140709 获取LoggingSessionInfo
        /// <summary>
        /// 根据客户标识获取客户连接信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoggingSessionInfo GetLoggingSessionByCustomerId(string customerId,string userId = null)
        {
            if (userId == null || userId == string.Empty) userId = "1";
            string conn = GetCustomerConn(customerId);

            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new JIT.CPOS.BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = conn;

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }

        /// <summary>
        /// 根据微信appId获取客户连接信息
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoggingSessionInfo GetLoggingSessionByAppId(string AppId, string userId = null)
        {
            string customerId = GetCustomerByAppId(AppId);
            if (userId == null || userId == string.Empty) userId = "1";
            string conn = GetCustomerConn(customerId);

            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            
            loggingSessionInfo.CurrentUser = new JIT.CPOS.BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = conn;

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }
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
        #endregion

        #region 获取客户标识
        public static string GetCustomerByAppId(string AppId)
        {
            string sql = "select top 1 CustomerId From cpos_ap..TCustomerWeiXinMapping where AppId = 'wx8a8c3152a369751a' and IsDelete = '0' ";
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion

        #endregion
    }
}
