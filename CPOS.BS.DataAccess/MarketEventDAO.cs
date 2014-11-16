/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:38
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
    /// 表MarketEvent的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketEventDAO : Base.BaseCPOSDAO, ICRUDable<MarketEventEntity>, IQueryable<MarketEventEntity>
    {
        #region 获取活动列表

        /// <summary>
        /// 获取活动列表数量
        /// </summary>
        public int GetEventListCount()
        {
            string sql = GetEventListSql();
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取活动列表
        /// </summary>
        public DataSet GetEventList(int Page, int PageSize)
        {
            int beginSize = Page * PageSize;
            int endSize = Page * PageSize + PageSize;

            string sql = GetEventListSql();
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("GetEventListSql:{0}", sql)
            });
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetEventListSql()
        {
            string sql = string.Empty;
            sql = " SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,(SELECT x.BrandName FROM dbo.MarketBrand x WHERE x.BrandID = a.brandID) BrandName ";
            sql += " ,CASE WHEN a.EventStatus = '1' THEN '未启用' ELSE '启用' END StatusDesc ";
            sql += "  ,(SELECT x.EventModeDesc FROM dbo.MarketEventMode x WHERE x.EventModeId = a.eventMode) EventModeDesc ";
            sql += " into #tmp ";
            sql += " from dbo.MarketEvent a ";
            sql += " where a.IsDelete='0' ";
            sql += " and a.customerId='" + CurrentUserInfo.CurrentUser.customer_id +"' ";
            sql += " order by a.EventCode desc ";
            return sql;
        }

        #endregion

        #region 获取活动详细信息
        /// <summary>
        /// 获取活动详细信息
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetMarketEventInfoById(string EventID)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT *,(SELECT x.BrandName FROM dbo.MarketBrand x WHERE x.BrandID = a.brandID) BrandName "
                       + " ,CASE WHEN a.EventStatus = '1' THEN '未启用' ELSE '启用' END StatusDesc "
                       + " ,(SELECT x.EventModeDesc FROM dbo.MarketEventMode x WHERE x.EventModeId = a.eventMode) EventModeDesc "
                       + " FROM dbo.MarketEvent a WHERE a.IsDelete = 0 and a.MarketEventID = '" + EventID + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取活动模板
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetMarketTemplateByEventID(string EventID)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.* FROM dbo.MarketTemplate a INNER JOIN dbo.MarketEvent b ON(a.TemplateID = b.TemplateID) "
                       + " WHERE a.IsDelete = 0 AND a.IsDelete = 0 AND b.MarketEventID  = '" + EventID + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取活动波段
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetMarketWaveBandByEventID(string EventID)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.* FROM MarketWaveBand a WHERE a.IsDelete = 0 AND a.MarketEventID = '" + EventID + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取活动门店
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetMarketStoreByEventID(string EventID)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.* FROM MarketWaveBand a WHERE a.IsDelete = 0 AND a.MarketEventID = '" + EventID + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取活动分析各个参数
        public int GetAnalysisResponsePersonCount(string EventID)
        {
            string sql = "SELECT COUNT(DISTINCT a.vipid) icount FROM dbo.MarketEventResponse a ";
                       //+ " WHERE a.MarketEventID = '"+EventID+"' AND a.IsDelete = 0";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 获取活动消费金额
        public decimal GetEventCurrentSales(string EventID)
        {
            string sql = "SELECT ISNULL(SUM(d.total_amount),0) FROM dbo.MarketEvent a "
                        + " INNER JOIN dbo.MarketPerson b ON(a.MarketEventID = b.MarketEventID) "
                        + " INNER JOIN dbo.Vip c ON(b.vipid = c.VIPID) "
                        + " INNER JOIN dbo.T_Inout d ON(b.VIPID = d.vip_no) "
                        + " WHERE a.MarketEventID = '"+EventID+"';";
            return Convert.ToDecimal(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion
    }
}
