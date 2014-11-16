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
    public class RoleMenuService: Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public RoleMenuService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 根据组织获取所有角色与菜单关系
        /// <summary>
        /// 根据组织获取所有角色与菜单关系
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public DataSet GetRoleMenuListByUnitIdAndAppCode(string appCode)
        {
            DataSet ds = new DataSet();
            string sql = " select distinct a.role_menu_id, a.role_id, a.menu_id from t_role_menu a"
                      +" inner join T_Menu b "
                      + " on(a.menu_id = b.menu_id) "
                      + " inner join T_Def_App c "
                      + " on(b.reg_app_id = c.def_app_id) "
                      + " where  c.def_app_code = '" + appCode  + "' "
                      + " and a.[status] = '1' "
                      + " and b.[status] = '1' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
