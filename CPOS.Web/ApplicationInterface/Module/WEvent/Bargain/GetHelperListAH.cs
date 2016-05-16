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
    public class GetHelperListAH : BaseActionHandler<GetHelperListRP,GetHelperListRD>
    {
        protected override GetHelperListRD ProcessRequest(APIRequest<GetHelperListRP> pRequest)
        {
            GetHelperListRP rp = pRequest.Parameters;
            GetHelperListRD rd = new GetHelperListRD();
            PanicbuyingKJEventJoinDetailBLL panicbuyingKJEventJoinDetailBll = new PanicbuyingKJEventJoinDetailBLL(CurrentUserInfo);
            DataSet ds = panicbuyingKJEventJoinDetailBll.GetHelperList(rp.EventId, rp.KJEventJoinId, rp.SkuId, rp.PageSize, rp.PageIndex);

            if (ds.Tables.Count > 0)
            {
                rd.HelperList = DataTableToObject.ConvertToList<Helper>(ds.Tables[0]);
                rd.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                rd.TotalPage = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            }
            return rd;
        }
    }
}