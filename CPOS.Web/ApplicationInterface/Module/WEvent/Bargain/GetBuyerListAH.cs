using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Event.Bargain.Response;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;

namespace JIT.CPOS.Web.ApplicationInterface.Module.WEvent.Bargain
{
    public class GetBuyerListAH : BaseActionHandler<GetBuyerListRP,GetBuyerListRD>
    {
        protected override GetBuyerListRD ProcessRequest(APIRequest<GetBuyerListRP> pRequest)
        {
            GetBuyerListRP rp = pRequest.Parameters;
            GetBuyerListRD rd = new GetBuyerListRD();
            PanicbuyingKJEventJoinBLL panicbuyingKJEventJoinBll = new PanicbuyingKJEventJoinBLL(CurrentUserInfo);

            DataSet ds = panicbuyingKJEventJoinBll.GetBuyerList(rp.EventId, rp.ItemId, rp.PageSize, rp.PageIndex);

            if (ds.Tables.Count > 0)
            {
                rd.BuyerList = DataTableToObject.ConvertToList<Buyer>(ds.Tables[0]);
                rd.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                rd.TotalPage = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            }
            return rd;
        }
    }
}