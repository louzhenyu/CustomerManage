using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Dealer.Request;
using JIT.CPOS.DTO.Module.VIP.Dealer.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Dealer
{
    /// <summary>
    /// 获取当前会员粉丝列表
    /// </summary>
    public class GetVipFansListAH : BaseActionHandler<GetVipFansListRP, GetVipFansListRD>
    {
        protected override GetVipFansListRD ProcessRequest(DTO.Base.APIRequest<GetVipFansListRP> pRequest)
        {
            var rd = new GetVipFansListRD();
            var VipBLL = new VipBLL(CurrentUserInfo);

            rd.VipFansList = VipBLL.GetVipFansList(pRequest.UserID, pRequest.Parameters.Code,pRequest.Parameters.VipName);

            return rd;
        }
    }
}