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
    /// ҵ����  
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
        /// �°��ȡ��б�
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public object GetPanicbuyingEventList(GetPanicbuyingItemListReqPara para)
        {
            //��ȡ�����еĻ
            List<vwItemPEventDetailEntity> onEventItemlist = this._currentDAO.GetOnPanicbuyingEventList(para);
            //��ȡ�����Ƴ��Ļ
            List<vwItemPEventDetailEntity> soonEventItemlist = this._currentDAO.GetSoonPanicbuyingEventList(para);
            //ʵ�������ؼ���
            List<vwPanicbuyingEventEntity> eventList = new List<vwPanicbuyingEventEntity> { };

            #region ������
            if (onEventItemlist.Count > 0)
            {
                //���ݻ�Ϳ�ʼʱ��ɸѡ
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
                    onEntity.EventStatusDesc = "������";
                    onEntity.PanicbuyingItemList = detailOnList.ToArray();

                    eventList.Add(onEntity);
                }
            }
            #endregion
            #region �����Ƴ�
            if (soonEventItemlist.Count > 0)
            {
                //���ݻ�Ϳ�ʼʱ��ɸѡ
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
                    soonEntity.EventStatusDesc = "�����Ƴ�";
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
                throw new Exception("δ�ҵ���ػ��Ʒ��Ϣ");

            #region ��Ƭ�б�
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

            #region sku�б�
            var skubll = new SkuService(loggingSessionInfo);
            var ds = skubll.GetItemSkuListByEventId(para.itemId, para.eventId);
            var skulist = ds.Tables[0].AsEnumerable().Select(t => new
            {
                skuId = t["skuId"].ToString(),
                skuProp1 = t["skuProp1"].ToString(),
                skuProp2 = t["skuProp2"].ToString(),
                price = t["price"].ToString(),
                salesPrice = t["salesPrice"].ToString(),
                discountRate = t["discountRate"].ToString(),//�ۿ�
                integral = string.IsNullOrEmpty(t["integral"].ToString()) ? "0" : t["integral"].ToString(),
                qty = Convert.ToInt32(t["qty"] is DBNull ? "0" : t["qty"]),
                overQty = Convert.ToInt32(t["overQty"] is DBNull ? "0" : t["overQty"])
            }).ToArray();
            #endregion

            #region �����û��б�
            var inoutbll = new T_InoutBLL(loggingSessionInfo);
            var dsSalesUsers = inoutbll.GetItemEventSalesUserList(para.itemId, para.eventId);
            var salesUserList = dsSalesUsers.Tables[0].AsEnumerable().Select(t => new
                {
                    userId = t["userId"].ToString(),
                    imageURL = t["imageURL"].ToString()
                }).ToArray();
            #endregion

            #region �ŵ���Ϣ
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

            #region Ʒ����Ϣ
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

            #region sku��Ϣ
            object skuInfo = null;
            if (skulist.Count() > 0)
            {
                var sku = skulist[0];
                var info = skubll.GetSkuInfoById(sku.skuId);
                skuInfo = new
                {
                    skuId = sku.skuId,
                    prop1DetailId = info.prop_1_id,
                    prop2DetailId = info.prop_2_id
                };
            }
            #endregion

            #region ������Ϣ
            object prop1List = null;
            //update by wzq 20140724
            if (skulist.Count() > 0)
            {
                var dsProp = inoutbll.GetItemProp1List(skulist[0].skuId);
                //var dsProp = inoutbll.GetItemProp1List(para.itemId);

                if (dsProp.Tables[0].Rows.Count > 0)
                {
                    prop1List = dsProp.Tables[0].AsEnumerable().Select(t => new
                    {
                        skuId = t["skuId"],
                        prop1DetailId = t["prop1DetailId"],
                        prop1DetailName = t["prop1DetailName"]
                    });
                }
            }
            
            #endregion

            #region �޹�����
            int canBuyCount=-1;//�ɹ�������
            int singlePurchaseQty = GetEventItemInfo(para.eventId.ToString(), para.itemId);//���Ʒ�޹�����
            if (!string.IsNullOrEmpty(userId))
            {
                if (singlePurchaseQty > 0)
                {
                    //��Ա�ɹ�����
                    int purchaseCount = GetVipPurchaseQty(userId, para.eventId, para.itemId);
                    canBuyCount = singlePurchaseQty - purchaseCount;
                }
            }

            #endregion

            var content = new
            {
                #region ��֯����
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
                salesCount = detail.MonthSalesCount.HasValue ? detail.MonthSalesCount.Value.ToString("0.##") : "0",
                beginLineSecond=GetBeginLineSecond(detail.BeginTime.Value),
                deadlineTime = detail.deadlineTime,
                discountRate = detail.DiscountRate,
                addedTime = detail.AddTime.Value.To19FormatString(),
                deadlineSecond = detail.RemainingSec,
                beginTime = detail.BeginTime.Value.To19FormatString(),
                qty = detail.Qty,
                overQty = detail.RemainingQty,
                stopReason = detail.StopReason,
                status = detail.Status,
                eventId = detail.EventId,
                imageList = imagelist,
                skuList = skulist,
                salesUserList = salesUserList,
                storeInfo = storeInfo,
                brandInfo = brandInfo,
                skuInfo = skuInfo,
                prop1List = prop1List,
                canBuyCount=canBuyCount,
                singlePurchaseQty=singlePurchaseQty,
                serviceDescription=detail.ServiceDescription,
                itemSortId=detail.ItemSortId
                #endregion
            };
            return content;
        }
        /// <summary>
        /// ��ȡ��ʼʱ���
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
        /// ��ȡ��Ա��������Ʒ����
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public int GetVipPurchaseQty(string vipId, Guid eventId,string itemId)
        {
            int purchaseCount = 0;//��������
            var temp = this._currentDAO.GetVipPurchaseQty(vipId, eventId,itemId);
            if (temp.Tables[0].Rows.Count > 0)
                purchaseCount = Convert.ToInt32(temp.Tables[0].Rows[0]["total_qty"]);
            return purchaseCount;
        }
        /// <summary>
        /// ��ȡ���Ʒ�޹�����
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