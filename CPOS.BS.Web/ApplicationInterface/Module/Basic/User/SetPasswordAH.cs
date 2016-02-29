using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Basic.User.Request;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility;
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.User
{
    public class SetPasswordAH : BaseActionHandler<SetPasswordRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetPasswordRP> pRequest)
        {

                //基础数据初始化
            string error = "";
            EmptyResponseData emptyResponseData = new EmptyResponseData();
            try
            {
                if (pRequest.Parameters.NewPassword.Length < 6)
                {
                    throw new APIException("新密码不小于6位。") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }


                string newPassword = MD5Helper.Encryption(pRequest.Parameters.NewPassword);
                string oldPassword = EncryptManager.Hash(pRequest.Parameters.OldPassword, HashProviderType.MD5);

                //组装参数
                UserInfo entity = new UserInfo();
                var serviceBll = new cUserService(CurrentUserInfo);
                entity = serviceBll.GetUserById(CurrentUserInfo, CurrentUserInfo.UserID);
                string apPassword = serviceBll.GetPasswordFromAP(CurrentUserInfo.ClientID, CurrentUserInfo.UserID);

                if (oldPassword == apPassword)
                {
                    entity.userRoleInfoList = new cUserService(CurrentUserInfo).GetUserRoles(CurrentUserInfo.UserID, PageBase.JITPage.GetApplicationId());
                    entity.User_Password = newPassword;
                    entity.ModifyPassword = true;
                    //new cUserService(CurrentUserInfo).SetUserInfo(entity, entity.userRoleInfoList, out error);
                    bool bReturn = serviceBll.SetUserPwd(CurrentUserInfo, newPassword, out error);
                    if (!bReturn) 
                    { 
                        throw new APIException(error) { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }
                }
                else
                {
                    throw new APIException("旧密码不正确") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
                return emptyResponseData;
            }
            catch(APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }
    }
}