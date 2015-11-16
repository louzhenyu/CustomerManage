using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.SysVipCardType.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.SysVipCardType
{
    public class DelSysVipCardTypeAH : BaseActionHandler<DelSysVipCardTypeRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<DelSysVipCardTypeRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            var vipCardBLL = new VipCardBLL(loggingSessionInfo);

            var vipCardTypeInfo = sysVipCardTypeBLL.GetByID(para.VipCardTypeID);
            if (vipCardTypeInfo != null)
            {
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardTypeID = para.VipCardTypeID }, null).FirstOrDefault();
                if (vipCardInfo == null)
                    sysVipCardTypeBLL.Delete(para.VipCardTypeID, null);
                else
                    throw new APIException("卡类型正在使用不可以删除！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
            return rd;
        }
    }
}