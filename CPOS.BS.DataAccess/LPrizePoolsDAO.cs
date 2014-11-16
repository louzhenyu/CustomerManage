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
    /// 表LPrizePools的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LPrizePoolsDAO : Base.BaseCPOSDAO, ICRUDable<LPrizePoolsEntity>, IQueryable<LPrizePoolsEntity>
    {
        #region
        public DataSet SetShakeOffLottery(string UserName, string UserID, string EventID, float Longitude, float Latitude)
        {
            DataSet ds = new DataSet();
            string sql = "exec shake_off_lottery '" + UserID + "','" + UserName + "'," + Longitude + "," + Latitude + ",'" + EventID + "' ";

            SqlParameter[] Parm = new SqlParameter[5];
            Parm[0] = new SqlParameter("@i_person_id", System.Data.SqlDbType.NVarChar, 100);
            Parm[0].Value = UserID;
            Parm[1] = new SqlParameter("@i_username", System.Data.SqlDbType.NVarChar, 100);
            Parm[1].Value = UserName;
            Parm[2] = new SqlParameter("@i_longitude", System.Data.SqlDbType.Float, 4);
            Parm[2].Value = Longitude;
            Parm[3] = new SqlParameter("@i_latitude", System.Data.SqlDbType.Float, 4);
            Parm[3].Value = Latitude;
            Parm[4] = new SqlParameter("@i_event_id", System.Data.SqlDbType.NVarChar, 100);
            Parm[4].Value = EventID;

            ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "shake_off_lottery", Parm);
            return ds;
        }
        #endregion

        /// <summary>
        /// 活动抽奖
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="vipId"></param>
        /// <param name="eventId"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetEventWinningInfo(string userName, string vipId, string eventId, float longitude, float latitude, string customerId, string reCommandId, int pointsLotteryFlag)
        {
            //string sql = "exec GetEventWinningInfo '" + vipId + "','"
            //    + userName + "','" + customerId + "'," + longitude + "," + latitude + ",'" + eventId + "' ";

            var parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@pVipId", System.Data.SqlDbType.NVarChar, 100) { Value = vipId };
            parm[1] = new SqlParameter("@pUserName", System.Data.SqlDbType.NVarChar, 100) { Value = userName };
            parm[2] = new SqlParameter("@pLongitude", System.Data.SqlDbType.Float, 4) { Value = longitude };
            parm[3] = new SqlParameter("@pLatitude", System.Data.SqlDbType.Float, 4) { Value = latitude };
            parm[4] = new SqlParameter("@pEventId", System.Data.SqlDbType.NVarChar, 100) { Value = eventId };
            parm[5] = new SqlParameter("@pCustomerId", System.Data.SqlDbType.NVarChar, 100) { Value = customerId };

            parm[6] = new SqlParameter("@pRecommandId", System.Data.SqlDbType.NVarChar, 100) { Value = reCommandId };

            parm[7] = new SqlParameter("@pPointsLotteryFlag", System.Data.SqlDbType.Int) { Value = pointsLotteryFlag };


            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "GetEventWinningInfo", parm);



        }


        public DataSet GetPersonWinnerList(string vipId, string eventId)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pVipId", Value = vipId });
            paras.Add(new SqlParameter() { ParameterName = "@pEventId", Value = eventId });
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.prizesid,a.prizename,a.prizedesc,a.ContentText as sponsor,");
            sql.Append("a.imageurl,a.displayindex,a.createtime from LPrizes a,LPrizeWinner b,LPrizePools c");
            sql.Append(" where a.PrizesID = b.PrizeID and a.prizesId = c.prizeId and b.PrizePoolID = c.PrizePoolsID");
            sql.Append(" and b.VipID = @pVipId and a.EventId = @pEventId and a.prizesId = c.prizeId");
            sql.Append(" and a.IsDelete = 0 and b.IsDelete = 0 and c.isdelete = 0");
            sql.Append(" order by a.createTime desc");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());

        }

        #region
        public DataSet SetShakeOffLotteryBySales(string UserName, string UserID, string EventID, float Longitude, float Latitude)
        {
            DataSet ds = new DataSet();
            string sql = "exec procSetLotteryBySales '" + UserID + "','" + UserName + "'," + Longitude + "," + Latitude + ",'" + EventID + "' ";

            SqlParameter[] Parm = new SqlParameter[5];
            Parm[0] = new SqlParameter("@i_person_id", System.Data.SqlDbType.NVarChar, 100);
            Parm[0].Value = UserID;
            Parm[1] = new SqlParameter("@i_username", System.Data.SqlDbType.NVarChar, 100);
            Parm[1].Value = UserName;
            Parm[2] = new SqlParameter("@i_longitude", System.Data.SqlDbType.Float, 4);
            Parm[2].Value = Longitude;
            Parm[3] = new SqlParameter("@i_latitude", System.Data.SqlDbType.Float, 4);
            Parm[3].Value = Latitude;
            Parm[4] = new SqlParameter("@i_event_id", System.Data.SqlDbType.NVarChar, 100);
            Parm[4].Value = EventID;

            ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "procSetLotteryBySales", Parm);
            return ds;
        }
        #endregion



        public DataSet GetUserPrizeWinnerLog(string eventId, string vipId)
        {
            var sql = new StringBuilder();
            sql.Append(" select b.PrizeName PrizeDesc,convert(nvarchar(10),c.CreateTime,121) CreateTime from LEvents a");
            sql.Append(" inner join LPrizes b on a.EventID = b.EventId");
            sql.Append(" inner join LPrizeWinner c on b.PrizesID = c.PrizeID");
            sql.AppendFormat(" where a.EventID = '{0}'",eventId);
            sql.AppendFormat(" and c.VipID = '{0}'",vipId);
            sql.Append(" and a.IsDelete = 0 and b.IsDelete = 0 and c.IsDelete = 0");
            sql.Append(" order by c.CreateTime desc");

            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
    }
}
