using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Collections.Generic;
using System.Linq;

namespace JIT.CPOS.BS.BLL
{
    public class SetRoleMenuJobBLL
    {
        /// <summary>
        /// 所有商户
        /// </summary>
        private Dictionary<string, string> _CustomerIDList
        { get; set; }
        /// <summary>
        /// SessionInfo
        /// </summary>
        private LoggingSessionInfo _T_loggingSessionInfo
        { get; set; }
        /// <summary>
        /// 基础信息
        /// </summary>
        private JIT.CPOS.BS.DataAccess.AppSysService _AppSysService
        { get; set; }
        /// <summary>
        /// 角色 BLL
        /// </summary>
        private T_RoleBLL _T_RoleBLL
        { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SetRoleMenuJobBLL()
        {
            _T_loggingSessionInfo = new LoggingSessionInfo();
            _T_loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
        }

        /// <summary>
        /// 种植 角色菜单 缓存
        /// </summary>
        public void AutoSetRoleMenuCache()
        {
            _CustomerIDList = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in _CustomerIDList)
            {
                //
                _T_loggingSessionInfo.ClientID = customer.Key;
                _T_loggingSessionInfo.CurrentLoggingManager.Connection_String = customer.Value;

                //
                _AppSysService = new JIT.CPOS.BS.DataAccess.AppSysService(_T_loggingSessionInfo);
                _T_RoleBLL = new T_RoleBLL(_T_loggingSessionInfo);

                //
                var roleList = new List<string>();
                try
                {
                    var roleEntities = _T_RoleBLL.QueryByEntity(new T_RoleEntity
                    {
                        customer_id = customer.Key
                    }, null);
                    if (roleEntities == null || roleEntities.Count() <= 0)
                    {
                        continue;
                    }

                    //
                    roleList = roleEntities.Select(it => it.role_id).ToList();
                }
                catch
                {
                    continue;
                }
                foreach (var roleID in roleList)
                {
                    var menuList = _AppSysService.GetRoleMenus(customer.Key, roleID);
                    if (menuList == null && menuList.Count <= 0)
                    {
                        continue;
                    }
                    new RedisRoleBLL().SetRole(customer.Key, roleID, menuList);
                }
            }
        }
    }
}
