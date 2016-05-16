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
    public class GetPanicbuyingKJEventListAH : BaseActionHandler<EventListRP, EventListRD>
    {
        protected override EventListRD ProcessRequest(APIRequest<EventListRP> pRequest)
        {
            EventListRP rp = pRequest.Parameters;
            EventListRD rd = new EventListRD();

            PanicbuyingEventBLL panicbuyingEventBll = new PanicbuyingEventBLL(CurrentUserInfo);

            List<PanicbuyingEventEntity> panicbuyingEventEntityList = panicbuyingEventBll.QueryByEntity(new PanicbuyingEventEntity() { EventTypeId = rp.EventTypeId, CustomerID = pRequest.CustomerID, EventStatus = 10 }, null).ToList();

            if (panicbuyingEventEntityList != null)
            {
                List<KJEventItemInfo> eventItemInfoList = panicbuyingEventBll.GetKJEventWithItemList(pRequest.CustomerID);

                rd.EventList = panicbuyingEventEntityList.AsEnumerable().Select(t => new EventInfo()
                {
                    EventId = t.EventId.ToString(),
                    BeginTime = t.BeginTime == null ? "" : t.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndTime = t.EndTime == null ? "" : t.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Status = t.BeginTime <= DateTime.Now ? 0 : 1,
                    EventItemList = eventItemInfoList.AsEnumerable().Select(n => new EventItem()
                    {
                        ItemId = n.ItemId,
                        ItemName = n.ItemName,
                        ImageUrl = n.ImageUrl,
                        ImageUrlThumb = string.IsNullOrEmpty(n.ImageUrl) ? "" : GetImageUrl(n.ImageUrl, "_120"),
                        ImageUrlMiddle = string.IsNullOrEmpty(n.ImageUrl) ? "" : GetImageUrl(n.ImageUrl, "_240"),
                        ImageUrlBig = string.IsNullOrEmpty(n.ImageUrl) ? "" : GetImageUrl(n.ImageUrl, "_480"),
                        Price = n.MinPrice,
                        BasePrice = n.MinBasePrice,
                        Qty = n.Qty,
                        PromotePersonCount = n.PromotePersonCount
                    }).ToList(),
                }).ToList();
            }

            return rd;
        }

        private string GetImageUrl(string sourceUrl, string add)
        {
            var extend = sourceUrl.Split('.').Last();
            var temp = sourceUrl.Trim(extend.ToArray()).Trim('.');
            return temp + add + "." + extend;
        }
    }
}