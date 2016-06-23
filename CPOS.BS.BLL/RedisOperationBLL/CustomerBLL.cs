using CPOS.Common.Core;
using JIT.CPOS.BS.BLL.RedisOperationBLL.Connection;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.Utility.DataAccess;
using RedisOpenAPIClient.Models.CC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL
{
    public class CustomerBLL : LazyInstance<CustomerBLL>
    {
        /// <summary>
        /// 获取所有商户ID
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetCustomerList()
        {
            var Result = new Dictionary<string, string>();
            string sql = @"select a.customer_id as CustomerID,'user id='+b.db_user+';password='+b.db_pwd+';data source='+b.db_server+';database='+b.db_name+';' as conn 
                        from  cpos_ap..t_customer as a left join cpos_ap..t_customer_connect as b on a.customer_id=b.customer_id 
						where a.customer_status=1";

            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            DataSet ds = sqlHelper.ExecuteDataset(sql);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Result.Add(ds.Tables[0].Rows[i]["CustomerID"].ToString(), ds.Tables[0].Rows[i]["conn"].ToString());
                }
            }
            return Result;
        }

        /// <summary>
        /// 获取BS用户登录信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public  LoggingSessionInfo GetBSLoggingSession(string customerId, string userId)
        {
            if (userId == null || userId == string.Empty) userId = "system";

            string conn = "";
            string name = "";




            CC_Connection connection = new RedisConnectionBLL().GetConnection(customerId);//从redis里获取商户数据库链接
         //   RedisXML _RedisXML = new RedisXML();
            //如果从缓存里获取不到信息，就从数据库读取，并种到缓存里
            if (connection == null || string.IsNullOrEmpty(connection.ConnectionStr) || string.IsNullOrEmpty(connection.Customer_Name))
            {
                //记录redis读取不成功，从数据库里读取数据的情况
               // _RedisXML.RedisReadDBCount("Connection", "商户数据库链接", 2);

                conn = GetCustomerConn(customerId);
                name = GetCustomerName(customerId);
                string code = GetCustomerCode(customerId);
                new RedisConnectionBLL().SetConnection(customerId, conn, name, code);
            }
            else
            {
                //记录redis读取日志
               // _RedisXML.RedisReadDBCount("Connection", "商户数据库链接", 1);
                conn = connection.ConnectionStr;
                name = connection.Customer_Name;
            }




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

        #region GetCustomerConn
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public  string GetCustomerConn(string customerId)
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

        public  string GetCustomerName(string customerId)
        {
            string sql = string.Format(
                "select a.customer_name from t_customer a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }

        /// <summary>
        /// 获取商户编码
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static string GetCustomerCode(string customerId)
        {
            string sql = string.Format(
                "select a.customer_code from t_customer a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion

    }
}
