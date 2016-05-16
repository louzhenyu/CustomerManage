/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:19
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// </summary>
    public partial class PanicbuyingEventBLL
    {
        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public string AddPanicbuyingEvent(PanicbuyingEventEntity pEntity, IDbTransaction pTran)
        {
            return this._currentDAO.AddPanicbuyingEvent(pEntity, pTran);
        }
        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<PanicbuyingEventEntity> GetPanicbuyingEvent(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetPanicbuyingEvent(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
        public PagedQueryResult<PanicbuyingEventEntity> GetPanicbuyingEventList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetPanicbuyingEventList(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
        public DataSet GetPanicbuyingEvent(string pEvenid)
        {
            return this._currentDAO.GetPanicbuyingEvent(pEvenid);

        }
        /// <summary>
        /// 获取活动详情
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public PanicbuyingEventEntity GetPanicbuyingEventDetails(object pID)
        {
            return this._currentDAO.GetPanicbuyingEventDetails(pID);
        }

        public DataSet BargainList(int pageIndex, int pageSize, string strEventName, int intEventStatus, string strBeginTime, string strEndTime)
        {
            return this._currentDAO.BargainList(pageIndex, pageSize, strEventName, intEventStatus, strBeginTime, strEndTime);
        }
        /// <summary>
        /// 根据主题id结束促销活动
        /// </summary>
        /// <param name="strCTWEventId"></param>
        public void EndOfEvent(string strCTWEventId)
        {
            this._currentDAO.EndOfEvent(strCTWEventId);
        }
        /// <summary>
        /// 根据主题id推迟促销活动
        /// </summary>
        /// <param name="strCTWEventId"></param>
        public void DelayEvent(string strCTWEventId, string strEndDate)
        {
            this._currentDAO.DelayEvent(strCTWEventId, strEndDate);
        }


        public List<KJEventItemInfo> GetKJEventWithItemList(string customerId)
        {
            List<KJEventItemInfo> eventItemList = new List<KJEventItemInfo>();
            DataSet ds = this._currentDAO.GetKJEventWithItemList(customerId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                eventItemList = DataTableToObject.ConvertToList<KJEventItemInfo>(ds.Tables[0]);
            }
            return eventItemList;
        }

        public KJEventItemDetailInfo GetKJEventWithItemDetail(string eventId, string ItemId)
        {
            KJEventItemDetailInfo eventItemDetail = null;
            DataSet ds = this._currentDAO.GetKJEventWithItemDetail(eventId, ItemId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                eventItemDetail = DataTableToObject.ConvertToObject<KJEventItemDetailInfo>(ds.Tables[0].Rows[0]);
            }
            return eventItemDetail;
        }
        public KJEventItemDetailInfo GetKJEventWithSkuDetail(string eventId, string SkuId)
        {
            KJEventItemDetailInfo eventItemDetail = null;
            DataSet ds = this._currentDAO.GetKJEventWithSkuDetail(eventId, SkuId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                eventItemDetail = DataTableToObject.ConvertToObject<KJEventItemDetailInfo>(ds.Tables[0].Rows[0]);
            }
            return eventItemDetail;
        }

        /// <summary>
        /// 处理砍价商品的库存销量相关信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="inoutDetailList"></param>
        public void SetKJEventOrder(string customerId, string orderId, string eventId, string KJEventJoinId, List<InoutDetailInfo> inoutDetailList)
        {
            PanicbuyingKJEventItemMappingBLL panicbuyingKJEventItemMappingBll = new PanicbuyingKJEventItemMappingBLL(CurrentUserInfo);
            PanicbuyingKJEventSkuMappingBLL panicbuyingKJEventSkuMappingBll = new PanicbuyingKJEventSkuMappingBLL(CurrentUserInfo);
            PanicbuyingEventOrderMappingBLL panicbuyingEventOrderMappingBll = new  PanicbuyingEventOrderMappingBLL(CurrentUserInfo);
            PanicbuyingKJEventJoinBLL panicbuyingKJEventJoinBll = new PanicbuyingKJEventJoinBLL(CurrentUserInfo);
            
            foreach(var i in inoutDetailList)
            {
                var itemEntity = panicbuyingKJEventItemMappingBll.GetPanicbuyingEventEntity(eventId,i.sku_id);
                itemEntity.SoldQty += Convert.ToInt32(i.enter_qty);
                itemEntity.LastUpdateTime = DateTime.Now;
                panicbuyingKJEventItemMappingBll.Update(itemEntity);

                var skuEntity = panicbuyingKJEventSkuMappingBll.QueryByEntity(new PanicbuyingKJEventSkuMappingEntity(){SkuID = i.sku_id,EventItemMappingID = itemEntity.EventItemMappingID.ToString()},null).FirstOrDefault();
                skuEntity.SoldQty += Convert.ToInt32(i.enter_qty);
                skuEntity.LastUpdateTime = DateTime.Now;
                panicbuyingKJEventSkuMappingBll.Update(skuEntity);
            }

            PanicbuyingEventOrderMappingEntity PanicbuyingEventOrderMappingEntity = new PanicbuyingEventOrderMappingEntity()
            {
                MappingId = Guid.NewGuid(),
                EventId = new Guid(eventId),
                OrderId = orderId,
                CustomerID = customerId
            };
            panicbuyingEventOrderMappingBll.Create(PanicbuyingEventOrderMappingEntity);

            var panicbuyingKJEventJoinEntity = panicbuyingKJEventJoinBll.GetByID(KJEventJoinId);
            panicbuyingKJEventJoinEntity.EventOrderMappingId = PanicbuyingEventOrderMappingEntity.MappingId;
            panicbuyingKJEventJoinBll.Update(panicbuyingKJEventJoinEntity);

        }
    }
}
