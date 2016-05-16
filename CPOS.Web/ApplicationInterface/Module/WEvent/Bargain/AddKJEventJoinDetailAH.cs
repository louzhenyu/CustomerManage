using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using JIT.CPOS.DTO.Module.Event.Bargain.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.WEvent.Bargain
{
    public class AddKJEventJoinDetailAH : BaseActionHandler<AddKJEventJoinDetailRP, AddKJEventJoinDetailRD>
    {
        protected override AddKJEventJoinDetailRD ProcessRequest(APIRequest<AddKJEventJoinDetailRP> pRequest)
        {
            var rp = pRequest.Parameters;
            var rd = new AddKJEventJoinDetailRD();
            var Bll = new PanicbuyingKJEventJoinDetailBLL(CurrentUserInfo);

            var PanicbuyingEventBll = new PanicbuyingEventBLL(CurrentUserInfo);
            var EventSkuMappingBll = new PanicbuyingKJEventSkuMappingBLL(CurrentUserInfo);
            var KJEventJoinBll = new PanicbuyingKJEventJoinBLL(CurrentUserInfo);
            var KJEventItemMappingBll = new PanicbuyingKJEventItemMappingBLL(CurrentUserInfo);
            var pTran = Bll.GetTran();
            //
            var EventData = PanicbuyingEventBll.GetByID(rp.EventId);
            if (EventData == null)
                throw new APIException("找不到砍价活动对象！");
            //
            var EventItemData = KJEventItemMappingBll.QueryByEntity(new PanicbuyingKJEventItemMappingEntity() { EventId = new System.Guid(rp.EventId), ItemID = rp.ItemId }, null).FirstOrDefault();
            if (EventData == null)
                throw new APIException("找不到砍价活动商品对象！");
            //
            var EventSkuMappingData = EventSkuMappingBll.QueryByEntity(new PanicbuyingKJEventSkuMappingEntity() { EventItemMappingID = EventItemData.EventItemMappingID.ToString(),SkuID=rp.SkuId }, null).FirstOrDefault();
            if (EventSkuMappingData == null)
                throw new APIException("找不到砍价活动商品Sku关系对象！");
            //
            var KJEventJoinData = KJEventJoinBll.GetByID(rp.KJEventJoinId);
            if (KJEventJoinData == null)
                throw new APIException("找不到砍价参与主表对象！");
            //判断重复帮砍 
            var Collection = Bll.QueryByEntity(new PanicbuyingKJEventJoinDetailEntity() { KJEventJoinId = KJEventJoinData.KJEventJoinId,VipId = pRequest.UserID }, null).ToList();
            if(Collection.Count>0)
                throw new APIException("您已经帮砍过了，不能重复帮砍！");
            #region 砍价业务处理
            //当前成交价
            decimal NowMoney = KJEventJoinData.SalesPrice.Value;
            if (NowMoney == EventSkuMappingData.BasePrice)
                throw new APIException("已经砍到底价，不能继续砍价！");
            if(EventSkuMappingData.BargainStartPrice==null||EventSkuMappingData.BargainEndPrice==null)
                throw new APIException("砍价起始、结束区间值不能为Null，错误数据！");
            //
            Random ran = new Random();
            int start = Convert.ToInt32(EventSkuMappingData.BargainStartPrice);
            int End = Convert.ToInt32(EventSkuMappingData.BargainEndPrice);
            int math = ran.Next(start, End);
            //砍价后的价格
            decimal Result = NowMoney - Convert.ToDecimal(math);
            if (Result < EventSkuMappingData.BasePrice)
            {//如果Result小于底价，那Result赋值为底价金额值
                Result = EventSkuMappingData.BasePrice.Value;
            }
            //砍了多少
            decimal BargainPrice = NowMoney - Result;
            //赋值
            rd.BargainPrice = BargainPrice;
            #endregion
            
            using (pTran.Connection)
            {
                try
                {
                    //添加砍价参与者信息
                    var AddData = new PanicbuyingKJEventJoinDetailEntity();
                    AddData.KJEventJoinDetailId = System.Guid.NewGuid();
                    AddData.KJEventJoinId = KJEventJoinData.KJEventJoinId;
                    AddData.EventId = new System.Guid(rp.EventId);
                    AddData.ItemId = rp.ItemId;
                    AddData.SkuId = rp.SkuId;
                    AddData.VipId = pRequest.UserID;
                    AddData.BargainPrice = BargainPrice;
                    AddData.MomentSalesPrice = Result;
                    AddData.CustomerId = CurrentUserInfo.ClientID;
                    //
                    Bll.Create(AddData, pTran);
                    //更新参与主表帮砍统计、成交价
                    KJEventJoinData.BargainPersonCount = KJEventJoinData.BargainPersonCount ?? 0;
                    KJEventJoinData.BargainPersonCount += 1;
                    KJEventJoinData.SalesPrice = KJEventJoinData.SalesPrice ?? 0;
                    KJEventJoinData.SalesPrice = Result;
                    KJEventJoinBll.Update(KJEventJoinData, pTran);
                    //更新砍价活动表帮砍人数统记
                    EventData.BargainPersonCount += 1;
                    PanicbuyingEventBll.Update(EventData, pTran);
                    //更新活动商品帮砍人数统计
                    EventItemData.BargainPersonCount = EventItemData.BargainPersonCount ?? 0;
                    EventItemData.BargainPersonCount += 1;
                    KJEventItemMappingBll.Update(EventItemData, pTran);
                    //提交
                    pTran.Commit();
                }
                catch (APIException ex)
                {
                    pTran.Rollback();
                    throw ex;
                }
                
            }
            return rd;
        }
    }
}