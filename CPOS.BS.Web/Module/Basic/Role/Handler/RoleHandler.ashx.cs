using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.Web.Module.Basic.Role.Handler
{
    /// <summary>
    /// RoleHandler
    /// </summary>
    public class RoleHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "search_role":
                    content = QueryRoleListData();
                    break;
                case "get_sys_menus_by_role_id":
                    content = GetSysMenuListByRoleIdData();
                    break;
                case "get_role_by_id":
                    content = GetRoleInfoByIdData();
                    break;
                case "role_save":
                    content = SaveRoleData();
                    break;
                case "role_delete":
                    content = DeleteData();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region QueryRoleListData
        /// <summary>
        /// 查询角色列表
        /// </summary>
        public string QueryRoleListData()
        {
            var responseData = new ResponseData();
            LoggingSessionInfo loggingSessionInfo = null;
            if (CurrentUserInfo != null)
            {
                loggingSessionInfo = CurrentUserInfo;
            }
            else
            {
                if (string.IsNullOrEmpty(Request("CustomerID")))
                {
                    responseData.success = false;
                    responseData.msg = "缺少商户标识";
                    return responseData.ToString();
                }
                else if (string.IsNullOrEmpty(Request("CustomerUserID")))
                {
                    responseData.success = false;
                    responseData.msg = "缺少登陆员工的标识";
                    return responseData.ToString();
                }
                else if (string.IsNullOrEmpty(Request("CustomerUserID")))
                {
                    responseData.success = false;
                    responseData.msg = "缺少登陆员工的标识";
                    return responseData.ToString();
                }
                else
                {
                    loggingSessionInfo = Default.GetBSLoggingSession(Request("CustomerID"), Request("CustomerUserID"));
                }
            }

            var form = Request("form").DeserializeJSONTo<RoleQueryEntity>();

            var appSysService = new AppSysService(loggingSessionInfo);//使用兼容模式
            RoleModel list = new RoleModel();

            string content = string.Empty;
            string key = string.Empty;
            if (form.app_sys_id != null && form.app_sys_id != string.Empty)
            {
                key = form.app_sys_id.Trim();
            }
            int maxRowCount = PageSize;//每页数量
            int limit = Utils.GetIntVal(Request("limit"));//传过来的参数
            if (limit != 0)
            {
                maxRowCount = PageSize = limit;
            }


            int page = Utils.GetIntVal(Request("page"));//第几页面
            if (page == 0) { page = 1; }
            int startRowIndex = (page - 1) * PageSize + 1;//因为row_number()从1开始


            list = appSysService.GetRolesByAppSysId(key, maxRowCount, startRowIndex
                , form.type_id ?? "", form.role_name ?? "", loggingSessionInfo.UserID);
            //在为用户配置门店角色关系时
            //多加一个参数，在这里选择门店，必须重新加载角色列表，因为创建用户角色门店关系时，角色必须和门店同一个type_level上
            if (!string.IsNullOrEmpty(form.unit_id))
            {
                t_unitBLL t_unitBll = new t_unitBLL(CurrentUserInfo);
                t_unitEntity t_unitEn = t_unitBll.GetByID(form.unit_id);

                if (t_unitEn != null)
                {
                    T_TypeBLL T_TypeBLL = new T_TypeBLL(CurrentUserInfo);
                    T_TypeEntity t_typeEn = T_TypeBLL.GetByID(t_unitEn.type_id);
                    list.RoleInfoList = list.RoleInfoList.Where(p => p.org_level == t_typeEn.type_Level).ToList();
                }

            }

            var jsonData = new JsonData();
            jsonData.totalCount = list.RoleInfoList.Count.ToString();
            jsonData.data = list.RoleInfoList;

            content = string.Format("{{\"totalCount\":{1},\"TotalPage\":{2},\"topics\":{0}}}",
                list.RoleInfoList.ToJSON(),
                list.ICount,
                list.TotalPage);
            return content;
        }
        #endregion

        #region GetSysMenuListByRoleIdData
        /// <summary>
        /// 通过RoleID获取菜单列表
        /// </summary>
        public string GetSysMenuListByRoleIdData()
        {
            var appSysService = new AppSysService(CurrentUserInfo);
            IList<MenuModel> data = new List<MenuModel>();
            string content = string.Empty;

            IList<MenuModel> src = new List<MenuModel>();

            string appSysId = "";
            string key = string.Empty;
            if (Request("role_id") != null && Request("role_id") != string.Empty)
            {
                key = Request("role_id").ToString().Trim();
            }

            if (Request("app_sys_id") != null && Request("app_sys_id") != string.Empty)
            {
                appSysId = Request("app_sys_id").ToString().Trim();
            }

            src = appSysService.GetAllMenusByAppSysId(appSysId);//获取所有菜单

            IList<MenuModel> roleMenuList = new List<MenuModel>();
            roleMenuList = appSysService.GetRoleMenus(CurrentUserInfo, key);//获取当前用户的的菜单

            foreach (var tmpSrcMenuObj in src)   //遍历所有菜单
            {
                foreach (var tmpRoleMenuObj in roleMenuList) //当前用户选中的角色
                {
                    if (tmpRoleMenuObj.Menu_Id == tmpSrcMenuObj.Menu_Id)
                    {
                        tmpSrcMenuObj.check_flag = "true";
                        break;
                    }
                    else
                    {
                        tmpSrcMenuObj.check_flag = "false";
                    }
                }
            }

            data = src.Where(c => c.Menu_Level == 1).ToList();//取第一层的
            foreach (var tmpMenuObj in data)//遍历第一层数据
            {
                tmpMenuObj.leaf_flag = tmpMenuObj.Menu_Level == 1 ? "false" : "true";
                tmpMenuObj.expanded_flag = tmpMenuObj.Menu_Level == 1 ? "true" : "false";
                //tmpMenuObj.cls_flag = tmpMenuObj.Menu_Level == 1 ? "folder" : "";

                foreach (var tmpSrcMenuObj in src)//找下面的字节点
                {
                    if (tmpSrcMenuObj.Parent_Menu_Id == tmpMenuObj.Menu_Id)
                    {
                        if (tmpMenuObj.children == null)
                            tmpMenuObj.children = new List<MenuModel>();
                        tmpSrcMenuObj.leaf_flag = "true";
                        tmpSrcMenuObj.cls_flag = "";
                        tmpMenuObj.children.Add(tmpSrcMenuObj);
                        GetSubMenus(tmpSrcMenuObj, src);
                    }
                }
            }







            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;


            content = jsonData.ToJSON();
            return content;
        }
        public void GetSubMenus(MenuModel menu, IList<MenuModel> menulist)
        {
            menu.leaf_flag =  "true";//默认写为true
            menu.expanded_flag =  "false";//不展开
            if (menulist != null && menulist.Count > 0)
            {
                menu.children = new List<MenuModel>();
                
                foreach (MenuModel subMenu in menulist)//遍历所有的菜单项
                {
                    if (subMenu.Parent_Menu_Id == menu.Menu_Id)
                    {
                        menu.leaf_flag = "false";//默认写为true
                        menu.children.Add(subMenu);//children和SubMenu是两组人写的
                        GetSubMenus(subMenu, menulist);//递归查找子元素的节点
                    }
                }
            }
        }
        #endregion

        #region GetRoleInfoByIdData
        /// <summary>
        /// 通过ID获取角色信息
        /// </summary>
        public string GetRoleInfoByIdData()
        {
            var service = new AppSysService(CurrentUserInfo);
            RoleModel data;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("role_id") != null && Request("role_id") != string.Empty)
            {
                key = Request("role_id").ToString().Trim();
            }

            data = service.GetRoleById(CurrentUserInfo, key);

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveRoleData
        /// <summary>
        /// 保存角色
        /// </summary>
        public string SaveRoleData()
        {
            var roleService = new RoleService(CurrentUserInfo);
            RoleModel obj = new RoleModel();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string role_id = string.Empty;
            if (Request("role") != null && Request("role") != string.Empty)
            {
                key = Request("role").ToString().Trim();
            }
            if (Request("role_id") != null && Request("role_id") != string.Empty)
            {
                role_id = Request("role_id").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<RoleModel>();

            //obj.Def_App_Id = "";
            //if (Request["app_sys_id"] != null && Request["app_sys_id"] != string.Empty)
            //{
            //    obj.Def_App_Id = Request["app_sys_id"].ToString().Trim();
            //}

            if (role_id.Trim().Length == 0 || role_id == "null" || role_id == "undefined")
            {
                obj.Role_Id = Utils.NewGuid();
            }
            else
            {
                obj.Role_Id = role_id;
            }


            if (obj.type_id == null || obj.type_id.Trim().Length == 0)
            {
                //responseData.success = false;
                //responseData.msg = "所属组织层级不能为空";
                //return responseData.ToJSON();
                obj.type_id = "";
                obj.org_level = 99;
            }
            else
            {
                T_TypeBLL typeBll = new T_TypeBLL(CurrentUserInfo);
                T_TypeEntity en = typeBll.GetByID(obj.type_id);
                if (en != null)
                {
                    obj.org_level = (int)en.type_Level;
                }
            }

            if (obj.Def_App_Id == null || obj.Def_App_Id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "应用系统不能为空";
                return responseData.ToJSON();
            }
         
            if (obj.Role_Name == null || obj.Role_Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "角色名称不能为空";
                return responseData.ToJSON();
            }

            //根据role_id 获取角色信息,系统保留角色的编码不允许修改，主要是admin、administrator等默认角色
            RoleModel roleOld = new AppSysService(CurrentUserInfo).GetRoleById(CurrentUserInfo, role_id);            
            if (roleOld != null && roleOld.Is_Sys == 1)
            {
                //throw (new System.Exception("不能删除系统保留的角色"));
                obj.Role_Code = roleOld.Role_Code;//还用原来的，不用role_name
                obj.Is_Sys = 1;
            }
            //if (obj.Is_Sys == null)
            //{
            //    responseData.success = false;
            //    responseData.msg = "是否系统保留不能为空";
            //    return responseData.ToJSON();
            //}
            if (obj.Role_Code == null || obj.Role_Code.Trim().Length == 0)
            {
                //responseData.success = false;
                //responseData.msg = "角色编码不能为空";
                //return responseData.ToJSON();
                obj.Role_Code = obj.Role_Name;
            }
          

            if (obj.RoleMenuInfoList != null)
            {
                foreach (var tmpRoleMenuObj in obj.RoleMenuInfoList)
                {
                    tmpRoleMenuObj.Role_Id = obj.Role_Id;
                }
            }

            obj.Create_Time = Utils.GetNow();
            obj.Create_User_Id = CurrentUserInfo.CurrentUser.User_Id;
            obj.Modify_Time = Utils.GetNow();
            obj.Modify_User_id = CurrentUserInfo.CurrentUser.User_Id;
            string strError = "";
            roleService.SetRoleInfo(obj, out strError);

            if (strError != "" && strError != "成功")
            {
                responseData.success = false;
                responseData.msg = strError;
                return responseData.ToJSON();
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 删除
        public string DeleteData()
        {
            var service = new RoleService(CurrentUserInfo);
            string content = string.Empty;
            string error = "删除成功";
            var responseData = new ResponseData();
            string key = string.Empty;
            try
            {
                if (Request("ids") != null && Request("ids") != string.Empty)
                {
                    key = Request("ids").ToString().Trim();
                }
                if (key == null || key.Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "请选择角色";
                    return responseData.ToJSON(); ;
                }

                var status = "-1";
                if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
                {
                    status = FormatParamValue(Request("status")).ToString().Trim();
                }

                var idList = key.Split(',');
                foreach (var tmpId in idList)
                {
                    if (tmpId.Trim().Length > 0)
                    {
                        service.DeleteRoleById(tmpId.Trim());
                    }
                }
                responseData.success = true;
                responseData.msg = error;
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message;
            }
            content = responseData.ToJSON();
            return content;
        }
        #endregion
    }

    #region QueryEntity
    public class RoleQueryEntity
    {
        public string app_sys_id;
        public string type_id;
        public string role_name;
        public string unit_id;
    }
    #endregion

}