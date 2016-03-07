using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.VIP.Dealer.Request;
using JIT.CPOS.DTO.Module.VIP.Dealer.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Dealer
{
    public class GetVipDealerAccountDetailAH : BaseActionHandler<GetVipDealerAccountDetailRP, GetVipDealerAccountDetailRD>
    {
        /// <summary>
        /// 获取会员经销商账户明细
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetVipDealerAccountDetailRD ProcessRequest(DTO.Base.APIRequest<GetVipDealerAccountDetailRP> pRequest)
        {
            var rd = new GetVipDealerAccountDetailRD();
            var VipBLL = new VipBLL(CurrentUserInfo);
            rd = VipBLL.GetVipDealerAccountDetail(pRequest.UserID);
            return rd;
        }
    }
}