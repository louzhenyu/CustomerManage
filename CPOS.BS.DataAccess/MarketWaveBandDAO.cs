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
    /// 表MarketWaveBand的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketWaveBandDAO : Base.BaseCPOSDAO, ICRUDable<MarketWaveBandEntity>, IQueryable<MarketWaveBandEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(MarketWaveBandEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(MarketWaveBandEntity entity, int Page, int PageSize)
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
        private string GetListSql(MarketWaveBandEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp ";
            sql += " from MarketWaveBand a ";
            sql += " where a.IsDelete='0' ";
            if (entity.MarketEventID != null && entity.MarketEventID.Trim().Length > 0)
            {
                sql += " and a.MarketEventID = '" + entity.MarketEventID + "' ";
            }
            if (entity.WaveBandID != null && entity.WaveBandID.Trim().Length > 0)
            {
                sql += " and a.WaveBandID = '" + entity.WaveBandID + "' ";
            }
            sql += " order by a.CreateTime desc ";
            return sql;
        }
        #endregion

        #region 根据活动获取波段集合
        public int GetWaveBandByEventIDCount(string EventID)
        {
            string sql = GetWaveBandByEventIDSql(EventID);
            sql += "select count(*) From #tmp";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        public DataSet GetWaveBandByEventID(string EventID,int Page,int PageSize)
        {
            int beginSize = Page * PageSize;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWaveBandByEventIDSql(EventID);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EventID">活动标识</param>
        /// <returns></returns>
        private string GetWaveBandByEventIDSql(string EventID)
        {
            string sql = "SELECT a.*,DisplayIndex = row_number() over(order by a.beginTime desc) "
                       + "  FROM dbo.MarketWaveBand a WHERE a.IsDelete=0 AND a.MarketEventID = '" + EventID + "' ;";

            sql = "SELECT *,DisplayIndex = row_number() over(order by x.beginTime desc) into #tmp FROM ( "
                + " SELECT 'xxxxx' WaveBandID,MarketEventID,BeginTime,EndTime,BeginTime FactBeginTime,EndTime FactEndTime,CreateTime,CreateBy,LastUpdateBy,LastUpdateTime,IsDelete "
                + " FROM dbo.MarketEvent a WHERE IsDelete = 0 AND MarketEventID = '" + EventID + "' "
                + " AND BeginTime IS not NULL  AND EndTime IS NOT NULL "
                + " UNION ALL "
                + " SELECT a.* FROM dbo.MarketWaveBand a WHERE a.IsDelete = 0 AND a.MarketEventID = '" + EventID + "' "
                + " ) x ";
            return sql;
        }
        #endregion
    }
}
