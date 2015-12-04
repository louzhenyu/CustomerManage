using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;
using System.Configuration;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.DataAccess
{
    public class OnlineShoppingItemService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public OnlineShoppingItemService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region GetWelfareItemList
        /// <summary>
        /// 获取校友福利商品列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemName"></param>
        /// <param name="itemTypeId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="isKeep">true 已收藏列表， false 所有列表</param>
        /// <param name="isStore"></param>
        /// <param name="socialSalesType">类型(0=按订单；1=按商品)</param>
        /// <returns></returns>
        public DataSet GetWelfareItemList(string userId, string itemName, string itemTypeId, int page, int pageSize, bool isKeep, string isExchange, string storeId, string isGroupBy, string ChannelId, int isStore, int socialSalesType)
        {
            //page = page < 0 ? 0 : page;
            page = page <= 0 ? 0 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = page * pageSize + 1;
            int endSize = page * pageSize + pageSize;
            
            /*
             *modify by donal 2014-9-25 17:53:16
             * 
             * 增加人人销售后，如果渠道为人人销售（6），那么直显示可以人人销售的商品（人人销售价格不为0）
             * 注释的是以前的sql拼接，新的在下面
             * 
            string sql = GetWelfareItemListSql(userId, itemName, itemTypeId, isKeep, isExchange, storeId, isGroupBy);
            sql += string.Format(@" 
                    select * From #tmp a where 1=1 and UnixLocalTime BETWEEN UnixBeginTime AND UnixEndTime and a.displayIndex between '{0}' and '{1}' order by a.displayindex;            
                    select count(*) count From #tmp a where a.UnixLocalTime BETWEEN a.UnixBeginTime AND a.UnixEndTime; drop table #tmp;", beginSize, endSize);
             */

            StringBuilder dbdSql = new StringBuilder();
            dbdSql.Append(GetWelfareItemListSql(userId, itemName, itemTypeId, isKeep, isExchange, storeId, isGroupBy, ChannelId, socialSalesType,isStore));
            dbdSql.Append(" select *,CASE WHEN a.EventTypeId IS NULL THEN 'GoodsDetail'  ELSE 'GroupGoodsDetail'		END	 GoodsType From #tmp a where 1=1 ");

            dbdSql.Append(string.Format(@" and a.displayIndex between '{0}' and '{1}' order by a.displayindex;", beginSize, endSize));
            dbdSql.Append("select count(*) count From #tmp a where ");        
            dbdSql.Append("a.UnixLocalTime BETWEEN a.UnixBeginTime AND a.UnixEndTime; drop table #tmp;");                     

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(dbdSql.ToString());
            return ds;
        }
        //public int GetWelfareItemListCount(string userId, string itemName, string itemTypeId, bool isKeep, string isExchange, string storeId)
        //{
        //    string sql = GetWelfareItemListSql(userId, itemName, itemTypeId, isKeep, isExchange, storeId);
        //    sql += " select count(*) count From #tmp a ";
        //    DataSet ds = new DataSet();
        //    var obj = this.SQLHelper.ExecuteScalar(sql);
        //    return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemName"></param>
        /// <param name="itemTypeId"></param>
        /// <param name="isKeep"></param>
        /// <param name="isExchange"></param>
        /// <param name="storeId"></param>
        /// <param name="isGroupBy"></param>
        /// <param name="channelId"></param>
        /// <param name="isStore"></param>
        /// <param name="socialSalesType">类型(0=按订单；1=按商品)</param>
        /// <returns></returns>
        public string GetWelfareItemListSql(string userId, string itemName, string itemTypeId, bool isKeep, string isExchange, string storeId, string isGroupBy, string channelId,int socialSalesType,int isStore = 0)
        {

            string sql = @"SELECT   displayIndex = Row_number() over(order by isnull(t.ItemDisplayIndex,0),t.BeginTime DESC) ,* 
            into #tmp 
            FROM 
            (";
            sql += " SELECT itemId = a.item_id ";
            sql += " ,itemName = a.item_name ";
            sql += " ,imageUrl = a.imageUrl ";
            sql += " ,imageUrl2 = a.imageUrl2 ";
            sql += " ,imageUrl3 = a.imageUrl3 ";
            sql += " ,TargetUrl='aldlinks://product/list/' ";
            sql += " ,price = CASE WHEN D.ItemId IS  NULL THEN A.Price ELSE D.Price  END";//" ,price = a.Price ";
            sql += " ,salesPrice = CASE WHEN D.ItemId IS  NULL THEN A.SalesPrice  ELSE D.SalesPrice   END"; //" ,salesPrice = a.SalesPrice ";
            sql += " ,ItemDisplayIndex ";
            sql += " ,BeginTime";
            sql += " ,discountRate = a.DiscountRate ";
            //sql += " ,displayIndex = row_number() over(order by a.ItemDisplayIndex asc, a.BeginTime desc) ";
            sql += " ,pTypeId = a.PTypeId ";
            sql += " ,pTypeCode = a.PTypeCode ";
            sql += " ,CouponURL = a.CouponURL ";
            sql += " ,salesPersonCount = a.SalesPersonCount ";
            sql += " ,itemCategoryName = a.ItemCategoryName ";
            sql += " ,prop_2_detail_name = a.prop_2_detail_name ";
            sql += " ,sku_prop_id3 = a.sku_prop_id3 ";
            sql += " ,skuId = a.SkuId ";
            sql += " ,isShoppingCart = case when c.vipid is null then 0 else 1 end ";
            sql += ",CONVERT(NVARCHAR(10),a.CreateTime,120) createDate ";
            sql += " ,itemTypeDesc = a.itemTypeDesc ";
            sql += " ,itemSortDesc = a.itemSortDesc ";
            sql += " ,salesQty = a.salesQty ";
            sql += " ,remark = a.item_remark ";
            sql += " ,isExchange = a.IsExchange ";
            sql += " ,integralExchange = a.IntegralExchange ";
            sql += " ,salesCount = (CASE WHEN ISNUMERIC(a.Qty) = 1 THEN CONVERT(DECIMAL, a.Qty) ELSE 0 END) - a.OverCount ";
            sql += " ,endTime = a.EndTime";
            sql += " ,EveryoneSalesPrice = a.EveryoneSalesPrice";
            sql += ",CASE ISNULL(vIsstore.vipStoreID,'')  WHEN '' THEN 0 ELSE 1 END AS isStore ";
            sql += " ,ReturnAmount = a.ReturnAmount";
            sql += @",UnixLocalTime=DATEDIFF(MINUTE,'1900-01-01',GETDATE() ) 
                    ,UnixBeginTime=DATEDIFF(MINUTE,'1900-01-01',a.BeginTime ) 
                    ,UnixEndTime=DATEDIFF(MINUTE,'1900-01-01',a.EndTime ) ";
            //sql += " into #tmp ";
            sql += ",d.EventId";
            sql+=",d.EventTypeId";
            sql += " FROM dbo.vw_item_detail a ";

            //if (!string.IsNullOrEmpty(itemTypeId))
            //{
            //    sql += " inner join ItemCategoryMapping e on (e.IsDelete='0' and a.item_id=e.ItemId  and e.ItemCategoryId = '" + itemTypeId + "') ";
            //}
            if (isKeep)
            {
                sql += " INNER JOIN dbo.ItemKeep b ON b.CreateBy = '" + userId + "' ";
            }
            sql += " left join (select * From ShoppingCart where vipid = '" + userId + "' and qty > 0 and isdelete= '0' ) c on(a.skuId = c.skuId) ";

            //查询是否是小店商品
            sql += " LEFT JOIN dbo.VipStore vIsstore ON vIsstore.IsDelete=0 AND vIsstore.ItemID=a.item_id AND vIsstore.VIPID='" + userId + "'";
            //2015-09-21  wujianxian  如果某个商品有参加活动 则只显示活动价
            sql += " LEFT JOIN (SELECT  B.ItemId ,B.Price ,B.SalesPrice,B.Qty,a.EventId,a.EventTypeId  FROM    [dbo].[PanicbuyingEvent] A  INNER JOIN [dbo].[PanicbuyingEventItemMapping] B ON A.EventId = B.EventId   WHERE   CustomerID = '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' AND GETDATE() BETWEEN A.BeginTime AND a.EndTime AND b.IsDelete=0) D ON D.ItemId = A.item_id";

            //我的小店商品
            if (isStore == 1)
            {
                sql += " INNER JOIN vipstore vipStore ON a.item_id = vipStore.itemid AND vipStore.isdelete =0 AND vipStore.vipid='" + userId + "'";
            }

            //Jermyn20131008 餐饮门店关系
            if (storeId != null && !storeId.Equals(""))
            {
                sql += " INNER JOIN (SELECT * FROM ItemStoreMapping WHERE UnitId='" + storeId + "') d ON(a.item_id = d.ItemId) ";
            }
            //if (!string.IsNullOrEmpty(itemTypeId))
            //{
            //    sql += string.Format(" inner join fnGetChildCategoryByID('{0}',1) e on a.item_category_id=''", itemTypeId);
            //}


            //用UnixLocalTime条件代替，放在查询外面过滤
            //sql += " AND (a.BeginTime <= GETDATE() AND a.EndTime >= GETDATE()) ";
            if (!string.IsNullOrEmpty(itemTypeId))
            {
                sql += string.Format(" inner join (select CategoryID from fnGetChildCategoryByID('{0}',1)) e on a.item_category_id=e.CategoryID ", itemTypeId);
            }
            sql += " WHERE 1 = 1 and a.customerId = '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' ";
            if (!string.IsNullOrEmpty(itemName))
            {
                sql += " AND (a.item_name LIKE '%" + itemName + "%' OR a.prop_2_detail_name LIKE '%" + itemName + "%' OR a.sku_prop_id3 LIKE '%" + itemName + "%') "; //通过商品名、颜色、材质查询
            }
            //if (!string.IsNullOrEmpty(itemTypeId))
            //{
            //    sql += " AND (a.item_category_id = '" + itemTypeId + "' ) ";
            //}
            #region Jermyn20140526 新版本不需要
            //if (!string.IsNullOrEmpty(isExchange))
            //{
            //    sql += " AND a.isExchange = '" + isExchange + "' ";
            //}
            #endregion
            if (!string.IsNullOrEmpty(isGroupBy))
            {
                sql += " AND a.PTypeId = '2' "; //团购商品
            }
            //员工销售/会员创客时，并且按商品设置时执行
            if ((channelId == "6" || channelId == "10") && socialSalesType==1)
            {
                sql += " AND a.EveryoneSalesPrice != 0 ";
            }
            sql += @"  ) t
            where  UnixLocalTime BETWEEN UnixBeginTime AND UnixEndTime ";
            return sql;
        }
        #endregion

        #region 获取福利商品明细信息

        /// <summary>
        /// 获取福利商品明细信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public DataSet GetItemDetailByItemId(string itemId, string userId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" 
SELECT itemId = a.item_id 
,itemName = a.item_name 
,pTypeId = a.PTypeId 
,pTypeCode = a.PTypeCode
,buyType = a.BuyType
,buyTypeDesc = CASE a.BuyType WHEN '1' THEN '马上预订' WHEN '2' THEN '立即抢购' WHEN '3' THEN '卖完啦' END
,salesPersonCount = a.SalesPersonCount 
,downloadPersonCount = a.DownloadPersonCount 
,overCount = a.OverCount
,useInfo = a.UseInfo
,tel = a.Tel
,endTime = a.EndTime
,couponURL = a.CouponURL 
,offersTips = a.OffersTips 
,isKeep = (SELECT top 1 KeepStatus FROM dbo.ItemKeep b WHERE b.ItemId = a.item_id AND b.VipId = '{0}')
,isShoppingCart = (SELECT isnull(count(*),0) FROM dbo.ShoppingCart b WHERE b.isdelete='0' and b.qty>0 and b.skuId = a.skuId AND b.vipid = '{0}')
,prop1Name=a.Prop1Name
,prop2Name=a.Prop2Name
,prop3Name=a.Prop3Name
,itemCategoryId=a.item_category_id
,itemCategoryName=a.ItemCategoryName
,isProp2= case when a.Prop2Name is null or a.Prop2Name = '' then '0' else '1' end 
,a.ItemTypeDesc
,a.ItemSortDesc
,a.salesQty
,a.item_remark remark 
,isExchange = a.IsExchange
,integralExchange = a.IntegralExchange 
,Forpoints = case when a.Forpoints  = '' then 0.00 else CONVERT(decimal(18,0), ISNULL( a.Forpoints,CONVERT(decimal(18,0),100))) end
,GG =  case when a.GG  = '' then 0.00 else 
CONVERT(decimal(18,0), ISNULL( a.GG,CONVERT(decimal(18,0),100))) end
,a.ItemIntroduce
,a.ItemParaIntroduce
,RoomImg=a.RoomImg
,RoomDesc=a.RoomDesc
,CONVERT(decimal(18,0), ISNULL( discountRate,CONVERT(decimal(18,0),100))) discountRate
FROM dbo.vw_item_detail a 
WHERE a.item_id = '{1}' ", userId, itemId);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }

        /// <summary>
        /// 获取商品图片集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        /// 8月22日更新，把下载二维码时生成的图片给过滤掉，不显示
        public DataSet GetItemImageList(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT imageId = a.ImageId ";
            sql += " ,imageURL = a.ImageURL ";
            sql += " FROM dbo.ObjectImages a ";
            sql += " WHERE a.ObjectId = '" + itemId
                + "' and ISNULL(Description,'') != '自动生成的产品二维码' AND a.IsDelete = 0 order by a.DisplayIndex asc,a.createtime asc ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取商品sku集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSkuList(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT skuId = a.sku_id ";
            sql += " ,skuProp1 = a.prop_1_detail_name ";
            sql += " ,skuCode = a.prop_1_detail_code ";
            sql += " ,price = a.Price ";
            sql += " ,salesPrice = a.SalesPrice ";
            sql += " ,ReturnCash = a.ReturnCash ";
            sql += " ,discountRate = a.DiscountRate ";
            sql += " ,integral = a.Integral ";
            sql += " FROM dbo.vw_sku_detail a ";
            sql += " WHERE a.item_id = '" + itemId + "' and a.status='1' order by a.prop1DetailOrder,a.SalesPrice  ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 购买用户集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSalesUserList(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT DISTINCT a.vip_no AS userId, imageURL = '' ";
            sql += " FROM dbo.T_Inout a ";
            sql += " INNER JOIN dbo.T_Inout_Detail b ON a.order_id = b.order_id ";
            sql += " INNER JOIN dbo.T_Sku c ON b.sku_id = c.sku_id ";
            sql += " WHERE (a.vip_no IS NOT NULL AND a.vip_no <> '') ";
            sql += " AND c.item_id = '" + itemId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemStoreInfo(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT TOP 1 storeId = a.UnitId ";
            sql += " ,storeName = b.unit_name ";
            sql += " ,address = b.unit_address ";
            sql += " ,imageURL = b.imageURL ";
            sql += " ,storeCount = (SELECT COUNT(*) FROM dbo.ItemStoreMapping WHERE ItemId = '" + itemId + "' and IsDelete=0) ";
            sql += " FROM dbo.ItemStoreMapping a ";
            sql += " INNER JOIN dbo.t_unit b ON a.UnitId = b.unit_id ";
            sql += " WHERE a.ItemId = '" + itemId + "' and a.IsDelete=0";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemBrandInfo(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT brandId = a.BrandId ";
            sql += " ,brandLogoURL = b.BrandLogoURL ";
            sql += " ,brandName = b.BrandName ";
            sql += " ,brandEngName = b.BrandEngName ";
            sql += " FROM dbo.vw_item_detail a ";
            sql += " INNER JOIN dbo.BrandDetail b ON a.BrandId = b.BrandId ";
            sql += " WHERE a.item_id = '" + itemId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region Jermyn20131121获取 商品属性集合
        public DataSet GetItemProp1List(string itemId)
        {
            string sql = "SELECT x.prop1DetailId,x.prop1DetailName,MAX(skuId) skuId FROM ( SELECT DISTINCT a.sku_id skuId,a.sku_prop_id1 prop1DetailId,ISNULL(b.prop_name,a.sku_prop_id1) prop1DetailName,isnull(b.display_index,0) display_index FROM dbo.T_Sku a "
                        + " LEFT JOIN dbo.T_Prop b "
                        + " ON(a.sku_prop_id1 = b.prop_id "
                        + " AND b.status = '1') "
                        + " WHERE a.status = '1' "
                        + " AND a.item_id = '" + itemId + "' ) x GROUP BY x.prop1DetailId,x.prop1DetailName,x.display_index order by x.display_index ";
            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetItemProp2List(string itemId, string propDetailId)
        {
            string sql = "SELECT DISTINCT a.sku_id skuId,a.sku_prop_id2 prop2DetailId,ISNULL(b.prop_name,a.sku_prop_id2) prop2DetailName "
                        + " FROM dbo.T_Sku a "
                        + " LEFT JOIN dbo.T_Prop b "
                        + " ON(a.sku_prop_id2 = b.prop_id "
                        + " AND b.status = '1') "
                        + " WHERE a.status = '1' "
                        + " AND a.sku_prop_id1 = '" + propDetailId + "' "
                        + " AND a.item_id = '" + itemId + "' order by b.display_index ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        public DataSet GetOrderOnline(string OrderId)
        {
            string sql = @" 
select vworder.* ,inout.actual_amount as ActualAmount,inout.Field3 AS linkMan,inout.Field6 AS linkTel,inout.field4 AS address

From vw_online_order vworder
LEFT JOIN T_Inout inout ON vworder.OrderId=inout.order_id
where vworder.OrderId = '" + OrderId + "' ";
            //sql = "select * from T_Inout";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #region SetOrderAddress
        public void SetOrderAddress(JIT.CPOS.BS.Entity.Interface.SetOrderEntity pEntity)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
UPDATE dbo.T_Inout SET Field14='{1}',Field6='{2}',field4='{3}',Field8='{4}'
WHERE order_id='{0}'
 ", pEntity.OrderId, pEntity.linkMan, pEntity.linkTel, pEntity.address, pEntity.DeliveryId);
            this.SQLHelper.ExecuteNonQuery(sql.ToString());
        }
        #endregion

        #region GetVipValidIntegral
        /// <summary>
        /// 获取Vip会员积分
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <returns></returns>
        public string GetVipValidIntegral(string pVipID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"
                                select
                                    ISNULL(ValidIntegral,0) ValidIntegral
                                from VipIntegral
                                where VipID='{0}' and IsDelete=0", pVipID);
            DataSet ds = this.SQLHelper.ExecuteDataset(sqlStr.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["ValidIntegral"].ToString();
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region GetItemIntegral
        public decimal GetItemIntegral(string pItemID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"
                                select top 1
                                    IntegralExchange
                                from vw_item_detail
                                where item_id='{0}' and ISNULL(IsDelete,0)=0", pItemID);
            return Convert.ToDecimal(this.SQLHelper.ExecuteScalar(sqlStr.ToString()));
        }
        #endregion

        #region UpdateVIPIntegral
        /// <summary>
        /// 修改用户积分信息
        /// </summary>
        /// <param name="integral"></param>
        /// <param name="vipID"></param>
        /// <param name="pTran"></param>
        public void UpdateVIPIntegral(decimal integral, string vipID, SqlTransaction pTran)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"
update VipIntegral set ValidIntegral='{0}'
from VipIntegral
where VipID='{1}' and ISNULL(IsDelete,0)=0
", integral, vipID);

            if (pTran != null)
                this.SQLHelper.ExecuteScalar(pTran, CommandType.Text, sqlStr.ToString());
            else
                this.SQLHelper.ExecuteScalar(sqlStr.ToString());
        }
        #endregion

        #region GetOrderAmmount
        public decimal GetOrderAmmount(string pOrderID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"
SELECT actual_amount FROM T_Inout 
where order_id='{0}' ", pOrderID);
            return Convert.ToDecimal(this.SQLHelper.ExecuteScalar(sqlStr.ToString()));
        }
        #endregion

        #region GetOrderPayCenterCode
        public string GetOrderPayCenterCode(string pOrderID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"
SELECT paymentcenter_id FROM T_Inout WHERE order_id='{0}'
", pOrderID);
            return Convert.ToString(this.SQLHelper.ExecuteScalar(sqlStr.ToString()));
        }
        #endregion

        #region SetOrderPayCenterCode
        public void SetOrderPayCenterCode(string pOrderID, string paymentOrderID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"
update T_Inout set paymentcenter_id='{0}' 
FROM T_Inout WHERE order_id='{1}'", paymentOrderID, pOrderID);
            this.SQLHelper.ExecuteScalar(sqlStr.ToString());
        }
        #endregion

        public DataSet GetStoreItemDailyStatuses(string beginDate, string endDate, string storeId, string itemId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
select 
sds.StoreItemDailyStatusID,sds.StoreID,sds.ChannelID,sds.SkuID,sds.StatusDate,sds.CanReserveBeginTime,
sds.CanReserveEndTime,sds.StockAmount,sds.UsedAmount,sds.StorePrice,sds.LowestPrice,sds.ReservationRemark,
sds.NeedCreditCard,sds.NeedPrepay,sds.CancellationRemark,sds.Remark,sds.Price1,sds.Price2,sds.Price3,
sds.Price4,sds.Price5,sds.Price6,sds.Price7,sds.Price8,sds.Price9,sds.Price10,sds.Col1,
sds.Col2,sds.Col3,sds.Col4,sds.Col5,
sds.Col6,sds.Col7,sds.Col8,sds.Col9,sds.Col10,sds.ClientID,
sds.CreateBy,sds.CreateTime,sds.LastUpdateBy,sds.LastUpdateTime,sds.IsDelete 
from StoreItemDailyStatus sds
inner join t_sku sku on sds.SkuID=sku.Sku_ID 
where sds.Isdelete=0 and sku.status=1 and sds.StatusDate between '{0}' and '{1}'", beginDate, endDate);
            if (!string.IsNullOrEmpty(storeId))
                strSql.AppendFormat(" AND sds.StoreID='{0}'", storeId);
            if (!string.IsNullOrEmpty(itemId))
                strSql.AppendFormat(" AND sku.Item_ID='{0}'", itemId);
            strSql.Append(" order by sds.statusdate,sds.storeId,sku.item_id ");
            return SQLHelper.ExecuteDataset(strSql.ToString());
        }

        public DataSet GetStoreItemDailyStatuses(string beginDate, string endDate, string storeId, string itemId,string userId,string customerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
select 
sds.StoreItemDailyStatusID,sds.StoreID,sds.ChannelID,sds.SkuID,sds.StatusDate,sds.CanReserveBeginTime,
sds.CanReserveEndTime,sds.StockAmount,sds.UsedAmount,sds.StorePrice,dbo.Fn_GetVipDiscountRate('{2}','{3}')*sds.LowestPrice LowestPrice,sds.ReservationRemark,
sds.NeedCreditCard,sds.NeedPrepay,sds.CancellationRemark,sds.Remark,sds.Price1,sds.Price2,sds.Price3,
sds.Price4,sds.Price5,sds.Price6,sds.Price7,sds.Price8,sds.Price9,sds.Price10,sds.Col1,
sds.Col2,sds.Col3,sds.Col4,sds.Col5,
sds.Col6,sds.Col7,sds.Col8,sds.Col9,sds.Col10,sds.ClientID,
sds.CreateBy,sds.CreateTime,sds.LastUpdateBy,sds.LastUpdateTime,sds.IsDelete 
from StoreItemDailyStatus sds
inner join t_sku sku on sds.SkuID=sku.Sku_ID 
where sds.Isdelete=0 and sku.status=1 and sds.StatusDate between '{0}' and '{1}'", beginDate, endDate,userId,customerId);
            if (!string.IsNullOrEmpty(storeId))
                strSql.AppendFormat(" AND sds.StoreID='{0}'", storeId);
            if (!string.IsNullOrEmpty(itemId))
                strSql.AppendFormat(" AND sku.Item_ID='{0}'", itemId);
            strSql.Append(" order by sds.statusdate,sds.storeId,sku.item_id ");
            return SQLHelper.ExecuteDataset(strSql.ToString());
        }

        #region GetOrderIntegral
        /// <summary>
        /// 我的兑换记录
        /// </summary>
        /// <param name="pVipId"></param>
        /// <returns></returns>
        public DataSet GetOrderIntegral(string pVipId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
                a.OrderIntegralID
	            ,a.Quantity
	            ,a.Integral
	            ,a.IntegralAmmount
	            ,a.VIPID
	            ,a.LinkMan
	            ,a.LinkTel
	            ,a.Address
	            ,CONVERT(varchar(100),a.CreateTime,23) CreateTime
	            ,b.item_id ItemID
	            ,item_name ItemName
            from OrderIntegral a
            inner join T_Item b on a.ItemID=b.item_id and b.status=1
            where a.VIPID='{0}' and a.IsDelete=0", pVipId);
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(strSql.ToString());
            return ds;
        }
        #endregion

        #region GetProvince
        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetProvince()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select 
	            MAX(city_id) CityID
	            ,city1_name Province
	            ,MAX(city_code) CityCode
            from T_City
            group by city1_name
            order by CityCode");
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(strSql.ToString());
            return ds;
        }
        #endregion

        #region GetCityByProvince
        /// <summary>
        /// 根据省份名称获取城市名称
        /// </summary>
        /// <param name="pProvince"></param>
        /// <returns></returns>
        public DataSet GetCityByProvince(string pProvince)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            MAX(city_id) CityID
	            ,city2_name CityName
	            ,MAX(city_code) CityCode
            from T_City
            where city1_name='{0}'
            group by city2_name
            order by CityCode", pProvince);
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(strSql.ToString());
            return ds;
        }
        #endregion

        #region 花间堂_获取门店区域属性信息
        /// <summary>
        /// 花间堂_获取门店区域属性信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetStoreArea()
        {
            //组装SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            distinct unit_prop.property_value CityName
            from T_Prop prop
            inner join T_Unit_Property unit_prop on prop.prop_id=unit_prop.property_id and prop.status=unit_prop.status
            where prop.prop_code='Area' and prop.status=1");
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region GetStoreListByCityName
        /// <summary>
        /// 根据城市名称获取酒店列表,并且根据当前的坐标计算出酒店的距离
        /// 根据开始结束日期 计算酒店房态信息
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet GetStoreListByCityName(Dictionary<string, string> pParams)
        {

           //    -- ,convert(nvarchar(18),convert(decimal(18,2),property.MinPrice*dbo.Fn_GetVipDiscountRate('{0}','{7}') ) ) MinPrice
           //,convert(nvarchar(18),convert(decimal(18,0),   isnull((select	AVG(LowestPrice) from   StoreItemDailyStatus sids where IsDelete=0 
            //and sids.storeid=unit.unit_id   and StatusDate>=cast('{4}' as datetime)  and StatusDate<cast('{5}' as datetime)),0)*dbo.Fn_GetVipDiscountRate('{7}','{0}')      ) )	 as MinPrice
		
            //拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            SELECT 
	            DISTINCT unit.unit_id StoreID  
	            ,unit.unit_name StoreName
	            ,unit.imageURL  
	            ,unit.unit_address [Address]  
	            ,unit.unit_tel Tel  
	            ,unit.longitude Longitude  
	            ,unit.dimension Latitude  
	            ,case when {2} = 0 and {3} = 0 then 0 else ABS(dbo.DISTANCE_TWO_POINTS({2},{3},unit.dimension,unit.longitude)) end Distance  
	     	 ,  convert(nvarchar(18),convert(decimal(18,0), isnull(   (  select  AVG(LowestPrice)	 from StoreItemDailyStatus c				   
	INNER JOIN   dbo.vw_item_detail d	  ON 	c.SkuID=d.skuid
	inner join ItemStoreMapping   e		ON e.ItemId=d.item_id
	            WHERE e.UnitId = unit.unit_id and c.IsDelete = 0	AND d.isdelete=0  AND e.isDELETE=0            
	and StatusDate>=cast('{4}' as datetime) AND StatusDate<cast('{5}' as datetime)
)*dbo.Fn_GetVipDiscountRate('{7}','{0}')  ,0)                    )) as MinPrice
	


                ,1 IsFull
            into #tmp
            FROM t_unit unit 
            LEFT join VwUnitProperty property on unit.unit_id=property.UnitId  
            INNER JOIN dbo.T_City city ON unit.unit_city_id=city.city_id
            left join T_Unit_Property unit_prop on unit.unit_id=unit_prop.unit_id
            left join T_Prop prop on unit_prop.property_id=prop.prop_id
            WHERE unit.type_id='EB58F1B053694283B2B7610C9AAD2742' and prop.prop_code='Area'
            and unit_prop.property_value like '%{1}%' and unit.Status=1 AND unit.customer_id='{0}'
            order by StoreID
            
            select * from #tmp
            order by (case Distance when 0 then 999999 else Distance end),Distance

            declare @m varchar(max)='{6}'
            declare @p varchar(max)=''
            select 
                @p=@p+'['+CONVERT(varchar(20),a.value)+'],'
            from dbo.fnSplitStr(@m,',') a
            set @p=LEFT(@p,LEN(@p)-1)
            declare @sql varchar(max)=''
            set @sql='
            SELECT *
            from(
	            SELECT 
		            DISTINCT a.StoreID
		            ,a.SkuID
		            ,a.StatusDate
		            ,a.StockAmount
		            ,'''' IsFull
	            FROM dbo.StoreItemDailyStatus a
	            inner join #tmp b on a.StoreID=b.StoreID
	            inner join ItemStoreMapping ism on b.StoreID = ism.UnitId
	            inner join T_Sku sku on ism.ItemId=sku.item_id and sku.status=1
	            WHERE a.IsDelete=0 AND a.StockAmount>0 and sku.sku_id=a.SkuID
	            AND a.StatusDate>= ''{4}'' AND   a.StatusDate<''{5}''
	            --GROUP BY a.StoreID,a.SkuID,a.StatusDate,a.StockAmount
	            --Order By a.StoreID
            ) AS c PIVOT(MAX(StockAmount) FOR StatusDate IN ('+@p + ')
            ) b '
            EXEC(@sql)
            
            drop table #tmp", pParams["pCustomerID"], pParams["pCityName"], pParams["pLat"], pParams["pLng"], pParams["pBeginDate"], pParams["pEndDate"], pParams["pDate"], pParams["pVipID"]);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region GetStoreDetailByStoreID
        /// <summary>
        /// 根据酒店ID获取房间列表
        /// 根据开始结束日期 计算酒店房态信息
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet GetStoreDetailByStoreID(Dictionary<string, string> pParams)
        {
            //拼接SQL
            //Price 市场价根据会员的等级来折扣
            //VipLevel会员等级
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            --根据门店，5
            SELECT 
	            a.item_id ItemID,
	            a.item_name ItemName,
	            a.imageUrl ImageUrl,
	 --          a.Price ,       ---换成房价表的数据
(select convert(decimal(18,0), avg(c.SourcePrice))	 from StoreItemDailyStatus c where c.SkuID=a.SkuId	 and StatusDate>=cast('{1}' as datetime) AND StatusDate<cast('{2}' as datetime)  ) as Price,
			

                --vip级别名
                VipLevelName =  case when  dbo.Fn_GetVipDiscountRate('{4}','{0}') <> 1 then
                dbo.Fn_GetVipLevelName('{4}','{0}') else  '' end,
	            ---按照vip算的价格
      --       SalesPrice = case when  dbo.Fn_GetVipDiscountRate('{4}','{0}') = 1 then a.SalesPrice
       --    else  a.SalesPrice *dbo.Fn_GetVipDiscountRate('{4}','{0}') end,
 SalesPrice = case when  dbo.Fn_GetVipDiscountRate('{4}','{0}') = 1 then  convert(decimal(18,0), (select avg(c.LowestPrice)	 from StoreItemDailyStatus c where c.SkuID=a.SkuId	  and isdelete=0 and StatusDate>=cast('{1}' as datetime) AND StatusDate<cast('{2}' as datetime)   )	)
              else convert(decimal(18,0), dbo.Fn_GetVipDiscountRate('{4}','{0}')*(select avg(c.LowestPrice)	 from StoreItemDailyStatus c where c.SkuID=a.SkuId and isdelete=0	and StatusDate>=cast('{1}' as datetime) AND StatusDate<cast('{2}' as datetime)   ))	 end,
                
                
           

	            cast(a.DiscountRate as nvarchar(2000)) DiscountRate,
	            row_number() OVER ( ORDER BY a.ItemDisplayIndex ASC, a.BeginTime DESC ) DisplayIndex,
	            a.PTypeId TypeID,
	            a.PTypeCode TypeCode,
	            a.CouponURL CouponURL,
	            a.SalesPersonCount SalesPersonCount,
	            a.ItemCategoryName ItemCategoryName,
	            a.SkuId SkuID,
	            CASE WHEN c.vipid IS NULL THEN 0 ELSE 1 END IsShoppingCart,
	            CONVERT(NVARCHAR(10), a.CreateTime, 120) CreateDate ,
	            a.itemTypeDesc ItemTypeDesc,
	            a.itemSortDesc ItemSortDesc,
	            a.salesQty SalesQty,
	            a.item_remark Item_remark,
	            a.IsExchange IsExchange,
	            a.IntegralExchange IntegralExchange,
	            d.UnitId StoreID,
                1 IsFull
            INTO   #tmp
            FROM   dbo.vw_item_detail a
            LEFT JOIN ( 
	            SELECT  
	            *
	            FROM ShoppingCart
	            WHERE vipid = '{4}' AND qty > 0 AND isdelete = '0'
            ) c ON ( a.skuId = c.skuId )
            INNER JOIN ( 
	            SELECT 
	            *
	            FROM ItemStoreMapping
	            WHERE UnitId = '{3}' and IsDelete = '0'
            ) d ON ( a.item_id = d.ItemId )
            WHERE  1 = 1 AND a.customerId = '{0}' AND ( a.BeginTime <= cast('{1}' as datetime) AND a.EndTime >= cast('{2}' as datetime)
            )  

            SELECT
            *
            FROM #tmp a
            WHERE  1 = 1 AND a.displayIndex BETWEEN '{5}' AND '{6}' 
            ORDER BY a.displayindex

           declare @m varchar(max)='{7}'
            declare @p varchar(max)=''
            select 
                @p=@p+'['+CONVERT(varchar(20),a.value)+'],'
            from dbo.fnSplitStr(@m,',') a
            set @p=LEFT(@p,LEN(@p)-1)
            declare @sql varchar(max)=''
            
            set @sql='
            SELECT *
            from(
	            SELECT 
                a.StoreID
                ,a.StatusDate
                ,a.SkuID
                ,a.StockAmount
            FROM dbo.StoreItemDailyStatus a                   ---这里用了房价表
            inner join #tmp b on a.StoreID=b.StoreID
            WHERE a.IsDelete=0 AND a.StockAmount>0
            AND a.StatusDate BETWEEN ''{1}'' AND ''{2}''
            --GROUP BY a.StoreID,a.StatusDate,a.SkuID,a.StockAmount
            --Order By a.StoreID
            ) AS c PIVOT(MAX(StockAmount) FOR StatusDate IN ('+@p + ')
            ) b '
            EXEC(@sql)    -----执行动态sql

            drop table #tmp", pParams["pCustomerID"], pParams["pBeginDate"], pParams["pEndDate"], pParams["pStoreID"], pParams["pVipID"], pParams["pPage"], pParams["pPageSize"], pParams["pDate"]);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region GetProvinceOfHS
        /// <summary>
        /// 华硕校园获取省份信息_根据校园大使获取
        /// </summary>
        /// <returns></returns>
        public DataSet GetProvinceOfHS()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            --省
            SELECT 
	            DISTINCT city.city1_name Province
            FROM dbo.Vip vip
            INNER JOIN Viprolemapping vrm ON vip.vipid=vrm.vipid AND vip.IsDelete=vrm.isdelete AND vip.ClientID=vrm.clientid
            INNER JOIN dbo.T_Role trole ON vrm.roleid=trole.role_id AND trole.status=1 AND vrm.clientid=trole.customer_id
            LEFT JOIN dbo.T_City city ON vip.Col2=city.city_code
            WHERE  trole.role_code='CampusAmbassadors' AND vip.ClientID='{0}' AND vip.IsDelete=0
            and city.city1_name is not null", loggingSessionInfo.ClientID);
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(strSql.ToString());
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("获取省份ＳＱＬ：{0}", strSql.ToString())
            });
            return ds;
        }
        #endregion

        #region GetCityByProvinceOfHS
        /// <summary>
        /// 根据省份名称获取城市名称
        /// </summary>
        /// <param name="pProvince"></param>
        /// <returns></returns>
        public DataSet GetCityByProvinceOfHS(string pClientID, string pProvince)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            SELECT city.city2_name CityName,max(city.city_code) CityCode
            FROM dbo.Vip vip
            INNER JOIN Viprolemapping vrm ON vip.vipid=vrm.vipid AND vip.IsDelete=vrm.isdelete AND vip.ClientID=vrm.clientid
            INNER JOIN dbo.T_Role trole ON vrm.roleid=trole.role_id AND trole.status=1 AND vrm.clientid=trole.customer_id
            LEFT JOIN dbo.T_City city ON vip.Col2=city.city_code
            WHERE  trole.role_code='CampusAmbassadors' AND city.city1_name='{1}'
            AND vip.ClientID='{0}' 
            AND vip.IsDelete=0
            group by city2_name", pClientID, pProvince);
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(strSql.ToString());
            return ds;
        }
        #endregion

        #region GetSchoolListByCityNameOfHS
        /// <summary>
        /// 根据城市名称获取学校信息
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet GetSchoolListByCityNameOfHS(Dictionary<string, string> pParams)
        {
            //拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            SELECT DISTINCT vip.DeliveryAddress School
            FROM dbo.Vip vip
            INNER JOIN Viprolemapping vrm ON vip.vipid=vrm.vipid AND vip.IsDelete=vrm.isdelete AND vip.ClientID=vrm.clientid
            INNER JOIN dbo.T_Role trole ON vrm.roleid=trole.role_id AND trole.status=1 AND vrm.clientid=trole.customer_id
            LEFT JOIN dbo.T_City city ON vip.Col2=city.city_code
            WHERE  trole.role_code='CampusAmbassadors' AND city.city2_name='{1}'
            AND vip.ClientID='{0}' 
            AND vip.IsDelete=0", pParams["pCustomerID"], pParams["pCityName"]);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region GetRoleID
        /// <summary>
        /// 华硕校园_获取角色ID
        /// </summary>
        /// <param name="pParams"></param>
        public string GetRoleID(string pClientID)
        {
            //拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
                role_id
            from T_Role
            where role_code='ASUSMembership' and customer_id='{0}' and status=1", pClientID);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("GetRoleID: {0}", strSql.ToString())
            });
            return this.SQLHelper.ExecuteScalar(strSql.ToString()).ToString();
        }
        #endregion

        #region getWEventByPhone
        /// <summary>
        /// 获取华硕校园_报名人员信息
        /// </summary>
        /// <param name="pMobile"></param>
        /// <returns></returns>
        public DataSet getWEventByPhone(string pUserID)
        {
            //组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            a.VipName name
	            ,a.Phone phone
	            ,b.city1_name province
	            ,b.city2_name city
	            ,a.col15 school
                ,c.Mapping
            from Vip a
            left join T_City b on a.Col11=b.city_code
            left join WEventUserMapping c on a.VIPID=c.UserID
            where a.IsDelete=0  and a.VIPID='{0}'", pUserID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region UpdateWEventByPhone
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pEntity"></param>
        public void UpdateWEventByPhone(WEventUserMappingEntity pEntity)
        {
            //组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            update a set UserName='{2}',Email='{3}',Class='{4}'
            from WEventUserMapping a
            where Mobile='{1}' and IsDelete=0 and EventID='{0}'", pEntity.EventID, pEntity.Mobile, pEntity.UserName, pEntity.Email, pEntity.Class);
            this.SQLHelper.ExecuteNonQuery(strSql.ToString());
        }
        #endregion

        #region HS_GetVipDetail

        #region HS_GetVipDetail
        public DataSet HS_GetVipDetail(string vipID)
        {
            //组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select  
                a.VipName,
				a.Phone,
				a.DeliveryAddress School,
				a.Col3 Code,
				b.city1_name Province,
				b.city2_name City
            from Vip a
            left join T_City b on a.Col2=b.city_code
            where a.VIPID='{0}' and a.IsDelete=0", vipID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #endregion

        #region GetItemDetailByItemIdAndStoreID
        /// <summary>
        /// 获取商品明细信息
        /// </summary>
        /// <param name="pStoreID"></param>
        /// <param name="pItemID"></param>
        /// <returns></returns>
        public DataSet GetItemDetailByItemIdAndStoreID(string pStoreID, string pItemID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            select
	            unit.unit_id storeId
	            ,unit.unit_name storeName
	            ,unit.unit_address address
	            ,unit.imageURL imageURL
	            ,vw.item_name
            from ItemStoreMapping store
            inner join t_unit unit on store.UnitId=unit.unit_id
            inner join vw_item_detail vw on vw.item_id=store.ItemId
            where store.UnitId='{1}' and store.ItemId='{2}' and store.IsDelete=0 and unit.customer_id='{0}'", CurrentUserInfo.ClientID, pStoreID, pItemID);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        #endregion

        #region GetStoreListByArea
        /// <summary>
        /// 根据区域名称获取门店列表
        /// </summary>
        /// <param name="pAreaName"></param>
        /// <returns></returns>
        public DataSet GetStoreListByArea(string pAreaName)
        {
            string strWhere = null;
            if (!string.IsNullOrEmpty(pAreaName))
            {
                strWhere = string.Format(" and unit_prop.property_value like '%{0}%'", pAreaName);
            }

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            distinct unit.unit_name storeName
	            ,unit.unit_id storeId
            from t_unit unit
            left join T_Unit_Property unit_prop on unit.unit_id=unit_prop.unit_id
            left join T_Prop prop on unit_prop.property_id=prop.prop_id
            where unit.customer_id='{0}' and prop.prop_code='Area' {1}", CurrentUserInfo.ClientID, strWhere);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region Asus

        #region AmbassadorLoginIn
        /// <summary>
        /// 华硕校园 大使登录
        /// </summary>
        /// <returns></returns>
        public DataSet AmbassadorLoginIn(Dictionary<string, string> pParams)
        {
            //接收条件
            string strWhere = null;
            if (pParams.ContainsKey("pCode"))
            {
                strWhere += string.Format(" and vip.VipCode='{0}'", pParams["pCode"]);
            }
            if (pParams.ContainsKey("pPass"))
            {
                strWhere += string.Format(" and vip.VipPasswrod='{0}'", pParams["pPass"]);
            }

            //组装SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            vip.VIPID
	            ,vip.VipName
	            ,vip.VipRealName
	            ,vip.VipCode
	            ,vip.Phone
	            ,vip.City
	            ,vip.DeliveryAddress
	            ,vip.VipPasswrod
	            ,vip.Col1 Province
	            ,vip.Col2 CityCode
	            ,vip.ClientID
	            ,r.role_name RoleName
            from Vip vip
            left join VIPRoleMapping mapping on vip.VIPID=mapping.VIPID and mapping.ClientID=vip.ClientID and mapping.IsDelete=0
            left join T_Role r on mapping.RoleID=r.role_id and r.status=1 and r.customer_id=vip.ClientID 
            where vip.ClientID='{0}' and vip.IsDelete=0 and r.role_code='CampusAmbassadors' {1}", CurrentUserInfo.ClientID, strWhere);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// 根据用户名或手机号 查找会员列表
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet GetUserList(Dictionary<string, string> pParams)
        {
            //接收参数
            string strWhere = null;
            if (pParams.ContainsKey("pParam"))
            {
                strWhere += string.Format(" and (vip.VipName like '%{0}%' or vip.Phone like '%{0}%')", pParams["pParam"]);
            }
            int pageSize = 15;
            int pageIndex = 1;
            int outint = 0;
            if (pParams.ContainsKey("pageSize"))
            {
                pageSize = int.TryParse(pParams["pageSize"], out outint) == true ? outint : 0;
            }
            if (pParams.ContainsKey("pageIndex"))
            {
                pageIndex = int.TryParse(pParams["pageIndex"], out outint) == true ? outint : 0;
            }

            //组装SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            --获取大使信息
            select
	            vip.VIPID
	            ,vip.Col16
	            ,vip.VipCode
	            into #Tmp
            from Vip vip
            left join VIPRoleMapping mapping on vip.VIPID=mapping.VIPID and mapping.ClientID=vip.ClientID and mapping.IsDelete=0
            inner join T_Role r on mapping.RoleID=r.role_id and r.status!=-1 and r.customer_id=vip.ClientID
            where vip.ClientID='{0}' and vip.VIPID='{1}' and vip.IsDelete=0 and r.role_code='CampusAmbassadors' and ISNULL(vip.VipCode,'')<>''

            --获取大使发展的会员列表
            select
	            vip.VIPID
	            ,vip.VipName
	            ,vip.Phone
	            ,vip.Col15 DeliveryAddress
	            ,ISNULL(vip.VipRealName,'') VipRealName
	            ,r.role_name RoleName
	            ,case when(select COUNT (1) from T_Inout inout where inout.Field13=vip.phone and inout.customer_id=vip.ClientID and inout.status<>-1)>0 then '已下单' else '未下单' end as OrdersStatus
	            ,ISNULL(vip.Col16,'') Remark
                ,a.VipCode Code
                ,ROW_NUMBER() OVER(ORDER BY vip.CreateTime DESC) AS rownumber
            into #reslut
            from #tmp a
            inner join Vip vip on vip.Col13=a.VipCode 
            left join VIPRoleMapping mapping on vip.VIPID=mapping.VIPID and mapping.ClientID=vip.ClientID and mapping.IsDelete=0
            inner join T_Role r on mapping.RoleID=r.role_id and r.status!=-1 and r.customer_id=vip.ClientID
            where vip.ClientID='{0}' and vip.IsDelete=0 and r.role_code='ASUSMembership' {2}

            select * from #reslut where rownumber>{3} and rownumber<={4}
            select count(1) from #reslut

            drop table #tmp
            drop table #reslut", CurrentUserInfo.ClientID, pParams["pUserID"], strWhere, (pageIndex - 1) * pageSize, pageIndex * pageSize);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region GetOrderList
        /// <summary>
        /// 获取用户订单列表
        /// </summary>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        public DataSet GetOrderList(string pUserID, int pageSize, int pageIndex)
        {
            #region 组装SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            inout.order_no OrdersNo
                ,inout.order_id OrdersID
	            ,ISNULL(inout.Field12,'') VipName
	            ,ISNULL(inout.Field13,'') Phone
	            ,inout.Field8 Model
	            ,inout.order_date OrderDate
	            ,opt.OptionText BuyWay
	            ,opt2.OptionText GetWay
	            ,inout.field17 Price
                ,opt3.OptionText OrdersStatusText
                ,ROW_NUMBER() OVER(ORDER BY inout.create_time DESC) AS rownumber
            into #reslut
            from T_Inout inout
            left join Vip vip on inout.vip_no=vip.VIPID and inout.customer_id=vip.ClientID and vip.IsDelete=0
            left join Options opt on inout.Field11=opt.OptionValue and opt.OptionName='BuyWay' and opt.ClientID=inout.customer_id and opt.IsDelete=0
            left join Options opt2 on inout.Field15=opt2.OptionValue and opt2.OptionName='GetWay' and opt.ClientID=inout.customer_id  and opt2.IsDelete=0
            left join Options opt3 on inout.Field7=opt3.OptionValue and opt3.OptionName='TInOutStatus' and opt3.CustomerID=inout.customer_id and opt3.IsDelete=0
            where inout.customer_id='{0}' and vip.vipID='{1}'

            select * from #reslut where rownumber>{2} and rownumber<={3}
            select count(1) from #reslut

            drop table #reslut", CurrentUserInfo.ClientID, pUserID, (pageIndex - 1) * pageSize, pageIndex * pageSize);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region GetBuyWay
        /// <summary>
        /// 获取购买方式信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetBuyWay()
        {
            //组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            OptionName
	            ,OptionValue
	            ,OptionText
            from Options
            where ClientID='{0}' and IsDelete=0 and OptionName='BuyWay'", CurrentUserInfo.ClientID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region GetWay
        /// <summary>
        /// 获取客户获取方式信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetWay()
        {
            //组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            OptionName
	            ,OptionValue
	            ,OptionText
            from Options
            where ClientID='{0}' and IsDelete=0 and OptionName='GetWay'", CurrentUserInfo.ClientID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region ForgetPassWord
        /// <summary>
        /// 根据邮箱找回大使密码
        /// </summary>
        /// <param name="pEmail"></param>
        /// <returns></returns>
        public DataSet ForgetPassWord(string pEmail)
        {
            //组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            vip.VIPID
                ,vip.VipPasswrod
                ,vip.Col7 Email
            from Vip vip
            left join VIPRoleMapping mapping on vip.VIPID=mapping.VIPID and mapping.ClientID=vip.ClientID and mapping.IsDelete=0
            left join T_Role r on mapping.RoleID=r.role_id and r.status!=-1 and r.customer_id=vip.ClientID 
            where vip.ClientID='{0}' and vip.IsDelete=0 and r.role_code='CampusAmbassadors' and vip.Col7='{1}'", CurrentUserInfo.ClientID, pEmail);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #endregion

        #region CEIBS

        #region GetAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataSet GetAlbumList(string pVipID, int pageSize, int pageIndex)
        {
            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            --获取视频列表
            declare @pVipID nvarchar(200)='{0}'
            declare @pClientID nvarchar(200)='{1}'
            select
            *,
            BrowseCount=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=1 and news.NewsID=AlbumId and news.CustomerID=@pClientID and news.IsDelete=0
          
              
            ),
            PraiseCount=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=2 and news.NewsID=AlbumId and news.CustomerID=@pClientID and news.IsDelete=0
            ),
            KeepCount=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=3 and news.NewsID=AlbumId and news.CustomerID=@pClientID and news.IsDelete=0
            ),
            IsPraise=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=2 and news.NewsID=AlbumId and news.VipID=@pVipID and news.CustomerID=@pClientID and news.IsDelete=0
            ),
            IsKeep=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=3 and news.NewsID=AlbumId and news.VipID=@pVipID and news.CustomerID=@pClientID and news.IsDelete=0
            )
            ,ROW_NUMBER() OVER(ORDER BY SortOrder) AS rownumber
            into #tmp
            from LEventsAlbum
            where [Type]=2 and IsDelete=0 and customerid='{1}'
            
            select * from #tmp where rownumber>{2} and rownumber<={3}
            select count(1) from #tmp

            drop table #tmp", pVipID, CurrentUserInfo.ClientID, (pageIndex - 1) * pageSize, pageIndex * pageSize);
            #endregion
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region AddEventStats
        /// <summary>
        /// 浏览 收藏数据的操作
        /// </summary>
        /// <param name="pNewsID">数据ID</param>
        /// <param name="pVipID">用户ID</param>
        /// <param name="pCountType">操作类型 <1.BrowseCount(浏览数量) 2.PraiseCount(赞的数量) 3.KeepCount(收藏数量)> </param>
        /// <param name="pNewsType">数据类型 <1.咨询 2.视频 3.活动></param>
        /// <returns></returns>
        public int AddEventStats(string pNewsID, string pVipID, string pCountType, string pObjectType)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(pVipID))
            {
                strSql.AppendFormat(@"
            declare @pCountType int = {4}
            declare @pNewsType int = {3} --1.资讯 2.视频 3.活动
            declare @pVipID nvarchar(200) = '{2}'
            declare @pNewsID nvarchar(200) = '{1}'
            declare @pClientID nvarchar(200) = '{0}'
           declare @rowcount int
           declare @objectId nvarchar(50) 
           select @rowcount=count(1) from EventStatsDetail where VipID='{2}' AND IsDelete='0'  AND ObjectID='{1}'
            if @pCountType=1
              begin
                 update EventStats set BrowseNum=BrowseNum+1 where ObjectID='{1}' 
              end
            else if @pCountType=2 
            begin
                if @rowcount<=0
                 begin 
                    update EventStats set PraiseNum=PraiseNum+1 where ObjectID='{1}'
                 end
                 else 
                 begin
                   update EventStats set PraiseNum=PraiseNum-1 where ObjectID='{1}'  
                 end 
              end
            else if @pCountType=3
              begin
                update EventStats set BookmarkNum=BookmarkNum+1 where ObjectID='{1}' 
              end   
               if @rowcount<=0
               begin
               insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,CreateTime,IsDelete)
			   values('" + Guid.NewGuid().ToString() + @"','{1}','{3}','{4}','{2}','{0}','{2}','{5}','0')
              end
              else
              begin
                update EventStatsDetail set IsDelete='1' where VipID='{2}' AND CountType='{4}'  AND ObjectID='{1}'
              end
             ", CurrentUserInfo.ClientID, pNewsID, pVipID, pObjectType, pCountType, DateTime.Now.ToString());
            }
            else
            {
                strSql.AppendFormat(@"
            declare @pCountType int = {4}
            declare @pNewsType int = {3} -- 1.资讯 2.视频 3.活动
            declare @pVipID nvarchar(200) = '{2}'
            declare @pNewsID nvarchar(200) = '{1}'
            declare @pClientID nvarchar(200) = '{0}'
           declare @rowcount int
           declare @objectId nvarchar(50) 
           select @objectId=ObjectID from EventStats where ObjectID='{1}' 
            if @pCountType=1
              begin
                 update EventStats set BrowseNum=BrowseNum+1 where  ObjectID='{1}' 
              end
            else if @pCountType=2 
              begin
                    update EventStats set PraiseNum=PraiseNum+1 where ObjectID='{1}' 
             end
            else if @pCountType=3
              begin
                update EventStats set BookmarkNum=BookmarkNum+1 where ObjectID='{1}' 
              end 
  
              insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,CreateTime,IsDelete)
			  values('" + Guid.NewGuid().ToString() + @"','{1}','{3}','{4}','{2}','{0}','{2}','{5}','0')", CurrentUserInfo.ClientID, pNewsID, pVipID, pObjectType, pCountType, DateTime.Now.ToString());
            }

            return this.SQLHelper.ExecuteNonQuery(strSql.ToString());

        }
        #endregion

        #region GetEventStats
        public DataSet GetEventStats()
        {
            #region 拼接SQL
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(
                @"  select 
    a.NewsID as NewsID,es.EventStatsID as eventStatsID
   ,a.NewsType
   ,a.NewsTypeText
   ,es.BrowseNum as BrowseCount,es.PraiseNum as PraiseCount,es.BookmarkNum as KeepCount
   ,(es.BrowseNum+es.PraiseNum+es.BookmarkNum) as AllCount 
                ,a.Title
                ,ISNULL(a.ImageUrl,'') ImageUrl
                ,ISNULL(a.VideoUrl,'') VideoUrl
                ,ISNULL(a.Intro,'') Intro
                ,ISNULL(a.[Description],'') [Description]
                ,a.CreateTime
                ,datediff(HOUR,a.CreateTime,GETDATE()) AgoTime
 from EventStats as es
 inner join  Options as op on op.OptionValue=es.ObjectType  and op.OptionName='Eventtats' and op.ClientID='{0}'
 inner join(
 select 
                    AlbumId NewsID
                    ,1 NewsType
                    ,'视频' NewsTypeText
                    ,Title
                    ,ImageUrl
                    ,[Description] VideoUrl
                    ,Intro
                    ,Intro [Description]
                    ,CreateTime
                from LEventsAlbum 
                where IsDelete=0 And CustomerId='{0}'
                union all
                select 
                    EventID NewsID 
                    ,2 NewsType
                    ,'活动' NewsTypeText
                    ,Title
                    ,ImageUrl
                    ,'' VideoUrl
                    , Intro
                    ,[Description]
                    ,CreateTime
                from LEvents
                 where IsDelete=0 And CustomerId='{0}' 
                union all
                select 
                    NewsID
                    ,3 NewsType
                    ,'资讯' NewsTypeText
                    ,NewsTitle Title
                    ,ImageUrl
                    ,'' VideoUrl
                    ,Intro
                    ,Content [Description]
                    ,CreateTime
                from LNews
                where IsDelete=0 And CustomerId='{0}')a on a.NewsID=convert(nvarchar(max),es.ObjectID) where es.IsDelete='0'"
                , CurrentUserInfo.ClientID));

            #endregion



            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetEventAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataSet GetEventAlbumList(string pVipID, int pageSize, int pageIndex)
        {

            #region sql
            StringBuilder strb = new StringBuilder();
            strb.Append(string.Format(@"
                                select Le.*,Ev.BookmarkNum as KeepCount,Ev.BrowseNum as BrowseCount,Ev.PraiseNum as PraiseCount,
                                IsPraise=(
                                select COUNT(1) from EventStatsDetail as Ed where Ed.ObjectID=Le.AlbumId and IsDelete='0' and Ed.CustomerID='{0}'
                                        and Ed.VipID='{1}' and  Ed.CountType='2' 
                                 ),
                                IsKeep=(
                                select COUNT(1) from EventStatsDetail as Ed where Ed.ObjectID=Le.AlbumId and Ed.CustomerID='{0}'
                                   and Ed.CountType='3' and IsDelete='0' and Ed.VipID='{1}' 
                                )
                                ,ROW_NUMBER() OVER(ORDER BY SortOrder) AS rownumber
                                 into #tmp
                                from LEventsAlbum as Le
                                inner join EventStats as Ev on Le.AlbumId=Ev.ObjectID and Ev.IsDelete='0' and Ev.CustomerID='{0}'
                                where Le.Type=2 and Le.IsDelete=0 and Le.CustomerID='{0}'", CurrentUserInfo.ClientID, CurrentUserInfo.UserID));
            strb.Append(string.Format(@"select * from #tmp where rownumber>{1} and rownumber<={2}
                                     select count(1) from #tmp
                                     drop table #tmp", CurrentUserInfo.ClientID, (pageIndex - 1) * pageSize, pageIndex * pageSize));

            #endregion
            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion

        #region GetMostDetail
        /// <summary>
        /// 获取点赞数量
        /// </summary>
        /// <param name="pEventsID"></param>
        /// <returns></returns>
        public int GetMostDetail(string pEventsID)
        {
            string str = string.Format("select isnull(PraiseNum,0) PraiseNum from EventStats where ObjectID='{0}'", pEventsID);
            // return (int)this.SQLHelper.ExecuteScalar(str);
            DataSet ds = this.SQLHelper.ExecuteDataset(str);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    return (int)ds.Tables[0].Rows[0][0];
                }
            }
            return 0;
        }
        #endregion

        #region GetCourseInfo
        /// <summary>
        /// 获取课程详细 
        /// </summary>
        /// <param name="pCourseType">课程类型<1=MBA 2=EMBA 3=FMBA 4=高级经理课程></param>
        /// <returns></returns>
        public DataSet GetCourseInfo(string pCourseType)
        {
            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            top 1 CourseId
	            ,CouseDesc --课程介绍
	            ,CourseName --课程名字
	            ,CourseSummary --课程简介
	            ,CourseFee --课程费用
	            ,CourseStartTime
	            ,CouseCapital
	            ,CouseContact
            from ZCourse
            where CourseTypeId={1} and IsDelete=0 and ClientID='{0}'", CurrentUserInfo.ClientID, pCourseType);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public DataSet GetUserInfo(string pVipID)
        {
            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            VIPID
                ,VipName
                ,'' Hobby
                ,HeadImgUrl
                ,Col50 notApproveReson
                ,vip.Status
                ,opt.OptionText
                ,opt.OptionTextEn
                ,PageIndex=case when ISNULL(mpb.Sort,0)=0 then 2 else mpb.Sort end 
            from vip
            left join Options opt on vip.Status=opt.OptionValue and opt.OptionName='VipStatus' and opt.ClientID=vip.ClientID and opt.IsDelete=vip.IsDelete
            left join MobilePageBlock mpb on mpb.CustomerID=vip.ClientID and mpb.IsDelete=vip.IsDelete and mpb.Remark=CAST(vip.[Status] as nvarchar(200))
            where vip.VIPID='{1}' and vip.ClientID='{0}' and vip.IsDelete=0", CurrentUserInfo.ClientID, pVipID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region GetUserList
        public DataSet GetUserList(string pVipName, string pClass, string pCompany, string pCity, string pVipID)
        {
            #region 接收条件
            string strWhere = "";
            if (!string.IsNullOrEmpty(pVipName))
            {
                strWhere += string.Format(" and vip.VipName like '%{0}%'", pVipName);
            }
            if (!string.IsNullOrEmpty(pClass))
            {
                strWhere += string.Format(" and vip.Col3 like '%{0}%'", pClass);
            }
            if (!string.IsNullOrEmpty(pCompany))
            {
                strWhere += string.Format(" and vip.Col5 like '%{0}%'", pCompany);
            }
            if (!string.IsNullOrEmpty(pCompany))
            {
                strWhere += string.Format(" and vip.City like '%{0}%'", pCity);
            }
            #endregion

            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            update Vip 
	            set Col49=ISNULL(Vip.Col49,0)+1
            where VIPID='{2}'

            select
	            VIPID
                ,VipName
                ,Col6 PositionName
                ,HeadImgUrl
                ,'' Hobby
                ,opt.OptionText ClassName
                ,SearchCount=(
		            select
			            ISNULL(vip.Col49,0) SearchCount
		            from Vip
		            where VIPID='{2}'
                )
            from Vip vip
            left join Options opt on vip.Col3=opt.OptionValue and opt.OptionName='VipClass' and opt.ClientID=vip.ClientID and vip.IsDelete=opt.IsDelete
            where vip.ClientID='{0}' and vip.IsDelete=0 {1}", CurrentUserInfo.ClientID, strWhere, pVipID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region getEventstatsDetailByNewsID
        /// <summary>
        /// 根据资讯ID更新浏览量
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public DataSet getEventstatsDetailByNewsID(ReqData<getNewsDetailByNewsIDEntity> pEntity)
        {
            StringBuilder strb = new StringBuilder();

            strb.AppendFormat(@"
            update EventStats 
                set  BrowseNum=isnull(BrowseNum,0)+1  
            where ObjectID='{0}' and CustomerID='{1}'
            select 
                L.NewsId
                ,L.NewsTitle,L.NewsSubTitle
                ,L.Content,
                convert(nvarchar(10),L.PublishTime,120)as PublishTime
                ,L.ContentUrl,
                L.ImageUrl
                ,L.ThumbnailImageUrl
                ,L.Author,L.Intro
                ,nc.BrowseNum as BrowseCount
                ,nc.PraiseNum as PraiseCount
                ,nc.ShareNum as ShareCount
                ,nc.BookmarkNum as BookmarkCount
                ,L.CollCount
                ,nc.ObjectID", pEntity.special.NewsID,this.CurrentUserInfo.ClientID);
            if (CurrentUserInfo.UserID == "1")
            {

                strb.AppendFormat(",'false' isPraise");
            }
            else
            {
                strb.AppendFormat(",(select case when count(1)>0 then 'true' else 'false' end  from  EventStatsDetail where nc.ObjectID=ObjectID and CountType='2' and VipID='{0}') isPraise ", CurrentUserInfo.UserID);
            }
            strb.AppendFormat(@" from LNews  as L
            left join EventStats as nc on  nc.ObjectID=l.NewsID
            and nc.IsDelete=l.IsDelete and nc.IsDelete='0'
            and nc.ObjectType='3'
            where l.NewsId='{0}'",pEntity.special.NewsID);

            strb.AppendLine(string.Format(@" 
            insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,IsDelete)
			values('" + Guid.NewGuid().ToString() + @"','{1}','3','1','{2}','{0}','{2}','0')", CurrentUserInfo.ClientID, pEntity.special.NewsID, pEntity.common.userId));

            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion

        #region GetVipPriceDataCount
        /// <summary>
        ///获取用户商品价格
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public DataSet GetVipPayMent(string pVipID)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(@"
            select 
            Vip.VipName as vipName,It.item_id as itemId,item_code as itemCode,item_name as itemName,item_price as itemPrice
            from Vip as Vip 
            inner join VIPRoleMapping as Vpm on Vip.VIPID=Vpm.VipId 
            inner join ItemRoleMapping as Irm on Irm.RoleId=Vpm.RoleId
            inner join T_ITEM_PRICE as Ip on Ip.item_id=Irm.ItemId
            left join T_Item as It on It.item_id=Ip.item_id 
            where Vip.VIPID='{0}' and Vip.ClientID='{1}'", pVipID, CurrentUserInfo.ClientID));
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region SubmitVipPayMent
        /// <summary>
        /// 缴会费提交订单
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="ItemId"></param>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public int SubmitVipPayMent(T_InoutEntity pEntity, string ItemId, string VipId)
        {
           
            var tran = this.SQLHelper.CreateTransaction();
            int res = 0;
            using(tran.Connection)
            {
                try
                {
                    StringBuilder strb = new StringBuilder();
                    strb.AppendLine(string.Format(@"
                     declare @sku_id nvarchar(50)
                     select @sku_id=sku_id from T_Sku where item_id='{0}'", ItemId));
//                    strb.AppendLine(string.Format(@"declare @begin datetime
//                                                  declare @end datetime"));
//                    strb.Append(string.Format(@"select @begin=BeginTime,@end=EndTime from T_ITEM where item_id='{0}'",ItemId));
                    strb.AppendLine(@"Insert into T_Inout(order_id,order_no,order_type_id,order_reason_id,red_flag,warehouse_id,total_amount,status,status_desc,vip_no,customer_id,order_date,Field7)");
                    strb.AppendLine(string.Format(@"Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11},'{12}'')",
                    pEntity.order_id, pEntity.order_no, pEntity.order_type_id, pEntity.order_reason_id, pEntity.red_flag, pEntity.warehouse_id, pEntity.total_amount, pEntity.status, pEntity.status_desc, pEntity.vip_no, CurrentUserInfo.ClientID,pEntity.order_date,pEntity.Field7));

                    strb.AppendLine(@"Insert into T_Inout_Detail(");
                    strb.AppendLine(@"order_detail_id,order_id,sku_id,order_qty)");
                    strb.AppendLine(string.Format(@"values('{0}','{1}',@sku_id,'{2}')", Guid.NewGuid().ToString(), pEntity.order_id, pEntity.total_amount));

                    strb.AppendLine(@"Insert into VipPayMent(");
                    strb.AppendLine(string.Format(@"VipPayMentID,VipID,EventID,CreateBy,IsDelete,Fee)"));
                    strb.AppendLine(string.Format(@"Values('{0}','{1}','{2}','{3}','0','{4}')", Guid.NewGuid().ToString(), VipId, pEntity.order_id, CurrentUserInfo.UserID, pEntity.total_amount));

                    res = this.SQLHelper.ExecuteNonQuery(strb.ToString());
                    tran.Commit();
  
                 
                }
                catch (Exception )
                {

                    throw;
                }
               
            }
            return res;
        }
        #endregion

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pEntity"></param>
        public void UpdateOrderAddDeliveryAmount(JIT.CPOS.BS.Entity.Interface.SetOrderEntity pEntity)
        {
            //组织SQL
            List<SqlParameter> ls = new List<SqlParameter>();
            string strSql = @"update T_Inout set total_amount=@total_amount,actual_amount=@actual_amount where order_id=@orderId";
            ls.Add(new SqlParameter("@total_amount", pEntity.TotalAmount));
            ls.Add(new SqlParameter("@actual_amount", pEntity.ActualAmount));
            ls.Add(new SqlParameter("@orderId", pEntity.OrderId));
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql, ls.ToArray());
        }
    }
}