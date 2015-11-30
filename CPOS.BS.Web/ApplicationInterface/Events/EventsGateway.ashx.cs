
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.IO;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;
using JIT.Utility;

using JIT.Utility.DataAccess.Query;
using System.Configuration;
using JIT.CPOS.Common;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Events
{
    /// <summary>
    /// EventsGateway 的摘要说明
    /// </summary>
    public partial class EventsGateway : BaseGateway
    {
        #region GetEventMerchandise 获取活动商品
        public string GetEventMerchandise(string pRequest)
        {
            EventMerchandiseRD rd = new EventMerchandiseRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<EventMerchandiseRP>>();
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                var bll = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);
                DataSet ds = bll.GetEventMerchandise(rp.Parameters.EventId);
                if (ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    rd.ItemList = DataTableToObject.ConvertToList<Item>(ds.Tables[0]);

                    foreach (var item in rd.ItemList)
                    {
                        DataSet skuds = bll.GetGetEventMerchandiseSku(item.EventItemMappingId.ToString());
                        if (skuds.Tables.Count > 0 && skuds.Tables[0] != null)
                        {
                            item.SkuList = DataTableToObject.ConvertToList<Sku>(skuds.Tables[0]);
                        }
                    }
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"Data\":{\"ItemList\":[{\"ItemID\":\"22\",\"ItemName\":\"美的\",\"ImageUrl\":\"http://www.o2omarketing.cn: 8400/Framework/Javascript/Other/kindeditor/attached/image/lzlj/album1.jpg\",\"SkuList\":[{\"kuID\":\"1111\",\"SkuName\":\"138L(银灰色)\",\"Qty\":\"250\",\"KeepQty\":\"200\",\"SoldQty\":\"30\",\"InverTory\":\"20\"}]}]}}";
        }

        #endregion

        #region RemoveEventItem 删除活动商品
        public string RemoveEventItem(string pRequest)
        {

            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<RemoveEventItemRP>>();
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

                //删除item
                var bll = new PanicbuyingEventItemMappingBLL(loggingSessionInfo);
                bll.Delete(new PanicbuyingEventItemMappingEntity { EventItemMappingId = rp.Parameters.EventItemMappingId });

                //删除item下的sku  add by donal 2014-11-13 10:27:56
                /*
                 以前删除团购商品时，没有删除团购商品下的sku，现在一并删除。提高数据准确性。
                 */
                var skuBll = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);
                skuBll.DeleteByItemMappingId(rp.Parameters.EventItemMappingId);
                var rsp = new SuccessResponse<IAPIResponseData>();
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }

            //return "{\"ResultCode\": 0,\"Message\": \"OK\"}";
        }
        #endregion

        #region RemoveEventItemSku 删除活动商品规格
        public string RemoveEventItemSku(string pRequest)
        {

            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<RemoveEventItemSkuRP>>();
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                var bll = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);
                var server = new PanicbuyingEventItemMappingBLL(loggingSessionInfo);

                var entity = bll.QueryByEntity(new PanicbuyingEventSkuMappingEntity { MappingId = rp.Parameters.MappingId }, null).FirstOrDefault();
                bll.Delete(new PanicbuyingEventSkuMappingEntity { MappingId = rp.Parameters.MappingId });

                var item = server.QueryByEntity(new PanicbuyingEventItemMappingEntity { EventItemMappingId = entity.EventItemMappingId }, null).FirstOrDefault();
                if (entity.Qty == null) entity.Qty = 0;
                if (entity.KeepQty == null) entity.KeepQty = 0;
                if (entity.SoldQty == null) entity.SoldQty = 0;

                item.Qty -= entity.Qty;
                item.KeepQty -= entity.KeepQty;
                item.SoldQty -= entity.SoldQty;
                server.Update(item);

                var rsp = new SuccessResponse<IAPIResponseData>();
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }


            // return "{\"ResultCode\": 0,\"Message\": \"OK\"}";
        }
        #endregion

        #region SaveEventItemSku 保存活动商品规格
        public string SaveEventItemSku(string pRequest)
        {

            try
            {

                var rp = pRequest.DeserializeJSONTo<APIRequest<SaveEventItemSkuRP>>();

                if (rp.Parameters.Qty < rp.Parameters.SoldQty)
                {
                    return "{\"ResultCode\": 100,\"Message\": \"保存失败!已售基数不能大于商品数量 \"}";
                }
                //if (rp.Parameters.Qty < (rp.Parameters.SoldQty + rp.Parameters.KeepQty))
                //{
                //    return "{\"ResultCode\": 100,\"Message\": \"保存失败!已售数量与真实数量之和 大于商品数量 \"}";
                //}
                if (rp.Parameters.Qty == 0)
                {
                    return "{\"ResultCode\": 100,\"Message\": \"保存失败!商品数量必须大于0 \"}";

                }
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                var bll = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);
                var server = new PanicbuyingEventItemMappingBLL(loggingSessionInfo);
                PanicbuyingEventSkuMappingEntity entity = bll.QueryByEntity(new PanicbuyingEventSkuMappingEntity { MappingId = rp.Parameters.MappingId }, null).FirstOrDefault();

                var item = server.QueryByEntity(new PanicbuyingEventItemMappingEntity { EventItemMappingId = entity.EventItemMappingId }, null).FirstOrDefault();
                item.Qty = item.Qty - entity.Qty + rp.Parameters.Qty;
                item.KeepQty = item.KeepQty - entity.KeepQty + rp.Parameters.KeepQty;
                if (item.SoldQty == null) item.SoldQty = 0;
                if (entity.SoldQty == null) entity.SoldQty = 0;
                item.SoldQty = item.SoldQty - entity.SoldQty + rp.Parameters.SoldQty;
                server.Update(item);
                entity.SalesPrice = rp.Parameters.SalesPrice;
                entity.Qty = rp.Parameters.Qty;
                entity.KeepQty = rp.Parameters.KeepQty;
                entity.SoldQty = rp.Parameters.SoldQty;
                entity.LastUpdateBy = loggingSessionInfo.UserID;
                bll.Update(entity, null);

                var rsp = new SuccessResponse<IAPIResponseData>();
                return rsp.ToJSON();

            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }

            // return "{\"ResultCode\": 0,\"Message\": \"OK\"}";
        }
        #endregion

        #region AddEventItemSku 添加活动商品规格
        public string AddEventItemSku(string pRequest)
        {

            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<AddEventItemSkuRP>>();


                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                var bll = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);


                var evententity = new PanicbuyingEventEntity();
                var ds = new PanicbuyingEventBLL(loggingSessionInfo).GetPanicbuyingEvent(rp.Parameters.EventId.ToString());
                if (ds != null && ds.Tables.Count > 0)
                {
                    evententity.BeginTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime"]);
                    evententity.EndTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime"]);
                }
                var entity = new PanicbuyingEventItemMappingBLL(loggingSessionInfo).QueryByEntity(new PanicbuyingEventItemMappingEntity { EventId = rp.Parameters.EventId, ItemId = rp.Parameters.ItemID }, null).FirstOrDefault();

                int disindex = new PanicbuyingEventItemMappingBLL(loggingSessionInfo).GteDisIndex(rp.Parameters.EventId.ToString());
                var skulist = rp.Parameters.SkuList;
                if (skulist == null || skulist.Count <= 0)
                {
                    return "{\"ResultCode\": 100,\"Message\": \"保存失败!未选择商品规格 \"}";
                }

                int myQty = 0;
                int myKeepQty = 0;
                foreach (var item in skulist)
                {
                    myQty += item.Qty;
                    myKeepQty += item.KeepQty;
                    if (item.Qty < item.SoldQty)
                    {
                        return "{\"ResultCode\": 100,\"Message\": \"保存失败!已售基数不能大于商品数量 \"}";
                    }
                    if (item.Qty == 0)
                    {
                        return "{\"ResultCode\": 100,\"Message\": \"保存失败!商品数量必须大于0 \"}";

                    }

                }
           

                if (entity == null)
                {
                    Guid myEventItemMappingId = Guid.NewGuid();
                    new PanicbuyingEventItemMappingBLL(loggingSessionInfo).Create(new PanicbuyingEventItemMappingEntity
                    {
                        EventItemMappingId = myEventItemMappingId,
                        EventId = rp.Parameters.EventId,
                        ItemId = rp.Parameters.ItemID,
                        BeginTime = evententity.BeginTime,
                        AddedTime=evententity.BeginTime,
                        Price=skulist[0].price,
                        SalesPrice=skulist[0].SalesPrice,
                        DiscountRate = decimal.Parse((skulist[0].SalesPrice / skulist[0].price).ToString("f2")) * 100,
                        EndTime = evententity.EndTime,
                        Qty = myQty,
                        KeepQty = myKeepQty,//
                        SoldQty = 0,//已售数量
                        DisplayIndex = disindex+1,
                        Status = 1,
                        IsDelete = 0,
                        CreateBy = loggingSessionInfo.UserID,
                        CreateTime = DateTime.Now,
                        SinglePurchaseQty=rp.Parameters.SinglePurchaseQty
                    });
                    foreach (var item in skulist)
                    {

                        bll.Create(new PanicbuyingEventSkuMappingEntity
                        {
                            MappingId = Guid.NewGuid(),
                            EventItemMappingId = myEventItemMappingId,
                            SkuId = item.SkuID,
                            Qty = item.Qty,
                            KeepQty = item.KeepQty,
                            SoldQty =0,   //这里怎么又变成0了
                            Status = 1,
                            IsDelete = 0,
                            CreateBy = loggingSessionInfo.UserID,
                            Price = item.price,
                            SalesPrice = item.SalesPrice
                        });
                    }
                }
                else
                {
                    rp.Parameters.EventItemMappingId = (Guid)entity.EventItemMappingId;
                    bll.DeleteEventItemSku(rp.Parameters.EventItemMappingId.ToString());
                    var PanicbuyingEventItemMappingentity = new PanicbuyingEventItemMappingBLL(loggingSessionInfo).GetByID(rp.Parameters.EventItemMappingId);


                    int mySoldQty = 0;
                    foreach (var item in skulist)
                    {
                        if (item.MappingId == null)
                        {
                            bll.Create(new PanicbuyingEventSkuMappingEntity
                            {
                                MappingId = Guid.NewGuid(),
                                EventItemMappingId = rp.Parameters.EventItemMappingId,
                                SkuId = item.SkuID,
                                Qty = item.Qty,
                               KeepQty=item.KeepQty, //KeepQty = 0,
                               SalesPrice = item.SalesPrice,
                                Status = 1,
                                IsDelete = 0,
                                CreateBy = loggingSessionInfo.UserID
                            });
                        }
                        else
                        {
                            var SkuMappingEntity = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo).GetEventItemSku(item.MappingId);
                            if (SkuMappingEntity != null)
                            {
                                if (SkuMappingEntity.SoldQty == null) SkuMappingEntity.SoldQty = 0;
                                mySoldQty += (int)SkuMappingEntity.SoldQty;
                                SkuMappingEntity.IsDelete = 0;
                                SkuMappingEntity.Qty = item.Qty;
                                SkuMappingEntity.KeepQty = item.KeepQty;
                                SkuMappingEntity.LastUpdateBy = loggingSessionInfo.UserID;
                                SkuMappingEntity.LastUpdateTime = DateTime.Now;
                                SkuMappingEntity.SalesPrice = item.SalesPrice;
                                bll.Update(SkuMappingEntity, null);
                            }


                        }


                    }
                    if (PanicbuyingEventItemMappingentity != null)
                    {
                        PanicbuyingEventItemMappingentity.KeepQty = myKeepQty;
                        PanicbuyingEventItemMappingentity.Qty = myQty;
                        PanicbuyingEventItemMappingentity.SoldQty = mySoldQty;
                        PanicbuyingEventItemMappingentity.SinglePurchaseQty = rp.Parameters.SinglePurchaseQty;
                        new PanicbuyingEventItemMappingBLL(loggingSessionInfo).Update(PanicbuyingEventItemMappingentity);
                    }
                }
                var rsp = new SuccessResponse<IAPIResponseData>();
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {

                throw;
            }
            // return "{\"ResultCode\": 0,\"Message\": \"OK\"}";
        }
        #endregion

        #region GetItemSku 获取活动商品规格
        public string GetItemSku(string pRequest)
        {
            //查询的语句是新的
            ItemSkuRD rd = new ItemSkuRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<ItemSkuRP>>();
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                var bll = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);
                var eventItemBll=new PanicbuyingEventItemMappingBLL(loggingSessionInfo);
                var entity = new PanicbuyingEventItemMappingBLL(loggingSessionInfo).QueryByEntity(new PanicbuyingEventItemMappingEntity { ItemId = rp.Parameters.ItemId, EventId = rp.Parameters.EventId }, null).FirstOrDefault();
                if (entity==null)
                {
                    entity = new PanicbuyingEventItemMappingEntity();
                    entity.EventItemMappingId = Guid.NewGuid();
                }
                //获取商品名称和限购数量
                DataSet dsEventItemInfo = eventItemBll.GetEventItemInfo(rp.Parameters.EventId.ToString(),rp.Parameters.ItemId);
                if (dsEventItemInfo.Tables[0].Rows.Count > 0 && dsEventItemInfo.Tables[0]!=null)
                {
                    rd.ItemName = dsEventItemInfo.Tables[0].Rows[0]["ItemName"].ToString();
                    rd.SinglePurchaseQty = int.Parse(dsEventItemInfo.Tables[0].Rows[0]["SinglePurchaseQty"].ToString());
                }

                DataSet ds = bll.GetItemSku(rp.Parameters.EventId.ToString(), rp.Parameters.ItemId, entity.EventItemMappingId.ToString());
                if (ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    rd.SkuList = DataTableToObject.ConvertToList<Sku>(ds.Tables[0]);
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }


            //return "{\"CustomerID\":\"c97fb511416d4370bfb1b87e62d54e0b\",\"Locale\":null,\"Token\":null,\"OpenID\":null,\"UserID\":\"\",\"JSONP\":null,\"Parameters\":{\"EventId\":\"1111\",\"ItemID\":\"1111\",\"SkuList\":[{\"MappingId\":\"11111111111111111111\",\"SkuName\":\"138L(银灰色)\",\"SkuID\":\"111111111111111\",\"Qty\":\"250\",\"KeepQty\":\"200\",\"SoldQty\":\"30\",\"InverTory\":\"20\"}]}}";
        }
        #endregion

        #region 活动列表、增、改、详情、活动商品排序 by Henry
        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetPanicbuyingEvent(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetPanicbuyingEventRP>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new PanicbuyingEventRD();
            var eventBll = new PanicbuyingEventBLL(loggingSessionInfo);
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (rp.Parameters.EventTypeId != 0)
            {
                complexCondition.Add(new EqualsCondition() { FieldName = "EventTypeId", Value = rp.Parameters.EventTypeId });
                complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            }
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "StatusValue", Direction = OrderByDirections.Desc });
            //查询

            var tempEvent = eventBll.GetPanicbuyingEvent(complexCondition.ToArray(), lstOrder.ToArray(), rp.Parameters.PageSize, rp.Parameters.PageIndex + 1);
            List<PanicbuyingEvent> eventList = new List<PanicbuyingEvent> { };
            eventList.AddRange(tempEvent.Entities.Select(t => new PanicbuyingEvent()
            {
                EventId = t.EventId,
                EventName = t.EventName,
                EventTypeId = t.EventTypeId,
                BeginTime = t.BeginTime.ToString("yyyy-MM-dd HH:mm"),
                EndTime = t.EndTime.ToString("yyyy-MM-dd HH:mm"),
                CustomerID = t.CustomerID,
                Qty = t.Qty,
                RemainQty = t.RemainQty,
                EventStatus = t.EventStatusStr
            }));
            rd.PanicbuyingEventList = eventList.ToArray();
            rd.TotalPage = tempEvent.PageCount;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true,\"Data\":{\"TotalPage\":1,\"PanicbuyingEventList\":[{\"CustomerID\":\"1E8CFF7F-214A-4DC2-BA1D-F61576A39824\",\"EventTypeId\":1,\"EventId\":\"1E8CFF7F-214A-4DC2-BA1D-F61576A39824\",\"EventName\":\"\u9524\u5B50\u624B\u673A\u56E2\u8D2D\",\"BeginTime\":\"2014-07-25 10:00\",\"EndTime\":\"2014-07-29 20:00\",\"Qty\":100,\"RemainQty\":10,\"EventStatus\":\"\u5DF2\u4E0A\u67B6\"},{\"CustomerID\":\"1E8CFF7F-214A-4DC2-BA1D-F61576A39824\",\"EventTypeId\":1,\"EventId\":\"1E8CFF7F-214A-4DC2-BA1D-F61576A39824\",\"EventName\":\"\u82F9\u679C\u624B\u673A\u56E2\u8D2D\",\"BeginTime\":\"2014-07-25 10:00\",\"EndTime\":\"2014-07-29 20:00\",\"Qty\":100,\"RemainQty\":10,\"EventStatus\":\"\u5DF2\u4E0A\u67B6\"}]}}";
        }
        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string AddPanicbuyingEvent(string pRequest)
        {
            var rd = new AddPanicbuyingEventRD();   //实例化返回参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddPanicbuyingEventRP>>();   //获取请求参数
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var eventBll = new PanicbuyingEventBLL(loggingSessionInfo);

            #region 判断参数是否有值
            //if (rp.Parameters.EventTypeId!=null)
            //    throw new APIException("请选择活动状态") { ErrorCode = 121 };
            #endregion

            #region 判断活动时间是否有交叉
            //查询参数
            //List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            //if (!string.IsNullOrEmpty(loggingSessionInfo.ClientID))
            //    complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID});
            //if (rp.Parameters.EventTypeId > 0)
            //    complexCondition.Add(new EqualsCondition() { FieldName = "EventTypeId", Value = rp.Parameters.EventTypeId });

            //PanicbuyingEventEntity[] eventList= eventBll.Query(complexCondition.ToArray(),null);
            //if (eventList.Count() > 0)
            //{
            //    foreach (var item in eventList)
            //    {
            //        //第一时间段的最大值 < 第二时间段的最小值 或者 第一时间段的最小值 > 第二时间段的最大值，说明时间没有交叉
            //        if (rp.Parameters.EndTime < item.BeginTime || rp.Parameters.BeginTime > item.EndTime)
            //        {}else
            //            throw new APIException("活动时间和" + item.EventName + "时间有交叉，请重新选择活动时间") { ErrorCode = 121 };
            //    }
            //}
            #endregion

            //判断时间是否在48小时内
            if (rp.Parameters.BeginTime == null || rp.Parameters.EndTime == null)
            {
                return "{\"ResultCode\": 100,\"Message\": \"时间不能为空!\"}";
            }
            else if ((rp.Parameters.EndTime-rp.Parameters.BeginTime).Days >=2)
            {
                return "{\"ResultCode\": 100,\"Message\": \"目前抢购只能设定48小时内!\"}";
            }
           
            PanicbuyingEventEntity entity = new PanicbuyingEventEntity();
            entity.EventName = rp.Parameters.EventName;
            entity.EventTypeId = rp.Parameters.EventTypeId;
            entity.EventStatus = rp.Parameters.EventStatus;
            //entity.CustomerID = rp.CustomerID;
            entity.BeginTime = rp.Parameters.BeginTime;
            entity.EndTime = rp.Parameters.EndTime;
            entity.CustomerID = loggingSessionInfo.ClientID;
            rd.EventID = eventBll.AddPanicbuyingEvent(entity, null);

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();

            //return "{\"ResultCode\":0,\"Message\":\"OK\"}";
        }
        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string UpdatePanicbuyingEvent(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddPanicbuyingEventRP>>();   //获取请求参数
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var eventBll = new PanicbuyingEventBLL(loggingSessionInfo);

            #region 判断活动时间是否有交叉
            ////查询参数
            //List<IWhereCondition> eventCondition = new List<IWhereCondition> { };
            //if (!string.IsNullOrEmpty(loggingSessionInfo.ClientID))
            //    eventCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            //if (rp.Parameters.EventTypeId > 0)
            //    eventCondition.Add(new EqualsCondition() { FieldName = "EventTypeId", Value = rp.Parameters.EventTypeId });

            //PanicbuyingEventEntity[] eventList = eventBll.Query(eventCondition.ToArray(), null);
            //if (eventList.Count() > 0)
            //{
            //    foreach (var item in eventList)
            //    {
            //        //第一时间段的最大值 < 第二时间段的最小值 或者 第一时间段的最小值 > 第二时间段的最大值，说明时间没有交叉
            //        if (rp.Parameters.EndTime < item.BeginTime || rp.Parameters.BeginTime > item.EndTime)
            //        { }
            //        else
            //            throw new APIException("活动时间和" + item.EventName + "时间有交叉，请重新选择活动时间") { ErrorCode = 121 };
            //    }
            //}
            #endregion

            //判断时间是否在48小时内
            //判断时间是否在48小时内
            if (rp.Parameters.BeginTime == null || rp.Parameters.EndTime == null)
            {
                return "{\"ResultCode\": 100,\"Message\": \"时间不能为空!\"}";
            }
            else if ((rp.Parameters.EndTime - rp.Parameters.BeginTime).Days >=10)
            {
                return "{\"ResultCode\": 100,\"Message\": \"目前抢购只能设定48小时内!\"}";
            }

            PanicbuyingEventEntity entity = new PanicbuyingEventEntity();
            entity.EventId = rp.Parameters.EventId;
            entity.EventName = rp.Parameters.EventName;
            entity.EventTypeId = rp.Parameters.EventTypeId;
            entity.EventStatus = rp.Parameters.EventStatus;
            entity.CustomerID = loggingSessionInfo.ClientID;
            entity.BeginTime = rp.Parameters.BeginTime;
            entity.EndTime = rp.Parameters.EndTime;
            eventBll.Update(entity);

            #region 修改商品状态

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "EventID", Value = rp.Parameters.EventId });

            var eventItemBll = new PanicbuyingEventItemMappingBLL(loggingSessionInfo);

            PanicbuyingEventItemMappingEntity[] eventItemList = eventItemBll.Query(complexCondition.ToArray(), null);
            if (eventItemList.Count() > 0)
            {
                int itemStatus = 0;
                switch (entity.EventStatus)
                {
                    case 10://未上架
                        itemStatus = -1;//无效
                        break;
                    case 20://已上架
                        itemStatus = 1;//有效
                        break;
                    case 30://已结束
                        itemStatus = -1;//无效
                        break;
                }
                foreach (var item in eventItemList)
                {
                    item.Status = itemStatus;
                    item.BeginTime = entity.BeginTime;
                    item.EndTime = entity.EndTime;
                    eventItemBll.Update(item);//修改商品状态，和老版本兼容
                }
            }
            #endregion

            var rsp = new SuccessResponse<IAPIResponseData>();
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":\"OK\"}";
        }
        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetPanicbuyingEventDetails(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddPanicbuyingEventRP>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var rd = new PanicbuyEventDetailRD();
            var eventBll = new PanicbuyingEventBLL(loggingSessionInfo);
            //查询
            var tempEvent = eventBll.GetPanicbuyingEventDetails(rp.Parameters.EventId);
            PanicbuyingEvent eventEntity = new PanicbuyingEvent();
            eventEntity.EventId = tempEvent.EventId;
            eventEntity.EventName = tempEvent.EventName;
            eventEntity.EventTypeId = tempEvent.EventTypeId;
            eventEntity.BeginTime = tempEvent.BeginTime.ToString("yyyy-MM-dd HH:mm");
            eventEntity.EndTime = tempEvent.EndTime.ToString("yyyy-MM-dd HH:mm");
            eventEntity.CustomerID = tempEvent.CustomerID;
            eventEntity.Qty = tempEvent.Qty;
            eventEntity.RemainQty = tempEvent.RemainQty;
            eventEntity.EventStatus = tempEvent.EventStatus.ToString();
            rd.PanicbuyingEvent = eventEntity;

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true,\"Data\":{\"PanicbuyingEvent\":[{\"CustomerID\":\"6176ffc2ae8144478a125eaea0ae438b\",\"EventTypeId\":1,\"EventId\":\"6176ffc2ae8144478a125eaea0ae438b\",\"EventName\":\"\u9524\u5B50\u624B\u673A\u56E2\u8D2D\",\"BeginTime\":\"2014-07-25 10:00\",\"EndTime\":\"2014-07-25 20:00\",\"Qty\":100,\"RemainQty\":10,\"EventStatus\":\"\u5DF2\u4E0A\u67B6\"}]}}";
        }
        /// <summary>
        /// 活动商品排序
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string UpdateEventItemDisplayIndex(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<UpdateDisplayIndexRP>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var eventBll = new PanicbuyingEventItemMappingBLL(loggingSessionInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrEmpty(rp.Parameters.EventID))
                complexCondition.Add(new EqualsCondition() { FieldName = "EventID", Value = rp.Parameters.EventID });
            //查询
            PanicbuyingEventItemMappingEntity[] eventList = eventBll.Query(complexCondition.ToArray(), null);

            PanicbuyingEventItemMappingEntity eventItem = eventList.Where(t => t.ItemId == rp.Parameters.ItemID).SingleOrDefault();
            PanicbuyingEventItemMappingEntity moveEventItem = eventList.Where(t => t.ItemId == rp.Parameters.MoveItemID).SingleOrDefault();

            int? tempIndex = eventItem.DisplayIndex;
            eventItem.DisplayIndex = moveEventItem.DisplayIndex;
            moveEventItem.DisplayIndex = tempIndex;

            eventBll.Update(eventItem);
            eventBll.Update(moveEventItem);

            //抽空加事务
            var rsp = new SuccessResponse<IAPIResponseData>();
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":\"OK\"}";
        }
        #endregion

        #region 分类列表、商品列表、保存图片信息 by Henry
        /// <summary>
        /// 商品一级分类查询
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetParentCategoryList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var rd = new ItemCategoryInfoRD();
            var itemCategoryBll = new ItemCategoryService(loggingSessionInfo);
            //获取一级分类
            var dsCategory = itemCategoryBll.GetLevel1ItemCategory(loggingSessionInfo.ClientID);
            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                var tmp = dsCategory.Tables[0].AsEnumerable().Select(t => new ItemCategoryInfo()
                {
                    CategoryId = t["CategoryId"].ToString(),
                    CategoryName = t["CategoryName"].ToString()
                });
                rd.ItemCategoryInfoList = tmp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true,\"Data\":{\"ItemCategoryInfoList\":[{\"CategoryId\":\"6176ffc2ae8144478a125eaea0ae438b\",\"CategoryName\":\"\u5BB6\u75351\"},{\"CategoryId\":\"6176ffc2ae8144478a125eaea0ae438b\",\"CategoryName\":\"\u5BB6\u75352\"},{\"CategoryId\":\"6176ffc2ae8144478a125eaea0ae438b\",\"CategoryName\":\"\u5BB6\u7535\"},{\"CategoryId\":\"6176ffc2ae8144478a125eaea0ae438b\",\"CategoryName\":\"\u5BB6\u75353\"},{\"CategoryId\":\"6176ffc2ae8144478a125eaea0ae438b\",\"CategoryName\":\"\u5BB6\u75354\"}]}}";
        }
        /// <summary>
        /// 商品列表查询
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetItemList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetItemListRP>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var rd = new ItemInfoListRD();
            var itemBll = new ItemService(loggingSessionInfo);
            //获取一级分类和递归子类下所有商品信息
            var dsItem = itemBll.GetItemList(loggingSessionInfo.ClientID, rp.Parameters.ItemCategoryID, rp.Parameters.ItemName, rp.Parameters.PageIndex, rp.Parameters.PageSize);
            int totalCount = itemBll.GetItemListCount(loggingSessionInfo.ClientID, rp.Parameters.ItemCategoryID, rp.Parameters.ItemName);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                var tmp = dsItem.Tables[0].AsEnumerable().Select(t => new ItemInfoList()
                {
                    ItemId = t["itemId"].ToString(),
                    ItemName = t["itemName"].ToString(),
                    ItemCategoryName = t["categoryName"].ToString()
                });
                rd.ItemInfoList = tmp.ToArray();
                int remainder = 0;
                rd.TotalPage = Math.DivRem(totalCount, rp.Parameters.PageSize, out remainder);
                if (remainder > 0)
                    rd.TotalPage++;
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true,\"Data\":{\"TotalPage\":1,\"ItemInfoList\":[{\"ItemId\":\"6176ffc2ae8144478a125eaea0ae438b\",\"ItemName\":\"\u51B0\u7BB1\",\"ItemCategoryName\":\"\u5BB6\u75351\"},{\"ItemId\":\"6176ffc2ae8144478a125eaea0ae438b\",\"ItemName\":\"\u6D17\u8863\u673A\",\"ItemCategoryName\":\"\u5BB6\u75351\"}]}}";
        }
        /// <summary>
        /// 保存图片信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SaveImage(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveImageRP>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var objectImageBll = new ObjectImagesBLL(loggingSessionInfo);


            ObjectImagesEntity newImages = new ObjectImagesEntity();
            newImages.ImageId = Guid.NewGuid().ToString();
            newImages.CustomerId = loggingSessionInfo.ClientID;
            newImages.ImageURL = rp.Parameters.ImageUrl;
            newImages.ObjectId = rp.Parameters.EventItemMappingId;

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrEmpty(rp.Parameters.EventItemMappingId))
                complexCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value = rp.Parameters.EventItemMappingId });
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            ObjectImagesEntity[] objectImageList = objectImageBll.Query(complexCondition.ToArray(), lstOrder.ToArray());
            if (objectImageList.Count() > 0)//如果此商品已有图片，则获取最后一个DisplayIndex++
                newImages.DisplayIndex = objectImageList[0].DisplayIndex + 1;
            else
                newImages.DisplayIndex = 0;

            objectImageBll.Create(newImages);

            var rsp = new SuccessResponse<IAPIResponseData>();
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":\"OK\"}";
        }
        #endregion

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = string.Empty;
            switch (pAction)
            {
                case "GetEventMerchandise":
                    rst = this.GetEventMerchandise(pRequest);
                    break;
                case "RemoveEventItem":
                    rst = this.RemoveEventItem(pRequest);
                    break;
                case "RemoveEventItemSku":
                    rst = this.RemoveEventItemSku(pRequest);
                    break;
                case "SaveEventItemSku":
                    rst = this.SaveEventItemSku(pRequest);
                    break;
                case "AddEventItemSku":
                    rst = this.AddEventItemSku(pRequest);
                    break;
                case "GetItemSku":
                    rst = this.GetItemSku(pRequest);
                    break;
                case "GetPanicbuyingEvent"://活动列表
                    rst = this.GetPanicbuyingEvent(pRequest);
                    break;
                case "AddPanicbuyingEvent"://创建活动
                    rst = this.AddPanicbuyingEvent(pRequest);
                    break;
                case "UpdatePanicbuyingEvent"://修改活动
                    rst = this.UpdatePanicbuyingEvent(pRequest);
                    break;
                case "GetPanicbuyingEventDetails"://活动详情
                    rst = this.GetPanicbuyingEventDetails(pRequest);
                    break;
                case "GetParentCategoryList"://获取商品一级分类
                    rst = this.GetParentCategoryList(pRequest);
                    break;
                case "GetItemList"://获取商品列表
                    rst = this.GetItemList(pRequest);
                    break;
                case "UpdateEventItemDisplayIndex"://活动商品排序
                    rst = this.UpdateEventItemDisplayIndex(pRequest);
                    break;
                case "SaveImage"://保存图片信息
                    rst = this.SaveImage(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
    }

    #region 获取活动商品/规格
    /// <summary>
    /// 请求参数
    /// </summary>
    public class EventMerchandiseRP : IAPIRequestParameter
    {
        public string EventId { set; get; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// 返回参数
    /// </summary>
    public class EventMerchandiseRD : IAPIResponseData
    {
        /// <summary>
        /// 商品集合
        /// </summary>
        public List<Item> ItemList { set; get; }

    }
    public class Item
    {
        public Guid EventItemMappingId { set; get; }
        public string ItemID { set; get; }
        public string ItemName { set; get; }
        public string ImageUrl { set; get; }

        public int SinglePurchaseQty { get; set; }

        public int displayindex { set; get; }
        /// <summary>
        /// 规格集合
        /// </summary>
        public List<Sku> SkuList { set; get; }

    }
    public class Sku
    {
        public Guid? MappingId { set; get; }
        public string SkuID { set; get; }

        public string SkuName { set; get; }

        public int Qty { set; get; }

        public int KeepQty { set; get; }

        public int SoldQty { set; get; }

        public int InverTory { set; get; }

        public string IsSelected { set; get; }

        public string StatusName { set; get; }

        public string Status { set; get; }

        public decimal price { set; get; }

        public decimal SalesPrice { set; get; }

    }

    #endregion

    #region 删除活动商品
    /// <summary>
    /// 请求参数
    /// </summary>
    public class RemoveEventItemRP : IAPIRequestParameter
    {
        public Guid EventItemMappingId { set; get; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 删除活动商品规格
    public class RemoveEventItemSkuRP : IAPIRequestParameter
    {
        public Guid MappingId { set; get; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 保存活动商品规格
    /// <summary>
    /// 请求参数
    /// </summary>
    public class SaveEventItemSkuRP : IAPIRequestParameter
    {
        public Guid MappingId { set; get; }

        public int Qty { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? SalesPrice { get; set; }
        public int KeepQty { set; get; }

        public int SoldQty { set; get; }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 活动商品规格获取
    public class ItemSkuRP : IAPIRequestParameter
    {
        public Guid? EventId { set; get; }

        public string ItemId { set; get; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }

    public class ItemSkuRD : IAPIResponseData
    {
        public string ItemName { get; set; }
        public int SinglePurchaseQty { get; set; }
        /// <summary>
        /// 商品集合
        /// </summary>
        public List<Sku> SkuList { set; get; }

    }

    #endregion

    #region 添加活动商品规格

    public class AddEventItemSkuRP : IAPIRequestParameter
    {

        public Guid EventId { set; get; }
        public string ItemID { set; get; }

        public Guid EventItemMappingId { set; get; }

        public int SinglePurchaseQty { get; set; }

        public List<Sku> SkuList { set; get; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region 参数对象：活动列表、增、改、详情、活动商品排序 by Henry
    /// <summary>
    /// 活动列表请求参数
    /// </summary>
    public class GetPanicbuyingEventRP : IAPIRequestParameter
    {
        /// <summary>
        /// 活动类型1=团购2=抢购3=热销
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示个数
        /// </summary>
        public int PageSize { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 活动列表返回参数
    /// </summary>
    public class PanicbuyingEventRD : IAPIResponseData
    {
        public PanicbuyingEvent[] PanicbuyingEventList { get; set; }
        public int TotalPage { get; set; }
    }
    /// <summary>
    /// 返回列表实体
    /// </summary>
    public class PanicbuyingEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String EventName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? EventTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }

        public string EventStatus { get; set; }

        public int Qty { get; set; }

        public int RemainQty { get; set; }
    }
    /// <summary>
    /// 添加活动参数对象
    /// </summary>
    public class AddPanicbuyingEventRP : IAPIRequestParameter
    {
        /// <summary>
        /// 活动ID,编辑时使用
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String EventName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? EventTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime { get; set; }

        public int EventStatus { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 添加活动返回对象
    /// </summary>
    public class AddPanicbuyingEventRD : IAPIResponseData
    {
        public string EventID { get; set; }
    }
    public class PanicbuyEventDetailRD : IAPIResponseData
    {
        public PanicbuyingEvent PanicbuyingEvent { get; set; }
    }
    /// <summary>
    /// 修改排序
    /// </summary>
    public class UpdateDisplayIndexRP : IAPIRequestParameter
    {
        public string EventID { get; set; }
        public string ItemID { get; set; }
        public string MoveItemID { get; set; }
        public void Validate()
        {
        }
    }
    #endregion

    #region 参数对象：分类列表、商品列表 by Henry
    /// <summary>
    /// 商品分类列表返回对象
    /// </summary>
    public class ItemCategoryInfoRD : IAPIResponseData
    {
        public ItemCategoryInfo[] ItemCategoryInfoList { get; set; }
    }
    /// <summary>
    /// 商品分类返回对象
    /// </summary>
    public class ItemCategoryInfo
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    /// <summary>
    /// 获取商品列表请求参数对象
    /// </summary>
    public class GetItemListRP : IAPIRequestParameter
    {
        public string ItemCategoryID { get; set; }
        public string ItemName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 获取商品列表返回对象
    /// </summary>
    public class ItemInfoListRD : IAPIResponseData
    {
        public ItemInfoList[] ItemInfoList { get; set; }
        public int TotalPage { get; set; }
    }
    /// <summary>
    /// 商品对象
    /// </summary>
    public class ItemInfoList
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCategoryName { get; set; }

    }
    /// <summary>
    /// 保存图片路径参数对象
    /// </summary>
    public class SaveImageRP : IAPIRequestParameter
    {
        public string EventItemMappingId { get; set; }
        public string ImageUrl { get; set; }
        public void Validate()
        {
        }
    }
    #endregion

}