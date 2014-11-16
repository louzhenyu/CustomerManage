/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/20 14:30:02
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using JIT.Utility;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;
using JIT.CPOS.BS.DataAccess;
using JIT.Utility.DataAccess;

using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// CEIBSBLL 
    /// </summary>
    public partial class CEIBSBLL : VIPDefindModuleBLL
    {
        public LoggingSessionInfo CurrentUserInfo;
        public CEIBSDAO _currentDAO;

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CEIBSBLL(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new CEIBSDAO(pUserInfo);
        }
        #endregion

        #region EventStatsPageData
        public DataSet EventStatsPageData(string Type,string ObjectID, int? pPageSize, int? pPageIndex)
        {
            return this._currentDAO.EventStatsPageData(Type,ObjectID, pPageSize, pPageIndex);
        
        }
        #endregion

        #region GetOptionID

        public DataSet GetOptionID(string ObjectType)
        {
            return this._currentDAO.GetOptionID(ObjectType);
        }
        #endregion

        #region EventStatsSave
        public int EventStatsSave(string id, string ObjectType, string ObjectID, string Sequence)
        {
            return this._currentDAO.SaveEventStats(id, ObjectType, ObjectID, "0", "0", "0", Sequence);
        }
        #endregion

        #region DelEventStats
        public int DelEventStats(string id)
        {
            return this._currentDAO.DelEventStats(id);
        }
        #endregion

        #region GetEventStatsDetail
        public DataSet GetEventStatsDetail(string EventStatsID)
        {
            return this._currentDAO.GetEventStatsDetail(EventStatsID);
        }
        #endregion
    }
}
