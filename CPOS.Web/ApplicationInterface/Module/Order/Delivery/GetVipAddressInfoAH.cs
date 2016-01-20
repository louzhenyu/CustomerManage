using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.Order.Delivery.Request;
using JIT.CPOS.DTO.Module.Order.Delivery.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Delivery
{
    public class GetVipAddressInfoAH : BaseActionHandler<GetVipAddressInfoRP, GetVipAddressInfoRD>
    {
        protected override GetVipAddressInfoRD ProcessRequest(DTO.Base.APIRequest<GetVipAddressInfoRP> pRequest)
        {
            var rd = new GetVipAddressInfoRD();
            var para = pRequest.Parameters;
            VipAddressBLL service = new VipAddressBLL(CurrentUserInfo);

            if (!string.IsNullOrWhiteSpace(para.VipAddressID))
            {
                rd.VipAddress = service.GetByID(para.VipAddressID);
            }
            return rd;
        }
    }
}