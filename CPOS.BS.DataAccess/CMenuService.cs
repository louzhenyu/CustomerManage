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
    public class CMenuService : Base.BaseCPOSDAO
    {
         #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public CMenuService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        /// <summary>
        /// 菜单集合
        /// </summary>
        /// <param name="menuInfoList"></param>
        /// <returns></returns>
        public bool SetMenuListInfo(IList<MenuModel> menuInfoList)
        {
            if (menuInfoList != null && menuInfoList.Count > 0)
            {
                foreach (MenuModel menuInfo in menuInfoList)
                {
                    #region
                    string sql = " INSERT INTO dbo.T_Menu "
                                + " ( menu_id , "
                                + "   reg_app_id , "
                                + "   menu_code , "
                                + "   parent_menu_id , "
                                + "   menu_level , "
                                + "   url_path , "
                                + "   icon_path , "
                                + "   display_index , "
                                + "   menu_name , "
                                + "   user_flag , "
                                + "   menu_eng_name , "
                                + "   status , "
                                + "   create_user_id , "
                                + "   create_time , "
                                + "   modify_user_id , "
                                + "   modify_time , "
                                + "   customer_id "
                                + "  ) "
                                + " select '"+menuInfo.Menu_Id+"' Menu_Id "
                                + " , '"+menuInfo.Reg_App_Id+"' Reg_App_Id "
                                + " ,'"+menuInfo.Menu_Code+"' Menu_Code "
                                + " , '"+menuInfo.Parent_Menu_Id+"' Parent_Menu_Id "
                                + " , '" + menuInfo.Menu_Level + "' Menu_Level "
                                + " , '" + menuInfo.Url_Path + "' Url_Path "
                                + " , '" + menuInfo.Icon_Path + "' Icon_Path "
                                + " , '" + menuInfo.Display_Index + "' Display_Index "
                                + " , '"+menuInfo.Menu_Name+"' Menu_Name "
                                + " , '"+menuInfo.User_Flag+"' User_Flag "
                                + " , '"+menuInfo.Menu_Eng_Name+"' Menu_Eng_Name "
                                + " , '"+menuInfo.Status+"' Status "
                                + " , '"+menuInfo.Create_User_Id+"' Create_User_Id "
                                + " , convert(nvarchar(100),GETDATE(),120) Create_Time "
                                + " ,'"+menuInfo.Modify_User_id+"' Modify_User_id "
                                + " ,convert(nvarchar(100),GETDATE(),120) Modify_Time "
                                + " ,'"+menuInfo.customer_id+"' customer_id ";
                    #endregion
                    this.SQLHelper.ExecuteNonQuery(sql);
                }
                string sql1 = "insert into t_role_menu(role_menu_id,role_id,menu_id,status,create_user_id,create_time) "
                            + " SELECT x. *FROM ( "
                            + " SELECT replace(newid(),'-','') roleMenuId "
                            + " ,role_id "
                            + " ,menu_id,b.STATUS,'--' create_user_id "
                            + " ,convert(varchar,getdate(),120) create_time  FROM t_role a,t_menu b "
                            + " WHERE a.customer_id = b.customer_id "
                            + " and a.customer_id =  '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' ) x "
                            + " LEFT JOIN t_role_menu y ON(x.role_id = y.role_id AND x.menu_id = y.menu_id) "
                            + " WHERE y.role_id IS null ";

                this.SQLHelper.ExecuteNonQuery(sql1);
            }
            return true;
        }
        #endregion
    }
}
