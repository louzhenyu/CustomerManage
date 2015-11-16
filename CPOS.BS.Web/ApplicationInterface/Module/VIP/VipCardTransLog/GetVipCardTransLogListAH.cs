using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VipCardTransLog.Request;
using JIT.CPOS.DTO.Module.VIP.VipCardTransLog.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipCardTransLog
{
    public class GetVipCardTransLogListAH : BaseActionHandler<GetVipCardTransLogListRP, GetVipCardTransLogListRD>
    {
        protected override GetVipCardTransLogListRD ProcessRequest(DTO.Base.APIRequest<GetVipCardTransLogListRP> pRequest)
        {
            var rd = new GetVipCardTransLogListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipCardTransLogBLL = new VipCardTransLogBLL(loggingSessionInfo);

            var vipCardTransLogList = vipCardTransLogBLL.GetVipCardTransLogList(para.VipCardCode);
            rd.VipCardTransLogList = vipCardTransLogList;

            return rd;
        }
    }
}