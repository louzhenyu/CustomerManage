using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Event.Request;
using JIT.CPOS.DTO.Module.WeiXin.Event.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Event
{
    public class GetEventListAH : BaseActionHandler<GetEventListRP, GetEventListRD>
    {
        private const int Error_EmptyData = 121;
        protected override GetEventListRD ProcessRequest(DTO.Base.APIRequest<GetEventListRP> pRequest)
        {
            var rd = new GetEventListRD();

            //string customerId = pRequest.CustomerID;
            string eventTypeId = pRequest.Parameters.EventTypeId;
            string eventName = pRequest.Parameters.EventName;

            int drawMethodId = pRequest.Parameters.DrowMethodId;
            bool? beginFlag = pRequest.Parameters.BeginFlag;
            bool? endFlag = pRequest.Parameters.EndFlag;
           
            int? pageSize = pRequest.Parameters.PageSize;
            int? pageIndex = pRequest.Parameters.PageIndex;

            int? eventStatus = pRequest.Parameters.EventStatus;

            //var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");

            var bll = new LEventsBLL(CurrentUserInfo);

            var ds = bll.GetEventList(CurrentUserInfo.ClientID, eventTypeId, eventName,drawMethodId, beginFlag, endFlag, eventStatus??0, pageIndex ?? 0, pageSize ?? 15);

            if (ds.Tables[0].Rows.Count == 0)
            {
                //throw new APIException("没有数据") { ErrorCode = Error_EmptyData };
                rd.EventList = null;
                rd.TotalPages = 0;
            }
            else
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new EventInfo
                {
                    EventId = t["EventId"].ToString(),
                    EventName = t["Title"].ToString(),
                    EventTypeId = t["EventTypeId"].ToString(),
                    EventTypeName = t["EventTypeName"].ToString(),
                    BegTime = t["BeginTime"].ToString(),
                    EndTime = t["EndTime"].ToString(),
                    //DisplayIndex = Convert.ToInt32(t["_row"]),
                    DrawMethod = t["DrawMethodName"].ToString(),
                    EventStatus = Convert.ToInt32(t["EventStatus"]),
                    EventStatusName = t["EventStatusName"].ToString(),
                    CityName = t["cityid"].ToString()
                });

                rd.EventList = temp.ToArray();
                int totalCount = bll.GetEventListCount(CurrentUserInfo.ClientID, eventTypeId, eventName, drawMethodId,
                    beginFlag, endFlag, eventStatus ?? 0);

                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount * 1.00 / (pageSize ?? 15) * 1.00)));
            }
            return rd;


        }
    }
}