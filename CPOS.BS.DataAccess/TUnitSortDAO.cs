/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/16 13:44:32
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
    /// 表TUnitSort的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TUnitSortDAO : Base.BaseCPOSDAO, ICRUDable<TUnitSortEntity>, IQueryable<TUnitSortEntity>
    {
        #region 获取某个客户的所有门店所在的城市列表，地级市

        /// <summary>
        /// 获取某个客户的所有门店所在的城市列表，地级市
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetCityListByCustomerId(string customerId)
        {
            string sql = string.Empty;
            sql += " SELECT b.city2_name AS cityName, COUNT(b.city2_name) AS cityCount FROM dbo.t_unit a ";
            sql += " INNER JOIN dbo.T_City b ON LEFT(a.unit_city_id,32) = b.city_id ";
            sql += " WHERE a.customer_id = '" + customerId + "' AND type_id ='EB58F1B053694283B2B7610C9AAD2742' and status='1' ";
            sql += " GROUP BY b.city2_name ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取门店集合

        public int GetStoreListByItemCount(string cityName, int Page, int PageSize, string Longitude, string Latitude)
        {
            string sql = GetStoreListByItemSql(cityName, Longitude, Latitude);
            sql += " select count(*) From #tmp ; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet GetStoreListByItem(string cityName, int Page, int PageSize, string Longitude, string Latitude)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = Page * PageSize;
            DataSet ds = new DataSet();
            string sql = GetStoreListByItemSql(cityName, Longitude, Latitude);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetStoreListByItemSql(string cityName, string Longitude, string Latitude)
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
                        + " FROM (select * From dbo.t_unit where type_id ='EB58F1B053694283B2B7610C9AAD2742' and status='1' and customer_id = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ) a ";
            if (cityName != null && !cityName.Equals(""))
            {
                sql += " INNER JOIN dbo.T_City b ON LEFT(a.unit_city_id,32) = b.city_id "
                 + " WHERE b.city2_name = '" + cityName + "' ";
            }
            sql += " ) x ";
            return sql;
        }
        #endregion
    }
}
