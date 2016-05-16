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
    public class GetItemListAH : BaseActionHandler<GetItemListRP,GetItemListRD>
    {
        protected override GetItemListRD ProcessRequest(APIRequest<GetItemListRP> pRequest)
        {
            GetItemListRP rp = pRequest.Parameters;
            GetItemListRD rd = new GetItemListRD();
  
            PanicbuyingEventBLL panicbuyingEventBll = new PanicbuyingEventBLL(CurrentUserInfo);
            List<KJEventItemInfo> eventItemInfoList = panicbuyingEventBll.GetKJEventWithItemList(pRequest.CustomerID);

            rd.ItemList = eventItemInfoList.AsEnumerable().Where(n => n.EventId == rp.EventId).Select(n => new EventItem()
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
            }).ToList();

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