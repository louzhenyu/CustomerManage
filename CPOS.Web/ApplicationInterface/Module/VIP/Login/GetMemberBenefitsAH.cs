using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Login
{
    public class GetMemberBenefitsAH : BaseActionHandler<EmptyRequestParameter, GetMemberBenefitsRD>
    {
        protected override GetMemberBenefitsRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            GetMemberBenefitsRD rd = new GetMemberBenefitsRD();
            var customerBasicSettingBll = new CustomerBasicSettingBLL(CurrentUserInfo);
            rd.MemberBenefits = customerBasicSettingBll.GetMemberBenefits(pRequest.CustomerID);
            return rd;
        }
    }
}