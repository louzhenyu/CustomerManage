/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/16 16:35:19
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
    /// 表PushIOSMessage的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PushIOSMessageDAO : Base.BaseCPOSDAO, ICRUDable<PushIOSMessageEntity>, IQueryable<PushIOSMessageEntity>
    {
        #region 市场活动使用APP推送消息
        /// <summary>
        /// 市场活动使用APP推送消息
        /// </summary>
        /// <param name="MarketingId">活动标识</param>
        /// <returns></returns>
        public bool SetMarketPushApp(string MarketEventId)
        {
            SqlParameter[] pars = new SqlParameter[] { 
                new SqlParameter("@MarketEventId",MarketEventId),
                new SqlParameter("@CustomerId",this.CurrentUserInfo.CurrentLoggingManager.Customer_Id.Trim())
            };

            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_Set_MarketEventApp", pars);
            return true;

        }
        #endregion
    }
}
