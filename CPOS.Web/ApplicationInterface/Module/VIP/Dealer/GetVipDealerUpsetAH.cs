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
    /// <summary>
    /// 获取设置会员经销商底价
    /// </summary>
    public class GetVipDealerUpsetAH : BaseActionHandler<GetVipDealerUpsetRP, GetVipDealerUpsetRD>
    {
        protected override GetVipDealerUpsetRD ProcessRequest(DTO.Base.APIRequest<GetVipDealerUpsetRP> pRequest)
        {
            var rd = new GetVipDealerUpsetRD();
            var VipBLL = new VipBLL(CurrentUserInfo);

            rd.Prices = VipBLL.GetSetVipDealerUpset().ToString();

            return rd;
        }
    }
}