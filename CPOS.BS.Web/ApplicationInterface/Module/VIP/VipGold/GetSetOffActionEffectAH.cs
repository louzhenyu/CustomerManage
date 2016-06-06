using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    public class GetSetOffActionEffectAH : BaseActionHandler<GetSetOffActionEffectRP, GetSetOffActionEffectRD>
    {
        protected override GetSetOffActionEffectRD ProcessRequest(DTO.Base.APIRequest<GetSetOffActionEffectRP> pRequest)
        {
            var rd = new GetSetOffActionEffectRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            return rd;
        }
    }
}