using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Extension.ActivityApply.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.ActivityApply
{
    public class SetActivityApplyAH : BaseActionHandler<SetActivityApplyRP, EmptyResponseData>
    {

        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetActivityApplyRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var vipActivityApplyBLL = new X_VipActivityApplyBLL(CurrentUserInfo);
            var vipActivityApply = new X_VipActivityApplyEntity()
            {
                VipID=CurrentUserInfo.UserID,
                CustomerID=CurrentUserInfo.ClientID,
                Nickname=para.Nickname,
                Territory=para.Territory,
                Age=para.Age,
                Phone=para.Phone,
                LikeTea=para.LikeTea
            };
            vipActivityApplyBLL.Create(vipActivityApply);

            return rd;
        }
    }
}