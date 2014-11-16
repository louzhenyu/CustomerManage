using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.WeiXin.Event.Request;
using JIT.CPOS.DTO.Module.WeiXin.Event.Response;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Event
{
    public class GetEventTypeListAH : BaseActionHandler<GetEventTypeListRP, GetEventTypeListRD>
    {
        private const int Error_EmptyData = 121;
        protected override GetEventTypeListRD ProcessRequest(APIRequest<GetEventTypeListRP> pRequest)
        {
            var rd = new GetEventTypeListRD();

            int? pageIndex = pRequest.Parameters.PageIndex;
            int? pageSize = pRequest.Parameters.PageSize;

            //string customerId = pRequest.CustomerID;

            //var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");

            var bll = new LEventsGenreBLL(CurrentUserInfo);

            var ds = bll.GetEventTypeList(CurrentUserInfo.ClientID, pageIndex ?? 0, pageSize ?? 15);

            if (ds.Tables[0].Rows.Count == 0)
            {
               // throw new APIException("没有数据") { ErrorCode = Error_EmptyData };
                rd.EventTypeList = null;
            }
            else
            {
                var temp =
              ds.Tables[0].AsEnumerable()
                  .Select(t => new EventTypeInfo
                  {
                     EventTypeId = t["EventTypeId"].ToString(),
                     EventTypeName = t["Title"].ToString()
                  });

                rd.EventTypeList = temp.ToArray();
            }
            int totalCount = bll.GetEventTypeCount(CurrentUserInfo.ClientID);
            rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount*1.00 / (pageSize ?? 3) * 1.00)));
            return rd;
        }
    }
}