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
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.BS.Web.Module.Basic.User.Handler
{
    /// <summary>
    /// UserHandler
    /// </summary>
    public class UserHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_user":
                    content = GetUserListData();
                    break;
                case "get_user_by_id":
                    content = GetUserInfoByIdData();
                    break;
                case "get_user_role_info_by_user_id":
                    content = GetUserRoleInfoByUserIdData();
                    break;
                case "user_save":
                    content = SaveUserData();
                    break;
                case "user_delete":
                    content = DeleteData();
                    break;
                case "revertPassword":
                    content = RevertPassword();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetUserListData
        /// <summary>
        /// 查询用户
        /// </summary>
        public string GetUserListData()
        {
            var form = Request("form").DeserializeJSONTo<UserQueryEntity>();

            var userService = new cUserService(CurrentUserInfo);
            UserInfo data;
            string content = string.Empty;

            string user_code = form.user_code == null ? string.Empty : form.user_code;
            string user_name = form.user_name == null ? string.Empty : form.user_name;
            string user_tel = form.user_tel == null ? string.Empty : form.user_tel;
            string user_status = form.user_status == null ? string.Empty : form.user_status;
            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            data = userService.SearchUserListByUnitID(   //SearchUserListByUnitID
                user_code,
                user_name,
                user_tel,
                user_status,
                maxRowCount,
                startRowIndex,
                CurrentUserInfo.CurrentUserRole.UnitId);

            var jsonData = new JsonData();
            jsonData.totalCount = data.ICount.ToString();
            jsonData.data = data.UserInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.UserInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region GetUserInfoByIdData
        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        public string GetUserInfoByIdData()
        {
            var userService = new cUserService(CurrentUserInfo);
            UserInfo data;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("user_id") != null && Request("user_id") != string.Empty)
            {
                key = Request("user_id").ToString().Trim();
            }

            data = userService.GetUserById(CurrentUserInfo, key);
            if (data != null)
            {
                data.userRoleInfoList = userService.GetUserRoles(key);
            }

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetUserRoleInfoByUserIdData
        /// <summary>
        /// 通过ID获取用户角色信息
        /// </summary>
        public string GetUserRoleInfoByUserIdData()
        {
            var userService = new cUserService(CurrentUserInfo);
            UserRoleInfo data = new UserRoleInfo();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("user_id") != null && Request("user_id") != string.Empty)
            {
                key = Request("user_id").ToString().Trim();
            }

            data.UserRoleInfoList = userService.GetUserRoles(key);
            if (data.UserRoleInfoList == null) data.UserRoleInfoList = new List<UserRoleInfo>();

            var jsonData = new JsonData();
            jsonData.totalCount = data.UserRoleInfoList.Count.ToString();
            jsonData.data = data.UserRoleInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.UserRoleInfoList.ToJSON(),
                data.UserRoleInfoList.Count);
            return content;
        }
        #endregion

        #region SaveUserData
        /// <summary>
        /// 保存用户
        /// </summary>
        public string SaveUserData()
        {
            var userService = new cUserService(CurrentUserInfo);
            UserInfo user = new UserInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string user_id = string.Empty;
            if (Request("user") != null && Request("user") != string.Empty)
            {
                key = Request("user").ToString().Trim();
            }
            if (Request("user_id") != null && Request("user_id") != string.Empty)
            {
                user_id = Request("user_id").ToString().Trim();
            }

            user = key.DeserializeJSONTo<UserInfo>();
            if (user.User_Status == null || user.User_Status.Trim().Length == 0)
            {
                user.User_Status = "1";
            }

            if (user_id.Trim().Length == 0)
            {
                user.User_Id = Utils.NewGuid();
                //user.UnitList = loggingSessionInfo.CurrentUserRole.UnitId;
            }
            else
            {
                user.User_Id = user_id;
            }

            if (user.User_Code == null || user.User_Code.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "用户工号不能为空";
                return responseData.ToJSON();
            }
            if (user.User_Name == null || user.User_Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "用户名称不能为空";
                return responseData.ToJSON();
            }
            if (user.User_Password == null || user.User_Password.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "用户密码不能为空";
                return responseData.ToJSON();
            }
            if (user.Fail_Date == null || user.Fail_Date.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "用户有效日期不能为空";
                return responseData.ToJSON();
            }
            if (user.User_Telephone == null || user.User_Telephone.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "用户手机不能为空";
                return responseData.ToJSON();
            }
            if (user.User_Email == null || user.User_Email.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "用户邮箱不能为空";
                return responseData.ToJSON();
            }
            if (user.userRoleInfoList == null || user.userRoleInfoList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "用户职务不能为空";
                return responseData.ToJSON();
            }

            foreach (var userRoleItem in user.userRoleInfoList)
            {
                userRoleItem.UserId = user.User_Id;
            }

            user.Create_Time = Utils.GetNow();
            user.Create_User_Id = CurrentUserInfo.CurrentUser.User_Id;
            user.Modify_Time = Utils.GetNow();
            user.Modify_User_Id = CurrentUserInfo.CurrentUser.User_Id;

            userService.SetUserInfo(user, user.userRoleInfoList, out error);

            responseData.success = true;
            responseData.msg = error;


            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region DeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string DeleteData()
        {
            var service = new cUserService(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "ID不能为空";
                return responseData.ToJSON();
            }

            var status = "-1";
            if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
            {
                status = FormatParamValue(Request("status")).ToString().Trim();
            }

            string[] ids = key.Split(',');
            foreach (var id in ids)
            {
                service.SetUserStatus(key, status, CurrentUserInfo);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region RevertPassword
        public string RevertPassword()
        {
            var responseData = new ResponseData();
            try
            {
                UserInfo user = new UserInfo();
                var userService = new cUserService(CurrentUserInfo);
                bool bl = userService.ModifyUserPassword(CurrentUserInfo, Request("user").ToString(), Request("password").ToString());
                responseData.success = true;
            }
            catch (Exception)
            {
                responseData.success = false;
                responseData.msg = "密码重置失败";
            }
            return responseData.ToJSON();
        }
        #endregion
    }

    #region QueryEntity
    public class UserQueryEntity
    {
        public string user_code;
        public string user_name;
        public string user_tel;
        public string user_status;
    }
    #endregion

}