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

namespace JIT.CPOS.BS.DataAccess
{
    public class RoleService: Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public RoleService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 角色保存
        /// <summary>
        /// 设置角色关系
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetRoleInfo(RoleModel roleInfo,out string strError,bool IsTran)
        {
             IDbTransaction tran = null;
             if (IsTran)
             {
                 tran = this.SQLHelper.CreateTransaction();
             }
            using (tran)
            {
                try
                {
                    if (roleInfo.customer_id == null || roleInfo.customer_id.Equals(""))
                    {
                        roleInfo.customer_id = this.CurrentUserInfo.CurrentLoggingManager.Customer_Id;
                    }
                    //判断是否存在相同的角色
                    if (IsExistRoleCode(roleInfo.Role_Code, roleInfo.Role_Id))
                    {
                        strError = "角色号码已经存在。";
                        //throw (new System.Exception(strError));
                    }
                    //2.1更新角色信息
                    if (!SetRoleUpdate(roleInfo, tran))
                    {
                        strError = "修改角色信息失败";
                        throw (new System.Exception(strError));
                    }
                    //2.2插入角色信息
                    if (!SetRoleInsert(roleInfo, tran))
                    {
                        strError = "插入角色信息失败";
                        throw (new System.Exception(strError));
                    }
                    if (roleInfo.RoleMenuInfoList != null )
                    {
                        if (roleInfo.RoleMenuInfoList.Count > 0)
                        {
                            if (!SetRoleMenuStatus(roleInfo, tran))
                            {
                                strError = "删除角色与菜单信息失败";
                                return false;
                            }

                            if (!SetRoleMenuUpdate(roleInfo, tran))
                            {
                                strError = "修改角色与菜单关系失败";
                                return false;
                            }

                            if (!SetRoleMenuInsert(roleInfo, tran))
                            {
                                strError = "插入角色与菜单关系失败";
                                return false;
                            }
                        }
                    }
                    if(IsTran) tran.Commit();
                    strError = "成功";
                    return true;
                }
                catch (Exception ex)
                {
                    if (IsTran) tran.Rollback();
                    throw (ex);
                }
            }
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetRoleUpdate(RoleModel roleInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "update t_role set def_app_id = '" + roleInfo.Def_App_Id + "' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "Role_Code", roleInfo.Role_Code);
            sql = pService.GetIsNotNullUpdateSql(sql, "Role_Name", roleInfo.Role_Name);
            sql = pService.GetIsNotNullUpdateSql(sql, "Role_Eng_Name", roleInfo.Role_Eng_Name);
            sql = pService.GetIsNotNullUpdateSql(sql, "Is_Sys", roleInfo.Is_Sys.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "Modify_Time", roleInfo.Modify_Time);
            sql = pService.GetIsNotNullUpdateSql(sql, "Modify_User_id", roleInfo.Modify_User_id);
            sql = sql + " where role_id = '" + roleInfo.Role_Id + "' ;";
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
        /// 插入角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetRoleInsert(RoleModel roleInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into t_role (role_id "
                      + " ,def_app_id "
                      + " ,role_code "
                      + " ,role_name "
                      + " ,role_eng_name "
                      + " ,is_sys "
                      + " ,status "
                      + " ,create_time "
                      + " ,create_user_id "
                      + " ,modify_time "
                      + " ,modify_user_id "
                      + " ,customer_id) "
                      + " select a.* From ( "
                      + " select '"+roleInfo.Role_Id+"' Role_Id "
                      + " ,'"+ roleInfo.Def_App_Id+"' Def_App_Id "
                      + " ,'" + roleInfo.Role_Code + "' Role_Code "
                      + " ,'" + roleInfo.Role_Name + "' Role_Name "
                      + " ,'" + roleInfo.Role_Eng_Name + "' Role_Eng_Name "
                      + " ,'" + roleInfo.Is_Sys + "' Is_Sys "
                      + " ,'1' [Status] "
                      + " ,'" + roleInfo.Create_Time + "' Create_Time "
                      + " ,'" + roleInfo.Create_User_Id + "' Create_user_Id "
                      + " ,'" + roleInfo.Create_Time + "' Modify_Time "
                      + " ,'" + roleInfo.Create_User_Id + "' Modify_User_Id "
                      + " ,'" + roleInfo.customer_id + "' customer_id "
                      + " ) a "
                      + " left join (select * From t_role where [status]='1') b "
                      + " on(a.role_code = b.role_code and a.customer_id = b.customer_id) "
                      + " where b.role_code is null "
                      + " ;";
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
        /// 判断角色号码是否重复
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        private bool IsExistRoleCode(string RoleCode, string RoleId)
        {
            string sql = "select count(*) From t_role where 1=1 and role_code = '"+RoleCode+"' and [status] = '1' and customer_id = '"+ this.loggingSessionInfo.CurrentLoggingManager.Customer_Id+"'";
            if (RoleId != null && !RoleId.Equals(""))
            {
                sql = sql + " and role_id != '" + RoleId + "'";
            }

            int count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count==1;
        }
        /// <summary>
        /// 删除角色与菜单关系
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetRoleMenuStatus(RoleModel roleInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region

            string sql = "update T_Role_Menu set status = '-1' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", roleInfo.Create_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", roleInfo.Create_Time);
            sql = sql + " where role_id = '" + roleInfo.Role_Id + "'  ";

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
        /// 修改角色与菜单关系
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetRoleMenuUpdate(RoleModel roleInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region
            string sql = "update T_Role_Menu set status = '1'";
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", roleInfo.Create_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", roleInfo.Create_Time);
            sql = sql + " From ( ";
            int i = 0;
            foreach (RoleMenuModel roleMenuRole in roleInfo.RoleMenuInfoList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + roleInfo.Role_Id + "' role_id "
                          + ",'" + roleMenuRole.Menu_Id + "' Menu_Id ";
                i++;
            }
            sql = sql + " ) a where ( T_Role_Menu.role_id = a.role_id and T_Role_Menu.menu_id = a.menu_id)";
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
        /// 插入角色与菜单关系
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetRoleMenuInsert(RoleModel roleInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "INSERT INTO T_Role_Menu "
                        + " ( "
                        + " role_menu_Id "
                        + " ,role_id "
                        + " ,menu_id "
                        + " ,status "
                        + " ,create_time "
                        + " ,create_user_id "
                        + " ,modify_time "
                        + " ,modify_user_id "
                        + " ) "
                        + " SELECT P.role_menu_Id "
                        + " ,P.role_id "
                        + " ,P.menu_id "
                        + " ,P.status "
                        + " ,P.create_time "
                        + " ,P.create_user_id "
                        + " ,P.modify_time "
                        + " ,P.modify_user_id "
                        + " FROM ( ";
            int i = 0;
            foreach (RoleMenuModel roleMenuInfo in roleInfo.RoleMenuInfoList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + NewGuid() + "' role_menu_Id "
                          + ",'" + roleInfo.Create_User_Id + "' modify_user_id "
                          + ",'" + roleInfo.Create_Time + "' modify_time "
                          + ",'" + roleInfo.Create_User_Id + "' create_user_id "
                          + ",'" + roleInfo.Create_Time + "' create_time "
                          + ",'1' status "
                          + ",'" + roleMenuInfo.Role_Id + "' role_id "
                          + ",'" + roleMenuInfo.Menu_Id + "' menu_id ";
                i++;

            }
            sql = sql + " ) P "
                      + " left join T_Role_Menu  b "
                      + " on(p.role_id = b.role_id "
                      + " and p.menu_id = b.menu_id) "
                      + " where b.role_menu_id is null;";


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

        #region 根据组织获取角色
        /// <summary>
        /// 根据组织获取角色
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public DataSet GetRoleListByUnitId(string unit_id)
        {
            DataSet ds = new DataSet();
            #region
            string sql = "select distinct a.role_id "
                          + " , a.def_app_id reg_app_id "
                          + " , a.role_code "
                          + " , a.create_user_id add_user_id "
                          + " , a.create_time add_date "
                          + " , a.role_name "
                          + " , a.Role_Eng_Name "
                          + " , a.modify_user_id "
                          + " , a.modify_time modify_date "
                          + " , a.is_sys "
                          + " , b.user_name as add_user_name "
                          + " , c.user_name as modify_user_name "
                          + " , a.role_name as role_desc "
                          + " , case when ISNULL(is_sys,0) = 1 then '是' else '否' end default_flag_desc "
                          + " from t_role a "
                          + " left join t_user b on a.create_user_id=b.user_id "
                          + " left join t_user c on a.modify_user_id=c.user_id "
                          + " inner join T_User_Role x "
                          + " on(a.role_id = x.role_id) "
                          + " where 1=1 "
                          + " and a.[status] = '1' "
                          + " and x.unit_id= '"+unit_id+"'"
                          + " and a.customer_id = '"+this.loggingSessionInfo.CurrentLoggingManager.Customer_Id+"' ; ";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetAllRoles()
        {
            DataSet ds = new DataSet();
            #region
            string sql = "select distinct a.role_id "
                          + " , a.def_app_id reg_app_id "
                          + " , a.role_code "
                          + " , a.create_user_id add_user_id "
                          + " , a.create_time add_date "
                          + " , a.role_name "
                          + " , a.Role_Eng_Name "
                          + " , a.modify_user_id "
                          + " , a.modify_time modify_date "
                          + " , a.is_sys "
                          + " , b.user_name as add_user_name "
                          + " , c.user_name as modify_user_name "
                          + " , a.role_name as role_desc "
                          + " , case when ISNULL(is_sys,0) = 1 then '是' else '否' end default_flag_desc "
                          + " from t_role a "
                          + " left join t_user b on a.create_user_id=b.user_id "
                          + " left join t_user c on a.modify_user_id=c.user_id "
                          + " inner join T_User_Role x "
                          + " on(a.role_id = x.role_id) "
                          + " where 1=1 "
                          + " and a.[status] = '1' "
                          + " and a.customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "'   order by a.role_code";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetRoleDefaultByCustomerId(string customer_id)
        {
            DataSet ds = new DataSet();
            #region
            string sql = "select distinct a.role_id "
                          + " , a.def_app_id reg_app_id "
                          + " , a.role_code "
                          + " , a.create_user_id add_user_id "
                          + " , a.create_time add_date "
                          + " , a.role_name "
                          + " , a.Role_Eng_Name "
                          + " , a.modify_user_id "
                          + " , a.modify_time modify_date "
                          + " , a.is_sys "
                          + " , b.user_name as add_user_name "
                          + " , c.user_name as modify_user_name "
                          + " , a.role_name as role_desc "
                          + " , case when ISNULL(is_sys,0) = 1 then '是' else '否' end default_flag_desc "
                          + " from t_role a "
                          + " left join t_user b on a.create_user_id=b.user_id "
                          + " left join t_user c on a.modify_user_id=c.user_id "
                          + " inner join T_User_Role x "
                          + " on(a.role_id = x.role_id) "
                          + " where 1=1 "
                          + " and a.[status] = '1' "
                          + " and a.customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' and a.role_code = 'Admin'; ";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 删除角色
        public bool DeleteRole(RoleModel roleInfo)
        {
            using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    if (GetUserCountByRoleId(roleInfo.Role_Id) > 0)
                    {
                        throw (new System.Exception("给角色设置了用户,就不能再删除角色了"));
                    }
                    if (!DeleteRoleInfo(roleInfo.Role_Id))
                    {
                        throw (new System.Exception("删除角色信息失败"));
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw (ex);
                }
            }
        }

        private int GetUserCountByRoleId(string role_id)
        {
            DataSet ds = new DataSet();
            string sql = "select isnull(count(*),0) count from t_user_role where role_id='"+role_id+"' and status = '1'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            int i = 0;
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            return i;
        }

        private bool DeleteRoleInfo(string role_id)
        {
            string sql = "update t_role set [status] = '-1' where role_id = '"+role_id+"' ;";
            sql = sql + "update t_role_menu set [status] = '-1' where role_id = '" + role_id + "' ;";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;

        }
        #endregion

        #region GetRoleByRoleCode
        /// <summary>
        /// 通过角色Code获取角色
        /// </summary>
        public DataSet GetRoleByRoleCode(string customerId, string roleCode)
        {
            DataSet ds = new DataSet();
            #region
            string sql = "select distinct a.role_id "
                          + " , a.def_app_id reg_app_id "
                          + " , a.role_code "
                          + " , a.create_user_id add_user_id "
                          + " , a.create_time add_date "
                          + " , a.role_name "
                          + " , a.Role_Eng_Name "
                          + " , a.modify_user_id "
                          + " , a.modify_time modify_date "
                          + " , a.is_sys "
                          + " , b.user_name as add_user_name "
                          + " , c.user_name as modify_user_name "
                          + " , a.role_name as role_desc "
                          + " , case when ISNULL(is_sys,0) = 1 then '是' else '否' end default_flag_desc "
                          + " from t_role a "
                          + " left join t_user b on a.create_user_id=b.user_id "
                          + " left join t_user c on a.modify_user_id=c.user_id "
                          + " where 1=1 "
                          + " and a.[status] = '1' "
                          + " and a.customer_id='" + customerId + "' and a.role_code='" + roleCode + "'; ";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region GetRoleList
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetRoleList()
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@"
SELECT * FROM dbo.T_Role
WHERE customer_id='{0}' AND is_sys=0 AND status=1 and isnull(table_name,'')<>''
order by create_time asc
", this.loggingSessionInfo.ClientID);
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
