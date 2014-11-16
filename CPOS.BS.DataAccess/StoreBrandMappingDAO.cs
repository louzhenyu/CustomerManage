/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/30 11:11:11
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表StoreBrandMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class StoreBrandMappingDAO : Base.BaseCPOSDAO, ICRUDable<StoreBrandMappingEntity>, IQueryable<StoreBrandMappingEntity>
    {
        #region 获取商品门店集合
        public int GetStoreListByItemCount(string ItemId
            , int Page
            , int PageSize
            , string Longitude
            , string Latitude)
        {
            string sql = GetStoreListByItemSql(ItemId, Longitude, Latitude);
            sql += " select count(*) From #tmp ; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet GetStoreListByItem(string ItemId
            , int Page
            , int PageSize
            , string Longitude
            , string Latitude)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = Page * PageSize;
            DataSet ds = new DataSet();
            string sql = GetStoreListByItemSql(ItemId, Longitude, Latitude);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetStoreListByItemSql(string ItemId, string Longitude, string Latitude)
        {
            string sql = "SELECT x.* "
                        + " ,DisplayIndex = row_number() over(order BY x.Distance asc)  into #tmp "
                        + " FROM ( "
                        + " SELECT a.unit_id StoreId "
                        + " ,a.unit_name StoreName,a.imageURL "
                        + " ,a.unit_address [Address] "
                        + " ,a.unit_tel Tel "
                        + " ,isnull(a.longitude,0) Longitude "
                        + " ,isnull(a.dimension,0) Latitude "
                        + " ,case when '" + Latitude + "' = '0' and '" + Longitude + "' = '0' then '0' else ABS(dbo.DISTANCE_TWO_POINTS(" + Convert.ToDouble(Latitude) + "," + Convert.ToDouble(Longitude) + ",a.dimension,a.longitude)) end Distance "
                        + " FROM (select * From dbo.t_unit where type_id ='EB58F1B053694283B2B7610C9AAD2742' and status='1' and customer_id = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ) a ";
            if (ItemId != null && !ItemId.Equals(""))
            {
                sql += " INNER JOIN ItemStoreMapping b "
                 + " ON(a.unit_id = b.UnitId) "
                 + " WHERE b.ItemId = '" + ItemId + "' ";
            }
            sql += " ) x ";
            return sql;
        }
        #endregion

        #region 获取门店详细信息
        public DataSet GetStoreDetail(string StoreId)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.unit_id StoreId "
                        + " ,a.unit_name StoreName "
                        + " ,a.unit_address [Address] "
                        + " ,a.unit_tel Tel "
                        + " ,a.unit_fax Fax "
                        + " ,a.longitude Longitude "
                        + " ,a.dimension Latitude "
                        + " ,1 DisplayIndex "
                        + " ,b.BrandId  "
                        + " ,b.BrandName "
                        + " ,b.BrandEngName ,(select top 1 ImageURL from ObjectImages where ObjectId=a.unit_id order by DisplayIndex) [imageURL] ,a.unit_remark UnitRemark" //update by wzq 门店介绍
                        + " ,c.UnitTypeContent UnitTypeContent"
                        + " ,convert(nvarchar(20),c.MinPrice) MinPrice"
                        + " ,c.SupportingContent SupportingContent"
                        + " ,c.HotContent HotContent"
                        + " ,c.OtherUnitCount OtherUnitCount"
                        + " ,c.IntroduceContent IntroduceContent"
                        + " ,c.HotelType HotelType"
                        + " ,c.IsApp IsApp"
                        + " FROM dbo.t_unit a "
                        + " LEFT JOIN (SELECT TOP 1 y.*,x.StoreId FROM StoreBrandMapping x "
                        + " 			INNER JOIN dbo.BrandDetail y ON(x.BrandId = y.BrandId)) b "
                        + " ON(a.unit_id = b.StoreId) "
                        + " left join VwUnitProperty c on c.UnitId=a.Unit_Id "
                        + " WHERE a.unit_id = '" + StoreId + "'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取同步福利门店品牌关系

        /// <summary>
        /// 获取同步福利门店品牌关系
        /// </summary>
        /// <param name="latestTime">最后同步时间</param>
        /// <returns></returns>
        public DataSet GetSynWelfareStoreBrandMappingList(string latestTime)
        {
            string sql = string.Empty;
            sql += " SELECT mappingid = a.MappingId ";
            sql += " , storeid = a.StoreId ";
            sql += " , brandid = a.BrandId ";
            sql += " , isdelete = a.IsDelete ";
            sql += " FROM dbo.StoreBrandMapping a ";
            sql += " WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(latestTime))
            {
                sql += " AND a.LastUpdateTime >= '" + latestTime + "' ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取城市的门店集合
        public int GetStoreListByCityCount(string ItemId
            , int Page
            , int PageSize
            , string Longitude
            , string Latitude)
        {
            string sql = GetStoreListByCitySql(ItemId, Longitude, Latitude);
            sql += " select count(*) From #tmp ; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet GetStoreListByCity(string ItemId
            , int Page
            , int PageSize
            , string Longitude
            , string Latitude
            ,string beginDate
            ,string endDate)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = Page * PageSize;
            DataSet ds = new DataSet();
            string sql = GetStoreListByCitySql(ItemId, Longitude, Latitude);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetStoreListByCitySql(string city, string Longitude, string Latitude)
        {
            string sql = "SELECT x.* "
                        + " ,DisplayIndex = row_number() over(order BY x.Distance asc)  into #tmp "
                        + " FROM ( "
                        + " SELECT a.unit_id StoreId "
                        + " ,a.unit_name StoreName,a.imageURL "
                        + " ,a.unit_address [Address] "
                        + " ,a.unit_tel Tel "
                        + " ,a.longitude Longitude "
                        + " ,a.dimension Latitude "
                        + " ,case when '" + Latitude + "' = '0' and '" + Longitude + "' = '0' then '0' else ABS(dbo.DISTANCE_TWO_POINTS(" + Convert.ToDouble(Latitude) + "," + Convert.ToDouble(Longitude) + ",a.dimension,a.longitude)) end Distance "
                //TODO:zhangwei 没有这两个字段+ " ,a.UnitTypeContent UnitTypeContent "
                        + " ,c.MinPrice MinPrice "
                        + " FROM dbo.t_unit a "
                        + " left join VwUnitProperty c on a.unit_id=c.UnitId "
                        + " INNER JOIN t_city b "
                        + " ON(a.unit_city_id = b.city_id) "
                        + " WHERE  a.type_id = 'EB58F1B053694283B2B7610C9AAD2742' and (b.city2_name like '%" + city + "%' or a.unit_address like '%" + city + "%') and a.customer_id = '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "'"
                        + " ) x ";
            return sql;
        }
        #endregion

    }
}
