/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 14:33:03
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表T_User的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_UserDAO : Base.BaseCPOSDAO, ICRUDable<T_UserEntity>, IQueryable<T_UserEntity>
    {
        #region 宝洁用户登录
        /// <summary>
        /// 宝洁用户登录
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet LoginPGUser(string customerID, string email, string userName, string pTypeID)
        {
            string sql = " SELECT user_password,tuser.USER_ID,user_name,user_gender,user_email,user_telephone,user_birthday,user_telephone,ro.role_name as RoleName";
            sql += " ,pgu.MANAGER_EMAIL SuperiorName ,pgu.FUNC AS Dept,CAST(pgu.JOB_BAND AS NVARCHAR(4)) AS JobFunc ,pgu.LOCATION  Location,pgch.NAME  Channel,pgu.SERVICE_YEAR AS ServiceYear";
            sql += " ,SUBORDINATE_COUNT AS SubordinateCount,(SELECT TOP 1 oi.ImageURL FROM ObjectImages oi WHERE oi.ObjectId = tuser.user_id AND oi.IsDelete = '0' ORDER BY oi.DisplayIndex) highImageUrl";
            sql += " FROM  T_User tuser ";//inner join TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId 
            sql += " INNER JOIN T_USER_ROLE userRole on userRole.user_id=tuser.user_id";
            sql += " INNER JOIN T_Role  ro on ro.role_id =userRole.role_id ";
            sql += " INNER JOIN PgUser pgu ON pgu.USER_ID=tuser.user_id";
            sql += " LEFT JOIN pgChannel pgch ON pgu.channel_id=pgch.channelid";
            sql += " LEFT JOIN UserDeptJobMapping depMapping ON depMapping.USER_ID = tuser.user_id";
            sql += " LEFT JOIN T_UNIT unit ON unit.unit_id = depMapping.UnitID AND unit.type_id = @type_id";
            sql += " LEFT JOIN JobFunction jobFunc ON jobFunc.JobFunctionID = depMapping.JobFunctionID";
            sql += " WHERE tuser.user_status = '1' ";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@type_id", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A

            if (!string.IsNullOrEmpty(customerID))
            {
                sql += " and  tuser.customer_id=@customer_id ";
                para.Add(new SqlParameter("@customer_id", customerID));
            }
            if (!string.IsNullOrEmpty(email))
            {
                sql += " and tuser.user_email=@user_email ";
                para.Add(new SqlParameter("@user_email", email));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sql += " and tuser.user_name=@user_name ";
                para.Add(new SqlParameter("@user_name", userName));
            }
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }

        #endregion

        #region 宝洁

        /// <summary>
        /// 根据Email集合返回用户ID结合
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pEmailList"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetUserByEmailList(string pCustomerID, List<string> pEmailList, string pTypeID)
        {
            string sql = " SELECT tuser.USER_ID,user_email";
            sql += " FROM  T_User tuser inner join TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId ";
            sql += " INNER JOIN T_USER_ROLE userRole on userRole.user_id=tuser.user_id";
            sql += " INNER JOIN T_Role  ro on ro.role_id =userRole.role_id ";
            sql += " INNER JOIN PgUser pgu ON pgu.USER_ID=tuser.user_id";
            sql += " LEFT JOIN pgChannel pgch ON pgu.channel_id=pgch.channelid";
            sql += " LEFT JOIN UserDeptJobMapping depMapping ON depMapping.USER_ID = tuser.user_id";
            sql += " LEFT JOIN T_UNIT unit ON unit.unit_id = depMapping.UnitID AND unit.type_id = '{0}'";
            sql += " LEFT JOIN JobFunction jobFunc ON jobFunc.JobFunctionID = depMapping.JobFunctionID";
            sql += " WHERE tuser.user_status = '1' ";
            sql += " and  tuser.customer_id='{1}' and tuser.user_email in('{2}') ";
            string email = string.Empty;
            foreach (var item in pEmailList)
                email += "'" + item + "',";
            if (!string.IsNullOrEmpty(email))
            {
                email = email.TrimEnd(',').TrimEnd('\'');
                email = email.Substring(1, email.Length - 1);
            }
            sql = string.Format(sql, pTypeID, pCustomerID, email);
            return this.SQLHelper.ExecuteDataset(sql);
            //List<SqlParameter> para = new List<SqlParameter>();
            //para.Add(new SqlParameter("@type_id", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A

            //if (!string.IsNullOrEmpty(pCustomerID))
            //{
            //    sql += " and  tuser.customer_id=@customer_id ";
            //    para.Add(new SqlParameter("@customer_id", pCustomerID));
            //}
            //if (pEmailList != null && pEmailList.Count > 0)
            //{
            //    string email = string.Empty;
            //    foreach (var item in pEmailList)
            //        email += "'" + item + "',";
            //    if (!string.IsNullOrEmpty(email))
            //    {
            //        email = email.TrimEnd(',').TrimEnd('\'');
            //        email = email.Substring(1, email.Length - 1);
            //        sql += " and tuser.user_email in(@user_email) ";
            //        para.Add(new SqlParameter("@user_email", email));
            //    }
            //}
            //return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }


        #region 根据UserID获取用户信息
        /// <summary>
        /// 根据UserID获取用户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="pUserID"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetPgUserByID(string pCustomerID, string pUserID, string pTypeID)
        {
            string sql = " SELECT tuser.USER_ID,user_name,user_gender,user_email,user_telephone,user_birthday,user_telephone,ro.role_name as RoleName";
            sql += " ,pgu.MANAGER_EMAIL SuperiorName ,pgu.FUNC AS Dept,CAST(pgu.JOB_BAND AS NVARCHAR(4)) AS JobFunc ,pgu.LOCATION  Location,pgch.NAME  Channel,pgu.SERVICE_YEAR AS ServiceYear";
            sql += " ,SUBORDINATE_COUNT AS SubordinateCount,(SELECT TOP 1 oi.ImageURL FROM ObjectImages oi WHERE oi.ObjectId = tuser.user_id AND oi.IsDelete = '0' ORDER BY oi.DisplayIndex) highImageUrl";
            sql += " FROM  T_User tuser inner join TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId ";
            sql += " INNER JOIN T_USER_ROLE userRole on userRole.user_id=tuser.user_id";
            sql += " INNER JOIN T_Role  ro on ro.role_id =userRole.role_id ";
            sql += " INNER JOIN PgUser pgu ON pgu.USER_ID=tuser.user_id";
            sql += " LEFT JOIN pgChannel pgch ON pgu.channel_id=pgch.channelid";
            sql += " LEFT JOIN UserDeptJobMapping depMapping ON depMapping.USER_ID = tuser.user_id";
            sql += " LEFT JOIN T_UNIT unit ON unit.unit_id = depMapping.UnitID AND unit.type_id = @type_id";
            sql += " LEFT JOIN JobFunction jobFunc ON jobFunc.JobFunctionID = depMapping.JobFunctionID";
            sql += " WHERE tuser.user_status = '1' ";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@type_id", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A

            if (!string.IsNullOrEmpty(pCustomerID))
            {
                sql += " and  tuser.customer_id=@customer_id ";
                para.Add(new SqlParameter("@customer_id", pCustomerID));
            }
            if (!string.IsNullOrEmpty(pUserID))
            {
                sql += " and tuser.user_id=@user_id ";
                para.Add(new SqlParameter("@user_id", pUserID));
            }
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }

        #endregion

        #region 返回指定字段的宝洁用户列表信息
        /// <summary>
        /// 返回指定字段的用户列表信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetPGSimpUserInfo(string customerID, string email, string phone, string userCode, string userName, int pageIndex, int pageSize, string pTypeID)
        {
            int beginSize = pageIndex * pageSize + 1;
            int endSize = (pageIndex + 1) * pageSize;
            string sql = "SELECT top 3000 * FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY dbo.fn_GetQuanPin(tuser.USER_NAME) ) rowId";
            sql += " ,tp.VoipAccount,tuser.user_id,user_code,user_name,user_gender,user_email,user_telephone,user_cellphone";
            sql += " ,tuser.modify_time,user_name_en,pgu.MANAGER_EMAIL SuperiorName,'' Integral ,ro.role_name AS RoleName";
            sql += " ,pgu.FUNC AS Dept,CAST(pgu.JOB_BAND AS NVARCHAR(4)) AS JobFunc ,pgu.LOCATION  Location,ISNULL(pgch.NAME, '') Channel ,pgu.SERVICE_YEAR AS ServiceYear";
            sql += " ,SUBORDINATE_COUNT AS SubordinateCount,( SELECT TOP 1 oi.ImageURL FROM ObjectImages oi WHERE oi.ObjectId = tuser.user_id AND oi.IsDelete ='0' ORDER BY oi.DisplayIndex) highImageUrl";
            sql += " FROM T_User tuser INNER JOIN TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId";
            sql += " INNER JOIN T_USER_ROLE userRole on userRole.user_id=tuser.user_id";
            sql += " INNER JOIN T_Role  ro on ro.role_id =userRole.role_id";
            sql += " INNER JOIN PgUser pgu ON pgu.USER_ID=tuser.user_id";
            sql += " LEFT JOIN pgChannel pgch ON pgu.channel_id=pgch.channelid";
            sql += " LEFT JOIN UserDeptJobMapping depMapping ON depMapping.USER_ID = tuser.user_id";
            sql += " LEFT JOIN T_UNIT unit ON unit.unit_id = depMapping.UnitID AND unit.type_id = @type_id";
            sql += " LEFT JOIN JobFunction jobFunc ON jobFunc.JobFunctionID = depMapping.JobFunctionID";
            sql += " WHERE tuser.user_status = '1' ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@type_id", pTypeID));

            if (!string.IsNullOrEmpty(customerID))
            {
                sql += " and  tuser.customer_id=@customer_id ";
                para.Add(new SqlParameter("@customer_id", customerID));
            }
            if (!string.IsNullOrEmpty(email))
            {
                sql += " and tuser.user_email=@user_email ";
                para.Add(new SqlParameter("@user_email", email));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                sql += " and tuser.user_telephone=@user_telephone ";
                para.Add(new SqlParameter("@user_telephone", phone));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                sql += " and tuser.user_code=@user_code ";
                para.Add(new SqlParameter("@user_code", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sql += " and tuser.user_name=@user_name ";
                para.Add(new SqlParameter("@user_name", userName));
            }

            sql += ") tt WHERE rowId between @beginSize and @endTSize ";
            para.Add(new SqlParameter("@beginSize", beginSize));
            para.Add(new SqlParameter("@endTSize", endSize));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region  返回所有宝洁用户信息列表
        /// <summary>
        /// 返回所有宝洁用户信息列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetPGUserInfo(string customerID, string email, string phone, string userCode, string userName, int pageIndex, int pageSize, string pTypeID)
        {
            int beginSize = pageIndex * pageSize + 1;
            int endSize = (pageIndex + 1) * pageSize;
            string sql = @"SELECT * FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY dbo.fn_GetQuanPin(tuser.USER_NAME)) rowId ,tuser.* , tp.* ,ro.role_name as RoleName";
            sql += " ,pgu.FUNC AS Dept,CAST(pgu.JOB_BAND AS NVARCHAR(4)) AS JobFunc ,pgu.LOCATION Location,pgch.NAME Channel,pgu.SERVICE_YEAR AS ServiceYear";
            sql += " ,SUBORDINATE_COUNT AS SubordinateCount,(SELECT TOP 1 oi.ImageURL  FROM ObjectImages oi WHERE oi.ObjectId = tuser.user_id AND oi.IsDelete = '0' ORDER BY  oi.DisplayIndex ) highImageUrl";
            sql += " FROM T_User tuser INNER JOIN TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId";
            sql += " INNER JOIN T_USER_ROLE userRole on userRole.user_id=tuser.user_id";
            sql += " INNER JOIN T_Role  ro on ro.role_id =userRole.role_id";
            sql += " INNER JOIN PgUser pgu ON pgu.USER_ID=tuser.user_id";
            sql += " LEFT JOIN pgChannel pgch ON pgu.channel_id=pgch.channelid";
            sql += " LEFT JOIN UserDeptJobMapping depMapping ON depMapping.USER_ID = tuser.user_id";
            sql += " LEFT JOIN T_UNIT unit ON unit.unit_id = depMapping.UnitID AND unit.type_id = @type_id";
            sql += " LEFT JOIN JobFunction jobFunc ON jobFunc.JobFunctionID = depMapping.JobFunctionID";
            sql += " WHERE tuser.user_status = '1' ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@type_id", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A

            if (!string.IsNullOrEmpty(customerID))
            {
                sql += " and  tuser.customer_id=@customer_id ";
                para.Add(new SqlParameter("@customer_id", customerID));
            }
            if (!string.IsNullOrEmpty(email))
            {
                sql += " and tuser.user_email=@user_email ";
                para.Add(new SqlParameter("@user_email", email));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                sql += " and tuser.user_telephone=@user_telephone ";
                para.Add(new SqlParameter("@user_telephone", phone));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                sql += " and tuser.user_code=@user_code ";
                para.Add(new SqlParameter("@user_code", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sql += " and tuser.user_name=@user_name ";
                para.Add(new SqlParameter("@user_name", userName));
            }

            sql += ") tt WHERE rowId between @beginSize and @endTSize ";
            para.Add(new SqlParameter("@beginSize", beginSize));
            para.Add(new SqlParameter("@endTSize", endSize));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region 返回宝洁用户信息变动的用户列表
        /// <summary>
        /// 返回宝洁用户信息变动的用户列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="pTypeID"></param>
        /// <param name="pLastUpdateDate">yyyy-MM-dd</param>
        /// <returns></returns>
        public DataSet GetPGTestingChangeUserInfo(string pCustomerID, string pTypeID, string pLastUpdateTime)
        {
            //检测用户表、用户职务表、用户角色表3表变动状态
            string sql = @"SELECT tuser.user_id,tuser.user_name FROM T_User tuser
                        INNER JOIN TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId
                        INNER JOIN T_USER_ROLE userRole ON userRole.user_id = tuser.user_id
                        INNER JOIN T_Role ro ON ro.role_id = userRole.role_id
                        LEFT JOIN UserDeptJobMapping depMapping ON depMapping.USER_ID = tuser.user_id
                        LEFT JOIN T_UNIT unit ON unit.unit_id = depMapping.UnitID
                        LEFT JOIN JobFunction jobFunc ON jobFunc.JobFunctionID = depMapping.JobFunctionID
                        WHERE tuser.user_status = '1' AND tuser.customer_id=@CustomerID 
                        AND ( tuser.modify_time > @TuserModifyTime OR depMapping.LastUpdateTime > @depMappingLastUpdateTime OR userRole.modify_time > @UserRoleModifyTime)";
            List<SqlParameter> para = new List<SqlParameter>(); //AND unit.type_id = @TypeID
            //para.Add(new SqlParameter("@TypeID", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            para.Add(new SqlParameter("@TuserModifyTime", pLastUpdateTime));
            para.Add(new SqlParameter("@depMappingLastUpdateTime", pLastUpdateTime));
            para.Add(new SqlParameter("@UserRoleModifyTime", pLastUpdateTime));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region 宝洁Channel
        public DataSet GetPgChannel()
        {
            string sql = "SELECT ChannelID,Name FROM dbo.PgChannel ";
            return SearchSql(sql);
        }
        #endregion
        #endregion

        #region 企信
        /// <summary>
        /// 企信登录
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet LoginQiXinUser(string customerID, string email, string userName, string pTypeID)
        {

            string sql = @"SELECT tuser.user_password,tuser.user_id,user_code,user_name,user_gender,user_email,user_telephone,user_cellphone,
                         tuser.modify_time,user_name_en,'' SuperiorName,'' Integral,ro.role_name AS RoleName,unit.UNIT_NAME AS Dept,
                        jobFunc.Name AS JobFunc ,( SELECT TOP 1 oi.ImageURL FROM  ObjectImages oi
                        WHERE oi.ObjectId = tuser.user_id AND oi.IsDelete = '0' ORDER BY  oi.DisplayIndex) highImageUrl
                                  FROM      T_User tuser
                                            inner join T_USER_ROLE userRole on userRole.user_id=tuser.user_id
                                            left join T_Role  ro on ro.role_id =userRole.role_id
                                            inner join UserDeptJobMapping depMapping on depMapping.USER_ID=tuser.user_id
                                            inner join T_UNIT unit on unit.unit_id=depMapping.UnitID  
                                            and unit.type_id=@typeID
                                            inner join JobFunction jobFunc on jobFunc.JobFunctionID=depMapping.JobFunctionID
                                  WHERE     tuser.user_status = '1' ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@typeID", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A

            if (!string.IsNullOrEmpty(customerID))
            {
                sql += " and  tuser.customer_id=@customer_id ";
                para.Add(new SqlParameter("@customer_id", customerID));
            }
            if (!string.IsNullOrEmpty(email))
            {
                sql += " and tuser.user_email=@user_email ";
                para.Add(new SqlParameter("@user_email", email));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sql += " and tuser.user_name=@user_name ";
                para.Add(new SqlParameter("@user_name", userName));
            }
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }

        #region 返回指定字段的用户列表信息
        /// <summary>
        /// 返回指定字段的用户列表信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetSimpUserInfo(string customerID, string email, string phone, string userCode, string userName, int pageIndex, int pageSize, string pTypeID)
        {
            int beginSize = pageIndex * pageSize + 1;
            int endSize = (pageIndex + 1) * pageSize;
            string sql = @"SELECT top 3000 * FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY dbo.fn_GetQuanPin(tuser.USER_NAME) ) rowId ,
                                    tp.VoipAccount,tuser.user_id,user_code,user_name,user_gender,user_email,user_telephone,
                                    user_cellphone,tuser.modify_time,user_name_en,'' SuperiorName,'' Integral ,
                                    ro.role_name AS RoleName,unit.UNIT_NAME AS Dept,jobFunc.Name AS JobFunc ,
                                    ( SELECT TOP 1
                                                oi.ImageURL
                                      FROM      ObjectImages oi
                                      WHERE     oi.ObjectId = tuser.user_id
                                                AND oi.IsDelete = '0'
                                      ORDER BY  oi.DisplayIndex
                                    ) highImageUrl
                          FROM      T_User tuser
                                   INNER JOIN TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId
                                    inner join T_USER_ROLE userRole on userRole.user_id=tuser.user_id
                                    inner join T_Role  ro on ro.role_id =userRole.role_id
                                    inner join UserDeptJobMapping depMapping on depMapping.USER_ID=tuser.user_id
                                    inner join T_UNIT unit on unit.unit_id=depMapping.UnitID  
                                    and unit.type_id=@typeID
                                    inner join JobFunction jobFunc on jobFunc.JobFunctionID=depMapping.JobFunctionID
                          WHERE     tuser.user_status = '1' ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@typeID", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A

            if (!string.IsNullOrEmpty(customerID))
            {
                sql += " and  tuser.customer_id=@customer_id ";
                para.Add(new SqlParameter("@customer_id", customerID));
            }
            if (!string.IsNullOrEmpty(email))
            {
                sql += " and tuser.user_email=@user_email ";
                para.Add(new SqlParameter("@user_email", email));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                sql += " and tuser.user_telephone=@user_telephone ";
                para.Add(new SqlParameter("@user_telephone", phone));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                sql += " and tuser.user_code=@user_code ";
                para.Add(new SqlParameter("@user_code", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sql += " and tuser.user_name=@user_name ";
                para.Add(new SqlParameter("@user_name", userName));
            }

            sql += ") tt WHERE rowId between @beginSize and @endTSize ";
            para.Add(new SqlParameter("@beginSize", beginSize));
            para.Add(new SqlParameter("@endTSize", endSize));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region 返回所有用户信息列表
        /// <summary>
        /// 返回所有用户信息列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetUserInfo(string customerID, string email, string phone, string userCode, string userName, int pageIndex, int pageSize, string pTypeID)
        {
            //if (pageIndex == 0) pageIndex = 1;
            //int beginSize = (pageIndex - 1) * pageSize + 1;
            //int endSize = pageIndex * pageSize;

            int beginSize = pageIndex * pageSize + 1;
            int endSize = (pageIndex + 1) * pageSize;
            //ORDER BY tuser.USER_ID 
            string sql = @"SELECT  * FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY dbo.fn_GetQuanPin(tuser.USER_NAME) ) rowId ,
                                            tuser.* , tp.* ,ro.role_name as RoleName,
                                             unit.UNIT_NAME as Dept ,jobFunc.Name as JobFunc,
                                            ( SELECT TOP 1
                                                        oi.ImageURL
                                              FROM      ObjectImages oi
                                              WHERE     oi.ObjectId = tuser.user_id
                                                        AND oi.IsDelete = '0'
                                              ORDER BY  oi.DisplayIndex
                                            ) highImageUrl
                                  FROM      T_User tuser
                                           INNER JOIN TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId
                                            inner join T_USER_ROLE userRole on userRole.user_id=tuser.user_id
                                            inner join T_Role  ro on ro.role_id =userRole.role_id
                                            inner join UserDeptJobMapping depMapping on depMapping.USER_ID=tuser.user_id
                                            inner join T_UNIT unit on unit.unit_id=depMapping.UnitID  
                                            and unit.type_id=@typeID
                                            inner join JobFunction jobFunc on jobFunc.JobFunctionID=depMapping.JobFunctionID
                                  WHERE     tuser.user_status = '1' ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@typeID", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A

            if (!string.IsNullOrEmpty(customerID))
            {
                sql += " and  tuser.customer_id=@customer_id ";
                para.Add(new SqlParameter("@customer_id", customerID));
            }
            if (!string.IsNullOrEmpty(email))
            {
                sql += " and tuser.user_email=@user_email ";
                para.Add(new SqlParameter("@user_email", email));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                sql += " and tuser.user_telephone=@user_telephone ";
                para.Add(new SqlParameter("@user_telephone", phone));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                sql += " and tuser.user_code=@user_code ";
                para.Add(new SqlParameter("@user_code", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sql += " and tuser.user_name=@user_name ";
                para.Add(new SqlParameter("@user_name", userName));
            }

            sql += ") tt WHERE rowId between @beginSize and @endTSize ";
            para.Add(new SqlParameter("@beginSize", beginSize));
            para.Add(new SqlParameter("@endTSize", endSize));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region 返回用户信息变动的用户列表
        /// <summary>
        /// 返回用户信息变动的用户列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="pTypeID"></param>
        /// <param name="pLastUpdateDate">yyyy-MM-dd</param>
        /// <returns></returns>
        public DataSet GetUserInfo(string pCustomerID, string pTypeID, string pLastUpdateTime)
        {
            //检测用户表、用户职务表、用户角色表3表变动状态
            string sql = @"SELECT  tuser.*,tp.*,ro.role_name AS RoleName,unit.UNIT_NAME AS Dept,jobFunc.Name AS JobFunc,
                        ( SELECT TOP 1 oi.ImageURL FROM ObjectImages oi WHERE oi.ObjectId = tuser.user_id AND oi.IsDelete = '0'
                        ORDER BY  oi.DisplayIndex) highImageUrl FROM T_User tuser
                        INNER JOIN TUserThirdPartyMapping tp ON tuser.user_id = tp.UserId
                        INNER JOIN T_USER_ROLE userRole ON userRole.user_id = tuser.user_id
                        INNER JOIN T_Role ro ON ro.role_id = userRole.role_id
                        INNER JOIN UserDeptJobMapping depMapping ON depMapping.USER_ID = tuser.user_id
                        INNER JOIN T_UNIT unit ON unit.unit_id = depMapping.UnitID
                        INNER JOIN JobFunction jobFunc ON jobFunc.JobFunctionID = depMapping.JobFunctionID
                        WHERE tuser.user_status <> '-1' AND unit.type_id = @TypeID AND tuser.customer_id=@CustomerID 
                        AND ( tuser.modify_time > @TuserModifyTime OR depMapping.LastUpdateTime > @depMappingLastUpdateTime OR userRole.modify_time > @UserRoleModifyTime)";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@TypeID", pTypeID));//4E1A9BB0E36F4D2B951E0F477ECB3C0A
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            para.Add(new SqlParameter("@TuserModifyTime", pLastUpdateTime));
            para.Add(new SqlParameter("@depMappingLastUpdateTime", pLastUpdateTime));
            para.Add(new SqlParameter("@UserRoleModifyTime", pLastUpdateTime));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region 获取用户单实例
        /// <summary>
        /// 获取用户单实例
        /// </summary>
        /// <param name="pID">user_id</param>
        public T_UserEntity GetUserEntityByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_User] where user_id='{0}' ", id.ToString());
            //读取数据
            T_UserEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }
        #endregion

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <returns></returns>
        public DataSet GetObjectImages(string pObjectID)
        {
            string sql = "SELECT ImageId,ObjectId,ImageURL,DisplayIndex,CreateTime,CreateBy,LastUpdateBy,LastUpdateTime,IsDelete";
            sql += ",CustomerId,Title,Description FROM dbo.ObjectImages WHERE ObjectId=@ObjectId AND IsDelete=0";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@ObjectId", pObjectID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region 执行提交数据
        /// <summary>
        /// 执行提交数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public int SubmitSql(string pSql)
        {
            return this.SQLHelper.ExecuteNonQuery(pSql);
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public DataSet SearchSql(string pSql)
        {
            return this.SQLHelper.ExecuteDataset(pSql);
        }
        #endregion

        #region 获取角色权限
        public DataSet GetUserRights(string pUserID, string pCustomerID)
        {
            string sql = @"SELECT * FROM dbo.T_User_Role AS tur
                           INNER JOIN dbo.T_Role_Menu AS trm ON tur.role_id = trm.role_id
                           INNER JOIN dbo.T_Menu AS tm ON tm.menu_id = trm.menu_id
                          WHERE user_id = @UserID
                          AND customer_id = @CustomerID
                          AND trm.status = 1 ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@UserID", pUserID));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        /// <summary>
        /// 据RightCode获取用户是否有权限
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pCustomerID"></param>
        /// <param name="pRightCode"></param>
        /// <returns></returns>
        public bool IsHasPower(string pUserID, string pRightCode)
        {
            DataSet pDsUserRight = GetUserRights(pUserID, this.CurrentUserInfo.CurrentUser.customer_id);
            bool f = false;
            //key:VIP020000 value:创建讨论组权限
            if (pDsUserRight.Tables != null && pDsUserRight.Tables.Count > 0 && pDsUserRight.Tables[0] != null && pDsUserRight.Tables[0].Rows.Count > 0)
            {
                if (pDsUserRight.Tables[0].Select("Menu_Code='" + pRightCode + "'").Length > 0)
                    f = true;
            }
            return f;
        }
        #endregion

        #region 导入PG用户

        public T_UserEntity GetUserEntityByEmail(string email, string customerId)
        {
            //参数检查
            if (string.IsNullOrEmpty(email))
                return null;
            string id = email.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_User] where user_email='{0}' and customer_id='{1}' and user_status=1 ", id.ToString(), customerId);
            //读取数据
            T_UserEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        public void ExportData(string pCustomerID, int pPageIndex, int pPageSize, LoggingSessionInfo pLoggingSessionInfo)
        {
            //客户id
            //"17fe67e2b69e4b50b67e725939586459"
            string customerID = pCustomerID;//宝洁customerId:17fe67e2b69e4b50b67e725939586459//

            //PGExportLocalLuOwner(pCustomerID, pLoggingSessionInfo);
            //return;

            //pPageIndex = 0;
            //pPageSize = 1;
            int sRow = pPageIndex * pPageSize + 1;//21 11 1
            int eRow = (pPageIndex + 1) * pPageSize;//30 20 10
            //var tran = this.SQLHelper.CreateTransaction();
            //using (tran.Connection)
            //{
            try
            {
                //PgUserDAO pgUser = new PgUserDAO(pLoggingSessionInfo);
                T_UserEntity tue = null;
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY ID) AS rowid,* FROM dbo.PgUserExport)tt WHERE rowid BETWEEN {0} AND {1}";
                sql = string.Format(sql, sRow, eRow);
                DataSet ds = SearchSql(sql);
                //return;

                //67F0BEE0-122B-4FC0-AB39-F3F9EFCA5FB6	P&G员工
                //E966FEAC-9DDD-4985-9628-7FAB29779614	P&G总监
                //36A3435F-236E-4D6A-B377-F2A3C01CD8B6	P&G经理

                List<string> listJob = new List<string>();
                listJob.Add("67F0BEE0-122B-4FC0-AB39-F3F9EFCA5FB6");
                listJob.Add("E966FEAC-9DDD-4985-9628-7FAB29779614");
                listJob.Add("36A3435F-236E-4D6A-B377-F2A3C01CD8B6");
                //B4A3718C-31B4-44D0-AF0B-213FAEB10361		P&G01	经理办公室
                //9A841B8D-1EDB-4887-B3D4-39FF74A42F8D		P&G02	市场部
                //A55616C9-0480-4DB1-A697-29C64CBE97BA		P&G03	销售部
                string sqlDept = "SELECT unit_id,unit_name,unit_code FROM dbo.t_unit WHERE type_id='4E1A9BB0E36F4D2B951E0F477ECB3C0A' AND customer_id='{0}'";
                sqlDept = string.Format(sqlDept, customerID);
                DataTable dTblDept = SearchSql(sqlDept).Tables[0];

                //List<string> listDept = new List<string>();
                //listDept.Add("B4A3718C-31B4-44D0-AF0B-213FAEB10361");
                //listDept.Add("9A841B8D-1EDB-4887-B3D4-39FF74A42F8D");
                //listDept.Add("A55616C9-0480-4DB1-A697-29C64CBE97BA");


                //创建人
                string createUserID = "PGAdmin";
                string lastUpdateUserID = "PGAdmin";

                DateTime dt = DateTime.Now;
                string strDt = dt.ToString("yyyy-MM-dd HH:mm:ss");
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string uSQL = "";
                    List<SqlParameter> para = null;
                    DataTable dTable = ds.Tables[0];
                    int existsEmaliNum = 0, notExistsEmailNum = 0;

                    string email = string.Empty, emailSuffix = "@pg.com";

                    foreach (DataRow row in dTable.Rows)
                    {
                        email = row["EMAIL"].ToString().ToLower().Contains(emailSuffix) ? row["EMAIL"].ToString() : (row["EMAIL"].ToString() + emailSuffix);
                        email = email.ToUpper();

                        tue = GetUserEntityByEmail(email, customerID);
                        if (tue != null)
                        {
                            existsEmaliNum++;
                            tue.user_name = row["FIRST_NAME"].ToString() + row["LAST_NAME"].ToString();
                            tue.user_name_en = row["KNOWN_AS"].ToString();
                            Update(tue);
                            uSQL = "UPDATE PgUser SET FIRST_NAME=@FIRST_NAME ,LAST_NAME=@LAST_NAME,KNOWN_AS=@KNOWN_AS,EMAIL=@EMAIL,JOB_BAND=@JOB_BAND WHERE USER_ID=@USER_ID ";
                            para = new List<SqlParameter>();
                            para.Add(new SqlParameter("@FIRST_NAME", row["FIRST_NAME"]));
                            para.Add(new SqlParameter("@LAST_NAME", row["LAST_NAME"]));
                            para.Add(new SqlParameter("@KNOWN_AS", row["KNOWN_AS"]));
                            para.Add(new SqlParameter("@EMAIL", email));
                            para.Add(new SqlParameter("@JOB_BAND", row["JOB_BAND"]));
                            para.Add(new SqlParameter("@USER_ID", tue.user_id));
                            this.SQLHelper.ExecuteNonQuery(CommandType.Text, uSQL, para.ToArray());
                            //continue;
                        }
                        else
                        {
                            notExistsEmailNum++;

                            tue = new T_UserEntity();

                            string userId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                            tue.user_id = userId;
                            tue.user_name = row["FIRST_NAME"].ToString() + row["LAST_NAME"].ToString();
                            tue.user_name_en = row["KNOWN_AS"].ToString();
                            if (row["GENDER"] != null && !string.IsNullOrEmpty(row["GENDER"].ToString()))
                            {
                                if (row["GENDER"].ToString().ToLower() == "Male".ToLower())
                                    tue.user_gender = "1";//男
                                else if (row["GENDER"].ToString().ToLower() == "Female".ToLower())
                                    tue.user_gender = "2";//女
                            }
                            else
                                tue.user_gender = "0";//未知

                            if (row["BIRTHDATE"] != null && !string.IsNullOrEmpty(row["BIRTHDATE"].ToString()))
                            {
                                tue.user_birthday = Convert.ToDateTime(row["BIRTHDATE"]).ToString("yyyy-MM-dd");
                            }

                            if (email != null && !string.IsNullOrEmpty(email))
                            {
                                tue.user_email = email;
                            }
                            if (row["MOBILE"] != null && !string.IsNullOrEmpty(row["MOBILE"].ToString()))
                            {
                                tue.user_telephone = row["MOBILE"].ToString();
                            }

                            tue.user_status = "1";
                            tue.user_status_desc = "正常";
                            tue.fail_date = "2020-01-02";
                            tue.user_code = "PG" + (Convert.ToInt32(row["rowid"].ToString()));// + 5600
                            tue.user_address = "";
                            tue.user_password = "e10adc3949ba59abbe56e057f20f883e";//密码123:202cb962ac59075b964b07152d234b70 //123456:e10adc3949ba59abbe56e057f20f883e 
                            tue.customer_id = customerID;
                            tue.create_user_id = tue.modify_user_id = createUserID;
                            tue.create_time = tue.modify_time = strDt;
                            tue.msn = "";
                            tue.qq = "";
                            tue.blog = "";
                            tue.user_postcode = "";
                            tue.user_cellphone = "";
                            tue.user_remark = "";

                            string pgSQL = "insert into PgUser ";
                            string pgColumns = "USER_ID,", pgValues = "'" + userId + "',";

                            string deptName = string.Empty, deptUnitID = "";

                            for (int i = 0; i < dTable.Columns.Count; i++)
                            {
                                if (dTable.Columns[i].ColumnName.ToString().ToLower() != "rowid".ToLower())
                                {
                                    if (dTable.Columns[i].ColumnName.ToLower() == "email")
                                    {
                                        if (row[dTable.Columns[i].ColumnName].ToString().ToLower().Contains(emailSuffix))
                                        {
                                            pgColumns += dTable.Columns[i].ColumnName + ",";
                                            pgValues += "'" + row[dTable.Columns[i].ColumnName] + "',";
                                        }
                                        else
                                        {
                                            pgColumns += dTable.Columns[i].ColumnName + ",";
                                            pgValues += "'" + row[dTable.Columns[i].ColumnName] + emailSuffix + "',";
                                        }
                                    }
                                    else
                                    {
                                        deptName = row["FUNC"] == null ? "" : row["FUNC"].ToString();
                                        pgColumns += dTable.Columns[i].ColumnName + ",";
                                        pgValues += "'" + row[dTable.Columns[i].ColumnName] + "',";
                                    }
                                }
                            }
                            pgColumns += "CreateBy,CreateTime,LastUpdateBy,LastUpdateTime,IsDelete";
                            pgValues += "'" + createUserID + "','" + strDt + "','" + lastUpdateUserID + "','" + strDt + "',0";
                            pgSQL += "(" + pgColumns + ") values (" + pgValues + ")";

                            //T_User表
                            //Create(tue, tran);
                            Create(tue);
                            //PgUser表
                            //this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, pgSQL);
                            this.SQLHelper.ExecuteNonQuery(CommandType.Text, pgSQL);
                            //T_User_Role 表
                            string roleId = "58B34963774C4D778C0D91E0B7356F87",//角色id (普通员工)
                                unitId = "52d10067c374494e8a95128bb1999161";//单位（宝洁总部）
                            string sqlRole = "INSERT INTO dbo.T_User_Role( ";
                            sqlRole += " user_role_id ,user_id , role_id ,unit_id , status ,create_time ,create_user_id ,modify_time ,modify_user_id ,default_flag) values (";
                            sqlRole += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}' )";
                            sqlRole = string.Format(sqlRole, Guid.NewGuid().ToString().Replace("-", "").ToLower(), userId, roleId, unitId, 1,
                                strDt, userId, strDt, lastUpdateUserID, 1);
                            //this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlRole);
                            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sqlRole);
                            //UserDeptJobMapping表
                            Random rd = new Random();
                            string sqlDeptJob = "INSERT INTO dbo.UserDeptJobMapping (";
                            sqlDeptJob += " UserDeptID,USER_ID,JobFunctionID,UserID,CustomerID,CreateTime,CreateUserID,LastUpdateTime,LastUpdateUserID,IsDelete,UnitID) values (";
                            sqlDeptJob += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";

                            DataRow[] drs = dTblDept.Select("unit_name='" + deptName + "'");
                            if (drs != null && drs.Length > 0)
                                deptUnitID = drs[0]["unit_id"].ToString();

                            sqlDeptJob = string.Format(sqlDeptJob, Guid.NewGuid().ToString(), userId, listJob[rd.Next(0, listJob.Count - 1)], userId,
                                customerID, strDt, createUserID, strDt, lastUpdateUserID, 0, deptUnitID);
                            //customerID, strDt, createUserID, strDt, lastUpdateUserID, 0, listDept[rd.Next(0, listDept.Count - 1)]);

                            //this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlDeptJob);
                            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sqlDeptJob);
                        }
                    }

                    Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "重复：" + existsEmaliNum + "条，新增：" + notExistsEmailNum + "条,共：" + dTable.Rows.Count });
                }
                //tran.Commit();
            }
            catch (Exception ex)
            {
                //回滚&转抛异常
                //tran.Rollback();
                throw;
            }
            //}
        }

        public void PGExportLocalLuOwner(string pCustomerID, LoggingSessionInfo pLoggingSessionInfo)
        {
            string sql = "SELECT * FROM  localLuOwner AS a  INNER JOIN  dbo.City AS b ON a.cityname=b.CityName WHERE email<>'NA'";
            DataSet ds = SearchSql(sql);
            string email = string.Empty, emailSuffix = "@pg.com";
            StringBuilder strb = new StringBuilder();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                string uSQL = "";
                DataTable dTable = ds.Tables[0];
                foreach (DataRow row in dTable.Rows)
                {
                    email = row["EMAIL"].ToString().ToLower().Contains(emailSuffix) ? row["EMAIL"].ToString() : (row["EMAIL"].ToString() + emailSuffix);
                    email = email.ToUpper();
                    uSQL = "UPDATE PgUser SET City='" + row["cityid"] + "',SpecialTitle='" + row["LocalLuOwner"] + "' WHERE EMAIL='" + email + "'";
                    this.SQLHelper.ExecuteNonQuery(uSQL);
                    strb.AppendLine(uSQL);
                }
            }
            Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = strb.ToString() });
        }

        #endregion

        #region 保存图片和缩略图
        /// <summary>
        /// 保存原图片和缩略图
        /// 图片对象FileData
        /// </summary>
        /// <param name="pOldFile">原图对象</param>
        /// <param name="PThumbs">缩略图集合对象</param>
        /// <param name="pObjectID">图片对象ID</param>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pUserID">创建修改UserID</param>
        public void SaveImageThumbs(FileData pOldFile, IList<FileData> pThumbs, string pObjectID, string pCustomerID, string pUserID)
        {
            //DisplayIndex的索引越大图片越大

            //原图片对象
            FileData oldFile = (FileData)pOldFile;
            string objectId = pObjectID;
            string sql = string.Format(@" update ObjectImages set IsDelete=1,LastUpdateBy='{0}',LastUpdateTime=GETDATE() 
                                             where ObjectId='{1}' ", pUserID, objectId);
            string isql = string.Empty;
            string strIsql = string.Empty;
            //缩略图集合对象
            int index = pThumbs.Count;
            for (int i = 0; i < pThumbs.Count; i++)
            {
                //缩略图
                FileData file = (FileData)pThumbs[i];
                isql = ";insert into ObjectImages (ImageId ,ObjectId ,ImageURL ,DisplayIndex,CreateTime,CreateBy,LastUpdateBy,LastUpdateTime,IsDelete,CustomerId,Title,Description)";
                isql += " values ('{0}','{1}','{2}','{3}',GETDATE(),'{4}','{5}',GETDATE(),0,'{6}','{7}','{8}')";
                isql = string.Format(isql, Guid.NewGuid().ToString(), objectId, file.url, i.ToString(), pUserID, pUserID, pCustomerID, file.name, file.type);
                strIsql += isql;
            }
            isql = ";insert into ObjectImages (ImageId ,ObjectId ,ImageURL ,DisplayIndex,CreateTime,CreateBy,LastUpdateBy,LastUpdateTime,IsDelete,CustomerId,Title,Description)";
            isql += " values ('{0}','{1}','{2}','{3}',GETDATE(),'{4}','{5}',GETDATE(),0,'{6}','{7}','{8}')";
            isql = string.Format(isql, Guid.NewGuid().ToString(), objectId, oldFile.url, index, pUserID, pUserID, pCustomerID, oldFile.name, oldFile.type);
            strIsql += isql;
            sql = sql + strIsql;
            SubmitSql(sql);
        }
        /// <summary>
        /// 保存原图片和缩略图
        /// 图片对象FileData
        /// </summary>
        /// <param name="pOldFile">原图对象</param>
        /// <param name="PThumbs">缩略图集合对象</param>
        /// <param name="pObjectID">图片对象ID</param>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pUserID">创建修改UserID</param>
        /// <param name="pIsDel">是否标志删除ObjectId以前的图片</param>
        public void SaveImageThumbs(FileData pOldFile, IList<FileData> pThumbs, string pObjectID, string pCustomerID, string pUserID, string pIsDel)
        {
            //DisplayIndex的索引越大图片越大

            //原图片对象
            FileData oldFile = (FileData)pOldFile;
            string objectId = pObjectID;

            string sql = "";
            if (pIsDel == "1")
                sql = string.Format(@" update ObjectImages set IsDelete=1,LastUpdateBy='{0}',LastUpdateTime=GETDATE() where ObjectId='{1}' ", pUserID, objectId);

            string isql = string.Empty;
            string strIsql = string.Empty;
            //缩略图集合对象
            int index = pThumbs.Count;
            for (int i = 0; i < pThumbs.Count; i++)
            {
                //缩略图
                FileData file = (FileData)pThumbs[i];
                isql = ";insert into ObjectImages (ImageId ,ObjectId ,ImageURL ,DisplayIndex,CreateTime,CreateBy,LastUpdateBy,LastUpdateTime,IsDelete,CustomerId,Title,Description)";
                isql += " values ('{0}','{1}','{2}','{3}',GETDATE(),'{4}','{5}',GETDATE(),0,'{6}','{7}','{8}')";
                isql = string.Format(isql, Guid.NewGuid().ToString(), objectId, file.url, i.ToString(), pUserID, pUserID, pCustomerID, file.name, file.type);
                strIsql += isql;
            }
            isql = ";insert into ObjectImages (ImageId ,ObjectId ,ImageURL ,DisplayIndex,CreateTime,CreateBy,LastUpdateBy,LastUpdateTime,IsDelete,CustomerId,Title,Description)";
            isql += " values ('{0}','{1}','{2}','{3}',GETDATE(),'{4}','{5}',GETDATE(),0,'{6}','{7}','{8}')";
            isql = string.Format(isql, Guid.NewGuid().ToString(), objectId, oldFile.url, index, pUserID, pUserID, pCustomerID, oldFile.name, oldFile.type);
            strIsql += isql;
            sql = sql + strIsql;
            SubmitSql(sql);
        }

        #endregion

        public void InsertUserRole(TUserRoleEntity entity)
        {
            string sqlRole = "INSERT INTO dbo.T_User_Role( ";
            sqlRole += " user_role_id ,user_id , role_id ,unit_id , status ,create_time ,create_user_id ,modify_time ,modify_user_id ,default_flag) values (";
            sqlRole += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}' )";
            sqlRole = string.Format(sqlRole, entity.user_role_id, entity.user_id, entity.role_id, entity.unit_id, entity.status,
                entity.create_time, entity.create_user_id, entity.modify_time, entity.modify_user_id, entity.default_flag);
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sqlRole);
        }


        #region 用户列表-qxht

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pUserId"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <param name="totalPage"></param>
        /// <returns></returns>
        public DataTable GetUserList(string pUserId, int pPageIndex, int pPageSize, out int totalPage, QueryUserEntity entity)
        {
            int begin = pPageIndex * pPageSize + 1;
            int end = (pPageIndex + 1) * pPageSize;

            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalSql = new StringBuilder();

            pagedSql.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER ( order by UserName ) rowId,* ");
            totalSql.AppendFormat("select count(1) ");

            string commSql = " FROM dbo.vw_user_qxht WHERE CustomerID=@CustomerID AND RoleID=@RoleID ";
            pagedSql.AppendFormat(commSql);
            totalSql.AppendFormat(commSql);

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@RoleID", entity.QRoleID));

            if (!string.IsNullOrEmpty(pUserId))
            {
                totalSql.AppendFormat(" AND UserID=@UserID ");
                pagedSql.AppendFormat(" AND UserID=@UserID ");
                param.Add(new SqlParameter("@UserID", pUserId));
            }

            if (!string.IsNullOrEmpty(entity.QUnitID))
            {
                totalSql.AppendFormat(" AND UnitID=@UnitID ");
                pagedSql.AppendFormat(" AND UnitID=@UnitID ");
                param.Add(new SqlParameter("@UnitID", entity.QUnitID));
            }

            if (!string.IsNullOrEmpty(entity.QUserName))
            {
                //entity.QUserName = entity.QUserName.Replace("'", "''");
                //totalSql.AppendFormat(" AND UserName like '%" + entity.QUserName + "%' ");
                //pagedSql.AppendFormat(" AND UserName like '%" + entity.QUserName + "%' ");

                totalSql.AppendFormat(" AND UserName like @UserName ");
                pagedSql.AppendFormat(" AND UserName like @UserName ");
                param.Add(new SqlParameter("@UserName", "%" + entity.QUserName + "%"));
            }
            pagedSql.AppendFormat(" ) tt WHERE tt.rowid BETWEEN @begin AND @end ");

            try
            {
                //计算总行数PageCount
                int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, totalSql.ToString(), param.ToArray()));
                int remainder = 0;
                //总页数totalPage
                totalPage = Math.DivRem(totalCount, pPageSize, out remainder);
                if (remainder > 0)
                    totalPage++;

                param.Add(new SqlParameter("@begin", begin));
                param.Add(new SqlParameter("@end", end));
                DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, pagedSql.ToString(), param.ToArray());
                if (ds != null)
                    return ds.Tables[0];
            }
            catch (Exception e)
            {
                totalPage = 0;
            }
            return null;
        }
        #endregion

        #region 员工字典
        /// <summary>
        /// 员工字典
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pUserName"></param>
        /// <returns></returns>
        public DataTable GetStaffDict(string pUserID, string pUserName)
        {
            string sql = " SELECT user_id AS UserID,user_name AS UserName FROM T_User WHERE customer_id=@CustomerID AND user_status=1 ";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));

            if (!string.IsNullOrEmpty(pUserID))
            {
                sql += " AND user_id=@UserID ";
                param.Add(new SqlParameter("@UserID", pUserID));
            }

            if (!string.IsNullOrEmpty(pUserName))
            {
                sql += " AND user_name like @UserName ";
                param.Add(new SqlParameter("@UserName", "%" + pUserName + "%"));
            }

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null)
                return ds.Tables[0];
            return null;
        }
        #endregion

        #endregion
    }
    /// <summary>
    /// 图像信息
    /// </summary>
    public class FileData
    {
        public string url { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
        public long size { get; set; }
        public string type { get; set; }
    }
}
