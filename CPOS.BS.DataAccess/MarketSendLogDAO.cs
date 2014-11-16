/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/5 10:22:18
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
    /// 表MarketSendLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketSendLogDAO : Base.BaseCPOSDAO, ICRUDable<MarketSendLogEntity>, IQueryable<MarketSendLogEntity>
    {
        #region 短信每天发送次数
        /// <summary>
        /// 短信每天发送次数
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="sendTypeId"></param>
        /// <returns></returns>
        public int GetSendCountByPhone(string Phone, string sendTypeId, string key)
        {
            string sql = "SELECT ISNULL(COUNT(*),0) icount FROM marketsendlog WHERE Phone = '" + Phone + "' AND MarketEventId='" + key + "' AND SendTypeId = '" + sendTypeId + "' AND IsDelete = '0'";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion
    }
}
