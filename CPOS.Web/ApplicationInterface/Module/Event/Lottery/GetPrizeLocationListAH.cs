using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Event.Lottery.Request;
using JIT.CPOS.DTO.Module.Event.Lottery.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
namespace JIT.CPOS.Web.ApplicationInterface.Module.Event.Lottery
{
    public class GetPrizeLocationListAH : BaseActionHandler<GetPrizeLocationListRP, GetPrizeLocationListRD>
    {
        protected override GetPrizeLocationListRD ProcessRequest(APIRequest<GetPrizeLocationListRP> pRequest)
        {
            var rd = new GetPrizeLocationListRD();
            var rp = pRequest.Parameters;
            

            var entityPrizeLocation = new LPrizeLocationEntity();
            var bllPrizeLocation = new LPrizeLocationBLL(this.CurrentUserInfo);

            if (string.IsNullOrEmpty(rp.EventID))
            {
                rd.ErrMsg = "EventID参数有误";
            }
            else
            {
                rd.EventID = rp.EventID;
                rd.PrizeLocationList = bllPrizeLocation.QueryByEntity(new LPrizeLocationEntity() { EventID = rp.EventID }, null).ToList();
            }

            return rd;
        }
    }
}