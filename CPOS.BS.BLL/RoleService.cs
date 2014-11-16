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
    /// 角色服务
    /// </summary>
    public class RoleService : BaseService
    {
        JIT.CPOS.BS.DataAccess.RoleService roleService = null;
        #region 构造函数
        public RoleService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            roleService = new DataAccess.RoleService(loggingSessionInfo);
        }
        #endregion

        #region 保存角色信息
        /// <summary>
        /// 设置角色保存信息（新建修改）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public string SetRoleInfo(RoleModel roleInfo, bool IsTran = true)
        {
            try
            {
                if (roleInfo != null)
                {
                    roleInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;

                    if (roleInfo.Modify_User_id == null || roleInfo.Modify_User_id.Equals(""))
                    {
                        roleInfo.Modify_User_id = loggingSessionInfo.CurrentUser.User_Id;
                        roleInfo.Modify_Time = GetCurrentDateTime();
                    }
                    if (roleInfo.Role_Id == null || roleInfo.Role_Id.Equals(""))
                    {
                        roleInfo.Role_Id = NewGuid();
                        if (roleInfo.Create_User_Id == null || roleInfo.Create_User_Id.Equals(""))
                        {
                            roleInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                            roleInfo.Create_Time = GetCurrentDateTime();
                        }

                    }
                    if (roleInfo.RoleMenuInfoList != null)
                    {
                        foreach (RoleMenuModel roleMenuInfo in roleInfo.RoleMenuInfoList)
                        {
                            roleMenuInfo.Role_Id = roleInfo.Role_Id;
                            roleMenuInfo.Create_User_Id = roleInfo.Create_User_Id;
                            roleMenuInfo.Create_Time = roleInfo.Create_Time;
                            roleMenuInfo.Modify_User_id = roleInfo.Modify_User_id;
                            roleMenuInfo.Modify_Time = roleInfo.Modify_Time;
                            roleMenuInfo.Status = 1;
                        }
                    }

                    string strError = string.Empty;
                    roleService.SetRoleInfo(roleInfo, out strError, IsTran);
                    return strError;
                }
                else
                {
                    return "RoleModel对象为空";
                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }




        #endregion

        #region 根据组织，获取角色信息
        /// <summary>
        /// 根据组织，获取角色信息
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录的信息</param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public IList<RoleModel> GetRoleListByUnitId(string unitId)
        {
            IList<RoleModel> roleInfoList = new List<RoleModel>();
            DataSet ds = new DataSet();
            ds = roleService.GetRoleListByUnitId(unitId);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                roleInfoList = DataTableToObject.ConvertToList<RoleModel>(ds.Tables[0]);
            }
            return roleInfoList;
        }

        /// <summary>
        /// 查询所有的角色
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录的信息</param>
        /// <returns></returns>
        public IList<RoleModel> GetAllRoles()
        {
            IList<RoleModel> roleInfoList = new List<RoleModel>();
            DataSet ds = new DataSet();
            ds = roleService.GetAllRoles();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                roleInfoList = DataTableToObject.ConvertToList<RoleModel>(ds.Tables[0]);
            }
            return roleInfoList;
        }

        /// <summary>
        /// 获取客户的默认角色
        /// </summary>
        /// <param name="loggingManager"></param>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public RoleModel GetRoleDefaultByCustomerId(string customer_id)
        {
            RoleModel roleInfo = new RoleModel();
            DataSet ds = new DataSet();
            ds = roleService.GetRoleDefaultByCustomerId(customer_id);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                roleInfo = DataTableToObject.ConvertToObject<RoleModel>(ds.Tables[0].Rows[0]);
            }
            return roleInfo;
        }
        #endregion

        #region 删除角色
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteRoleById(string roleId)
        {
            //string strResult = string.Empty;
            try
            {
                RoleModel role = new AppSysService(loggingSessionInfo).GetRoleById(loggingSessionInfo, roleId);
                if (role == null)
                {
                    throw (new System.Exception("角色不存在"));
                }
                //不能删除系统保留的角色
                if (role.Is_Sys == 1)
                {
                    throw (new System.Exception("不能删除系统保留的角色"));
                }
                bool b = roleService.DeleteRole(role);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GetRoleByRoleCode
        /// <summary>
        /// 通过角色Code获取角色
        /// </summary>
        public RoleModel GetRoleByRoleCode(string customerId, string roleCode)
        {
            RoleModel roleInfo = new RoleModel();
            DataSet ds = new DataSet();
            ds = roleService.GetRoleByRoleCode(customerId, roleCode);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                roleInfo = DataTableToObject.ConvertToObject<RoleModel>(ds.Tables[0].Rows[0]);
            }
            return roleInfo;
        }
        #endregion

        #region GetRoleList
        public DataSet GetRoleList()
        {
            return roleService.GetRoleList();
        }
        #endregion
    }
}
