using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.Event.Request;
using JIT.CPOS.DTO.Module.WeiXin.Event.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Event
{
    public class GetDrawMethodListAH : BaseActionHandler<GetDrawMethodListRP, GetDrawMethodListRD>
    {
        protected override GetDrawMethodListRD ProcessRequest(DTO.Base.APIRequest<GetDrawMethodListRP> pRequest)
        {
            var rd = new GetDrawMethodListRD();

            string eventTypeId = pRequest.Parameters.EventTypeId;
            int? pageSize = pRequest.Parameters.PageSize;
            int? pageIndex = pRequest.Parameters.PageIndex;

            var bll = new LEventDrawMethodBLL(CurrentUserInfo);

            var ds = bll.GetDrawMethodList(this.CurrentUserInfo.ClientID,eventTypeId, pageIndex ?? 0, pageSize ?? 15);

            if (ds.Tables[0].Rows.Count == 0)
            {
                rd.DrawMethodList = null;
            }
            else
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new DrawMethodInfo
                {
                    DrawMethodId = Convert.ToInt32(t["DrawMethodID"]),
                    DrawMethodName = t["DrawMethodName"].ToString()
                });

                rd.DrawMethodList = temp.ToArray();
            }
            int totalCount = bll.GetDrawTotalCount(CurrentUserInfo.ClientID, eventTypeId);

            rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount*1.00 / (pageSize ?? 15) * 1.00)));
            return rd;
        }
    }
}