using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using JIT.Utility.ExtensionMethod;
using System.Data;
/********************************************************************************

    * 创建时间: 2014-9-25 11:29:40
    * 作    者：donal
    * 说    明：人人销售
    * 修改时间：2014-9-25 11:30:02
    * 修 改 人：donal

*********************************************************************************/

namespace JIT.CPOS.BS.DataAccess.EveryoneSales
{
    public partial class EveryoneSalesDAO : Base.BaseCPOSDAO
    {

        /// <summary>
        /// 构造函数 
        /// </summary>
        public EveryoneSalesDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }

        /// <summary>
        /// 店员账户统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetEveryOneAmount(string ChannelId, string UserID, string CustomerId)
        {
            var parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@CustomerID", CustomerId);
            parm[1] = new SqlParameter("@UserID", UserID);
            parm[2] = new SqlParameter("@ChanelId", ChannelId );
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetSaleUserOrder", parm);
        }

        /// <summary>
        /// 订单月统计列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetOrderAmount(string CustomerId, string UserID, int PageSize, int PageIndex, string ChannelId)
        {
            var parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@CustomerID", CustomerId);
            parm[1] = new SqlParameter("@UserID", UserID);
            parm[2] = new SqlParameter("@PageSize", PageSize);
            parm[3] = new SqlParameter("@PageIndex", PageIndex);
            parm[4] = new SqlParameter("@ChanelId", ChannelId);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetOrderList", parm);
        }
        
        /// <summary>
        /// 集客榜
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipCount(string CustomerID, int PageSize, int PageIndex, string ChanelId)
        {
            var parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@CustomerID", CustomerID);
            parm[1] = new SqlParameter("@PageSize", PageSize);
            parm[2] = new SqlParameter("@PageIndex", PageIndex);
            parm[3] = new SqlParameter("@ChanelId", ChanelId);
   
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetSetOffTopN", parm);
        }

        /// <summary>
        /// 集客榜排名 By UserID
        /// </summary>
        /// <returns></returns>
        public DataSet GetRankingByUserID(string CustomerID, string UserID, string ChanelId)
        {
            var parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@CustomerID", CustomerID);
            parm[1] = new SqlParameter("@UserID", UserID);
            parm[2] = new SqlParameter("@ChanelId", ChanelId);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetRankingByUserID", parm);
        }

        /// <summary>
        /// 收入榜
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipAccount(string CustomerID, int PageSize, int PageIndex, string ChanelId)
        {
            var parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@CustomerID", CustomerID);
            parm[1] = new SqlParameter("@PageSize", PageSize);
            parm[2] = new SqlParameter("@PageIndex", PageIndex);
            parm[3] = new SqlParameter("@ChanelId", ChanelId);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetUserIncomeTopN", parm);
        }

        /// <summary>
        /// 收入榜排名 By UserID
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipAccountRankingByUserID(string CustomerID, string UserID, string ChanelId)
        {
            var parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@CustomerID", CustomerID);
            parm[1] = new SqlParameter("@UserID", UserID);
            parm[2] = new SqlParameter("@ChanelId", ChanelId);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetVipAccountRankingByUserID", parm);
        }

        /// <summary>
        /// 月收入统计列表
        /// </summary>    
        public DataSet GetAmountDetail(string CustomerId,string UserID, int PageSize, int PageIndex, string ChanelId)
        {
            var parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@CustomerId", CustomerId);
            parm[1] = new SqlParameter("@UserID", UserID);
            parm[2] = new SqlParameter("@PageSize", PageSize);
            parm[3] = new SqlParameter("@PageIndex", PageIndex);
            parm[4] = new SqlParameter("@ChanelId", ChanelId);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetUserAmountDetail", parm);
        }
    }
}
