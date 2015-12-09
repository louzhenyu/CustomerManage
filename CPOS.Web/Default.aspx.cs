using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.CPOS.Web
{
    public partial class Default : Page
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
            public int IsAtoC;  //Henry20140905 是否同步会员
            public string channelId;//渠道ID
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
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static LoggingSessionInfo GetLoggingSession(string customerId, string userId)
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = ConfigurationManager.AppSettings["Conn"].Trim();

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }
        public static LoggingSessionInfo GetLoggingSession()
        {
            return GetLoggingSession(
                ConfigurationManager.AppSettings["customer_id"].Trim(),
                ConfigurationManager.AppSettings["user_id"].Trim());
        }
        public static LoggingSessionInfo GetLjLoggingSession(string userId, string customerId)
        {
            if (userId == null || userId.Trim().Length == 0) userId = "1";
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = ConfigurationManager.AppSettings["Conn_lj"].Trim();

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }
        public static LoggingSessionInfo GetLjLoggingSession()
        {
            return GetLjLoggingSession("", "");
        }
        public static LoggingSessionInfo GetLjLoggingSession(string userId)
        {
            return GetLjLoggingSession(userId, "");
        }

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
            if (!string.IsNullOrEmpty(conn))
            {
                //用户角色信息
                cUserService userService = new cUserService(loggingSessionInfo);
                string applicationId = "649F8B8BDA9840D6A18130A5FF4CB9C8";//[T_Def_App] app
                IList<UserRoleInfo> userRoleList = userService.GetUserRoles(loggingSessionInfo.UserID, applicationId);
                if (userRoleList != null && userRoleList.Count > 0)
                {
                    loggingSessionInfo.CurrentUserRole = new UserRoleInfo();
                    loggingSessionInfo.CurrentUserRole.UserId = loggingSessionInfo.UserID;
                    //loggingSessionInfo.CurrentUserRole.UserName = login_user.User_Name;
                    loggingSessionInfo.CurrentUserRole.RoleId = userRoleList[0].RoleId;
                    loggingSessionInfo.CurrentUserRole.RoleCode = userRoleList[0].RoleCode;
                    loggingSessionInfo.CurrentUserRole.RoleName = userRoleList[0].RoleName;

                    loggingSessionInfo.CurrentUserRole.UnitId = userService.GetDefaultUnitByUserIdAndRoleId(
                            loggingSessionInfo.CurrentUserRole.UserId, loggingSessionInfo.CurrentUserRole.RoleId);

                }
                loggingSessionInfo.ClientName = name;
            }
            return loggingSessionInfo;
        }
        /// <summary>
        /// 获取ALD会员登陆信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="isAToC"></param>
        /// <returns></returns>
        public static LoggingSessionInfo GetBSLoggingSession(string customerId, string userId, int isAToC)
        {
            if (userId == null || userId == string.Empty) userId = "1";
            if (isAToC == 1)
                userId = GetVipIDByALDVipID(customerId, userId);//同步会员，并获取商户库VipID
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
        /// <summary>
        /// 获取AP用户登录信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoggingSessionInfo GetAPLoggingSession(string userId)
        {
            if (userId == null || userId == string.Empty) userId = "1";

            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = "";

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = "";
            loggingSessionInfo.Conn = ConfigurationManager.AppSettings["APConn"];

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = "";
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }
        #endregion

        #region Base64/Image
        public static Bitmap GetImageFromBase64(string base64string)
        {
            byte[] b = Convert.FromBase64String(base64string);
            MemoryStream ms = new MemoryStream(b);
            Bitmap bitmap = new Bitmap(ms);
            return bitmap;
        }
        public static string GetBase64FromImage(System.Drawing.Bitmap bmp)
        {
            string strbaser64 = "";
            try
            {
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                strbaser64 = Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                throw new Exception("error:" + ex.ToString());
            }
            return strbaser64;
        }
        #endregion

        #region ToStr
        public static string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public static string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public static string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public static string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public static string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public static string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public static string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public static string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public static string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
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
            string conn = ConfigurationManager.AppSettings["APConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }

        public static string GetCustomerName(string customerId)
        {
            string sql = string.Format(
                "select a.customer_name from t_customer a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["APConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion

        #region 将ALD会员同步到商户并获取商户VipID add by henry 2014-9-3
        public static string GetVipIDByALDVipID(string customerId, string aldVipId)
        {
            var parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = customerId };
            parm[1] = new SqlParameter("@ALDVipId", System.Data.SqlDbType.NVarChar) { Value = aldVipId };
            string conn = ConfigurationManager.AppSettings["ALDMemberConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            DataSet dsVipId = sqlHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetCustomerVipIdByALDVipId", parm);
            string userId = "1";
            if (dsVipId.Tables[0].Rows.Count > 0)
            {
                userId = dsVipId.Tables[0].Rows[0]["CustomerVipId"].ToString();
            }
            return userId;
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
            string conn = ConfigurationManager.AppSettings["APConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion

        #region WriteLog
        public static void WriteLog(LoggingSessionInfo currentUserInfo, string ifName, ReqData ReqContent,
            Default.LowerRespData respObj, string specialParams)
        {
            try
            {
                if (ReqContent == null)
                {
                    ReqContent = new ReqData();
                    ReqContent.common = new ReqCommonData();
                }
                SysVisitLogsBLL logService = new SysVisitLogsBLL(currentUserInfo);
                SysVisitLogsEntity logObj = new SysVisitLogsEntity();
                logObj.LogsID = CPOS.Common.Utils.NewGuid();
                logObj.Locale = ReqContent.common.locale;
                logObj.UserID = ReqContent.common.userId;
                logObj.SessionID = ReqContent.common.sessionId;
                logObj.Version = ReqContent.common.version;
                logObj.Plat = ReqContent.common.plat;
                logObj.DeviceToken = ReqContent.common.deviceToken;
                logObj.OSInfo = ReqContent.common.osInfo;
                logObj.ChannelId = ReqContent.common.channel;
                logObj.WeiXinId = ReqContent.common.weiXinId;
                logObj.OpenId = ReqContent.common.openId;

                logObj.LogType = ifName;
                logObj.ResultCode = respObj.code;
                logObj.ResultDescription = respObj.description;
                logObj.SpecialParams = specialParams;

                logObj.IpAddress = GetClientIP();

                if (logObj.UserID == null)
                {
                    logObj.UserID = currentUserInfo.UserID;
                }
                if (logObj.UserID == null)
                {
                    logObj.UserID = "1";
                }

                logService.Create(logObj);
            }
            catch (Exception ex)
            {
                respObj.exception = "日志写入错误: ";//+ ex.ToString()
                Loggers.Debug(new DebugLogInfo() { Message = ifName + " " + respObj.exception });
            }
        }

        public static void WriteLog(LoggingSessionInfo currentUserInfo, string ifName, ReqData ReqContent,
            Default.LowerRespData respObj)
        {
            WriteLog(currentUserInfo, ifName, ReqContent, respObj, null);
        }

        public static string GetClientIP()
        {
            try
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return userHostAddress;
            }
            catch (Exception ex)
            {
                return "获取IP地址失败:" + ex.ToString();
            }
        }
        #endregion

        #region ReqiAlumniData
        public class ReqiAlumniData
        {
            public ReqiAlumniCommonData common;
        }
        public class ReqiAlumniCommonData
        {
            public string weiXinId;
            public string openId;
            public string customerId;
        }
        #endregion
    }
}