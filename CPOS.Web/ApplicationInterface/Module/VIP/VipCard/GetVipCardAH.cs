using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipCard.Response;
using JIT.CPOS.DTO.Module.VIP.VipCard.Resquest;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipCard
{
    public class GetVipCardAH : BaseActionHandler<GetVipCardRP,GetVipCardRD>
    {
        protected override GetVipCardRD ProcessRequest(APIRequest<GetVipCardRP> pRequest)
        {
            VipCardVipMappingBLL vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var para = pRequest.Parameters;
            GetVipCardRD vipCardRD = new GetVipCardRD();

            vipCardRD.VipCardCode = vipCardVipMappingBLL.BindVirtualItem(para.VipID, para.VipCardCode, "", para.ObjectTypeId);

            return vipCardRD;
        
        }

    }
}