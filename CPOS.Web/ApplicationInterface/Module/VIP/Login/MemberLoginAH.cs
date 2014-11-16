using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL.Vip;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Login
{
    public class MemberLoginAH : BaseActionHandler<MemberLoginRP, AuthCodeLoginRD>
    {
        #region 错误码

        const int ERROR_VIPID_NOTEXISTS = 331;  //会员账号不存在
        const int ERROR_PASSWORD_NOEXISTS = 332; //登录密码错误
        #endregion
        protected override AuthCodeLoginRD ProcessRequest(APIRequest<MemberLoginRP> pRequest)
        {
            AuthCodeLoginRD rd = new AuthCodeLoginRD();
            rd.MemberInfo = new MemberInfo();
            var vipLoginBLL = new VipBLL(base.CurrentUserInfo);
            string mobile = ""; ;
            var VipLoginInfo = vipLoginBLL.GetLoginInfo(pRequest.Parameters.VipNo, mobile, pRequest.Parameters.Password);
            if (VipLoginInfo == null)
                throw new APIException("会员账号不存在") { ErrorCode = ERROR_VIPID_NOTEXISTS };
            if (pRequest.Parameters.Password != VipLoginInfo.VipPasswrod)
                throw new APIException("登录密码错误") { ErrorCode = ERROR_PASSWORD_NOEXISTS };

            #region VIP来源更新
            switch (pRequest.Parameters.VipSource.Value)
            {
                case 4:
                case 9:
                    VipLoginInfo.VipSourceId = pRequest.Parameters.VipSource.ToString();
                    vipLoginBLL.Update(VipLoginInfo,null);
                    break;
            }
            #endregion

            if (string.IsNullOrEmpty(VipLoginInfo.ClientID))
            {
                VipLoginInfo.ClientID = pRequest.CustomerID;
                vipLoginBLL.Update(VipLoginInfo,null);
            }
            rd.MemberInfo.Mobile = VipLoginInfo.Phone;//手机号码
            rd.MemberInfo.Name = string.IsNullOrEmpty(VipLoginInfo.UserName) ? VipLoginInfo.Phone : VipLoginInfo.UserName; //姓名
            rd.MemberInfo.VipID = VipLoginInfo.VIPID;  //组标识
            rd.MemberInfo.VipName = VipLoginInfo.VipName; //会员名
            rd.MemberInfo.VipRealName = VipLoginInfo.VipRealName;
            rd.MemberInfo.VipNo = VipLoginInfo.VipCode;
            return rd;
        }
    }
}