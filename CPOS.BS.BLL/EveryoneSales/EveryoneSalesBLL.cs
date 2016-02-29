using JIT.CPOS.BS.DataAccess.EveryoneSales;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
/********************************************************************************

    * 创建时间: 2014-9-25 11:29:40
    * 作    者：donal
    * 说    明：人人销售
    * 修改时间：2014-9-25 11:30:02
    * 修 改 人：donal

*********************************************************************************/

namespace JIT.CPOS.BS.BLL
{
    public partial class EveryoneSalesBLL
    {
        private BasicUserInfo CurrentUserInfo;
        private EveryoneSalesDAO _currentDAO;

        public EveryoneSalesBLL(LoggingSessionInfo pUserInfo)
        {
            this._currentDAO = new EveryoneSalesDAO(pUserInfo);
            this.CurrentUserInfo = pUserInfo;
        }

        /// <summary>
        /// 店员账户统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetEveryOneAmount(string ChannelId, string UserID, string CustomerId)
        {
            return this._currentDAO.GetEveryOneAmount(ChannelId, UserID, CustomerId);
        }

        /// <summary>
        /// 订单月统计列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetOrderAmount(string CustomerId, string UserID, int PageSize, int PageIndex, string ChannelId)
        {
            return this._currentDAO.GetOrderAmount(CustomerId,UserID,PageSize,PageIndex,ChannelId);
        }

        /// <summary>
        /// 集客榜
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipCount(string CustomerID, int PageSize, int PageIndex, string ChanelId)
        {
            return this._currentDAO.GetVipCount(CustomerID, PageSize, PageIndex,ChanelId);
        }
        /// <summary>
        /// 集客榜排名 By UserID
        /// </summary>
        /// <returns></returns>
        public DataSet GetRankingByUserID(string CustomerID, string UserID, string ChanelId)
        {
            return this._currentDAO.GetRankingByUserID(CustomerID, UserID, ChanelId);
        }

        /// <summary>
        /// 收入榜
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipAccount(string CustomerID, int PageSize, int PageIndex, string ChanelId)
        {
            return this._currentDAO.GetVipAccount(CustomerID, PageSize, PageIndex, ChanelId);
        }

        /// <summary>
        /// 收入榜排名 By UserID
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipAccountRankingByUserID(string CustomerID, string UserID, string ChanelId)
        {
            return this._currentDAO.GetVipAccountRankingByUserID(CustomerID, UserID, ChanelId);
        }
        /// <summary>
        /// 月收入统计列表
        /// </summary>
        public DataSet GetAmountDetail(string CustomerId, string UserID, int PageSize, int PageIndex, string ChanelId)
        {
            return this._currentDAO.GetAmountDetail(CustomerId,UserID, PageSize, PageIndex, ChanelId);
        }
    }
}
