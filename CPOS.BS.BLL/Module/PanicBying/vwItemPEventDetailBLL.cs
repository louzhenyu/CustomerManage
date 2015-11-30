/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 17:25:36
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class vwItemPEventDetailBLL
    {
        public void Load(IDataReader rd, out vwItemPEventDetailEntity m)
        {
            this._currentDAO.Load(rd, out m);
        }

        public object GetListByParameters(GetPanicbuyingItemListReqPara para)
        {
            var ds = GetListData(para);
            var count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            bool isNext = count > Convert.ToInt32(para.pageSize) * Convert.ToInt32(para.page);
            List<vwItemPEventDetailEntity> templist = new List<vwItemPEventDetailEntity> { };
            using (var rd = ds.Tables[1].CreateDataReader())
            {
                while (rd.Read())
                {
                    vwItemPEventDetailEntity m;
                    this.Load(rd, out m);
                    templist.Add(m);
                }
            }
            var detailList = templist.Select(t => new
            {
                itemId = t.ItemID,
                eventId = t.EventId,
                itemName = t.ItemName,
                imageUrl = t.ImageUrl,
                imageUrlThumb = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_120"),
                imageUrlMiddle = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_240"),
                imageUrlBig = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_480"),
                price = t.Price.Value.ToString("0.##"),
                salesPrice = t.SalesPrice.Value.ToString("0.##"),
                discountRate = t.DiscountRate.Value.ToString("0.##"),
                displayIndex = t.DisplayIndex,
                itemCategoryName = t.ItemCategoryName,
                salesPersonCount = t.SalesPersonCount,
                skuId = t.SkuId,
                createDate = t.CreateTime.Substring(0, 10),
                salesCount = t.SalesQty,
                deadlineTime = t.deadlineTime,
                deadlineSecond = Convert.ToInt64((t.EndTime.Value - DateTime.Now).TotalSeconds),
                addedTime = t.AddTime.Value.To19FormatString(),
                beginTime = t.BeginTime.Value.To19FormatString(),
                endTime = t.EndTime.Value.To19FormatString(),
                qty = t.Qty,
                overQty = t.Qty - t.SalesQty,
                stopReason = t.StopReason,
                status = t.Status
            });
            return new { isNext = isNext ? "1" : "0", itemList = detailList.ToArray() };
        }
        /// <summary>
        /// 新版获取活动列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public object GetPanicbuyingEventList(GetPanicbuyingItemListReqPara para)
        {
            //获取进行中的活动
            List<vwItemPEventDetailEntity> onEventItemlist = this._currentDAO.GetOnPanicbuyingEventList(para);
            //获取即将推出的活动
            List<vwItemPEventDetailEntity> soonEventItemlist = this._currentDAO.GetSoonPanicbuyingEventList(para);
            //实例化返回集合
            List<vwPanicbuyingEventEntity> eventList = new List<vwPanicbuyingEventEntity> { };

            #region 抢购中
            if (onEventItemlist.Count > 0)
            {
                //根据活动和开始时间筛选
                var onEventList = onEventItemlist.GroupBy(t => new { t.EventId, t.EndTime })
                                                     .Select(t => new { EventID = t.Key.EventId, EndTime = t.Key.EndTime });
                foreach (var eventModel in onEventList)
                {
                    var detailOnList = onEventItemlist.Where(t => t.EventId == eventModel.EventID).Select(t => new vwPanicbuyingItemInfo()
                    {
                        ItemId = t.ItemID,
                        EventId = t.EventId,
                        ItemName = t.ItemName,
                        ImageUrl = t.ImageUrl,
                        ImageUrlThumb = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_120"),
                        ImageUrlMiddle = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_240"),
                        ImageUrlBig = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_480"),
                        Price = t.Price.Value.ToString("0.##"),
                        SalesPrice = t.SalesPrice.Value.ToString("0.##"),
                        //DiscountRate = t.DiscountRate.ToString(),
                        DiscountRate = t.DiscountRate.Value.ToString("0."),
                        DisplayIndex = t.DisplayIndex,
                        ItemCategoryName = t.ItemCategoryName,
                        SalesPersonCount = t.SalesPersonCount,
                        SkuId = t.SkuId,
                        CreateDate = t.CreateTime.Substring(0, 10),
                        SalesCount = t.SalesQty,
                        DeadlineTime = t.deadlineTime,
                        DeadlineSecond = Convert.ToInt64((t.EndTime.Value - DateTime.Now).TotalSeconds),
                        AddedTime = t.AddTime.Value.To19FormatString(),
                        BeginTime = t.BeginTime.Value.To19FormatString(),
                        EndTime = t.EndTime.Value.To19FormatString(),
                        Qty = t.Qty,
                        OverQty = t.Qty - t.SalesQty,
                        StopReason = t.StopReason,
                        Status = t.Status
                    });
                    vwPanicbuyingEventEntity onEntity = new vwPanicbuyingEventEntity();
                    onEntity.DeadlineSecond = Convert.ToInt64((onEventItemlist[0].EndTime.Value - DateTime.Now).TotalSeconds);
                    onEntity.EventStatusDesc = "抢购中";
                    onEntity.PanicbuyingItemList = detailOnList.ToArray();

                    eventList.Add(onEntity);
                }
            }
            #endregion
            #region 即将开始
            if (soonEventItemlist.Count > 0)
            {
                //根据活动和开始时间筛选
                var soonEventList = soonEventItemlist.GroupBy(t => new { t.EventId, t.BeginTime })
                                                     .Select(t => new { EventID = t.Key.EventId, BeginTime = t.Key.BeginTime });
                vwPanicbuyingEventEntity soonEntity = null;
                foreach (var eventModel in soonEventList)
                {
                    var detailSoonList = soonEventItemlist.Where(t => t.EventId == eventModel.EventID).Select(t => new vwPanicbuyingItemInfo()
                    {
                        ItemId = t.ItemID,
                        EventId = t.EventId,
                        ItemName = t.ItemName,
                        ImageUrl = t.ImageUrl,
                        ImageUrlThumb = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_120"),
                        ImageUrlMiddle = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_240"),
                        ImageUrlBig = string.IsNullOrEmpty(t.ImageUrl) ? "" : GetImageUrl(t.ImageUrl, "_480"),
                        Price = t.Price.Value.ToString("0.##"),
                        SalesPrice = t.SalesPrice.Value.ToString("0.##"),
                        DiscountRate = t.DiscountRate.Value.ToString("0."),
                        //DiscountRate = t.DiscountRate.ToString(),
                        DisplayIndex = t.DisplayIndex,
                        ItemCategoryName = t.ItemCategoryName,
                        SalesPersonCount = t.SalesPersonCount,
                        SkuId = t.SkuId,
                        CreateDate = t.CreateTime.Substring(0, 10),
                        SalesCount = t.SalesQty,
                        DeadlineTime = t.deadlineTime,
                        DeadlineSecond = Convert.ToInt64((t.EndTime.Value - DateTime.Now).TotalSeconds),
                        AddedTime = t.AddTime.Value.To19FormatString(),
                        BeginTime = t.BeginTime.Value.To19FormatString(),
                        EndTime = t.EndTime.Value.To19FormatString(),
                        Qty = t.Qty,
                        OverQty = t.Qty - t.SalesQty,
                        StopReason = t.StopReason,
                        Status = t.Status
                    });
                    soonEntity = new vwPanicbuyingEventEntity();
                    soonEntity.DeadlineSecond = Convert.ToInt64((eventModel.BeginTime.Value - DateTime.Now).TotalSeconds);
                    soonEntity.EventStatusDesc = "即将开始";
                    soonEntity.PanicbuyingItemList = detailSoonList.ToArray();
                    eventList.Add(soonEntity);
                }
            }
            #endregion

            return eventList.ToArray();
        }
        public object GetDetailByParameters(GetPanicbuyingItemDetailReqPara para,string userId)
        {
            LoggingSessionInfo loggingSessionInfo = this.CurrentUserInfo as LoggingSessionInfo;
            var detail = GetByEventIDAndItemID(para.eventId, para.itemId);
            if (detail == null)
                throw new Exception("未找到相关活动商品信息");

            #region 照片列表
            var imagebll = new ObjectImagesBLL(loggingSessionInfo);
            var tempImageList = imagebll.GetObjectImagesByObjectId(para.itemId);
            var imagelist = tempImageList.Select(t => new
            {
                imageId = t.ImageId,
                imageUrl = t.ImageURL,
                imageUrlThumb = GetImageUrl(t.ImageURL, "_120"),
                imageUrlMiddle = GetImageUrl(t.ImageURL, "_240"),
                imageUrlBig = GetImageUrl(t.ImageURL, "_480")

            }).ToArray();
            #endregion

            #region sku列表
            var skubll = new SkuService(loggingSessionInfo);
            var ds = skubll.GetItemSkuListByEventId(para.itemId, para.eventId);
            var skulist = ds.Tables[0].AsEnumerable().Select(t => new
            {
                skuId = t["skuId"].ToString(),
                skuProp1 = t["skuProp1"].ToString(),
                skuProp2 = t["skuProp2"].ToString(),
                price = t["price"] is DBNull ? "0" : Double.Parse(t["price"].ToString()).ToString("0.##"),
                salesPrice = t["salesPrice"] is DBNull ? "0" : Double.Parse(t["salesPrice"].ToString()).ToString("0.##"),
                discountRate = string.IsNullOrEmpty(t["discountRate"].ToString()) ? "0" : t["discountRate"].ToString(),//折扣
                integral = string.IsNullOrEmpty(t["integral"].ToString()) ? "0" : t["integral"].ToString(),
                //qty = Convert.ToInt32(t["qty"] is DBNull ? "0" : t["qty"]),
                //overQty = Convert.ToInt32(t["overQty"] is DBNull ? "0" : t["overQty"])
            }).ToArray();
            #endregion
            #region 购买用户列表
            var inoutbll = new T_InoutBLL(loggingSessionInfo);
            var dsSalesUsers = inoutbll.GetItemEventSalesUserList(para.itemId, para.eventId);
            var salesUserList = dsSalesUsers.Tables[0].AsEnumerable().Select(t => new
                {
                    userId = t["userId"].ToString(),
                    imageURL = t["imageURL"].ToString()
                }).ToArray();
            #endregion

            #region 门店信息
            object storeInfo = null;
            var dsStore = inoutbll.GetEventStoreByItemAndEvent(para.itemId, para.eventId);
            if (dsStore.Tables[0].Rows.Count > 0)
            {
                var row = dsStore.Tables[0].Rows[0];
                storeInfo = new
                {
                    storeId = row["storeid"],
                    storeName = row["storeName"],
                    address = row["address"],
                    imageURL = row["imageURL"],
                    phone=row["phone"],
                    storeCount = row["storeCount"]
                };
            }
            #endregion

            #region 品牌信息
            var dsBrand = inoutbll.GetItemBrandInfo(para.itemId);
            object brandInfo = null;
            if (dsBrand.Tables[0].Rows.Count > 0)
            {
                var row = dsBrand.Tables[0].Rows[0];
                brandInfo = new
                {
                    brandId = row["brandId"],
                    brandLogoURL = row["brandLogoURL"],
                    brandName = row["brandName"],
                    brandEngName = row["brandEngName"]
                };
            }
            #endregion

            #region sku信息
            IList<object> skuInfoList = new List<object>();
            object skuInfo = null;
            if (skulist.Count() > 0)
            {
                for (int i = skulist.Count() - 1; i >= 0; i--)
                {
                    var sku = skulist[i];
                    var info = skubll.GetSkuInfoById(sku.skuId);
                    skuInfoList.Add(new
                    {
                        skuId = sku.skuId,
                        prop1DetailId = info.prop_1_id,
                        prop2DetailId = info.prop_2_id
                    });
                    if (i == 0)
                    {
                        skuInfo = new
                            {
                                skuId = sku.skuId,
                                prop1DetailId = info.prop_1_id,
                                prop2DetailId = info.prop_2_id
                            };
                    }
                }
               
            }
            #endregion

            #region 属性信息
            IList<object> prop1List = new List<object>();
            object prop1 = null;
            //update by wzq 20140724
            if (skulist.Count() > 0)
            {
                for (int c = skulist.Count() - 1; c >= 0; c--)
                {
                    var dsProp = inoutbll.GetItemProp1List(skulist[c].skuId);
                    //var dsProp = inoutbll.GetItemProp1List(para.itemId);
                    if (dsProp.Tables[0].Rows.Count > 0)
                    {
                        prop1List.Add( dsProp.Tables[0].AsEnumerable().Select(t => new
                        {
                            skuId = t["skuId"],
                            prop1DetailId = t["prop1DetailId"],
                            prop1DetailName = t["prop1DetailName"]
                        }).First());
                    }
                    if (c == 0)
                    {
                        prop1 = dsProp.Tables[0].AsEnumerable().Select(t => new
                        {
                            skuId = t["skuId"],
                            prop1DetailId = t["prop1DetailId"],
                            prop1DetailName = t["prop1DetailName"]
                        }).First();
                    }
                }
            }
            
            #endregion

            #region 限购处理
            int canBuyCount=-1;//可购买数量
            int singlePurchaseQty = GetEventItemInfo(para.eventId.ToString(), para.itemId);//活动商品限购数量
            if (!string.IsNullOrEmpty(userId))
            {
                if (singlePurchaseQty > 0)
                {
                    //会员采购数量
                    int purchaseCount = GetVipPurchaseQty(userId, para.eventId, para.itemId);
                    canBuyCount = singlePurchaseQty - purchaseCount;
                }
            }

            #endregion

            #region 获取商户信息 add by Henry 2014-10-10
            var customerBll = new t_customerBLL(loggingSessionInfo);
            var customerBasicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
            t_customerEntity customerEntity = customerBll.GetByCustomerID(loggingSessionInfo.CurrentUser.customer_id);           //获取商户名称
            var customerInfo = new
            {
                CustomerName = customerEntity == null ? "" : customerEntity.customer_name, //商户名称
                ImageURL = customerBasicSettingBll.GetSettingValueByCode("AppLogo"),      //商户Logo
                CustomerMobile = customerBasicSettingBll.GetSettingValueByCode("CustomerMobile")
            };
            #endregion

            var content = new
            {
                #region 组织属性
                itemId = detail.ItemID,
                itemName = detail.ItemName,
                salesPersonCount = detail.SalesPersonCount,
                useInfo = detail.UseInfo,
                tel = detail.Tel,
                endTime = detail.EndTime.Value.To19FormatString(),
                offersTips = detail.OffersTips,
                prop1Name = detail.Prop1Name,
                prop2Name = detail.Prop2Name,
                itemCategoryName = detail.ItemCategoryName,
                itemCategoryId = detail.ItemCategoryID,
                itemIntroduce = detail.ItemIntroduce,
                itemParaIntroduce = detail.ItemParaIntroduce,
                //salesCount = detail.MonthSalesCount.HasValue ? detail.MonthSalesCount.Value.ToString("0.##") : "0",
                salesCount=detail.SalesCount,   //销量 update by Henry 2014-11-12
                beginLineSecond=GetBeginLineSecond(detail.BeginTime.Value),
                deadlineTime = detail.deadlineTime,
                discountRate = detail.DiscountRate == null ? 0 : Convert.ToDecimal((detail.DiscountRate / 10).Value.ToString("0.0")),//update by Henry 2014-10-20
                addedTime = detail.AddTime.Value.To19FormatString(),
                deadlineSecond = detail.RemainingSec,
                beginTime = detail.BeginTime.Value.To19FormatString(),
                qty = detail.Qty,
                overQty = detail.RemainingQty,
                stopReason = detail.StopReason,
                status = detail.Status,
                eventId = detail.EventId,
                eventTypeID=detail.EventTypeID,
                imageList = imagelist,
                skuList = skulist, //数组
                skuInfoList = skuInfoList, //数组
                salesUserList = salesUserList,
                storeInfo = storeInfo,
                brandInfo = brandInfo,
                skuInfo = skuInfo,
                prop1List = prop1List, //数组
                prop1 = prop1, //object
                canBuyCount=canBuyCount,
                singlePurchaseQty=singlePurchaseQty,
                serviceDescription=detail.ServiceDescription,
                itemSortId=detail.ItemSortId,
                CustomerInfo = customerInfo
                #endregion
            };
            return content;
        }
        /// <summary>
        /// 获取开始时间戳
        /// </summary>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public long GetBeginLineSecond(DateTime beginTime)
        {
            long beginLineSecond = 0;
         
           
            if (DateTime.Now < beginTime)
            {
                 TimeSpan tSpan = new TimeSpan();
                    tSpan = beginTime - DateTime.Now;
                beginLineSecond= Convert.ToInt64(tSpan.TotalSeconds);
            }
            return beginLineSecond;
        }
        public void ExecProcPEventItemQty(SetOrderInfoReqPara para, T_InoutEntity pEntity, SqlTransaction tran)
        {
            this._currentDAO.ExecProcPEventItemQty(para, pEntity, tran);
        }

        private vwItemPEventDetailEntity GetByEventIDAndItemID(Guid eventId, string itemId)
        {
            var temp = this._currentDAO.GetByEventIDAndItemID(eventId, itemId);
            if (temp.Length > 0)
                return temp[0];
            else
                return null;
        }
        /// <summary>
        /// 获取会员购买活动的商品个数
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public int GetVipPurchaseQty(string vipId, Guid eventId,string itemId)
        {
            int purchaseCount = 0;//购买数量
            var temp = this._currentDAO.GetVipPurchaseQty(vipId, eventId,itemId);
            if (temp.Tables[0].Rows.Count > 0)
                purchaseCount = Convert.ToInt32(temp.Tables[0].Rows[0]["total_qty"]);
            return purchaseCount;
        }
        /// <summary>
        /// 获取活动商品限购数量
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public int GetEventItemInfo(string eventId, string itemid)
        {
            int singlePurchaseQty = 0;
            DataSet dsEventInfo = this._panicbuyingEventItemMappingDAO.GetEventItemInfo(eventId, itemid);
            if (dsEventInfo.Tables[0].Rows.Count > 0)
                singlePurchaseQty = int.Parse(dsEventInfo.Tables[0].Rows[0]["SinglePurchaseQty"].ToString());
            return singlePurchaseQty;
        }
        public vwItemPEventDetailEntity GetByEventIDAndItemID(Guid? eventId)
        {
            var temp = this._currentDAO.GetByEventIDAndItemID(eventId);
            if (temp.Length > 0)
                return temp[0];
            else
                return null;
        }

        private DataSet GetListData(GetPanicbuyingItemListReqPara para)
        {
            return this._currentDAO.GetListData(para);
        }

        private string GetImageUrl(string sourceUrl, string add)
        {
            var extend = sourceUrl.Split('.').Last();
            var temp = sourceUrl.Trim(extend.ToArray()).Trim('.');
            return temp + add + "." + extend;
        }
    }
}