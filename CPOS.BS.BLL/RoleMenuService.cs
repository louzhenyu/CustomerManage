using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 角色与菜单关系
    /// </summary>
    public class RoleMenuService : BaseService
    {
         JIT.CPOS.BS.DataAccess.RoleMenuService roleMenuService = null;
        #region 构造函数
        public RoleMenuService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            roleMenuService = new DataAccess.RoleMenuService(loggingSessionInfo);
        }
        #endregion

        #region 根据组织获取所有角色与菜单关系
        /// <summary>
        /// 根据组织与系统，获取角色与菜单关系
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="appCode">系统号码</param>
        /// <returns></returns>
        public IList<RoleMenuModel> GetRoleMenuListByUnitIdAndAppCode( string appCode)
        {
            //("RoleMenu.SelectByUnitIdAndAppCode", _ht);
            IList<RoleMenuModel> RoleMenuInfoList = new List<RoleMenuModel>();
            DataSet ds = new DataSet();
            ds = roleMenuService.GetRoleMenuListByUnitIdAndAppCode(appCode);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                RoleMenuInfoList = DataTableToObject.ConvertToList<RoleMenuModel>(ds.Tables[0]);
            }
            return RoleMenuInfoList;
        }
        #endregion
    }
}
