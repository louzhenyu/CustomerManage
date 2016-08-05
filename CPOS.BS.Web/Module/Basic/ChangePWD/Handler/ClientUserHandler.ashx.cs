using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.Extension;
using System.Collections.Specialized;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using System.Web.Script.Serialization;
using System.Data;
using JIT.Utility;
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.BS.Web.Module.Basic.ChangePWD.Handler
{
    /// <summary>
    /// ClientUserHandler 的摘要说明
    /// </summary>
    public class ClientUserHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (pContext.Request.QueryString["btncode"])
            {
                case "search":
                    res = GetClientUserInfo();
                    break;
                case "setpwd":
                    res = SetPassWord(pContext.Request.Form["id"], pContext.Request.Form["OldPwd"], pContext.Request.Form["UserPWD"]);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        private string SetPassWord(string pID,string pOldPWD,string pNewPWD)
        {
            string error = "";
            string pNewPass = MD5Helper.Encryption(pNewPWD);
            //pOldPWD = MD5Helper.Encryption(pOldPWD);
            pOldPWD = EncryptManager.Hash(pOldPWD, HashProviderType.MD5);
            string res = "{success:false,msg:'保存失败'}";
            //组装参数
            UserInfo entity = new UserInfo();
            var serviceBll = new cUserService(CurrentUserInfo);
            entity = serviceBll.GetUserById(CurrentUserInfo, pID);
            string apPwd = serviceBll.GetPasswordFromAP(CurrentUserInfo.ClientID, pID);
            //if (pOldPWD == entity.User_Password)
            if(pOldPWD == apPwd)
            {
                entity.userRoleInfoList = new cUserService(CurrentUserInfo).GetUserRoles(pID, PageBase.JITPage.GetApplicationId());
                entity.User_Password = pNewPass;
                entity.ModifyPassword = true;
                //new cUserService(CurrentUserInfo).SetUserInfo(entity, entity.userRoleInfoList, out error);
                bool bReturn = serviceBll.SetUserPwd(CurrentUserInfo, pNewPass, out error);
                res = "{success:true,msg:'" + error + "'}";
            }
            else
            {
                res = "{success:false,msg:'旧密码不正确'}";
            }
            return res;
        }

        #region GetClientUserInfo
        /// <summary>
        /// 根据UserID获取用户密码信息
        /// </summary>
        /// <returns></returns>
        private string GetClientUserInfo()
        {
            UserInfo entity = new UserInfo();
            var pUserID = CurrentUserInfo.UserID;
            return "[" + new cUserService(CurrentUserInfo).GetUserById(CurrentUserInfo, pUserID).ToJSON() + "]";
        }
        #endregion                
    }
}