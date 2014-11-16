/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/15 10:33:17
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
using System.Text;

using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.DataAccess.Base
{
    /// <summary>
    /// 管理平台的数据访问基类 
    /// </summary>
    public abstract class BaseCPOSDAO : BaseDAO<LoggingSessionInfo>
    {
        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        protected LoggingSessionInfo loggingSessionInfo { get; set; }

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pUserInfo">当前的用户信息</param>
        /// <param name="pIsLoggingTSQL">是否将所执行的TSQL语句以日志的形式记录下来</param>
        public BaseCPOSDAO(LoggingSessionInfo userInfo, bool pIsLoggingTSQL)
            : base(userInfo, new DirectConnectionStringManager(userInfo.CurrentLoggingManager.Connection_String))
        {
            //bool pIsLoggingTSQL = false; // Base.ConfigInfo.IsLoggingTSQL;
            BasicUserInfo pUserInfo = new BasicUserInfo();
            pUserInfo.ClientID = null;
            pUserInfo.UserID = null;

            var connectionString = userInfo.CurrentLoggingManager.Connection_String;
            var sqlHelper = new DefaultSQLHelper(connectionString);
            sqlHelper.CurrentUserInfo = pUserInfo;
            this.SQLHelper = sqlHelper;

            if (pIsLoggingTSQL)
            {
                //给数据访问助手挂载执行完毕事件，以记录所有执行的SQL
                this.SQLHelper.OnExecuted += new EventHandler<SqlCommandExecutionEventArgs>(SQLHelper_OnExecuted);
            }
        }

        public BaseCPOSDAO(LoggingSessionInfo userInfo)
            : base(userInfo, new DirectConnectionStringManager(null))
        {
            bool pIsLoggingTSQL = true; // Base.ConfigInfo.IsLoggingTSQL;
            BasicUserInfo pUserInfo = new BasicUserInfo();
            pUserInfo.ClientID = null;
            pUserInfo.UserID = null;

            var connectionString = userInfo.CurrentLoggingManager.Connection_String;
            var sqlHelper = new DefaultSQLHelper(connectionString);
            sqlHelper.CurrentUserInfo = pUserInfo;
            this.SQLHelper = sqlHelper;

            if (pIsLoggingTSQL)
            {
                //给数据访问助手挂载执行完毕事件，以记录所有执行的SQL
                this.SQLHelper.OnExecuted += new EventHandler<SqlCommandExecutionEventArgs>(SQLHelper_OnExecuted);
            }
        }
        #endregion

        #region 事件处理
        /// <summary>
        /// SQL助手执行完毕后，记录日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SQLHelper_OnExecuted(object sender, SqlCommandExecutionEventArgs e)
        {
            if (e != null)
            {
                var log = new DatabaseLogInfo();
                //获取用户信息
                if (e.UserInfo != null)
                {
                    log.ClientID = e.UserInfo.ClientID;
                    log.UserID = e.UserInfo.UserID;
                }
                //获取T-SQL相关信息
                if (e.Command != null)
                {
                    TSQL tsql = new TSQL();
                    tsql.CommandText = e.Command.GenerateTSQLText();
                    if (e.Command.Connection != null)
                    {
                        tsql.DatabaseName = e.Command.Connection.Database;
                        tsql.ServerName = e.Command.Connection.DataSource;
                    }
                    tsql.ExecutionTime = e.ExecutionTime;
                    log.TSQL = tsql;
                }
                Loggers.DEFAULT.Database(log);
            }
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 从跟踪堆栈中获取第一个数据访问类的调用
        /// </summary>
        /// <param name="pStackTraces"></param>
        protected StackTraceInfo GetFirstDAClassCallFrom(StackTraceInfo[] pStackTraces)
        {
            if (pStackTraces != null)
            {
                foreach (var item in pStackTraces)
                {
                    if (item.Class != "JIT.ManagementPlatform.DataAccess.Base.BaseCPOSDAO")
                        return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <returns></returns>
        public string GetCurrentDateTime()
        {
            return GetDateTime(DateTime.Now);
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 生成guid
        /// </summary>
        /// <returns></returns>
        protected string NewGuid()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "");
        }
        #endregion
    }
}
