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
    public class JoinInKJEventAH : BaseActionHandler<JoinInKJEventRP, JoinInKJEventRD>
    {
        protected override JoinInKJEventRD ProcessRequest(APIRequest<JoinInKJEventRP> pRequest)
        {
            JoinInKJEventRP rp = pRequest.Parameters;
            JoinInKJEventRD rd = new JoinInKJEventRD();

            PanicbuyingKJEventJoinBLL panicbuyingKJEventJoinBll = new PanicbuyingKJEventJoinBLL(CurrentUserInfo);
            PanicbuyingEventBLL panicbuyingEventBll = new PanicbuyingEventBLL(CurrentUserInfo);
            PanicbuyingKJEventItemMappingBLL panicbuyingKJEventItemMappingBll = new PanicbuyingKJEventItemMappingBLL(CurrentUserInfo);
            if (string.IsNullOrEmpty(rp.EventId))
            {
                throw new APIException("EventId不能为空");
            }
            if (string.IsNullOrEmpty(rp.SkuId))
            {
                throw new APIException("SkuId不能为空");
            }

            #region 砍价参与
            PanicbuyingKJEventJoinEntity panicbuyingKJEventJoinEntity = new PanicbuyingKJEventJoinEntity()
            {
                KJEventJoinId = Guid.NewGuid(),
                EventId = new Guid(rp.EventId),
                ItemId = rp.ItemId,
                SkuId = rp.SkuId,
                VipId = pRequest.UserID,
                CustomerId = pRequest.CustomerID,
                SalesPrice = rp.Price,
            };
            panicbuyingKJEventJoinBll.Create(panicbuyingKJEventJoinEntity);
            #endregion

            #region 更新参与砍价活动人数统计
            var panicbuyingEventEnetity = panicbuyingEventBll.QueryByEntity(new PanicbuyingEventEntity() { EventId = new Guid(rp.EventId) }, null).FirstOrDefault();
            if(panicbuyingEventEnetity != null)
            {
                panicbuyingEventEnetity.PromotePersonCount += 1;
                //panicbuyingEventEnetity.BargainPersonCount += 1;
                panicbuyingEventBll.Update(panicbuyingEventEnetity);
            }
            #endregion

            #region 更新发起砍价商品活动人数统计
            var panicbuyingKJEventItemMappingEntity = panicbuyingKJEventItemMappingBll.QueryByEntity(new PanicbuyingKJEventItemMappingEntity() { EventId = new Guid(rp.EventId), ItemID = rp.ItemId }, null).FirstOrDefault();
            if (panicbuyingKJEventItemMappingEntity != null)
            {
                panicbuyingKJEventItemMappingEntity.PromotePersonCount += 1;
                panicbuyingKJEventItemMappingBll.Update(panicbuyingKJEventItemMappingEntity);
            }
            #endregion


            rd.KJEventJoinId = panicbuyingKJEventJoinEntity.KJEventJoinId.ToString();
            return rd;
        }
    }
}