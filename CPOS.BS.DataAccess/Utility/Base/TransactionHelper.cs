/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/10/31 19:35:24
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
using System.Text;

using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.DataAccess.Base
{
    /// <summary>
    /// 事务助手 
    /// </summary>
    public class TransactionHelper : BaseDAO<LoggingSessionInfo>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TransactionHelper(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, new DirectConnectionStringManager(pUserInfo.CurrentLoggingManager.Connection_String))
        {
        }
        #endregion
        
        #region 创建事务
        /// <summary>
        /// 创建一个事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction CreateTransaction()
        {
            return this.SQLHelper.CreateTransaction();
        }
        #endregion
    }
}
