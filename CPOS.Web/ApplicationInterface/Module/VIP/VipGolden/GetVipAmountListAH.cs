using JIT.CPOS.DTO.Module;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{

    /// <summary>
    /// 微信/App端 会员余额 列表
    /// </summary>
    public class GetVipAmountListAH : BaseActionHandler<GetVipAmountListRP, GetVipAmountListRD>
    {
        protected override GetVipAmountListRD ProcessRequest(DTO.Base.APIRequest<GetVipAmountListRP> pRequest)
        {
            var rd = new GetVipAmountListRD();

            return rd;
        }
    }
}