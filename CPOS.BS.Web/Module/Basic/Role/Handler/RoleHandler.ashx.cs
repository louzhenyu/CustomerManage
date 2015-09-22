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
            var form = Request("form").DeserializeJSONTo<RoleQueryEntity>();

            var appSysService = new AppSysService(CurrentUserInfo);
            RoleModel list = new RoleModel();

            string content = string.Empty;
            string key = string.Empty;
            if (form.app_sys_id != null && form.app_sys_id != string.Empty)
            {
                key = form.app_sys_id.Trim();
            }
            list = appSysService.GetRolesByAppSysId(key, 1000, 0);

            var jsonData = new JsonData();
            jsonData.totalCount = list.RoleInfoList.Count.ToString();
            jsonData.data = list.RoleInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                list.RoleInfoList.ToJSON(),
                list.RoleInfoList.Count);
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

            src = appSysService.GetAllMenusByAppSysId(appSysId);

            IList<MenuModel> roleMenuList = new List<MenuModel>();
            roleMenuList = appSysService.GetRoleMenus(CurrentUserInfo, key);

            foreach (var tmpSrcMenuObj in src)
            {
                foreach (var tmpRoleMenuObj in roleMenuList)
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

            data = src.Where(c => c.Menu_Level == 1).ToList();
            foreach (var tmpMenuObj in data)
            {
                tmpMenuObj.leaf_flag = tmpMenuObj.Menu_Level == 1 ? "false" : "true";
                tmpMenuObj.expanded_flag = tmpMenuObj.Menu_Level == 1 ? "true" : "false";
                //tmpMenuObj.cls_flag = tmpMenuObj.Menu_Level == 1 ? "folder" : "";

                foreach (var tmpSrcMenuObj in src)
                {
                    if (tmpSrcMenuObj.Parent_Menu_Id == tmpMenuObj.Menu_Id)
                    {
                        if (tmpMenuObj.children == null)
                            tmpMenuObj.children = new List<MenuModel>();
                        tmpSrcMenuObj.leaf_flag = "true";
                        tmpSrcMenuObj.cls_flag = "";
                        tmpMenuObj.children.Add(tmpSrcMenuObj);
                    }
                }
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
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

            if (obj.Def_App_Id == null || obj.Def_App_Id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "应用系统不能为空";
                return responseData.ToJSON();
            }
            if (obj.Role_Code == null || obj.Role_Code.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "角色编码不能为空";
                return responseData.ToJSON();
            }
            if (obj.Role_Name == null || obj.Role_Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "角色名称不能为空";
                return responseData.ToJSON();
            }
            //if (obj.Is_Sys == null)
            //{
            //    responseData.success = false;
            //    responseData.msg = "是否系统保留不能为空";
            //    return responseData.ToJSON();
            //}

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
            string strError="";
            roleService.SetRoleInfo(obj,out strError);

            if(strError!=""){
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
    }
    #endregion

}