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
    /// 表MarketEventResponse的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketEventResponseDAO : Base.BaseCPOSDAO, ICRUDable<MarketEventResponseEntity>, IQueryable<MarketEventResponseEntity>
    {
        #region 获取活动响应人群信息
        public DataSet GetEventResponseInfo(string EventID, int Page, int PageSize)
        {
            DataSet ds = new DataSet();
            int beginSize = Page * PageSize;
            int endSize = Page * PageSize + PageSize;

            string sql = GetEventResponseInfoSql(EventID);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            //Loggers.Debug(new DebugLogInfo()
            //{
            //    Message = string.Format("GetEventListSql:{0}", sql)
            //});
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public int GetEventResponseInfoCount(string EventID)
        {
            string sql = GetEventResponseInfoSql(EventID);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// xxxxxxxxxxxx
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        private string GetEventResponseInfoSql(string EventID)
        {

            string sql = "SELECT a.*,b.VipCode,b.VipName,b.VipLevel ,CONVERT(NVARCHAR(20), GETDATE(),120) StatisticsTime  "
                        + " ,DisplayIndex = row_number() over(order by a.CreateTime desc,b.VipName ) "
                        + " into #tmp FROM dbo.MarketEventResponse a "
                        + " INNER JOIN dbo.Vip b "
                        + " ON(a.VIPID = b.VIPID) "
                        + " WHERE a.IsDelete = '0' AND b.IsDelete = '0' "
                        + " AND (a.MarketEventID = '"+EventID+"' "
                        + " or a.MarketEventID in ( SELECT CASE WHEN EventStatus='2' THEN '4' ELSE '0' end FROM dbo.MarketEvent WHERE MarketEventID='"+EventID+"'))";
            return sql;
        }
        #endregion
    }
}
