using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Register
{
    public class MergeVipInfoAH : BaseActionHandler<MergeVipInfoRP, MergeVipInfoRD>
    {

        #region 错误码
        const int ERROR_AUTHCODE_NOTEXISTS = 330;
        const int ERROR_AUTHCODE_INVALID = 331;
        const int ERROR_AUTHCODE_NOT_EQUALS = 333;
        const int ERROR_AUTHCODE_WAS_USED = 332;
        const int ERROR_LACK_MOBILE = 312;
        const int ERROR_LACK_AUTHCODE = 315;
        const int ERROR_AUTO_MERGE_MEMBER_FAILED = 313;
        #endregion

        protected override MergeVipInfoRD ProcessRequest(DTO.Base.APIRequest<MergeVipInfoRP> pRequest)
        {
            //参数验证
            if (string.IsNullOrWhiteSpace(pRequest.Parameters.AuthCode))
            {
                throw new APIException("请求参数中缺少AuthCode或值为空.") { ErrorCode = ERROR_LACK_AUTHCODE };
            }
            if (string.IsNullOrWhiteSpace(pRequest.Parameters.Mobile))
            {
                throw new APIException("请求参数中缺少Mobile或值为空.") { ErrorCode = ERROR_LACK_MOBILE };
            }
            //
            MergeVipInfoRD rd = new MergeVipInfoRD();
            var bll = new VipBLL(this.CurrentUserInfo);
            var codebll = new RegisterValidationCodeBLL(base.CurrentUserInfo);

            #region 验证验证码
            var entity = codebll.GetByMobile(pRequest.Parameters.Mobile);
            if (entity == null)
                throw new APIException("未找到此手机的验证信息") { ErrorCode = ERROR_AUTHCODE_NOTEXISTS };
            if (entity.IsValidated.Value == 1)
                throw new APIException("此验证码已被使用") { ErrorCode = ERROR_AUTHCODE_WAS_USED };
            if (entity.Expires.Value < DateTime.Now)
                throw new APIException("此验证码已失效") { ErrorCode = ERROR_AUTHCODE_INVALID };
            if (entity.Code != pRequest.Parameters.AuthCode)
                throw new APIException("验证码不正确.") { ErrorCode = ERROR_AUTHCODE_NOT_EQUALS };
            #endregion

            JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo() { Message = string.Format("action={0},request={1}", "MergeVipInfo", pRequest.ToJSON()) });
            if (!bll.MergeVipInfo(pRequest.CustomerID, pRequest.UserID, pRequest.Parameters.Mobile))
            {
                throw new APIException("合并会员信息失败") { ErrorCode = ERROR_AUTO_MERGE_MEMBER_FAILED };
            }
            return rd;
        }
    }
}