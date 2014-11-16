/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:46
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
    /// 表LPrizes的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LPrizesDAO : Base.BaseCPOSDAO, ICRUDable<LPrizesEntity>, IQueryable<LPrizesEntity>
    {
        #region 活动奖品列表
        /// <summary>
        /// 根据标识获取奖品数量
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int GetEventPrizesCount(string EventID)
        {
            string sql = GetEventPrizesSql(EventID);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 根据标识获取奖品信息
        /// </summary>
        public DataSet GetEventPrizesList(string EventID, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventPrizesSql(EventID);
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndexLast between '" + beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetEventPrizesSql(string EventID)
        {
            BasicUserInfo pUserInfo = new BasicUserInfo();

            string sql = "";
            sql += "SELECT a.* "
                + " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex ) "
                + " into #tmp FROM LPrizes a "
                + " where a.IsDelete='0' and a.EventID = '" + EventID + "' ";
            //sql += " order by a.DisplayIndex ";
            return sql;
        }
        #endregion

        #region
        /// <summary>
        /// 根据品牌分组，获取信息
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetLPrizesGroupBrand(string EventID,string RoundId)
        {
            string sql = "SELECT DISTINCT a.PrizeName,a.LogoURL,a.PrizeShortDesc,a.DisplayIndex,a.PrizeDesc "
                        + " FROM dbo.LPrizes a inner join LEventRountPrizesMapping b on(a.PrizesID = b.PrizesID) "
                        + " inner join LEventRound c on(b.RoundId = c.RoundId and a.EventId = c.EventId) "
                        + " WHERE 1=1 "
                        + " and a.EventID = '" + EventID + "' "
                        + " AND a.IsDelete = '0' "
                        + " and b.IsDelete = '0' "
                        + " and c.IsDelete = '0' "
                        + " and c.RoundId = '" + RoundId + "' order by a.DisplayIndex  ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 我的中奖名单
        /// <summary>
        /// 我的中奖名单
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetEventPrizesByVipId(string EventId, string VipId)
        {
            string sql = "select a.* From LPrizes a "
                        + " inner join LPrizeWinner b on(a.PrizesID = b.PrizeID) "
                        + " where a.IsDelete = '0' "
                        + " and b.IsDelete = '0' "
                        + " and b.VipID = '" + VipId + "' "
                        + " and a.EventId = case when '" + EventId + "' = ''  then a.EventId else '" + EventId + "' end "
                        + " order by CreateTime desc ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 获取奖品列表
        /// <summary>
        /// 获取奖品列表
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetPrizesByEventId(string EventId)
        {
            string sql = "select a.* From LPrizes a "
                        + " where a.IsDelete = '0' "
                        + " and a.EventId = '" + EventId + "' "
                        + " order by a.displayIndex desc ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 获取奖品人员列表
        /// <summary>
        /// 获取奖品人员列表
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetPrizeWinnerByPrizeId(string PrizeId)
        {
            string sql = "select a.* From LPrizeWinner a "
                        + " where a.IsDelete = '0' "
                        + " and a.PrizeId = '" + PrizeId + "' "
                        + " order by a.CreateTime asc ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 活动轮次奖品列表
        /// <summary>
        /// 根据标识获取奖品数量
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int GetEventRoundPrizesCount(string EventID, string RoundId)
        {
            string sql = GetEventRoundPrizesSql(EventID, RoundId);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 根据标识获取奖品信息
        /// </summary>
        public DataSet GetEventRoundPrizesList(string EventID, string RoundId, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventRoundPrizesSql(EventID, RoundId);
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndexLast between '" + beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetEventRoundPrizesSql(string EventID, string RoundId)
        {
            BasicUserInfo pUserInfo = new BasicUserInfo();

            string sql = "";
            sql += " select a.*  ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex )  ";
            sql += " into #tmp from ( ";
            sql += " SELECT distinct a.PrizesID, a.PrizeName "
                + " ,a.DisplayIndex "
                + " ,(select c.PrizesCount from LEventRountPrizesMapping c where c.isDelete='0' and a.PrizesID=c.PrizesID and c.roundId=b.roundId and b.roundId='" + RoundId + "') PrizesCount "
                + " ,(select c.WinnerCount from LEventRountPrizesMapping c where c.isDelete='0' and a.PrizesID=c.PrizesID and c.roundId=b.roundId and b.roundId='" + RoundId + "') WinnerCount "
                + "  FROM LPrizes a "
                + " left join LEventRound b on (a.EventId=b.EventId and b.isDelete='0' ) "
                + " where a.IsDelete='0' and a.EventID = '" + EventID + "' ";
            if (RoundId != null && RoundId.Length > 0)
            {
                sql += " and b.RoundId='" + RoundId + "' ";
            }
            sql += " ) a ";
            return sql;
        }
        #endregion
    }
}
