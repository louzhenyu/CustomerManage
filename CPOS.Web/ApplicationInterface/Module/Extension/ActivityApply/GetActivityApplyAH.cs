using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Extension.ActivityApply.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.ActivityApply
{
    public class GetActivityApplyAH : BaseActionHandler<EmptyRequestParameter, GetActivityApplyRD>
    {
        protected override GetActivityApplyRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetActivityApplyRD();
            var para = pRequest.Parameters;
            var vipActivityApplyBLL = new X_VipActivityApplyBLL(CurrentUserInfo);
            var vipActivityApply = vipActivityApplyBLL.QueryByEntity(new X_VipActivityApplyEntity() {VipID=CurrentUserInfo.UserID}, null).FirstOrDefault();
            int isApply = 0;//是否报名
            if (vipActivityApply != null)
            {
                isApply =1 ;
            }
            rd.IsApply = isApply;

            return rd;
        }
    }
}