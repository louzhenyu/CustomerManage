/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/31 15:58:37
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
    /// 表MHAdArea的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MHAdAreaDAO : BaseCPOSDAO, ICRUDable<MHAdAreaEntity>, IQueryable<MHAdAreaEntity>
    {
        #region 获取广告集合

        /// <summary>
        /// 获取广告集合  1 商品类型，2商品，3. 自定义链接 4商品分组
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetAdList(string homeId)
        {
            string sql = string.Empty;
            sql += " SELECT adId = a.AdAreaId ";
            sql += " , imageUrl = a.ImageUrl ";
            sql += " , displayIndex = a.DisplayIndex ";
            sql += " , objectId = a.ObjectId ";
            sql += " , typeId = a.ObjectTypeId ";
            sql += " ,url";
            sql += " , objectName = CASE a.ObjectTypeId ";
            sql += " WHEN 1 THEN (SELECT b.item_category_name FROM dbo.T_Item_Category b with(nolock) WHERE b.customerid='" + this.CurrentUserInfo.ClientID + "' and b.item_category_id = a.ObjectId and bat_id=1  ) ";
            sql += " WHEN 2 THEN (SELECT b.item_name FROM dbo.T_Item b with(nolock) WHERE  b.customerid='" + this.CurrentUserInfo.ClientID + "' and b.item_id = a.ObjectId ) ";
            sql += " WHEN 4 THEN (SELECT b.item_category_name FROM dbo.T_Item_Category b with(nolock) WHERE b.customerid='" + this.CurrentUserInfo.ClientID + "' and b.item_category_id = a.ObjectId and bat_id=2  ) ";
            sql += " ELSE '' END ";
            sql += " FROM dbo.MHAdArea a ";
            sql += " WHERE a.IsDelete = 0  ";
            sql += " AND a.HomeId = '" + homeId + "' ";
            sql += " ORDER BY a.DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetMHSearchArea(string homeId)
        {
            string sql = string.Empty;
            sql += "select	 * from MHSeachArea  a WHERE a.IsDelete = 0  ";
            sql += " AND a.HomeId = '" + homeId + "' ";
            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取客户下的图片广告集合
        /// </summary>
        /// <returns></returns>
        public MHAdAreaEntity[] GetByCustomerID()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select b.* from dbo.MobileHome a left join dbo.MHAdArea b on a.homeid=b.homeid  where a.customerid='{0}' and a.IsDelete=0 and b.IsDelete=0 and IsActivate=1", this.CurrentUserInfo.ClientID);

            List<MHAdAreaEntity> list = new List<MHAdAreaEntity>();
            using (var rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MHAdAreaEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //
            return list.ToArray();
        }
        
        public MHAdAreaEntity[] GetAdByHomeId(string strHomeId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select a.* from dbo.MHAdArea a  where a.homeid='{0}' and a.IsDelete=0 order by DisplayIndex", strHomeId);

            List<MHAdAreaEntity> list = new List<MHAdAreaEntity>();
            using (var rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MHAdAreaEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //
            return list.ToArray();
        }
        #endregion

        #region 删除广告数据
        public int DeleteAdByHomeId(string strHomeId)
        {
            string strSql = string.Format("Delete [dbo].[MHAdArea] where [HomeId]='{0}'", strHomeId);
            return Convert.ToInt32(SQLHelper.ExecuteScalar(strSql));
        }

        #endregion


        #region 获取活动区域数据

        /// <summary>
        /// 获取活动区域数据
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetEventInfo(string homeId)
        {
            string sql = string.Empty;
            sql += " SELECT eventAreaItemId = a.ItemAreaId ";
            sql += " ,areaFlag, typeId = b.EventTypeId ";//获取了typeId
            sql += " , eventId = a.EventId ";
            sql += " , itemId = a.ItemId ";
            sql += " , displayIndex = a.DisplayIndex ";
            sql += " , itemName = c.ItemName ";
            sql += " , imageUrl = c.ImageUrl ";
            sql += " , qty = c.Qty ";
            sql += " , price = c.Price ";
            sql += " , salesPrice = c.SalesPrice ";
            sql += " , discountRate = c.DiscountRate ";
            sql += " , remainingSec = c.RemainingSec ";
            sql += " , ShowStyle ";
            sql += " FROM dbo.MHItemArea a ";
            sql += " left JOIN dbo.PanicbuyingEvent b ON a.EventId = b.EventId ";
            sql += " left JOIN dbo.vwPEventItemDetail c ON a.EventId=c.EventId and a.ItemId = c.ItemID ";
            sql += " WHERE a.IsDelete = 0 ";
            sql += " AND a.HomeId = '" + homeId + "' ";
            sql += " ORDER BY a.DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetEventInfoByGroupId(string homeId, string strGroupId)
        {
            string sql = string.Empty;
            sql += " SELECT eventAreaItemId = a.ItemAreaId ";
            sql += " ,areaFlag, typeId = b.EventTypeId ";//获取了typeId
            sql += " , eventId = a.EventId ";
            sql += " , itemId = a.ItemId ";
            sql += " , displayIndex = a.DisplayIndex ";
            //sql += " , itemName = c.ItemName ";
            sql += " , imageUrl = a.ItemImageUrl ";
            //sql += " , imageUrl = (CASE WHEN a.areaFlag='eventList' THEN c.ImageUrl ELSE  a.ItemImageUrl END ) ";
            //sql += " , qty = c.Qty ";
            //sql += " , price = c.Price ";
            //sql += " , salesPrice = c.SalesPrice ";
            //sql += " , discountRate = c.DiscountRate ";
            sql += " , remainingSec = b.RemainingSec ";
            sql += " , ShowStyle ,b.EventName";
            sql += " FROM dbo.MHItemArea a ";
            sql += " left JOIN dbo.VwPanicBuyingEvent b ON a.EventId = b.EventId ";
            //sql += " left JOIN dbo.vwPEventItemDetail c ON a.EventId=c.EventId ";
            sql += " WHERE a.IsDelete = 0 ";
            sql += " AND a.HomeId = '" + homeId + "' ";
            sql += " AND a.GroupId = '" + strGroupId + "' ";
            sql += " ORDER BY a.DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetEventListInfoByGroupId(string homeId, string strGroupId)
        {
            string sql = string.Empty;
            sql += " SELECT eventAreaItemId = a.ItemAreaId ";
            sql += " ,areaFlag, typeId = b.EventTypeId ";//获取了typeId
            sql += " , eventId = a.EventId ";
            sql += " , itemId = a.ItemId ";
            sql += " , displayIndex = a.DisplayIndex ";
            //sql += " , itemName = c.ItemName ";
            //sql += " , imageUrl = a.ItemImageUrl ";
            sql += " , imageUrl = (CASE WHEN a.areaFlag='eventList' THEN c.ImageUrl ELSE  a.ItemImageUrl END ) ";
            //sql += " , qty = c.Qty ";
            //sql += " , price = c.Price ";
            //sql += " , salesPrice = c.SalesPrice ";
            //sql += " , discountRate = c.DiscountRate ";
            sql += " , remainingSec = b.RemainingSec ";
            sql += " , ShowStyle ,b.EventName";
            sql += " FROM dbo.MHItemArea a ";
            sql += " left JOIN dbo.VwPanicBuyingEvent b ON a.EventId = b.EventId ";
            sql += " left JOIN dbo.vwPEventItemDetail c ON a.EventId=c.EventId AND a.ItemId=c.ItemID";
            sql += " WHERE a.IsDelete = 0 ";
            sql += " AND a.HomeId = '" + homeId + "' ";
            sql += " AND a.GroupId = '" + strGroupId + "' ";
            sql += " ORDER BY a.DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 获取分类分组ID

        /// <summary>
        /// 获取分类分组ID
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetCategoryGroupId(string homeId)
        {
            string sql = string.Empty;
            sql += " SELECT GroupID ";
            sql += " FROM dbo.MHCategoryArea a ";
            sql += " WHERE a.IsDelete = 0 ";
            sql += " AND a.HomeId = '" + homeId + "' ";
            sql += " GROUP BY GroupID ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取商品集合

        /// <summary>
        /// 获取商品集合
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public DataSet GetItemList(string groupId, string homeId)
        {
            string sql = string.Empty;
            sql += " SELECT categoryAreaId = a.CategoryAreaId ";
            sql += " , displayIndex = a.DisplayIndex ";
            sql += " , imageUrl = a.ImageUrlObject ";
            sql += " , navName,url";
            sql += " , objectId = a.ObjectId ";
            sql += " , typeId = a.ObjectTypeId ";
            sql += " , objectName = CASE a.ObjectTypeId ";
            sql += " WHEN 1 THEN (SELECT b.item_category_name FROM dbo.T_Item_Category b WHERE b.item_category_id = a.ObjectId and bat_id=1) ";
            sql += " WHEN 2 THEN (SELECT b.item_name FROM dbo.T_Item b WHERE b.item_id = a.ObjectId) ";
            sql += " WHEN 4 THEN (SELECT b.item_category_name FROM dbo.T_Item_Category b WHERE b.item_category_id = a.ObjectId and bat_id=2) ";
            sql += " ELSE '' END ";
            sql += " FROM dbo.MHCategoryArea a ";
            sql += " WHERE a.IsDelete = 0 AND a.GroupID = '" + groupId + "' ";
            sql += " and homeId = '" + homeId + "'";
            sql += " ORDER BY a.DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetCategoryProductList(string groupId, string homeId, int intShowCount)
        {
            string sql = string.Empty;
            sql += string.Format(@"
                    DECLARE @ObjectId NVARCHAR(50)  
                    SELECT @ObjectId=ObjectId FROM dbo.MHCategoryArea
                    WHERE GroupID='{0}';

                    WITH tmp(item_category_id, parent_id)
                      AS(
                        SELECT  item_category_id ,
                                parent_id
                        FROM    T_Item_Category
                        WHERE   item_category_id =@ObjectId  
                        UNION ALL
                        SELECT  a.item_category_id ,
                                a.parent_id
                        FROM    T_Item_Category a
                                JOIN tmp b ON a.parent_id = b.item_category_id
                        WHERE   a.item_category_id IS NOT NULL
                    ) ", groupId);
            if (intShowCount > 0)
            {
                sql += "SELECT Top " + intShowCount + " ";
            }
            else
            {
                sql += "SELECT ";
            }
            sql += "  categoryAreaId = a.CategoryAreaId ";
            sql += " , displayIndex = a.DisplayIndex ";
            sql += " , navName,url";
            sql += " , objectId = a.ObjectId ";
            sql += " , typeId = a.ObjectTypeId,GroupId ";
            sql += " , objectName = CASE a.ObjectTypeId ";
            sql += " WHEN 1 THEN (SELECT b.item_category_name FROM dbo.T_Item_Category b WHERE b.item_category_id = a.ObjectId) ";
            sql += " WHEN 2 THEN (SELECT b.item_name FROM dbo.T_Item b WHERE b.item_id = a.ObjectId) ";
            sql += " ELSE '' END ";
            sql += " ,b.Price,b.SalesPrice";
            sql += " ,CASE WHEN b.Price IS NOT NULL AND b.Price<>0 AND b.SalesPrice<>b.Price THEN CAST(CAST(b.SalesPrice*10/b.Price AS DECIMAL(18,1)) AS NVARCHAR(10))+'折' WHEN b.Price IS  NULL OR b.Price=0 OR b.SalesPrice=b.Price THEN '' END DiscountRate";
            sql += " ,b.item_id AS ItemID,b.item_name AS ItemName,b.imageUrl ImageUrl";
            sql += " ,(  select  Cast(prop_value as DECIMAL) prop_value   from  t_prop as tp left join T_Item_Property  as tip on tip.prop_id=tp.prop_id where  tp.prop_code ='SalesCount' and item_id=b.item_id ) SalesCount";

            sql += " FROM dbo.MHCategoryArea a ";
            sql += "  INNER JOIN [vw_item_detail] b ON a.ObjectId=b.item_category_id ";
            sql += " WHERE a.IsDelete = 0 AND a.GroupID = '" + groupId + "' ";
            sql += " and homeId = '" + homeId + "'";
            sql += " ORDER BY a.DisplayIndex ";


            // 取skuid
            sql += @";SELECT sku_id,item_id FROM dbo.T_Sku sku WITH(nolock) WHERE EXISTS(SELECT 1 FROM dbo.MHCategoryArea a
            INNER JOIN [vw_item_detail] b ON a.ObjectId=b.item_category_id
                                             AND b.IsDelete = 0
                                             WHERE   a.IsDelete = 0
            
            AND a.GroupId = '" + groupId + @"'
            AND HomeId = '" + homeId + @"'
                                         AND b.item_id=sku.item_id) AND sku.status=1 ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetGroupProductList(string groupId, string homeId, int intShowCount)
        {
            string sql = string.Empty;
            if (intShowCount > 0)
            {
                sql += "SELECT Top " + intShowCount + " ";
            }
            else
            {
                sql += "SELECT ";
            }
            sql += "  categoryAreaId = a.CategoryAreaId ";
            sql += " , displayIndex = a.DisplayIndex ";
            sql += " , navName,url";
            sql += " , objectId = a.ObjectId ";
            sql += " , typeId = a.ObjectTypeId,GroupId ";
            sql += " , objectName = CASE a.ObjectTypeId ";
            sql += " WHEN 1 THEN (SELECT b.item_category_name FROM dbo.T_Item_Category b WHERE b.item_category_id = a.ObjectId) ";
            sql += " WHEN 2 THEN (SELECT b.item_name FROM dbo.T_Item b WHERE b.item_id = a.ObjectId) ";
            sql += " ELSE '' END ";
            sql += " ,b.Price,b.SalesPrice";
            sql += " ,CASE WHEN b.Price IS NOT NULL AND b.Price<>0 AND b.SalesPrice<>b.Price THEN CAST(CAST(b.SalesPrice*10/b.Price AS DECIMAL(18,1)) AS NVARCHAR(10))+'折' WHEN b.Price IS  NULL OR b.Price=0 OR b.SalesPrice=b.Price THEN '' END DiscountRate";
            sql += " ,b.item_id AS ItemID,b.item_name AS ItemName,b.imageUrl ImageUrl";
            sql += " ,(  select  Cast(prop_value as DECIMAL) prop_value   from  t_prop as tp left join T_Item_Property  as tip on tip.prop_id=tp.prop_id where  tp.prop_code ='SalesCount' and item_id=b.item_id ) SalesCount";
            //sql += ",(SELECT sku_id+',' FROM  dbo.T_Sku WHERE item_id=b.item_id FOR XML PATH('')) AS 'sku_ids'";

            sql += " FROM dbo.MHCategoryArea a ";
            sql += "    INNER JOIN [ItemCategoryMapping] c ON a.ObjectId=c.ItemCategoryId AND c.IsDelete=0";
            sql += "  INNER JOIN [vw_item_detail] b ON c.Itemid=b.item_id  AND b.IsDelete=0";
            sql += " WHERE a.IsDelete = 0 AND a.GroupID = '" + groupId + "' ";
            sql += " and homeId = '" + homeId + "'";
            sql += " ORDER BY a.DisplayIndex ";

            // 取skuid
            sql += @";SELECT sku_id,item_id FROM dbo.T_Sku sku WITH(nolock) WHERE EXISTS(SELECT 1 FROM dbo.MHCategoryArea a
            INNER JOIN [ItemCategoryMapping] c ON a.ObjectId = c.ItemCategoryId
                                                  AND c.IsDelete = 0
            INNER JOIN [vw_item_detail] b ON c.ItemId = b.item_id
                                             AND b.IsDelete = 0
                                             WHERE   a.IsDelete = 0
            AND a.GroupId = '" + groupId + @"'
            AND HomeId = '" + homeId + @"'
                                         AND b.item_id=sku.item_id) AND sku.status=1 ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetModelTypeIdByGroupId(string groupId)
        {
            string customerId = CurrentUserInfo.ClientID;
            string sql = string.Format("select styleType,titleName,titleStyle,modelTypeId,modelname as modelTypeName,ShowCount,ShowName,ShowPrice,ShowDiscount,DisplayIndex,ShowSalesPrice,ShowSalesQty from MHCategoryAreaGroup where groupValue = {0} and customerId = '{1}'   and IsDelete=0", groupId, customerId);//要把删除的排除掉

            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetModelTypeIdByGroupId(string groupId, string strHomeID)
        {
            string customerId = CurrentUserInfo.ClientID;
            string sql = string.Format("select GroupId as CategoryAreaGroupId,styleType,titleName,titleStyle,modelTypeId,modelname as modelTypeName,ShowCount,ShowName,ShowPrice,ShowDiscount,DisplayIndex,ShowSalesPrice,ShowSalesQty from MHCategoryAreaGroup where groupValue = {0} and customerId = '{1}' and HomeId='{2}'  and IsDelete=0", groupId, customerId, strHomeID);//要把删除的排除掉

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 更新商品分类区域表

        /// <summary>
        /// 更新商品分类区域表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateMHCategoryArea(MHCategoryAreaEntity entity)
        {
            string sql = string.Empty;
            sql += " UPDATE dbo.MHCategoryArea SET ";
            sql += " ObjectId = '" + entity.ObjectId + "', ";
            sql += " ObjectTypeId = '" + entity.ObjectTypeId + "', ";
            sql += " GroupID = '" + entity.GroupID + "', ";
            sql += " ObjectName = '" + entity.ObjectName + "', ";
            sql += " ImageUrlObject = '" + entity.ImageUrlObject + "', ";
            sql += " navName = '" + entity.navName + "', ";
            sql += " url = '" + entity.url + "', ";
            sql += " DisplayIndex = '" + entity.DisplayIndex + "' ";

            sql += " WHERE CategoryAreaId = '" + entity.CategoryAreaId + "' ";


            this.SQLHelper.ExecuteNonQuery(sql);
        }
        public void DeleteItemCategoryByGroupIdandHomeID(int GroupID, Guid HomeId)
        {
            string sql = string.Empty;
            List<SqlParameter> ls = new List<SqlParameter>();
            sql = @"delete from MHCategoryArea where HomeId=@HomeId and GroupId=@GroupId";
            ls.Add(new SqlParameter("@HomeId", HomeId));
            ls.Add(new SqlParameter("@GroupId", GroupID));
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, ls.ToArray());
        }
        public void DeleteCategoryGroupByGroupIdandCustomerId(int GroupID, string customerId)
        {
            string sql = string.Empty;
            List<SqlParameter> ls = new List<SqlParameter>();
            sql = @"delete from MHCategoryAreaGroup where customerId=@customerId and GroupValue=@GroupValue";
            ls.Add(new SqlParameter("@customerId", customerId));
            ls.Add(new SqlParameter("@GroupValue", GroupID));
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, ls.ToArray());
        }
        public void DeleteCategoryGroupByGroupIdandCustomerId(int GroupId, string customerId, string strHomeId)
        {
            string sql = string.Empty;
            List<SqlParameter> ls = new List<SqlParameter>();
            sql = @"delete from MHCategoryAreaGroup where customerId=@customerId and GroupId=@GroupId and HomeId=@HomeId";
            ls.Add(new SqlParameter("@customerId", customerId));
            ls.Add(new SqlParameter("@GroupId", GroupId));
            ls.Add(new SqlParameter("@HomeId", strHomeId));
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, ls.ToArray());
        }
        #endregion
    }
}
