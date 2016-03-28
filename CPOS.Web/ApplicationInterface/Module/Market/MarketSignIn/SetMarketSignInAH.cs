using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Market.MarketSignIn.Requset;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Market.MarketSignIn
{
    public class SetMarketSignInAH : BaseActionHandler<SetMarketSignInRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetMarketSignInRP> pRequest)
        {
            var rp = pRequest.Parameters;
            EmptyResponseData rd = new EmptyResponseData();
            MarketSignInBLL marketSignInbll = new MarketSignInBLL(CurrentUserInfo);
            MarketNamedApplyBLL marketNamedApplyBll = new MarketNamedApplyBLL(CurrentUserInfo);
            var marketNamedApplyEntityArray = marketNamedApplyBll.QueryByEntity(new MarketNamedApplyEntity() { MarketEventID = rp.MarketEventID, VipId = rp.VipID }, null);
            if (marketNamedApplyEntityArray.Length != 0)
            {
                var marketSignInEntityArray = marketSignInbll.QueryByEntity(new MarketSignInEntity() { EventID = rp.MarketEventID, VipID = rp.VipID }, null);
                if (marketSignInEntityArray.Length == 0)
                {
                    MarketSignInEntity marketSignInEntity = new MarketSignInEntity()
                    {
                        SignInID = Guid.NewGuid().ToString(),
                        OpenID = "",
                        EventID = rp.MarketEventID,
                        UserId = pRequest.UserID,
                        VipID = rp.VipID
                    };
                    marketSignInbll.Create(marketSignInEntity);
                }
                else
                {
                    throw new APIException("该会员已签到") { ErrorCode = 340 };
                }
            }
            else
            {
                throw new APIException("该会员没有申请该活动") { ErrorCode = 340 };
            }
            return rd;
        }
    }
}