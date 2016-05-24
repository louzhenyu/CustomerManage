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
    /// <summary>
    /// 基础信息
    /// </summary>
    public class AppSysService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public AppSysService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 得到MenuId通过UrlPath
        /// <summary>
        /// 得到MenuId通过UrlPath xiaowen,qin 2016.5.19
        /// </summary>
        /// <param name="targetStr"></param>
        /// <returns></returns>
        public List<string> GetMenuIds(string targetStr)
        {
            if (CurrentUserInfo == null || string.IsNullOrWhiteSpace(targetStr))
            {
                return null;
            }
            List<string> list = new List<string>();
            targetStr = System.Web.HttpUtility.UrlDecode(targetStr);
            string sql = "select menu_id from t_menu where status=1 and url_path=@targetStr and customer_id=@customer_id";
            List<SqlParameter> plist = new List<SqlParameter>() { 
            new SqlParameter("@targetStr",targetStr),
            new SqlParameter("@customer_id",CurrentUserInfo.ClientID)
            };
            var result = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, plist.ToArray());
            foreach (DataRow dr in result.Tables[0].Rows)
            {
                list.Add((string)dr["menu_id"]);
            }
            return list;
        }
        #endregion

        #region 根据角色获取菜单
        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        public DataSet GetRoleMenus(string roleId)
        {
            string sql = "select  a.Menu_Id,"
                      + " a.Reg_App_Id, "
                      + " a.Menu_Code,"
                      + " a.Parent_Menu_Id,"
                      + " a.Menu_Name"
                      + " ,a.Menu_Level"
                      + " ,a.Url_Path"
                      + " ,a.Icon_Path"
                      + " ,a.Display_Index"
                      + " ,a.Menu_Name"
                      + " ,a.User_Flag"
                      + " ,a.Menu_Eng_Name"
                      + " ,a.Status"
                      + " ,a.Create_User_Id"
                      + " ,a.Create_Time"
                      + " ,a.Modify_User_id"
                      + " ,a.Modify_Time"
                      + " from t_menu a"
                      + " inner join (select distinct x.menu_id,y.def_app_id from t_role_menu x,T_Role y where x.role_id = y.role_id and x.role_id='" + roleId + "' and x.status = '1') b "
                      + " on(a.menu_id = b.menu_id "
                      + " and a.reg_app_id = b.def_app_id) "
                      + " where a.status = '1'"

                      //+ " and a.menu_id in (select distinct menu_id from t_role_menu where role_id='"+roleId+"' and status = '1')"
                      + " order by a.menu_level, a.display_index,a.create_time";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region
        /// <summary>
        /// 根据角色标识，获取角色全集
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        public DataTable GetRoleById(string roleId)
        {
            string sql = "select distinct a.role_id "
                         + " , a.def_app_id Def_App_Id "
                         + "  , a.role_code "
                         + "  , a.create_user_id "
                         + "  , a.create_time "
                         + "  , a.role_name "
                         + "  , a.Role_Eng_Name "
                         + "  , a.modify_user_id "
                         + "  , a.modify_time "
                         + "  , a.is_sys "
                         + "  , b.user_name as create_user_name "
                         + "  , c.user_name as modify_user_name "
                         + "  , a.role_name as role_desc "
                         + "  , case when ISNULL(is_sys,0) = 1 then '是' else '否' end default_flag_desc "//系统保留
                         + "  , (select def_app_name From T_Def_App x where x.def_app_id = a.def_app_id) Def_App_Name "//系统名称
                          + "  , a.type_id,a.org_level "
                         + "  from t_role a "  //
                         + "  left join t_user b on a.create_user_id=b.user_id "
                         + " left join t_user c on a.modify_user_id=c.user_id"
                         + " where a.role_id= '" + roleId + "' "
                         + " and a.customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "';";
            DataTable dt = new DataTable();
            dt = this.SQLHelper.ExecuteDataset(sql).Tables[0];
            return dt;
        }
        #endregion

        #region 获取单据号码
        /// <summary>
        /// 订单号码
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public string GetNo(string prefix)
        {
            string sql = "Declare @T Table(i int) "
                      + " insert into  @T "
                      + " select  count(*)  From t_seqno; "
                      + " insert into t_seqno "
                      + " select '1' From @T where i is null or i = 0;"
                      + " update t_seqno "
                      + " set no_value = ISNULL(no_value,0)+1;"
                      + " select * From t_seqno;";
            Int64 val = Convert.ToInt64(this.SQLHelper.ExecuteScalar(sql));
            return prefix + val.ToString().PadLeft(8, '0');

        }
        #endregion

        #region 应用系统
        /// <summary>
        /// 获取所有应用系统
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllAppSyses()
        {
            DataSet ds = new DataSet();
            string sql = "select  a.Def_App_Id,a.Def_App_Code,a.Def_App_Name from t_def_app a ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region
        public int SearchRoleByAppSysIdCount(Hashtable _ht)
        {
            string sql = SearchRoleByAppSysIdPub(_ht);
            sql = sql + " select @iCount; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet SearchRoleByAppSysIdList(Hashtable _ht)
        {
            DataSet ds = new DataSet();
            string sql = SearchRoleByAppSysIdPub(_ht);
            sql = sql + "select distinct a.role_id "
                  + " , a.def_app_id reg_app_id,a.Def_App_Id "
                  + " , app.def_app_code "
                  + " , app.def_app_name "
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
                  +",e.type_name" //层级名称
                       + ",a.org_level" //层级名称
                  + " , case when ISNULL(is_sys,0) = 1 then '是' else '否' end default_flag_desc "
                  + " , d.row_no ,a.type_id"
                  + " , @iCount icount "
                  + " from t_role a "
                  + " left join t_user b on a.create_user_id=b.user_id "
                  + " left join t_user c on a.modify_user_id=c.user_id "
                  + " left join t_def_app app on a.def_app_id=app.def_app_id "
                       + " left join t_type e  on a.type_id=e.type_id "
                  + " inner join @TmpTable d "
                  + " on(a.role_id = d.role_id) "
                  + " where 1=1 and a.[status] = '1' "
                  + " and d.row_no  >= '" + _ht["StartRow"].ToString() + "' and  d.row_no <= '" + _ht["EndRow"] + "'  "
                  + " order  by a.modify_time desc ;";   // by a.def_app_id, a.role_code 
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public string SearchRoleByAppSysIdPub(Hashtable _ht)
        {
            string sql = " Declare @TmpTable Table "
                      + " (role_id nvarchar(100) "
                      + " ,row_no int "
                      + " ); "
                      + " Declare @iCount int; "
                      + " insert into @TmpTable(role_id,row_no) "
                      + " select a.role_id "
                      + " ,row_no=row_number() over(order by a.modify_time desc) "
                      + " From t_role a "
                      + " where 1=1 and a.[status] = '1' ";
            PublicService pService = new PublicService();
            sql = pService.GetLinkSql(sql, "a.def_app_id", _ht["ApplicationId"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.Customer_Id", _ht["CustomerId"].ToString(), "=");
            sql = pService.GetLinkSql(sql, "a.role_name", _ht["role_name"]==null?"": _ht["role_name"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.type_id", _ht["type_id"] == null ? "" : _ht["type_id"].ToString(), "=");
            sql = pService.GetLinkSql(sql, "a.type_id", _ht["type_id"] == null ? "" : _ht["type_id"].ToString(), "=");
            sql += " and a.role_code!='Administrator'";//不显示超级管理员
            if (string.IsNullOrEmpty(_ht["UserID"].ToString()))
            {
                sql += @" and  a.org_level >=( select min(z.type_level)  from T_User_Role x inner join t_role y 
                                  on x.role_id=y.role_id
                                  inner join t_type z on y.type_id=z.type_id where type_code!='OnlineShopping'  
                                   and  x.user_id='" + _ht["UserID"].ToString() + "')";
            }   
            sql = sql + " select @iCount = COUNT(*) From @TmpTable; ";
            return sql;
        }
        #endregion


        #region 菜单
        /// <summary>
        /// 获取某个系统的所有菜单
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public DataSet GetAllMenusByAppSysCode(string appCode)
        {
            DataSet ds = new DataSet();
            #region
            string sql = "select  a.Menu_Id, "
                      + " a.Reg_App_Id, "
                      + " a.Menu_Code, "
                      + " a.Parent_Menu_Id, "
                      + " a.Menu_Name "
                      + " ,a.Menu_Level "
                      + " ,a.Url_Path "
                      + " ,a.Icon_Path "
                      + " ,a.Display_Index "
                      + " ,a.Menu_Name "
                      + " ,a.User_Flag "
                      + " ,a.Menu_Eng_Name "
                      + " ,a.Status "
                      + " ,a.Create_User_Id "
                      + " ,a.Create_Time "
                      + " ,a.Modify_User_id "
                      + " ,a.Modify_Time "
                      + " from t_menu a "
                      + " where a.status = '1' and exists (select 1 from t_def_app b  "
                      + " where b.def_app_code= '" + appCode + "' and b.def_app_id=a.reg_app_id) "
                      + " and customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' "
                      + " order by a.menu_level, a.display_index";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetAllMenusByAppSysId(string appId)
        {
            DataSet ds = new DataSet();
            #region
            string sql = "select  a.Menu_Id, "
                      + " a.Reg_App_Id, "
                      + " a.Menu_Code, "
                      + " a.Parent_Menu_Id, "
                      + " a.Menu_Name "
                      + " ,a.Menu_Level "
                      + " ,a.Url_Path "
                      + " ,a.Icon_Path "
                      + " ,a.Display_Index "
                      + " ,a.Menu_Name "
                      + " ,a.User_Flag "
                      + " ,a.Menu_Eng_Name "
                      + " ,a.Status "
                      + " ,a.Create_User_Id "
                      + " ,a.Create_Time "
                      + " ,a.Modify_User_id "
                      + " ,a.Modify_Time "
                      + " from t_menu a "
                      + " where a.status = '1' and exists (select 1 from t_def_app b  "
                      + " where b.def_app_id= '" + appId + "' and b.def_app_id=a.reg_app_id) "
                      + " and customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' "
                      + " order by a.menu_level, a.display_index";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetMenuListCount(MenuModel entity)
        {
            string sql = GetMenuListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetMenuList(MenuModel entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetMenuListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetMenuListSql(MenuModel entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.display_index asc) ";
            sql += " ,b.Def_App_Name Reg_App_Name ";
            sql += " ,c.menu_name parent_menu_name ";
            sql += " into #tmp ";
            sql += " from t_menu a ";
            sql += " left join t_def_app b on b.Def_App_Id=a.reg_app_id ";
            sql += " left join t_menu c on c.menu_id=a.parent_menu_id ";
            sql += " where a.status='1' ";
            sql += " and a.customer_id='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.Menu_Id != null && entity.Menu_Id.Trim().Length > 0)
            {
                sql += " and a.Menu_Id = '" + entity.Menu_Id + "' ";
            }
            if (entity.Parent_Menu_Id != null && entity.Parent_Menu_Id.Trim().Length > 0)
            {
                sql += " and a.Parent_Menu_Id = '" + entity.Parent_Menu_Id + "' ";
            }
            if (entity.Reg_App_Id != null && entity.Reg_App_Id.Trim().Length > 0)
            {
                sql += " and a.Reg_App_Id = '" + entity.Reg_App_Id + "' ";
            }
            if (entity.Menu_Name != null && entity.Menu_Name.Trim().Length > 0)
            {
                sql += " and a.Menu_Name like '%" + entity.Menu_Name + "%' ";
            }
            if (entity.Menu_Code != null && entity.Menu_Code.Trim().Length > 0)
            {
                sql += " and a.Menu_Code like '%" + entity.Menu_Code + "%' ";
            }
            return sql;
        }

        /// <summary>
        /// 获取所有应用系统
        /// </summary>
        /// <returns></returns>
        public void DeleteMenuById(string menuId)
        {
            string sql = "update t_menu set status='-1' where menu_id='" + menuId + "' ";
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 检查菜单是否有下一层
        /// </summary>
        /// <param name="meauId"></param>
        /// <returns></returns>
        public bool IsCheckMeauLast(string menuId)
        {
            string sql = "select count(1) from t_menu where parent_menu_id='" + menuId + "' and status='1'";
            int res = (int)this.SQLHelper.ExecuteScalar(sql);
            if (res > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断号码是否唯一
        /// </summary>
        /// <returns></returns>
        public int IsExistMenuCode(string menu_code, string menu_id)
        {
            string sql = " select count(*) From t_menu where status=1 and menu_code = '" + menu_code + "' and customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString() + "'";
            if (menu_id != null && !menu_id.Equals(""))
            {
                PublicService pService = new PublicService();
                sql = pService.GetLinkSql(sql, "menu_id", menu_id, "!=");
            }
            var count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));

            return count;

        }

        /// <summary>
        /// 保存信息
        /// </summary>
        public bool SetMenuInfo(MenuModel menuInfo, bool IsTrans, string strDo, out string strError)
        {
            IDbTransaction tran = null;
            if (IsTrans)
            {
                tran = this.SQLHelper.CreateTransaction();
            }
            using (tran)
            {
                try
                {
                    //4.提交用户信息
                    if (!UpdateMenu(menuInfo, tran))
                    {
                        strError = "更新失败";
                        throw (new System.Exception(strError));
                    }
                    if (!InsertMenu(menuInfo, tran))
                    {
                        strError = "插入失败";
                        throw (new System.Exception(strError));
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
        private bool UpdateMenu(MenuModel userInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "update t_menu set menu_code = '" + userInfo.Menu_Code + "' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "reg_app_id", userInfo.Reg_App_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "parent_menu_id", userInfo.Parent_Menu_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "menu_level", userInfo.Menu_Level.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "url_path", userInfo.Url_Path);
            sql = pService.GetIsNotNullUpdateSql(sql, "icon_path", userInfo.Icon_Path);
            sql = pService.GetIsNotNullUpdateSql(sql, "display_index", userInfo.Display_Index.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "menu_name", userInfo.Menu_Name);
            sql = pService.GetIsNotNullUpdateSql(sql, "user_flag", userInfo.User_Flag.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "menu_eng_name", userInfo.Menu_Eng_Name);
            sql = pService.GetIsNotNullUpdateSql(sql, "status", userInfo.Status.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", userInfo.Create_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sql = sql + " where menu_id = '" + userInfo.Menu_Id + "' ;";
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
        private bool InsertMenu(MenuModel userInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into t_menu (menu_id "
                      + " ,reg_app_id "
                      + " ,menu_code "
                      + " ,parent_menu_id "
                      + " ,menu_level "
                      + " ,url_path "
                      + " ,icon_path "
                      + " ,display_index "
                      + " ,menu_name "
                      + " ,user_flag "
                      + " ,menu_eng_name "
                      + " ,status "
                      + " ,create_user_id "
                      + " ,create_time "
                      + " ,modify_user_id "
                      + " ,modify_time "
                      + " ,customer_id "
                      + " )  "
                      + " select a.* From ( "
                      + " select '" + userInfo.Menu_Id + "' menu_id "
                      + " ,'" + userInfo.Reg_App_Id + "' reg_app_id "
                      + " ,'" + userInfo.Menu_Code + "' menu_code "
                      + " ,'" + userInfo.Parent_Menu_Id + "' parent_menu_id "
                      + " ,'" + userInfo.Menu_Level + "' menu_level "
                      + " ,'" + userInfo.Url_Path + "' url_path "
                      + " ,'" + userInfo.Icon_Path + "' icon_path "
                      + " ,'" + userInfo.Display_Index + "' display_index "
                      + " ,'" + userInfo.Menu_Name + "' menu_name "
                      + " ,'" + userInfo.User_Flag + "' user_flag "
                      + " ,'" + userInfo.Menu_Eng_Name + "' menu_eng_name "
                      + " ,'" + userInfo.Status + "' status "
                      + " ,'" + userInfo.Create_User_Id + "' create_user_id "
                      + " ,'" + userInfo.Create_Time + "' create_time "
                      + " ,'" + userInfo.Modify_User_id + "' modify_user_id "
                      + " ,'" + userInfo.Modify_Time + "' modify_time "
                      + " ,'" + userInfo.customer_id + "' customer_id "
                      + " ) a"
                      + " left join T_menu b"
                      + " on(a.menu_id = b.menu_id)"
                      + " where b.menu_id is null ; ";
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
    }
}
