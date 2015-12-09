/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:47
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
    /// 表LPrizeWinner的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LPrizeWinnerDAO : Base.BaseCPOSDAO, ICRUDable<LPrizeWinnerEntity>, IQueryable<LPrizeWinnerEntity>
    {
        #region 根据微信ID和活动ID 获取中奖信息
        /// <summary>
        /// 根据openID和活动ID 获取中奖信息
        /// </summary>
        /// <param name="vipID">vipID</param>
        /// <param name="eventID">活动ID</param>
        /// <returns></returns>
        public SqlDataReader GetWinnerInfo(string vipID, string eventID)
        {
            string sql = " SELECT distinct b.PrizeShortDesc, a.PrizeWinnerID FROM dbo.LPrizeWinner a "
                        + " left JOIN dbo.LPrizes b ON a.PrizeID = b.PrizesID "
                        + " WHERE a.isDelete = 0  and b.isDelete = 0 "
                        + " AND a.VipID = '" + vipID + "' AND b.EventId = '" + eventID + "' AND a.HasConvert = 0";

            SqlDataReader obj = this.SQLHelper.ExecuteReader(sql);
            return obj;
        }

        public string GetWinnerInfoString(string vipID, string eventID)
        {
            string sql = " SELECT distinct b.PrizeShortDesc  + case when LEN( c.PrizePoolsID) < 10 then c.PrizePoolsID else '' end  PrizeShortDesc  FROM dbo.LPrizeWinner a "
                        + " inner join LPrizePools c on(a.PrizePoolID = c.PrizePoolsID) "
                        + " left JOIN dbo.LPrizes b ON a.PrizeID = b.PrizesID "
                        + " WHERE a.isDelete = 0  and b.isDelete = 0 "
                        + " AND a.VipID = '" + vipID + "' AND b.EventId = '" + eventID + "' AND a.HasConvert = 0";

            string obj = Convert.ToString(this.SQLHelper.ExecuteScalar(sql));
            return obj;
        }
        #endregion

        #region
        public DataSet GetPrizesWinnerByGroupBrand(string PrizeBrand, string EventId, long Timestamp, string RoundId)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT distinct c.* FROM LPrizeWinner a "
                        + " INNER JOIN dbo.LPrizes b ON(b.PrizesID = a.PrizeID)  "
                        + " INNER JOIN vip c on(a.vipid = c.vipid) inner join LEventRountPrizesMapping d on(b.PrizesID = d.PrizesID)"
                        + " WHERE b.PrizeName = '" + PrizeBrand + "' AND a.isdelete = '0' AND b.EventId='" + EventId + "' and d.RoundId = '" + RoundId + "' "
                        + " AND dbo.DateToTimestamp(a.createtime) > '" + Timestamp + "' and a.RoundId= '" + RoundId + "';";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取最大时间戳
        public string GetMaxTimestamp(string EventId, string RoundId)
        {
            string sql = "SELECT isnull(MAX(dbo.DateToTimestamp(a.createtime)),dbo.DateToTimestamp(GETDATE())) FROM LPrizeWinner a "
                    + " INNER JOIN dbo.LPrizes b ON(a.PrizeID = b.PrizesID) inner join LEventRountPrizesMapping d on(b.PrizesID = d.PrizesID)"
                    + " WHERE 1=1 "
                    + " AND a.IsDelete = '0' "
                    + " AND b.IsDelete = '0' and d.Isdelete='0' "
                    + " AND b.EventId = '" + EventId + "' and d.RoundId= '" + RoundId + "' "
                    + " GROUP BY b.EventId;";
            return Convert.ToString(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 获取活动中奖名单 Jermyn20131211
        /// <summary>
        /// 获取中奖名单
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public DataSet GetPrizeWinnerListByEventId(string EventId, int page, int pageSize)
        {
            DataSet ds = new DataSet();
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = GetPrizeWinnerListByEventIdSql(EventId);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public int GetPrizeWinnerListByEventIdCount(string EventId)
        {
            string sql = GetPrizeWinnerListByEventIdSql(EventId);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        private string GetPrizeWinnerListByEventIdSql(string EventId)
        {
            string sql = "select a.*,b.PrizeName,b.PrizeDesc,c.VipName "
                        + " ,DisplayIndex=row_number() over(order by a.createtime desc) "
                        + " into #tmp From LPrizeWinner a "
                        + " inner join LPrizes b on(a.PrizeID = b.PrizesID) "
                        + " inner join Vip c on(a.VipID = c.VIPID) "
                        + " where a.IsDelete = '0' "
                        + " and b.IsDelete = '0' "
                        + " and b.EventId = '" + EventId + "'";
            return sql;
        }
        #endregion

        public DataSet GetPrizeCouponTypeMapping(string prizeWinnerID, IDbTransaction tran)
        {
            string sql = string.Format(@"select c.* From dbo.LPrizeWinner a
                                            inner join lPrizes b on a.PrizeID=b.PrizesID
                                            inner join dbo.PrizeCouponTypeMapping c on b.PrizesID=c.PrizesID
                                            where prizeWinnerID='{0}'", prizeWinnerID);
            return SQLHelper.ExecuteDataset((SqlTransaction)tran, CommandType.Text, sql);
        }
        /// <summary>
        /// 根据活动Id获取最近10名中奖者名单
        /// </summary>
        /// <param name="strEventId"></param>
        /// <returns></returns>
        public DataSet GetTop10PizewWinnerListByEventId(string strEventId)
        {
            string strSql = string.Format(@"SELECT TOP 10  o.OptionText  PrizeLevel,P.PrizeName,PW.CreateTime,V.VipName
                                            FROM    dbo.LPrizeWinner PW WITH(NOLOCK) 
                                            INNER JOIN dbo.LPrizes P WITH(NOLOCK) ON PW.PrizeID = P.PrizesID AND P.EventId = '{0}'
                                            LEFT JOIN Options o ON P.PrizeLevel=o.OptionValue  and o.OptionName='PrizeGrade'
		                                    INNER JOIN dbo.Vip V WITH(NOLOCK) ON PW.VipID=V.VIPID
                                            ORDER BY PW.CreateTime DESC
                                ", strEventId);
            return SQLHelper.ExecuteDataset(CommandType.Text, strSql);

        }
    }
}
