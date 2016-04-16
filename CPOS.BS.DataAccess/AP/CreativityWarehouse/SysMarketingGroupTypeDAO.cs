/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 13:34:28
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表SysMarketingGroupType的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SysMarketingGroupTypeDAO : Base.BaseCPOSDAO, ICRUDable<SysMarketingGroupTypeEntity>, IQueryable<SysMarketingGroupTypeEntity>
    {
        public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;

        public SysMarketingGroupTypeDAO(LoggingSessionInfo pUserInfo, string connectionString)
            : base(pUserInfo)
        {
            this.StaticConnectionString = connectionString;
            this.SQLHelper = StaticSqlHelper;
        }
        protected ISQLHelper StaticSqlHelper
        {
            get
            {
                if (null == staticSqlHelper)
                    staticSqlHelper = new DefaultSQLHelper(StaticConnectionString);
                return staticSqlHelper;
            }
        }


        public DataSet GetMarketingGroupType(LoggingSessionInfo loggingSessionInfo)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerID", loggingSessionInfo.ClientID));
            string sql = @"select ActivityGroupId,Name,ActivityGroupCode,Remark,CreateTime,CreateBy,LastUpdateTime,LastUpdateBy,IsDelete,DisplayIndex
                     ,(  select count(1) from T_CTW_LEvent b where b.ActivityGroupId = a.ActivityGroupId and status=10   and customerid=@CustomerID ) as WaitPublishEvent
                      ,(  select count(1) from T_CTW_LEvent c where c.ActivityGroupId = a.ActivityGroupId and status=20 and customerid=@CustomerID) as RuningEvent
                        ,(  select count(1) from T_CTW_LEvent d where d.ActivityGroupId = a.ActivityGroupId and status=40 and customerid=@CustomerID) as EndEvent
                     from SysMarketingGroupType  a
                     where isdelete=0 
                     order by DisplayIndex ";
            string conn = loggingSessionInfo.CurrentLoggingManager.Connection_String;
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);

            var result = sqlHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());
            return result;

        }

    }
}
