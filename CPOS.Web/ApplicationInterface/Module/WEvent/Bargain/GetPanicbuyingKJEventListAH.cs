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

            List<PanicbuyingEventEntity> panicbuyingEventEntityList = panicbuyingEventBll.QueryByEntity(new PanicbuyingEventEntity() { EventTypeId = rp.EventTypeId, CustomerID = pRequest.CustomerID }, null).ToList();
            if (panicbuyingEventEntityList.Count>0)
            {
                var ResultList = panicbuyingEventEntityList.Where(t => t.EndTime >= DateTime.Now && t.EventStatus == 20).ToList();

                if (ResultList.Count>0)
                {
                    List<KJEventItemInfo> eventItemInfoList = panicbuyingEventBll.GetKJEventWithItemList(pRequest.CustomerID);

                    rd.EventList = ResultList.Select(t => new EventInfo()
                    {
                        EventId = t.EventId.ToString(),
                        BeginTime = t.BeginTime == null ? "" : t.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        EndTime = t.EndTime == null ? "" : t.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        Seconds = 0,//Convert.ToInt64(t.EndTime.Subtract(DateTime.Now).TotalSeconds) >= 0 ? Convert.ToInt64(t.EndTime.Subtract(DateTime.Now).TotalSeconds) : 0,
                        Status = t.BeginTime > DateTime.Now ? 1 : t.EndTime < DateTime.Now ? 2 : 0,
                        EventItemList = eventItemInfoList.AsEnumerable().Where(n => n.EventId == t.EventId.ToString()).Select(n => new EventItem()
                        {
                            ItemId = n.ItemId,
                            ItemName = n.ItemName,
                            ImageUrl = n.ImageUrl,
                            ImageUrlThumb = string.IsNullOrEmpty(n.ImageUrl) ? "" : GetImageUrl(n.ImageUrl, "_120"),
                            ImageUrlMiddle = string.IsNullOrEmpty(n.ImageUrl) ? "" : GetImageUrl(n.ImageUrl, "_240"),
                            ImageUrlBig = string.IsNullOrEmpty(n.ImageUrl) ? "" : GetImageUrl(n.ImageUrl, "_480"),
                            Price = n.MinPrice,
                            BasePrice = n.MinBasePrice,
                            Qty = n.Qty - n.SoldQty,
                            PromotePersonCount = n.PromotePersonCount
                        }).ToList(),
                    }).ToList();

                    foreach (var item in rd.EventList)
                    {
                        var BeginTime = Convert.ToDateTime(item.BeginTime);
                        var EndTime = Convert.ToDateTime(item.EndTime);
                        if (BeginTime > DateTime.Now)
                            item.Seconds = Convert.ToInt64(BeginTime.Subtract(DateTime.Now).TotalSeconds) >= 0 ? Convert.ToInt64(BeginTime.Subtract(DateTime.Now).TotalSeconds) : 0;
                        else
                            item.Seconds = Convert.ToInt64(EndTime.Subtract(DateTime.Now).TotalSeconds) >= 0 ? Convert.ToInt64(EndTime.Subtract(DateTime.Now).TotalSeconds) : 0;
                    }
                    rd.EventList = rd.EventList.OrderBy(t => t.Status).ThenBy(t => t.Seconds).ToList();

                }
                else
                {
                    rd.IsAllEnd = 1;
                }
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