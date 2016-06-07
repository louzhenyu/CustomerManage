using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.Utility;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public class AppSysService : BaseService
    {
        JIT.CPOS.BS.DataAccess.AppSysService appSysService = null;
        #region 构造函数
        public AppSysService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            appSysService = new DataAccess.AppSysService(loggingSessionInfo);
        }
        #endregion

        #region 角色
        /// <summary>
        /// 根据角色Id获取角色
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public RoleModel GetRoleById(LoggingSessionInfo loggingSession, string roleId)
        {
            RoleModel roleInfo = new RoleModel();
            DataTable dt = appSysService.GetRoleById(roleId);
            if (dt != null && dt.Rows.Count > 0)
            {
                roleInfo = DataTableToObject.ConvertToObject<RoleModel>(dt.Rows[0]);
            }
            return roleInfo;
        }

        #endregion

        public DataSet GetMenuByIds(string[] ids)
        {
            return appSysService.GetMenuByIds(ids);
        }
        #region 菜单
        /// <summary>
        /// 获取某个角色所能操作的菜单列表
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public IList<MenuModel> GetRoleMenus(LoggingSessionInfo loggingSessionInfo, string roleId, int getMode = 0)
        {
            //分隔出角色ID和单位ID
            string[] arr_role = roleId.Split(new char[] { ',' });
            roleId = arr_role[0];

            //DataSet ds = appSysService.GetRoleMenus(roleId);//从Dal层获取数据

            //IList<MenuModel> menulist = new List<MenuModel>();

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //menulist = //DataTableToObject.ConvertToList<MenuModel>(ds.Tables[0]);//菜单列表转换成实体对象
            var menulist = default(List<MenuModel>);
            menulist = new RedisRoleBLL().GetRole(loggingSessionInfo.ClientID, roleId);
            if (menulist == null || menulist.Count <= 0)//从redis没有读取到，从数据库读取
            {
                menulist = appSysService.GetRoleMenus(loggingSessionInfo.ClientID, roleId);
                new RedisRoleBLL().SetRole(loggingSessionInfo.ClientID, roleId, menulist);
                new RedisXML().RedisReadDBCount("RoleID", "角色权限相关", 2);
            }
            else
            {
                new RedisXML().RedisReadDBCount("RoleID", "角色权限相关", 1);
            }

            #region 修改菜单权限Bug
            //修改菜单权限Bug
            //if (getMode != 0)
            //{
            //    var r = (from m in menulist
            //             where !menulist.Select(x => x.Menu_Id).Contains(m.Parent_Menu_Id) && m.Parent_Menu_Id != "--"
            //             select m.Parent_Menu_Id).Distinct().ToArray();
            //    var newMenuDbSet = appSysService.GetMenuByIds(r);
            //    if (newMenuDbSet != null)
            //    {
            //        List<MenuModel> newMenuList = DataTableToObject.ConvertToList<MenuModel>(newMenuDbSet.Tables[0]);
            //        menulist.AddRange(newMenuList);
            //    }
            //}
            if (getMode == 0)
            {
                //for (int i = 0; i < 10000; i++)
                //{
                //    var r = (from m in menulist
                //             where !menulist.Select(x => x.Menu_Id).Contains(m.Parent_Menu_Id) && m.Parent_Menu_Id != "--"
                //             select m.Parent_Menu_Id).Distinct().ToArray();
                //    var newMenuDbSet = appSysService.GetMenuByIds(r);
                //    if (newMenuDbSet != null && newMenuDbSet.Tables[0].Rows.Count > 0)
                //    {
                //        List<MenuModel> newMenuList = DataTableToObject.ConvertToList<MenuModel>(newMenuDbSet.Tables[0]);
                //        menulist.AddRange(newMenuList);
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
                menulist = GetAllMenuList(menulist);
            }

            #endregion
            //var newMenuList2 = new List<MenuModel>();
            //for (int i = 1; i <= 10; i++)
            //{
            //    var l = menulist.Where(x => x.Menu_Level == i).Select(x => x);
            //    if (i == 1)
            //    {
            //        newMenuList2.AddRange(l);
            //    }
            //    else
            //    {
            //        foreach (var m in l)
            //        {
            //            var parentMenu = newMenuList2.Where(x => x.Menu_Id == m.Parent_Menu_Id).ToList().FirstOrDefault();
            //            if (parentMenu != null)
            //            {
            //                if (parentMenu.SubMenuList == null)
            //                    parentMenu.SubMenuList = new List<MenuModel>();
            //                parentMenu.SubMenuList.Add(m);
            //            }
            //        }
            //    }
            //}
            //return newMenuList2;

            if (menulist != null && menulist.Count > 0)
            {
                foreach (MenuModel menu in menulist)
                {
                    menu.SubMenuList = new List<MenuModel>();
                    foreach (MenuModel subMenu in menulist)//遍历所有的菜单项
                    {
                        if (subMenu.Parent_Menu_Id == menu.Menu_Id)
                        {
                            menu.SubMenuList.Add(subMenu);
                        }
                    }
                }
            }
            return menulist;
        }
        /// <summary>
        /// 补全父Menu
        /// </summary>
        /// <param name="menulist"></param>
        /// <returns></returns>
        public List<MenuModel> GetAllMenuList(List<MenuModel> menulist)
        {
            for (int i = 0; i < 20; i++)
            {
                var r = (from m in menulist
                         where !menulist.Select(x => x.Menu_Id).Contains(m.Parent_Menu_Id) && m.Parent_Menu_Id != "--"
                         select m.Parent_Menu_Id).Distinct().ToArray();
                var newMenuDbSet = appSysService.GetMenuByIds(r);
                if (newMenuDbSet != null && newMenuDbSet.Tables[0].Rows.Count > 0)
                {
                    List<MenuModel> newMenuList = DataTableToObject.ConvertToList<MenuModel>(newMenuDbSet.Tables[0]);
                    menulist.AddRange(newMenuList);
                }
                else
                {
                    break;
                }
            }
            return menulist;
        }

        /// <summary>
        /// 获取某个角色所能操作的菜单列表,不递归包含子集
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public IList<MenuModel> GetRoleMenusList(LoggingSessionInfo loggingSessionInfo, string roleId)
        {
            //分隔出角色ID和单位ID
            string[] arr_role = roleId.Split(new char[] { ',' });
            roleId = arr_role[0];

            //DataSet ds = appSysService.GetRoleMenus(roleId);//从Dal层获取数据

            //IList<MenuModel> menulist = new List<MenuModel>();

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //menulist = DataTableToObject.ConvertToList<MenuModel>(ds.Tables[0]);//菜单列表转换成实体对象
            var menulist = default(List<MenuModel>);
            menulist = new RedisRoleBLL().GetRole(loggingSessionInfo.ClientID, roleId);
            if (menulist == null || menulist.Count <= 0)
            {
                menulist = appSysService.GetRoleMenus(loggingSessionInfo.ClientID, roleId);
                menulist = GetAllMenuList(menulist);
                new RedisRoleBLL().SetRole(loggingSessionInfo.ClientID, roleId, menulist);
                new RedisXML().RedisReadDBCount("RoleID", "角色权限相关", 2);
            }
            else
            {
                menulist = GetAllMenuList(menulist);
                new RedisXML().RedisReadDBCount("RoleID", "角色权限相关", 1);
            }
            //if (menulist != null && menulist.Count > 0)
            //{
            //    foreach (MenuModel menu in menulist)
            //    {
            //        menu.SubMenuList = new List<MenuModel>();
            //        foreach (MenuModel subMenu in menulist)//遍历所有的菜单项
            //        {
            //            if (subMenu.Parent_Menu_Id == menu.Menu_Id)
            //            {
            //                menu.SubMenuList.Add(subMenu);
            //            }
            //        }
            //    }
            //}
            //}
            return menulist;
        }

        #endregion 菜单

        #region 根据Url得到MenuId
        /// <summary>
        /// 根据Url得到MenuId xiaowen.qin 2016.5.19
        /// </summary>
        /// <param name="currentUri"></param>
        /// <returns></returns>
        public List<string> GetMenuIds(Uri currentUri)
        {
            //2016.5
            var targetStr = currentUri.PathAndQuery;
            var index = targetStr.IndexOf("mid=") - 1;
            if (index >= 0)
            {
                targetStr = targetStr.Substring(0, index);
            }
            var result = appSysService.GetMenuIds(targetStr);
            return result;
        }
        #endregion

        /// <summary>
        /// 获取某个角色所能操作的菜单列表
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public MenuModel GetRoleMenusByPMenuCode(LoggingSessionInfo loggingSessionInfo, string roleId, string menu_code, out string errMsg)
        {
            //分隔出角色ID和单位ID
            string[] arr_role = roleId.Split(new char[] { ',' });
            roleId = arr_role[0];
            //DataSet ds = appSysService.GetRoleMenus(roleId);//从Dal层获取数据
            //IList<MenuModel> menulist = new List<MenuModel>();
            MenuModel currentMenu = new MenuModel();
            errMsg = "";
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //menulist = DataTableToObject.ConvertToList<MenuModel>(ds.Tables[0]);//菜单列表转换成实体对象

            var menulist = default(List<MenuModel>);
            menulist = new RedisRoleBLL().GetRole(loggingSessionInfo.ClientID, roleId);
            if (menulist == null || menulist.Count <= 0)
            {
                menulist = appSysService.GetRoleMenus(loggingSessionInfo.ClientID, roleId);
                new RedisRoleBLL().SetRole(loggingSessionInfo.ClientID, roleId, menulist);
                new RedisXML().RedisReadDBCount("RoleID", "角色权限相关", 2);
            }
            else
            {
                new RedisXML().RedisReadDBCount("RoleID", "角色权限相关", 1);
            }

            var currentMenuList = menulist.Where(p => p.Menu_Code == menu_code).ToArray();
            if (currentMenuList == null || currentMenuList.Length == 0)
            {
                errMsg = "没有找到对应菜单编码的菜单";
            }
            else
            {
                currentMenu = currentMenuList[0];
                GetSubMenus(currentMenu, menulist);
            }
            //}
            return currentMenu;
        }



        public void GetSubMenus(MenuModel menu, IList<MenuModel> menulist)
        {
            if (menulist != null && menulist.Count > 0)
            {
                menu.SubMenuList = new List<MenuModel>();
                foreach (MenuModel subMenu in menulist)//遍历所有的菜单项
                {
                    if (subMenu.Parent_Menu_Id == menu.Menu_Id)
                    {
                        menu.SubMenuList.Add(subMenu);
                        GetSubMenus(subMenu, menulist);//递归查找子元素的节点
                    }
                }
            }
        }





        /// <summary>
        /// 获取单据号
        /// </summary>
        /// <param name="loggingSessionInfo">model</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public string GetNo(string prefix)
        {

            return appSysService.GetNo(prefix);
        }

        /// <summary>
        /// 获取某个应用系统下的所有菜单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="appCode">应用系统编码</param>
        /// <returns></returns>
        public IList<MenuModel> GetAllMenusByAppSysCode(string appCode)
        {
            IList<MenuModel> menuInfoList = new List<MenuModel>();
            DataSet ds = new DataSet();
            ds = appSysService.GetAllMenusByAppSysCode(appCode);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                menuInfoList = DataTableToObject.ConvertToList<MenuModel>(ds.Tables[0]);
            }
            return menuInfoList;
        }

        public IList<MenuModel> GetAllMenusByAppSysId(string appId)
        {
            IList<MenuModel> menuInfoList = new List<MenuModel>();
            DataSet ds = new DataSet();
            ds = appSysService.GetAllMenusByAppSysId(appId);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                menuInfoList = DataTableToObject.ConvertToList<MenuModel>(ds.Tables[0]);
            }
            return menuInfoList;
        }

        #region 获取所有的应用系统列表
        /// <summary>
        /// 获取所有的应用系统列表
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录用户的Session信息</param>
        /// <returns></returns>
        public IList<AppSysModel> GetAllAppSyses()
        {
            IList<AppSysModel> appSysInfoList = new List<AppSysModel>();
            DataSet ds = appSysService.GetAllAppSyses();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                appSysInfoList = DataTableToObject.ConvertToList<AppSysModel>(ds.Tables[0]);
            }
            return appSysInfoList;
        }

        #endregion

        #region 获取某个应用系统下的所有角色
        /// <summary>
        /// 获取某个应用系统下的所有角色
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="appSysId">应用系统Id</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns></returns>
        public RoleModel GetRolesByAppSysId(string appSysId, int maxRowCount, int startRowIndex, string type_id, string role_name, string UserID)
        {
            RoleModel roleInfo = new RoleModel();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("ApplicationId", appSysId);
            hashTable.Add("StartRow", startRowIndex);
            hashTable.Add("EndRow", startRowIndex + maxRowCount - 1);//结束页
            hashTable.Add("MaxRowCount", maxRowCount);
            hashTable.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            hashTable.Add("UserID", UserID);//用户标识
            hashTable.Add("type_id", type_id ?? "");
            hashTable.Add("role_name", role_name ?? "");
            int iCount = appSysService.SearchRoleByAppSysIdCount(hashTable);//cSqlMapper.Instance().QueryForObject<int>("Role.SelectByApplicationIdCount", hashTable);
            IList<RoleModel> roleInfoList = new List<RoleModel>();
            DataSet ds = new DataSet();
            ds = appSysService.SearchRoleByAppSysIdList(hashTable);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                roleInfoList = DataTableToObject.ConvertToList<RoleModel>(ds.Tables[0]);
            }
            //roleInfoList = cSqlMapper.Instance().QueryForList<RoleModel>("Role.SelectByApplicationId", hashTable);
            roleInfo.ICount = iCount;

            //取模
            int mo = iCount % maxRowCount;
            roleInfo.TotalPage = iCount / maxRowCount + (mo == 0 ? 0 : 1);

            roleInfo.RoleInfoList = roleInfoList;
            return roleInfo;
        }

        #endregion

        #region GetList
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public IList<MenuModel> GetMenuList(MenuModel entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<MenuModel> list = new List<MenuModel>();
            DataSet ds = new DataSet();
            ds = appSysService.GetMenuList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<MenuModel>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetMenuListCount(MenuModel entity)
        {
            return appSysService.GetMenuListCount(entity);
        }
        public void DeleteMenuById(string menuId)
        {
            appSysService.DeleteMenuById(menuId);
        }

        public bool IsCheckMeauLast(string menuId)
        {
            return appSysService.IsCheckMeauLast(menuId);
        }
        public void IsExistMenuCode(string menu_code, string menu_id)
        {
            appSysService.IsExistMenuCode(menu_code, menu_id);
        }
        public void SetMenuInfo(MenuModel menuInfo, bool IsTrans, string strDo, out string strError)
        {
            appSysService.SetMenuInfo(menuInfo, IsTrans, strDo, out strError);
        }
        #endregion

    }
}
