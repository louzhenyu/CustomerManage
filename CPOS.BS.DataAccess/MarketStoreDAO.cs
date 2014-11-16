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
    /// 表MarketStore的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketStoreDAO : Base.BaseCPOSDAO, ICRUDable<MarketStoreEntity>, IQueryable<MarketStoreEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(MarketStoreEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(MarketStoreEntity entity, int Page, int PageSize)
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
        private string GetListSql(MarketStoreEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.*, b.StoreCode, b.BusinessDistrict, b.Address, b.MembersCount, b.SalesYear, b.Opened, b.Longitude, b.Latitude ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp ";
            sql += " from MarketStore a ";
            sql += " inner join Store b on a.StoreID=b.StoreID ";
            sql += " where a.IsDelete='0' ";
            if (entity.MarketEventID != null && entity.MarketEventID.Trim().Length > 0)
            {
                sql += " and a.MarketEventID = '" + entity.MarketEventID + "' ";
            }
            if (entity.MarketStoreID != null && entity.MarketStoreID.Trim().Length > 0)
            {
                sql += " and a.MarketStoreID = '" + entity.MarketStoreID + "' ";
            }
            sql += " order by a.CreateTime desc ";
            return sql;
        }
        #endregion

        public void WebDelete(MarketStoreEntity entity)
        {
            string sql = "";
            sql += " delete MarketStore where 1=1 ";
            sql += " and MarketEventID='" + entity.MarketEventID + "' ";

            if (entity.MarketStoreID != null && entity.MarketStoreID.Length > 0)
            {
                sql += " and MarketStoreID='" + entity.MarketStoreID + "' ";
            }
            this.SQLHelper.ExecuteScalar(sql);
        }
    }
}
