using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.Event.Request;
using JIT.CPOS.DTO.Module.WeiXin.Event.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Event
{
    public class GetRecommendAH : BaseActionHandler<GetRecommendRP, GetRecommendRD>
    {
        protected override GetRecommendRD ProcessRequest(DTO.Base.APIRequest<GetRecommendRP> pRequest)
        {
            var rd = new GetRecommendRD();

            var bll = new LEventsBLL(CurrentUserInfo);

            rd.EventId = bll.GetRecommendId(CurrentUserInfo.ClientID);

            return rd;

        }
    }
}