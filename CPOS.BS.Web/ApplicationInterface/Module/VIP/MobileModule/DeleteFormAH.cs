using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.MobileModule
{
    public class DeleteFormAH : BaseActionHandler<DeleteFormRP, DeleteFormRD>
    {

        protected override DeleteFormRD ProcessRequest(DTO.Base.APIRequest<DeleteFormRP> pRequest)
        {
            new MobileModuleBLL(CurrentUserInfo).Delete( Guid.Parse(pRequest.Parameters.MobileModuleID), null);
            return new DeleteFormRD{ IsSuccess = true};
        }
    }
}