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

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Login
{


    public class ChangeVipPwdAH : BaseActionHandler<ChangeVipPWDRP, ChangeVipPWDRD>
    {

        #region 错误码
        const int ERROR_LOGGIN_NOUSER = 320;
        const int ERROR_LOGGIN_PWD = 321;
        #endregion

        protected override ChangeVipPWDRD ProcessRequest(DTO.Base.APIRequest<ChangeVipPWDRP> pRequest)
        {
            ChangeVipPWDRD rd = new ChangeVipPWDRD();

            pRequest.Parameters.Validate();

            #region 验证用户
            var bll = new VipBLL(base.CurrentUserInfo);
            var entity = bll.GetByID(pRequest.Parameters.VipID);
            if (entity != null)
            {
                if (entity.VipPasswrod != pRequest.Parameters.SourcePWD)
                {
                    throw new APIException("原密码错误") { ErrorCode = ERROR_LOGGIN_PWD };
                }
                else
                {
                    entity.VipPasswrod = pRequest.Parameters.NewPWD;
                    bll.Update(entity);
                }
            }
            else
            {
                throw new APIException("用户不存在") { ErrorCode = ERROR_LOGGIN_NOUSER };
            }
            #endregion

            return rd;
        }
    }
}