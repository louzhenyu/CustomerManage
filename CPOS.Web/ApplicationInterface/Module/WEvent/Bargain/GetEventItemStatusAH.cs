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
    public class GetEventItemStatusAH : Base.BaseActionHandler<GetEventItemStatusRP,GetEventItemStatusRD>
    {
        protected override GetEventItemStatusRD ProcessRequest(APIRequest<GetEventItemStatusRP> pRequest)
        {
            GetEventItemStatusRP rp = pRequest.Parameters;
            GetEventItemStatusRD rd = new GetEventItemStatusRD();
            PanicbuyingKJEventJoinBLL panicbuyingKJEventJoinbll = new PanicbuyingKJEventJoinBLL(CurrentUserInfo);
            PanicbuyingEventBLL panicbuyingEventbll = new PanicbuyingEventBLL(CurrentUserInfo);
            PanicbuyingKJEventItemMappingBLL panicbuyingKJEventItemMappingBll = new PanicbuyingKJEventItemMappingBLL(CurrentUserInfo);
            rd.status = 0;
            
            var panicbuyingKJEventJoinEntity = panicbuyingKJEventJoinbll.GetByID(rp.KJEventJoinId);
            if (panicbuyingKJEventJoinEntity != null)
            {
                var panicbuyingEventEntity = panicbuyingEventbll.QueryByEntity(new PanicbuyingEventEntity() { EventId = panicbuyingKJEventJoinEntity.EventId }, null).FirstOrDefault();
                if (panicbuyingEventEntity == null || panicbuyingEventEntity.EndTime < DateTime.Now || panicbuyingEventEntity.EventStatus == 10)
                {
                    rd.status = 2;
                }
                else
                {
                    var panicbuyingKJEventItemMappingEntity = panicbuyingKJEventItemMappingBll.QueryByEntity(new PanicbuyingKJEventItemMappingEntity() { EventId = panicbuyingKJEventJoinEntity.EventId, ItemID = panicbuyingKJEventJoinEntity.ItemId }, null).FirstOrDefault();
                    bool isEnd = Convert.ToDateTime(panicbuyingKJEventJoinEntity.CreateTime).AddHours(Convert.ToDouble(panicbuyingKJEventItemMappingEntity.BargaingingInterval)) <= DateTime.Now ? true : false;
                    if (isEnd)
                    {
                        rd.status = 3;
                    }
                }
            }
            else
            {
                rd.status = 1;
            }
            return rd;
        }
    }
}