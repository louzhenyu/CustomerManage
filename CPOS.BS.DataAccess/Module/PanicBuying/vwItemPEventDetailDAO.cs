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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表vwItemPEventDetail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class vwItemPEventDetailDAO : Base.BaseCPOSDAO, ICRUDable<vwItemPEventDetailEntity>, IQueryable<vwItemPEventDetailEntity>
    {
        public DataSet GetListData(GetPanicbuyingItemListReqPara para)
        {
            StringBuilder sqlParaSub = new StringBuilder();
            sqlParaSub.AppendFormat(" and customerId='{0}'", this.CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(para.itemName))
                sqlParaSub.AppendFormat(" and itemName like '%{0}%'", para.itemName);
            if (para.eventId != null)
                sqlParaSub.AppendFormat(" and eventId ='{0}'", para.eventId);
            if (!string.IsNullOrEmpty(para.eventTypeId))
                sqlParaSub.AppendFormat(" and eventTypeId='{0}'", para.eventTypeId);
            if (!string.IsNullOrEmpty(para.itemTypeId))
                sqlParaSub.AppendFormat(" and itemCategoryId='{0}'", para.itemTypeId);
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(string.Format("select count(*) from vwItemPEventDetail where 1=1 {0}", sqlParaSub.ToString()));
            sql.AppendLine(string.Format("select * from (select row_number()over(order by displayindex) _row,* from vwItemPEventDetail where 1=1 {0}) t where t._row>{1}*{2} and t._row<=({1}+1)*{2}",
                sqlParaSub, (Convert.ToInt32(para.page) - 1) < 0 ? 0 : (Convert.ToInt32(para.page) - 1), para.pageSize));
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        /// <summary>
        /// 获取进行中的活动
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public List<vwItemPEventDetailEntity> GetOnPanicbuyingEventList(GetPanicbuyingItemListReqPara para)
        {
            StringBuilder sqlParaSub = new StringBuilder();
            sqlParaSub.AppendFormat(" and customerId='{0}'", this.CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(para.itemName))
                sqlParaSub.AppendFormat(" and itemName like '%{0}%'", para.itemName);
            if (para.eventId != null)
                sqlParaSub.AppendFormat(" and eventId ='{0}'", para.eventId);
            if (!string.IsNullOrEmpty(para.eventTypeId))
                sqlParaSub.AppendFormat(" and eventTypeId='{0}'", para.eventTypeId);
            if (!string.IsNullOrEmpty(para.itemTypeId))
                sqlParaSub.AppendFormat(" and itemCategoryId='{0}'", para.itemTypeId);
            sqlParaSub.AppendFormat(" and (BeginTime<GETDATE() and EndTime>GETDATE()) and (EventStatus is null or EventStatus=20 )");

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(string.Format("select count(*) from vwPEventItemDetail where 1=1 {0}", sqlParaSub.ToString()));
            sql.AppendLine(string.Format("select * from (select row_number()over(order by displayindex) _row,* from vwPEventItemDetail where 1=1 {0}) t where t._row>{1}*{2} and t._row<=({1}+1)*{2}",
                sqlParaSub, (Convert.ToInt32(para.page) - 1) < 0 ? 0 : (Convert.ToInt32(para.page) - 1), para.pageSize));
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());

            List<vwItemPEventDetailEntity> eventList = new List<vwItemPEventDetailEntity> { };
            using (var rd = ds.Tables[1].CreateDataReader())
            {
                while (rd.Read())
                {
                    vwItemPEventDetailEntity m;
                    this.Load(rd, out m);
                    eventList.Add(m);
                }
            }
            return eventList;
        }
        /// <summary>
        /// 获取即将推出的活动
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public List<vwItemPEventDetailEntity> GetSoonPanicbuyingEventList(GetPanicbuyingItemListReqPara para)
        {
            StringBuilder sqlParaSub = new StringBuilder();
            sqlParaSub.AppendFormat(" and customerId='{0}'", this.CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(para.itemName))
                sqlParaSub.AppendFormat(" and itemName like '%{0}%'", para.itemName);
            if (para.eventId != null)
                sqlParaSub.AppendFormat(" and eventId ='{0}'", para.eventId);
            if (!string.IsNullOrEmpty(para.eventTypeId))
                sqlParaSub.AppendFormat(" and eventTypeId='{0}'", para.eventTypeId);
            if (!string.IsNullOrEmpty(para.itemTypeId))
                sqlParaSub.AppendFormat(" and itemCategoryId='{0}'", para.itemTypeId);
            sqlParaSub.AppendFormat(" and BeginTime>GETDATE() and (EventStatus is null or EventStatus=20 )");

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(string.Format("select count(*) from vwPEventItemDetail where 1=1 {0}", sqlParaSub.ToString()));
            sql.AppendLine(string.Format("select * from (select row_number()over(order by displayindex) _row,* from vwPEventItemDetail where 1=1 {0}) t where t._row>{1}*{2} and t._row<=({1}+1)*{2}",
                sqlParaSub, (Convert.ToInt32(para.page) - 1) < 0 ? 0 : (Convert.ToInt32(para.page) - 1), para.pageSize));
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());

            List<vwItemPEventDetailEntity> eventList = new List<vwItemPEventDetailEntity> { };
            using (var rd = ds.Tables[1].CreateDataReader())
            {
                while (rd.Read())
                {
                    vwItemPEventDetailEntity m;
                    this.Load(rd, out m);
                    eventList.Add(m);
                }
            }
            return eventList;
        }


        public void ExecProcPEventItemQty(SetOrderInfoReqPara para, T_InoutEntity pEntity, SqlTransaction tran)
        {
            string sql = string.Format("exec spPEventItemQty '{0}','{1}','{2}','{3}'", para.customerId, para.eventId, pEntity.order_id, para.userId);
            if (tran != null)
                this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
            else
                this.SQLHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改活动库存
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="eventId"></param>
        /// <param name="orderId"></param>
        /// <param name="vipId"></param>
        /// <param name="tran"></param>
        public void ExecProcPEventItemQty(string customerId,string eventId,string orderId,string vipId,SqlTransaction tran)
        {
            string sql = string.Format("exec spPEventItemQty '{0}','{1}','{2}','{3}'",customerId,eventId,orderId,vipId);
            if (tran != null)
                this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
            else
                this.SQLHelper.ExecuteNonQuery(sql);
        }
        public vwItemPEventDetailEntity[] GetByEventIDAndItemID(Guid? eventId, string itemId)
        {
            List<vwItemPEventDetailEntity> list = new List<vwItemPEventDetailEntity> { };
            string sql = string.Format(@" select ISNULL(qty,0)-ISNULL(RemainingQty,0) AS SalesCount,ItemID,ItemCategoryID,ItemCode,ItemCode,ItemName,ItemNameEn,ItemNameShort,Pyzjm,ItemRemark	,
				 CreateBy,CreateTime,LastUpdateBy,LastUpdateTime,ImageUrl,CustomerID,Tel,ItemUnit,ItemSortId,EventId,EventTypeID,EventStatus
				 ,Qty,RemainingQty,BeginTime,EndTime,Status,AddedTime,RemainingSec,UseInfo,ServiceDescription,BuyType,OffersTips,IsOnline
				 ,Price,SalesPrice,convert(decimal(18,1),DiscountRate) as DiscountRate,DisplayIndex,IsFirst,StopReason,SalesPersonCount,DownloadPersonCount,BrandID,IsIAlumniItem
				 ,IsExchange,IntegralExchange,MonthSalesCount,ItemCategoryName,SkuId,Prop1Name,Prop2Name,ItemDisplayIndex,ItemTypeDesc
				 ,ItemSortDesc,SalesQty,Forpoints,ItemIntroduce,ItemParaIntroduce,ScanCodeIntegral,EdProp,FactoryName,GG,degree	,IsEveryoneSales,EveryoneSalesPrice
				 ,EventLastUpdateTime
             from	vwPEventItemDetail
                 where 1=1 and eventId='{0}' and itemId='{1}'", eventId, itemId);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    vwItemPEventDetailEntity m;
                    this.Load(rd, out m);
                    if (rd["ServiceDescription"] != DBNull.Value)
                        m.ServiceDescription = Convert.ToString(rd["ServiceDescription"]);
                    if (rd["ItemSortId"] != DBNull.Value)
                        m.ItemSortId = int.Parse(rd["ItemSortId"].ToString());
                    if (rd["SalesCount"] != DBNull.Value)
                        m.SalesCount = int.Parse(rd["SalesCount"].ToString());

                    list.Add(m);
                }
                return list.ToArray();
            }
        }
        /// <summary>
        /// 获取会员购买活动的商品个数
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public DataSet GetVipPurchaseQty(string vipId, Guid eventId, string itemId)
        {
            //            string sql = @"select ISNULL(total_qty,0) as total_qty from T_Inout a 
            //                        inner join PanicbuyingEventOrderMapping b on b.OrderId=a.order_id
            //                        where b.IsDelete=0 and a.vip_no='" +vipId+"' and b.EventId='"+eventId+"'";
            string sql = @"select sum(order_qty) as total_qty from T_Inout a 
                            inner join PanicbuyingEventOrderMapping b on b.OrderId=a.order_id
                            inner join T_Inout_Detail c on a.order_id=c.order_id
                            inner join vw_sku d on d.sku_id=c.sku_id
                            where  b.IsDelete=0 and d.item_id='" + itemId + "' and a.vip_no='" + vipId +
                            "' and a.STATUS<>800 and b.EventId='" + eventId + "'group by b.EventId";
            return this.SQLHelper.ExecuteDataset(sql);
        }

        public vwItemPEventDetailEntity[] GetByEventIDAndSkuID(Guid? eventId,string SkuID)
        {
            List<vwItemPEventDetailEntity> list = new List<vwItemPEventDetailEntity> { };
            string sql = string.Format("select * from [vwSkuPEventDetail] where 1=1 and eventId='{0}' and skuId='{1}'", eventId, SkuID);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    vwItemPEventDetailEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
                return list.ToArray();
            }
        }
    }
}
