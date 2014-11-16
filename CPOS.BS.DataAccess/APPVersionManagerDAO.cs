/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/7 16:29:54
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
    /// 表APPVersionManager的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class APPVersionManagerDAO : Base.BaseCPOSDAO, ICRUDable<APPVersionManagerEntity>, IQueryable<APPVersionManagerEntity>
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        public APPVersionManagerDAO(LoggingSessionInfo pUserInfo, string connectionString)
            : base(pUserInfo)
        {
            this.StaticConnectionString = connectionString;
        }
        public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;
        protected ISQLHelper StaticSqlHelper
        {
            get
            {
                if (null == staticSqlHelper)
                    staticSqlHelper = new DefaultSQLHelper(StaticConnectionString);
                return staticSqlHelper;
            }
        }

        /// <summary>
        /// 获取当前客户客户端版本信息
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="channel">渠道</param>
        /// <param name="plat">平台</param>
        /// <returns></returns>
        public APPVersionManagerEntity GetAppVersion(string customerId, int channel, string plat)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from dbo.AppVersionManager ");
            sql.Append(" where isdelete=0 and customerId=@customerId and channel=@channel and plat=@plat");
            var sqlParameters = new List<SqlParameter>();
            var p1 = new SqlParameter("@customerId", SqlDbType.NVarChar, 200);
            p1.Value = customerId;
            var p2 = new SqlParameter("@channel", SqlDbType.Int);
            p2.Value = channel;
            var p3 = new SqlParameter("@plat", SqlDbType.NVarChar, 100);
            p3.Value = plat;
            sqlParameters.Add(p1);
            sqlParameters.Add(p2);
            sqlParameters.Add(p3);
            var ds = this.StaticSqlHelper.ExecuteDataset(CommandType.Text, sql.ToString(), sqlParameters.ToArray());
            if (ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                var entity = new APPVersionManagerEntity()
                {
                    Channel = Convert.ToInt32(row["Channel"]),
                    Plat = row["Plat"].ToString(),
                    PlatName = row["PlatName"].ToString(),
                    VersionID = row["VersionID"].ToString(),
                    VersionNoLowest = row["VersionNoLowest"].ToString(),
                    VersionNoUpdate = row["VersionNoUpdate"].ToString(),
                    Notice = row["Notice"].ToString(),
                    DownloadURL = row["DownloadURL"].ToString()
                };
                return entity;
            }
            return null;
        }
    }
}
