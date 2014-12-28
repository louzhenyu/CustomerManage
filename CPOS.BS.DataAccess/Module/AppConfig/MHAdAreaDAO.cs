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
        /// 获取广告集合
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
            sql += ",url";
            sql += " , objectName = CASE a.ObjectTypeId ";

            sql += " WHEN 1 THEN (SELECT b.Title FROM dbo.LEvents b WHERE b.EventID = a.ObjectId) ";
            sql += " WHEN 2 THEN (SELECT b.NewsTitle FROM dbo.LNews b WHERE b.NewsId = a.ObjectId) ";
            sql += " WHEN 3 THEN (SELECT b.item_name FROM dbo.T_Item b WHERE b.item_id = a.ObjectId) ";
            sql += " WHEN 4 THEN (SELECT b.unit_name FROM dbo.t_unit b WHERE b.unit_id = a.ObjectId AND type_id = 'EB58F1B053694283B2B7610C9AAD2742') ";
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
            sql.AppendFormat("select b.* from dbo.MobileHome a left join dbo.MHAdArea b on a.homeid=b.homeid  where a.customerid='{0}' and a.IsDelete=0 and b.IsDelete=0 ", this.CurrentUserInfo.ClientID);

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
            sql += " FROM dbo.MHItemArea a ";
            sql += " INNER JOIN dbo.PanicbuyingEvent b ON a.EventId = b.EventId ";
            sql += " INNER JOIN dbo.vwPEventItemDetail c ON a.EventId=c.EventId and a.ItemId = c.ItemID ";
            sql += " WHERE a.IsDelete = 0 ";
            sql += " AND a.HomeId = '" + homeId + "' ";
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
            sql += " WHEN 1 THEN (SELECT b.item_category_name FROM dbo.T_Item_Category b WHERE b.item_category_id = a.ObjectId) ";
            sql += " WHEN 2 THEN (SELECT b.item_name FROM dbo.T_Item b WHERE b.item_id = a.ObjectId) ";
            sql += " ELSE '' END ";
            sql += " FROM dbo.MHCategoryArea a ";
            sql += " WHERE a.IsDelete = 0 AND a.GroupID = '" + groupId + "' ";
            sql += " and homeId = '" + homeId + "'";
            sql += " ORDER BY a.DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetModelTypeIdByGroupId(string groupId)
        {
            string customerId = CurrentUserInfo.ClientID;
            string sql = string.Format("select styleType,titleName,titleStyle,modelTypeId,modelname as modelTypeName from MHCategoryAreaGroup where groupValue = {0} and customerId = '{1}' and IsDelete=0", groupId, customerId);//要把删除的排除掉

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
        #endregion
    }
}
