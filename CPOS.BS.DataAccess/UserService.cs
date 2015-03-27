using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;
using System.Configuration;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 用户方法类
    /// </summary>
    public partial class UserService : Base.BaseCPOSDAO
    {
        #region 构造函数

        
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public UserService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 用户查询

        /// <summary>
        /// 获取查询结果数量
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public int SearchUserCount(Hashtable _ht)
        {
            string sql = SearchPublicSql(_ht);
            sql = sql + " select @iCount; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet SearchUserList(Hashtable _ht)
        {
            DataSet ds = new DataSet();
            string sql = SearchPublicSql(_ht);
            sql = sql + "select "
                      + " a.user_id "
                      + ",user_name "
                      + ",user_gender "
                      + ",user_code "
                      + ",user_birthday "
                      + ",user_password "
                      + ",user_email "
                      + ",user_identity "
                      + ",user_telephone "
                      + ",user_cellphone "
                      + ",user_address "
                      + ",user_postcode "
                      + ",user_remark "
                      + ",user_name_en "
                      + ",qq "
                      + ",msn "
                      + ",blog "
                      + ",create_user_id "
                      + ",create_time "
                      + ",modify_user_id "
                      + ",modify_time "
                      + ",user_status "
                      + ",user_status_desc "
                      + ",fail_date "
                      + ",b.row_no "
                      + ", @iCount icount "
                      + "From t_user a "
                      + "inner join @TmpTable b "
                      + "on(a.user_id = b.user_id) "
                      + "where 1=1 "
                      + "and b.row_no  > '" + _ht["StartRow"].ToString() + "' and  b.row_no <= '" + _ht["EndRow"] + "' "
                      + " ;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public DataSet SearchUserListByUnitID(Hashtable _ht)
        {
            DataSet ds = new DataSet();
            string sql = SearchPublicSql(_ht);
            sql = sql + "select "
                      + " a.user_id "
                      + ",user_name "
                      + ",user_gender,(case user_gender when 1 then '男' else '女' end) as  user_genderText"
                      + ",user_code "
                      + ",user_birthday "
                      + ",user_password "
                      + ",user_email "
                      + ",user_identity "
                      + ",User_Telephone "
                      + ",user_cellphone "
                      + ",user_address "
                      + ",user_postcode "
                      + ",user_remark "
                      + ",user_name_en "
                      + ",qq "
                      + ",msn "
                      + ",blog "
                      + ",a.create_user_id "
                      + ",a.create_time "
                      + ",a.modify_user_id "
                      + ",a.modify_time "
                      + ",user_status "
                      + ",user_status_desc "
                      + ",fail_date "
                           + @",isnull((	select top 1 c.unit_name from 	  T_User_Role b  inner join t_unit c on	  b.unit_id=c.unit_id where b.user_id=a.user_id),'') as UnitName "//UnitName
                      + ",b.row_no "
                      + ", @iCount icount "
                      + "From t_user a "
                      + "inner join @TmpTable b "                     
                      + "on(a.user_id = b.user_id) "
                      + "where 1=1 "
                   
                      + "and b.row_no  > '" + _ht["StartRow"].ToString() + "' and  b.row_no <= '" + _ht["EndRow"] + "' "
                      + " ;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取查询地公共部分
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public string SearchPublicSql(Hashtable _ht)
        {
            PublicService pService = new PublicService();
            string sql = "Declare @TmpTable Table "
                     + " (user_id nvarchar(100) "
                     + " ,row_no int "
                     + " ); "
                     + " Declare @iCount int; "
                     + " insert into @TmpTable(user_id,row_no) "
                     + " select a.user_id,rownum_=row_number() over(order by a.user_code) "   //使用了rownum_的方式
                     + " from t_user a where 1=1 "
            + @"and user_id in (select user_id from  T_User_Role c inner join   vw_unit_level d ON d.unit_id = c.unit_id "
                + @"and d.path_unit_id like '%" + _ht["UnitID"] + "%' )";
            sql = pService.GetLinkSql(sql, "a.User_Name", _ht["UserName"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.User_Code", _ht["UserCode"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.User_Status", _ht["UserStatus"].ToString(), "=");
            sql = pService.GetLinkSql(sql, "a.User_CellPhone", _ht["CellPhone"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.customer_id", _ht["CustomerId"].ToString(), "=");
            sql = sql + " select @iCount = COUNT(*) From @TmpTable; ";

         
                //+ "left join T_User_Role c on b.user_id=c.user_id "
                //      + "INNER JOIN vw_unit_level d ON d.unit_id = c.unit_id "
                //      + "and d.path_unit_id like '%"+_ht["UnitID"]+"%' "

            return sql;
        }

        #endregion

        #region 用户保存
        /// <summary>
        /// 判断用户号码是否唯一
        /// </summary>
        /// <returns></returns>
        public int IsExistUserCode(string user_code,string user_id)
        {
            string sql = " select count(*) From t_user where 1=1 and user_code = '"+user_code+"' and customer_id = '"+this.loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString()+"'";
            if (user_id != null && !user_id.Equals(""))
            {
                PublicService pService = new PublicService();
                sql = pService.GetLinkSql(sql, "User_Id", user_id, "!=");
            }
            var count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));

            return count;
      
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="userRoleInfos"></param>
        /// <param name="IsTrans"></param>
        /// <param name="strDo"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetUserInfo(UserInfo userInfo, IList<UserRoleInfo> userRoleInfos,  bool IsTrans, string strDo,out string strError)
        {
            IDbTransaction tran = null;
            if (IsTrans) {
                tran = this.SQLHelper.CreateTransaction();
            }
            using (tran)
            {
                try
                {
                    //4.提交用户信息
                    if (!UpdateUser(userInfo, tran))
                    {
                        strError = "更新用户单据主表失败";
                        throw (new System.Exception(strError));
                    }
                    if (!InsertUser(userInfo, tran))
                    {
                        strError = "插入用户单据主表失败";
                        throw (new System.Exception(strError));
                    }

                    if (!DeleteUserRole(userInfo, tran))
                    {
                        strError = "删除用户与角色单据明细失败";
                        throw (new System.Exception(strError));
                    }
                    if (userRoleInfos != null)
                    {
                        foreach (UserRoleInfo userRoleInfo in userRoleInfos)
                        {
                            if (!InsertUserRole(userRoleInfo, tran))
                            {
                                strError = "插入用户与角色单据明细失败!";
                                throw (new System.Exception(strError));
                            }
                        }
                    }

                    if (IsTrans) tran.Commit();
                    strError = "成功";
                    return true;
                }
                catch (Exception ex)
                {
                    if (IsTrans) tran.Rollback();
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 更新出入库表主信息
        /// </summary>
        /// <param name="inoutInfo"></param>
        /// <param name="pTran"></param>
        private bool UpdateUser(UserInfo userInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "update t_user set user_code = '" + userInfo.User_Code + "' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "user_name", userInfo.User_Name);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_gender", userInfo.User_Gender);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_birthday", userInfo.User_Birthday);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_name_en", userInfo.User_Name_En);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_email", userInfo.User_Email);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_identity", userInfo.User_Identity);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_telephone", userInfo.User_Telephone);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_cellphone", userInfo.User_Cellphone);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_address", userInfo.User_Address);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_postcode", userInfo.User_Postcode);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_remark", userInfo.User_Remark);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_status", userInfo.User_Status);
            sql = pService.GetIsNotNullUpdateSql(sql, "qq", userInfo.QQ);
            sql = pService.GetIsNotNullUpdateSql(sql, "msn", userInfo.MSN);
            sql = pService.GetIsNotNullUpdateSql(sql, "blog", userInfo.Blog);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", userInfo.Create_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sql = pService.GetIsNotNullUpdateSql(sql, "User_Status_Desc", userInfo.User_Status_Desc);
            sql = pService.GetIsNotNullUpdateSql(sql, "Fail_Date", userInfo.Fail_Date);
            if (userInfo.ModifyPassword)
            {
                sql = pService.GetIsNotNullUpdateSql(sql, "user_password", userInfo.User_Password);
            }
            sql = sql + " where user_id = '" + userInfo.User_Id + "' ;";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }


        /// <summary>
        /// 插入出入库表主信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool InsertUser(UserInfo userInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into t_user (user_id "
                      + " ,user_code "
                      + " ,user_name "
                      + " ,user_gender "
                      + " ,user_birthday "
                      + " ,user_password "
                      + " ,user_email "
                      + " ,user_identity "
                      + " ,user_telephone "
                      + " ,user_cellphone "
                      + " ,user_address "
                      + " ,user_postcode "
                      + " ,user_remark "
                      + " ,user_status "
                      + " ,user_name_en "
                      + " ,qq "
                      + " ,msn "
                      + " ,blog "
                      + " ,create_user_id "
                      + " ,create_time "
                      + " ,modify_user_id "
                      + " ,modify_time "
                      + " ,user_status_desc "
                      + " ,fail_date "
                      + " ,customer_id "
                      + " )  "
                      + " select a.* From ( "
                      + " select '" + userInfo.User_Id + "' user_id "
                      + " ,'"+ userInfo.User_Code +"' user_code "
                      + " ,'"+ userInfo.User_Name +"' user_name "
                      + " ,'"+ userInfo.User_Gender +"' user_gender "
                      + " ,'"+ userInfo.User_Birthday +"' user_birthday "
                      + " ,'"+ userInfo.User_Password +"' user_password "
                      + " ,'"+ userInfo.User_Email +"' user_email "
                      + " ,'"+ userInfo.User_Identity +"' user_identity "
                      + " ,'"+ userInfo.User_Telephone +"' user_telephone "
                      + " ,'"+ userInfo.User_Cellphone +"' user_cellphone "
                      + " ,'"+ userInfo.User_Address +"' user_address "
                      + " ,'"+ userInfo.User_Postcode +"' user_postcode "
                      + " ,'"+ userInfo.User_Remark +"' user_remark "
                      + " ,'"+ userInfo.User_Status +"' user_status "
                      + " ,'"+ userInfo.User_Name_En +"' user_name_en "
                      + " ,'"+ userInfo.QQ +"' qq "
                      + " ,'"+ userInfo.MSN +"' msn "
                      + " ,'"+ userInfo.Blog +"' blog "
                      + " ,'"+ userInfo.Create_User_Id +"' create_user_id "
                      + " ,'"+ userInfo.Create_Time +"' create_time "
                      + " ,'"+ userInfo.Modify_User_Id +"' modify_user_id "
                      + " ,'"+ userInfo.Modify_Time +"' modify_time "
                      + " ,'"+ userInfo.User_Status_Desc +"' user_status_desc "
                      + " ,'" + userInfo.Fail_Date + "' fail_date "
                      + " ,'"+ userInfo.customer_id +"' customer_id "
                      + " ) a"
                      + " left join T_User b"
                      + " on(a.user_id = b.user_id)"
                      + " where b.user_id is null ; ";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 删除出入库单据明细
        /// </summary>
        /// <param name="inoutInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool DeleteUserRole(UserInfo userInfo, IDbTransaction pTran)
        {
            string sql = "delete t_user_role where user_id = '" + userInfo.User_Id + "' ;";
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 插入出库人单据明细
        /// </summary>
        /// <param name="inoutInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public bool InsertUserRole(UserRoleInfo userRoleInfo, IDbTransaction pTran)
        {
            #region
            
            string sql = "insert into t_user_role "
                        + " ( "
                        + " user_role_id "
                        + " ,user_id "
                        + " ,role_id "
                        + " ,unit_id "
                        + " ,status "
                        + " ,create_time "
                        + " ,create_user_id "
                        + " ,modify_time "
                        + " ,modify_user_id "
                        + " ,default_flag "
                        
                        + " )"
                        + "select  '" + userRoleInfo.Id + "' "
                        + " ,'" + userRoleInfo.UserId + "'  "
                        + " ,'" + userRoleInfo.RoleId + "'  "
                        + " ,'" + userRoleInfo.UnitId + "'  "
                        + " ,'1'  "
                        + " ,'" + System.DateTime.Now.ToString() + "'  "
                        + " ,'" + this.loggingSessionInfo.CurrentUser.User_Id + "'  "
                        + " ,'" + System.DateTime.Now.ToString() + "'  "
                        + " ,'" + this.loggingSessionInfo.CurrentUser.User_Id + "'  "
                        + " ,'" + userRoleInfo.DefaultFlag + "'  "
                        ;
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        #endregion


        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserPassword"></param>
        /// <returns></returns>
        public int ModifyUserPassword(string UserId, string UserPassword)
        {
            string sql = "update t_user set user_password = '" + UserPassword + "',modify_time = '"+ System.DateTime.Now.ToString()+"' where user_id = '" + UserId + "' ";
            var count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count;
        }
        #endregion


        #region 修改用户状态
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool SetUpdateUserStatus(UserInfo userInfo)
        {
            string sql = "update t_user " 
                       + " set user_status = '"+userInfo.User_Status+"' "
                       + " ,User_Status_Desc = '"+userInfo.User_Status_Desc+"' "
                       + " ,Modify_Time =  '"+ userInfo.Modify_Time +"' "
                       + " ,Modify_User_Id = '"+ userInfo.Modify_User_Id+"' "
                       + " where user_id = '"+ userInfo.User_Id+"' ;";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }

        #endregion

        /// <summary>
        /// 判读用户是否合法
        /// </summary>
        /// <returns></returns>
        public int IsExistUser()
        {
            string strSql = "select isnull(count(*),0) From t_user where user_id='" + 
                loggingSessionInfo.CurrentLoggingManager.User_Id.ToString() + "'";

            var count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(strSql));

            return count;
        }
        /// <summary>
        /// 根据用户标识获取用户详细信息
        /// </summary>
        /// <param name="user_id">用户标识</param>
        /// <returns></returns>
        public DataTable GetUserById(string user_id)
        {
            string sql = "";
            sql += "select distinct a.* ";
            sql += " ,b.user_name create_user_name ";
            sql += " ,c.user_name modify_user_name ";
            sql += " from t_user a ";
            sql += " left join t_user b on a.create_user_id=b.user_id ";
            sql += " left join t_user c on a.modify_user_id=c.user_id ";
            sql += " where a.user_id = '" + user_id + "'";
            DataTable dt = this.SQLHelper.ExecuteDataset(sql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取用户登陆密码
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetPasswordFromAP(string customerId, string userId)
        {
            string sql = @"select cu_pwd from cpos_ap.dbo.t_customer_user
                        where customer_id='{0}' and customer_user_id='{1}'";
            return SQLHelper.ExecuteScalar(string.Format(sql, customerId, userId)) as string;
        }
        #region 根据客户ID获取当前所有销售人员 2014-10-16
        /// <summary>
        /// 根据客户ID和门店号获取当前所有服务顾问（不填门店号则获取所有）
        /// </summary>
        /// <param name="user_id">用户标识</param>
        /// <returns></returns>
        public DataTable GetUserByCustomerID(string customerID,string unitID,string roleCode)
        {
            string sql = "";

            sql += "select u.user_name as SName,u.user_id as SalesmanID from T_User u ";
            sql += " left join T_User_Role ur on u.user_id=ur.user_id  ";
            sql += " left join T_Role r on ur.role_id=r.role_id ";
            sql += " where u.customer_id='" + customerID + "' ";

            sql += " and r.role_code='" + roleCode + "' ";

            if (!string.IsNullOrEmpty(unitID))
            {
                sql += "  and ur.unit_id='" + unitID + "' ";
            }
            DataTable dt = this.SQLHelper.ExecuteDataset(sql).Tables[0];
            return dt;
        }

        #endregion
        /// <summary>
        /// 获取用户在某种角色下的缺省的单位
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        public DataSet GetDefaultUnitByUserIdAndRoleId(string userId, string roleId)
        {
            DataSet ds = new DataSet();
            string sql = " select user_role_id Id, user_id userId, role_id RoleId, unit_id UnitId,default_flag defaultFlag, "
            + " null as userName, null as unitName, null as RoleName, null as applicationDescription "
            + "  from t_user_role "
            + "  where default_flag=1 and user_id='" + userId + "' and role_id='"+roleId+"' and  status = '1'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
         }

        #region 根据用户标识获取角色信息
        /// <summary>
        /// 根据用户标识获取角色信息
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="applicationId">系统标识</param>
        /// <returns></returns>
        public DataSet GetUserRoles(string userId, string applicationId)
        {
            string sql = "select a.user_role_id Id"
                      + " , a.user_id UserId"
                      + " , a.unit_id UnitId"
                      + " , a.role_id RoleId"
                      + " , a.default_flag DefaultFlag"
                      + " , e.role_name as  RoleName"
                      + " , c.unit_code + ' - ' + c.unit_name as  UnitName"
                      + " , d.user_name UserName"
                      + " , f.def_app_name as  ApplicationDescription"
                      + " from t_user_role a "
                      + " inner join t_unit c on a.unit_id=c.unit_id "
                      + " inner join t_user d on a.user_id=d.user_id "
                      + " inner join t_role e on a.role_id=e.role_id "
                      + " left join T_Def_App f "
                      + " on e.def_app_id=f.def_app_id where a.user_id='" + userId + "' and e.def_app_id =case when '" + applicationId + "'='' then e.def_app_id else '" + applicationId + "' end "
                      + " and  a.status = '1' order by e.def_app_id, e.role_code, a.default_flag desc, c.unit_code ;" ;

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region Dashboard登陆
        /// <summary>
        /// Dashboard登陆
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public DataSet GetSignInDashboard(UserInfo userInfo)
        {
            string sql = " SELECT c.user_id,c.user_name,'' imageUrl FROM dbo.T_Role a "
                    + " INNER JOIN dbo.T_User_Role b "
                    + " ON(a.role_id = b.role_id) "
                    + " INNER JOIN dbo.T_User c ON(b.user_id = c.user_id AND a.customer_id = c.customer_id) "
                    + " WHERE a.role_code = 'Dashboard' "
                    + " and a.customer_id = '" + userInfo.customer_id + "' "
                    + " AND (c.user_code ='" + userInfo.User_Name + "' OR c.user_name = '" + userInfo.User_Name + "')  "
                    + " AND c.user_password = '" + userInfo.User_Password + "' ";

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region IsExistUserCode
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExistUserCode(string userCode)
        {
            string sql = "select isnull(count(*),0) from t_user a where a.user_code='" + userCode + "'";
            var count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count > 0 ? true : false;
        }
        #endregion

        #region IsExistEmail
        /// <summary>
        /// 判断email是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExistEmail(string email)
        {
            string sql = "select isnull(count(*),0) from t_user a where a.user_email='" + email + "'";
            var count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count > 0 ? true : false;
        }
        #endregion

        #region GetUserNameByEmail
        /// <summary>
        /// GetUserNameByEmail
        /// </summary>
        /// <returns></returns>
        public string GetUserNameByEmail(string email)
        {
            string sql = "select user_name from t_user a where a.user_email='" + email + "'";
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == DBNull.Value || obj == null ? string.Empty : obj.ToString();
        }
        #endregion

        #region GetUserInfoByEmail
        /// <summary>
        /// GetUserInfoByEmail
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserInfoByEmail(string email)
        {
            DataSet ds = new DataSet();
            string sql = "select a.* from t_user a where a.user_email='" + email + "'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region UpdateUserPassword
        /// <summary>
        /// UpdateUserPassword
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserPassword(string userId, string password)
        {
            string sql = "update t_user set user_password='" + password + "' ,modify_user_id='" + userId + "',modify_time=CONVERT(nvarchar(20), GETDATE(),120) where user_id='" + userId + "'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region AddUserToDex
        /// <summary>
        /// AddUserToDex
        /// </summary>
        /// <returns></returns>
        public Hashtable AddUserToDex(Hashtable certInfo)
        {
            Hashtable ht = new Hashtable();
            try
            {
                string sql = "insert into t_dex_cert  ";
                sql += " (cert_id, user_id, user_code, customer_id, customer_code, cert_pwd, create_user_id, create_time, modify_user_id, modify_time, cert_type_id, cert_status) ";
                sql += " values ('" + certInfo["CertId"] + "' ";
                sql += " ,'" + certInfo["UserId"] + "' ";
                sql += " ,'" + certInfo["UserCode"] + "' ";
                sql += " ,'" + certInfo["CustomerId"] + "' ";
                sql += " ,'" + certInfo["CustomerCode"] + "' ";
                sql += " ,'" + certInfo["CertPwd"] + "' ";
                sql += " ,'" + certInfo["CreateUserId"] + "' ";
                sql += " ,'" + certInfo["CreateTime"] + "' ";
                sql += " ,'" + certInfo["ModifyUserId"] + "' ";
                sql += " ,'" + certInfo["ModifyTime"] + "' ";
                sql += " ,'90417A16B4924C11A5DF3CFF0D391CCB' ";
                sql += " ,'0' ";
                sql += " ) ";

                this.SQLHelper.ExecuteNonQuery(sql);
                ht["status"] = "True";
            }
            catch (Exception ex)
            {
                ht["status"] = "False";
                ht["error_code"] = ex.ToString();
            }
            return ht;
        }
        #endregion

        #region UpdateUserToDex
        /// <summary>
        /// UpdateUserToDex
        /// </summary>
        /// <returns></returns>
        public Hashtable UpdateUserToDex(Hashtable certInfo)
        {
            Hashtable ht = new Hashtable();
            try
            {
                string sql = "update t_dex_cert set ";
                sql += " user_code='" + certInfo["UserCode"] + "' ";
                sql += " ,customer_id='" + certInfo["CustomerId"] + "' ";
                sql += " ,customer_code='" + certInfo["CustomerCode"] + "' ";
                sql += " ,modify_user_id='" + certInfo["ModifyUserId"] + "' ";
                sql += " ,modify_time='" + certInfo["ModifyTime"] + "' ";

                if (certInfo["ModifyPassword"] != null && certInfo["ModifyPassword"].ToString() == "1" && 
                    certInfo["CertPwd"] != null && certInfo["CertPwd"].ToString().Length > 0)
                {
                    sql += " ,cert_pwd='" + certInfo["CertPwd"] + "' ";
                }

                sql += " where user_id='" + certInfo["UserId"] + "'";

                this.SQLHelper.ExecuteNonQuery(sql);
                ht["status"] = "True";
            }
            catch (Exception ex)
            {
                ht["status"] = "False";
                ht["error_code"] = ex.ToString();
            }
            return ht;
        }
        #endregion

        #region CheckUserToDex
        /// <summary>
        /// CheckUserToDex
        /// </summary>
        /// <returns></returns>
        public bool CheckUserToDex(Hashtable certInfo)
        {
            string sql = "select count(*) from t_dex_cert ";
            sql += " where user_id='" + certInfo["UserId"] + "'";
            var obj = this.SQLHelper.ExecuteScalar(sql);
            if (obj == DBNull.Value) return false;
            var count = Convert.ToInt32(obj);
            return count > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 获取订单消息信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetOrderMessage(string orderId)
        {
            DataSet ds = new DataSet();
            string sql = "select * From ("
                + " select distinct a.order_no OrderNo"
                + " ,isnull(a.Field10,a.Status_Desc) OrderStatus "
                + " ,d.user_id UserId"
                + " ,ISNULL(a.field7,a.status) OrderStatusCode"
                + " ,a.create_time"
                + " ,c.VipId VipId"
                + " ,isnull(a.Field3,c.VipName) VipName"
                + " ,ISNULL(a.field6,c.phone) Phone"
                + " ,a.total_amount Amount"
                + " ,a.actual_amount ActualAmount"
                + " ,b.IsCallSMSPush"
                + " ,b.IsCallEmailPush"
                + " ,d.user_id CallUserId"
                + " ,d.user_email CallUserEmail"
                + " ,d.user_telephone CallUserPhone"
                + " ,d.user_name CallUserName"
                + " From T_Inout a"
                + " inner join VwUnitProperty b on( "
                + " a.sales_unit_id = b.UnitId"
                + " or a.purchase_unit_id = b.UnitId)"
                + " left join Vip c"
                + " on(a.vip_no = c.VIPID)"
                + " inner join (select  d.user_id,d.user_telephone,d.user_email,d.user_name,a.unit_id UnitId,a.customer_id From vw_unit_level a"
                + " inner join T_User_Role b on(a.path_unit_id like '%' + b.unit_id + '%')"
                + " inner join T_Role c on(b.role_id = c.role_id)"
                + " inner join T_User d on(b.user_id = d.user_id and a.customer_id = d.customer_id)"
                + " where  c.role_code = 'CallCenter'"
                + " and c.status = '1'"
                + " ) d on(b.UnitId = d.UnitId and b.CustomerId = d.customer_id)"
                + " where (a.order_id = '" + orderId + "' or a.order_no = '" + orderId + "')"
                + " and d.customer_id = '" + CurrentUserInfo.CurrentUser.customer_id + "'"
                + " ) x where x.IsCallSMSPush  = 1"
                + " or x.isCallEmailPush =1";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetOrderMessage2(string orderId)
        {
            DataSet ds = new DataSet();
            string sql = "select a.order_id OrderId,a.order_no OrderNo,a.Vip_No VipId"
                + " ,isnull(a.Field10,a.Status_Desc) OrderStatus  "
                + " ,ISNULL(a.field7,a.status) OrderStatusCode"
                + " ,isnull(a.Field3,b.VipName) VipName"
                + " ,ISNULL(a.field6,b.phone) Phone"
                + " ,ISNULL(a.field3,b.Email) Email"
                + " ,a.total_amount Amount"
                + " ,a.actual_amount ActualAmount"
                + " From T_Inout a"
                + " left join Vip b on(a.vip_no = b.VIPID)"
                + " where a.order_id = '" + orderId + "' or a.order_no = '" + orderId + "'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetUserListByRoleCode(string roleCode)
        {
            DataSet ds = new DataSet();
            string sql = "select a.* "
                + " from T_User a"
                + " inner join T_User_Role b on a.user_id=b.user_id"
                + " inner join T_Role c on b.role_id=c.role_id"
                + " where a.user_status='1' and b.status='1' and c.status='1' "
                + " and c.role_code='" + roleCode + "'"
                + " and a.customer_id='" + CurrentUserInfo.CurrentUser.customer_id + "'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

    }
}
