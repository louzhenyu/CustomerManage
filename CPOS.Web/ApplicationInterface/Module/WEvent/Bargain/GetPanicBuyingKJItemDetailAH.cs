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
    public class GetPanicBuyingKJItemDetailAH : BaseActionHandler<GetPanicBuyingKJItemDetailRP, GetPanicbuyingKJItemDetailRD>
    {
        protected override GetPanicbuyingKJItemDetailRD ProcessRequest(APIRequest<GetPanicBuyingKJItemDetailRP> pRequest)
        {
            GetPanicBuyingKJItemDetailRP rp = pRequest.Parameters;
            GetPanicbuyingKJItemDetailRD rd = new GetPanicbuyingKJItemDetailRD();

            OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(CurrentUserInfo);
            ItemService itemServiceBll = new ItemService(CurrentUserInfo);
            var customerBasicSettingBll = new CustomerBasicSettingBLL(CurrentUserInfo);
            var panicbuyingEventBll = new PanicbuyingEventBLL(CurrentUserInfo);
            var panicbuyingKJEventItemMappingBll = new PanicbuyingKJEventItemMappingBLL(CurrentUserInfo);
            var panicbuyingKJEventSkuMappingBll = new PanicbuyingKJEventSkuMappingBLL(CurrentUserInfo);
            var panicbuyingKJEventJoinBll = new PanicbuyingKJEventJoinBLL(CurrentUserInfo);
            var panicbuyingKJEventJoinDetailBll = new PanicbuyingKJEventJoinDetailBLL(CurrentUserInfo);
            var vipBll = new VipBLL(CurrentUserInfo);

            var vipEntity = vipBll.GetByID(pRequest.UserID);
            if (vipEntity != null)
            {
                rd.HeadImageUrl = vipEntity.HeadImgUrl;
            }

            #region 判断是否已经参与
            rd.isPromoted = 0;
            if (!string.IsNullOrEmpty(rp.KJEventJoinId))
            {
                var panicbuyingKJEventJoinEntity = panicbuyingKJEventJoinBll.GetByID(rp.KJEventJoinId);
                if (panicbuyingKJEventJoinEntity == null)
                {
                    rd.isPromoted = 0;
                }
                else
                {
                    rd.isPromoted = 1;

                    var VipData = vipBll.GetByID(panicbuyingKJEventJoinEntity.VipId);
                    rd.HeadImageUrl = VipData.HeadImgUrl;
                }
            }
            #endregion

            //为复用原来接口所设置参数
            DateTime dtBeginTime = Convert.ToDateTime("9999/01/01");
            DateTime dtEndTime = Convert.ToDateTime("9999/01/01");


            #region 砍价活动商品基本信息
            KJEventItemDetailInfo eventItemInfo = panicbuyingEventBll.GetKJEventWithItemDetail(rp.EventId, rp.ItemId);
            if (eventItemInfo != null)
            {
                rd.ItemId = eventItemInfo.ItemId;
                rd.ItemName = eventItemInfo.ItemName;
                rd.MinPrice = eventItemInfo.MinPrice;
                rd.MinBasePrice = eventItemInfo.MinBasePrice;
                rd.SinglePurchaseQty = eventItemInfo.SinglePurchaseQty;
                rd.PromotePersonCount = eventItemInfo.PromotePersonCount;
                rd.CurrentQty = eventItemInfo.CurrentQty;
                rd.SoldQty = eventItemInfo.SoldQty;
                rd.EventEndTime = eventItemInfo.EventEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                rd.Seconds = Convert.ToInt64(eventItemInfo.EventEndTime.Subtract(DateTime.Now).TotalSeconds < 0 ? 0 : eventItemInfo.EventEndTime.Subtract(DateTime.Now).TotalSeconds);
                rd.PropName1 = eventItemInfo.Prop1Name;
                rd.PropName2 = eventItemInfo.Prop2Name;
                rd.ItemIntroduce = eventItemInfo.ItemIntroduce;

            #endregion

                #region
                if (eventItemInfo.EventEndTime < DateTime.Now)
                {
                    rd.isEventEnd = 0;
                }
                else if (eventItemInfo.EventBeginTime > DateTime.Now)
                {//活动未开始
                    rd.isEventEnd = 2;
                    rd.Seconds = Convert.ToInt64(eventItemInfo.EventBeginTime.Subtract(DateTime.Now).TotalSeconds < 0 ? 0 : eventItemInfo.EventBeginTime.Subtract(DateTime.Now).TotalSeconds);
                }
                else
                {
                    rd.isEventEnd = 1;
                }
                #endregion

                #region 商品图片
                var dsImages = itemService.GetItemImageList(rp.ItemId);
                if (dsImages != null && dsImages.Tables.Count > 0 && dsImages.Tables[0].Rows.Count > 0)
                {
                    rd.ImageList = DataTableToObject.ConvertToList<ImageInfo>(dsImages.Tables[0]);
                }
                #endregion

                #region 商品详情页
                if (rp.type == 1)
                {
                    #region 关于商品所有Sku信息
                    var dsSkus = itemServiceBll.GetItemSkuList(rp.ItemId, pRequest.UserID, pRequest.CustomerID, dtBeginTime, dtEndTime);
                    var panicbuyingKJEventSkuMappingList = panicbuyingKJEventSkuMappingBll.QueryByEntity(new PanicbuyingKJEventSkuMappingEntity() { EventItemMappingID = eventItemInfo.EventItemMappingID }, null).ToList();
                    if (dsSkus != null && dsSkus.Tables.Count > 0 && dsSkus.Tables[0].Rows.Count > 0)
                    {
                        rd.SkuInfoList = DataTableToObject.ConvertToList<ItemSkuInfo>(dsSkus.Tables[0]);
                    }
                    rd.SkuInfoList = rd.SkuInfoList.Join(panicbuyingKJEventSkuMappingList, n => n.skuId, m => m.SkuID, (n, m) => new ItemSkuInfo()
                    {
                        skuId = n.skuId,
                        skuProp1 = n.skuProp1,
                        skuProp2 = n.skuProp2,
                        BasePrice = m.BasePrice.ToString(),
                        price = m.Price.ToString(),
                        SalesCount = m.SoldQty.ToString(),
                        Stock = (m.Qty - m.SoldQty).ToString(),
                    }).Distinct().ToList();
                    #endregion

                    #region 商品属性
                    var dsProp1 = panicbuyingKJEventItemMappingBll.GetKJItemProp1List(rp.ItemId, rp.EventId);
                    if (dsProp1 != null && dsProp1.Tables.Count > 0 && dsProp1.Tables[0].Rows.Count > 0)
                    {
                        rd.Prop1List = DataTableToObject.ConvertToList<SkuProp1>(dsProp1.Tables[0]);
                    }
                    #endregion
                    rd.status = 1;
                }
                #endregion

                #region 帮砍页面
                if (rp.type == 2 && rd.isPromoted == 1)
                {
                    if (!string.IsNullOrEmpty(rp.SkuId))
                    {
                        KJItemSkuInfo kJItemSkuInfo = panicbuyingKJEventSkuMappingBll.GetKJItemSkuInfo(rp.EventId, rp.SkuId, rp.KJEventJoinId);
                        rd.SkuInfoList = new List<ItemSkuInfo>();

                        ItemSkuInfo itemSkuInfo = new ItemSkuInfo();
                        itemSkuInfo.skuId = kJItemSkuInfo.skuId;
                        itemSkuInfo.skuProp1 = kJItemSkuInfo.skuProp1;
                        itemSkuInfo.skuProp2 = kJItemSkuInfo.skuProp2;
                        itemSkuInfo.price = kJItemSkuInfo.price.ToString();
                        itemSkuInfo.Stock = kJItemSkuInfo.Stock;
                        rd.SkuInfoList.Add(itemSkuInfo);

                        rd.BargainedPrice = kJItemSkuInfo.price - kJItemSkuInfo.SalesPrice;
                        rd.MinPrice = kJItemSkuInfo.price;
                        rd.MinBasePrice = kJItemSkuInfo.BasePrice;
                        rd.EventSKUMappingId = kJItemSkuInfo.EventSKUMappingId;

                        double EventTime = Convert.ToInt64(eventItemInfo.EventEndTime.Subtract(DateTime.Now).TotalSeconds < 0 ? 0 : eventItemInfo.EventEndTime.Subtract(DateTime.Now).TotalSeconds);
                        double tempTime = (kJItemSkuInfo.CreateTime.AddHours(Convert.ToDouble(eventItemInfo.BargaingingInterval)) - DateTime.Now).TotalSeconds;
                        if (EventTime > tempTime)
                            rd.Seconds = Convert.ToInt64(tempTime < 0 ? 0 : tempTime);
                        else
                            rd.Seconds = Convert.ToInt64(EventTime < 0 ? 0 : EventTime);


                        decimal tempPrice = rd.MinPrice - rd.MinBasePrice;
                        if (tempPrice != 0)
                        {
                            rd.BargainedRate = Math.Round(rd.BargainedPrice / (tempPrice), 2);
                        }
                        else
                        {
                            rd.BargainedRate = 0;
                        }

                        if (!string.IsNullOrEmpty(rp.KJEventJoinId))
                        {
                            var panicbuyingKJEventJoinDetailEntity = panicbuyingKJEventJoinDetailBll.QueryByEntity(new PanicbuyingKJEventJoinDetailEntity() { KJEventJoinId = new Guid(rp.KJEventJoinId), VipId = pRequest.UserID }, null).FirstOrDefault();
                            if (panicbuyingKJEventJoinDetailEntity == null)
                            {
                                rd.status = 2;
                            }
                            else
                            {
                                rd.status = 3;
                            }
                        }
                        else
                        {
                            rd.status = 2;
                        }
                    }
                }
                #endregion

                rd.DeliveryDesc = customerBasicSettingBll.GetSettingValueByCode("DeliveryStrategy");
                rd.CustomerShortName = customerBasicSettingBll.GetSettingValueByCode("CustomerShortName");
                rd.WebLogo = customerBasicSettingBll.GetSettingValueByCode("WebLogo");
                rd.QRCodeURL = customerBasicSettingBll.GetSettingValueByCode("GuideQRCode");
            }
            else
            {
                throw new APIException("此活动已不存在") { ErrorCode = 100 };
            }
            return rd;
        }
    }
}