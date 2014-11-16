/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    /// 表Store的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class StoreDAO : Base.BaseCPOSDAO, ICRUDable<StoreEntity>, IQueryable<StoreEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(StoreEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(StoreEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(StoreEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp ";
            sql += " from Store a ";
            sql += " where a.IsDelete='0' ";
            if (entity.StoreID != null && entity.StoreID.Trim().Length > 0)
            {
                sql += " and a.StoreID = '" + entity.StoreID + "' ";
            }
            if (entity.StoreIDs != null && entity.StoreIDs.Trim().Length > 0)
            {
                sql += " and a.StoreID in (" + entity.StoreIDs + ") ";
            }
            sql += " order by a.CreateTime desc ";
            return sql;
        }
        #endregion

        #region 获取同步福利门店

        /// <summary>
        /// 获取同步福利门店
        /// </summary>
        /// <param name="latestTime">最后同步时间</param>
        /// <returns></returns>
        public DataSet GetSynWelfareStoreList(string latestTime)
        {
            string sql = string.Empty;
            sql += " SELECT storeId = a.unit_id ";
            sql += " , storeCode = a.unit_code ";
            sql += " , storeName = a.unit_name ";
            sql += " , storeDesc = a.unit_remark ";
            sql += " , displayIndex = row_number() over(order by a.unit_name) ";
            sql += " , [address] = a.unit_address ";
            sql += " , content = a.unit_contact ";
            sql += " , tel = a.unit_tel ";
            sql += " , weiXinCode = a.weiXinId ";
            sql += " , email = a.unit_email ";
            sql += " , longitude = a.longitude ";
            sql += " , latitude = a.dimension ";
            sql += " , isdelete =  CASE a.[status] WHEN '1' THEN '0' ELSE '1' END ";
            sql += " FROM dbo.t_unit a ";
            sql += " WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(latestTime))
            {
                sql += " AND a.modify_time >= '" + latestTime + "' ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取城市列表
        public DataSet GetCityDsByUnit(string cityName, string cityCode,string customerId)
        {
            var sql = new StringBuilder();
            var sqlWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(cityName) && cityName != "")
            {
                sqlWhere.AppendFormat(" and b.city2_name like '%{0}%'", cityName);
            }
            if (!string.IsNullOrEmpty(cityCode) && cityCode != "")
            {
                sqlWhere.AppendFormat(" and dbo.FnGetCharacterInfo(city2_name,'py') like '%{0}%'", cityCode);
            }

            sql.Append(" select top 10  OptionValue CityCode,OptionText CityName,dbo.FnGetCharacterInfo(OptionText,'py') as ABC,'1' type  From Options");
            sql.Append(" where OptionName='HotCity'  and IsDelete = 0  ");
            sql.Append(" union all");
            sql.Append(" select distinct left(b.city_code,4) cityCode,b.city2_name cityName,");
            sql.Append(" dbo.FnGetCharacterInfo(b.city2_name,'py') ABC,'2' type ");
            sql.Append(" From t_unit a");
            sql.Append(" inner join T_City b on(a.unit_city_id = b.city_id)");

            sql.AppendFormat(" where a.customer_id = '{0}'",customerId);
            sql.Append(sqlWhere);

            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        #endregion 

        #region 获取星级和价格区间列表
        public DataSet GetHotleStarAndPrice()
        {
            var sqlWhere = new StringBuilder();            
            var sql = new StringBuilder();
          
            sql.Append(" select top 10  OptionValue StarsCode,OptionText StarsName ");
            sql.Append(" From Options where OptionName='Stars' order by Sequence;");

            sql.Append("select  OptionText PriceName,OptionValue PriceBegin,OptionTextEn PriceEnd,OptionsID ");
            sql.Append(" From Options where OptionName='HotlesRoomPrice' order by Sequence;");

            return this.SQLHelper.ExecuteDataset(sql.ToString());

 
        }
        #endregion

        #region 获取酒店列表

        public DataSet GetHotleList(string cityCode,string dateFrom ,string dateTo,string hotleName,
            decimal priceFrom,decimal priceTo,string hotleStra,decimal log,decimal lat,string customerId,string orderItem,string orderType,decimal radius)
        {
            var parm = new SqlParameter[13];
            parm[0] = new SqlParameter("@CustomerId", System.Data.SqlDbType.NVarChar) { Value = customerId };
            parm[1] = new SqlParameter("@CityCode", System.Data.SqlDbType.NVarChar, 100) { Value = cityCode };
            parm[2] = new SqlParameter("@CheckInDate", System.Data.SqlDbType.NVarChar, 100) { Value = dateFrom };
            parm[3] = new SqlParameter("@CheckOutDate", System.Data.SqlDbType.NVarChar, 100) { Value = dateTo };
            parm[4] = new SqlParameter("@KeyWords", System.Data.SqlDbType.NVarChar, 200) { Value = hotleName };
            parm[5] = new SqlParameter("@Star", System.Data.SqlDbType.NVarChar, 100) { Value = hotleStra };
            parm[6] = new SqlParameter("@StartPrice", System.Data.SqlDbType.Decimal, 100) { Value = priceFrom };

            parm[7] = new SqlParameter("@EndPrice", System.Data.SqlDbType.Decimal, 100) { Value = priceTo };

            parm[8] = new SqlParameter("@Longitude", System.Data.SqlDbType.Decimal) { Value = log };
            parm[9] = new SqlParameter("@Latitude", System.Data.SqlDbType.Decimal) { Value = lat };

            parm[10] = new SqlParameter("@OrderItem", System.Data.SqlDbType.NVarChar) { Value = orderItem };
            parm[11] = new SqlParameter("@OrderType", System.Data.SqlDbType.NVarChar) { Value = orderType };
            parm[12] = new SqlParameter("@Radius", System.Data.SqlDbType.Decimal) { Value = radius };


            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetSercherHotel", parm);          
 
        }

        #endregion

        #region 获取酒店详情和房型列表 测试
        public DataSet GetHotelDetails(string unitID)
        {
            string sql = "select * from VwHotelUnit where UnitID='"+unitID+"'";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetHotelRoomSku(string unitID,DateTime beginDate,DateTime endDate)
        {
            string sql = "SELECT  distinct vw.*,fn.FloatPrice FROM dbo.FnGetValidRoomPrice('"
                +beginDate+"','"+endDate+"' ) fn inner join VwHotelRoomSku vw on vw.RoomID =fn.RoomID where fn.UnitId='"+unitID+"'";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion 
    }
}
