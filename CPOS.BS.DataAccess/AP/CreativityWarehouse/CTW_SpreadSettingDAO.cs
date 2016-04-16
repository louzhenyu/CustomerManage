/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 18:04:05
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
    /// 表T_CTW_SpreadSetting的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CTW_SpreadSettingDAO : Base.BaseCPOSDAO, ICRUDable<CTW_SpreadSettingEntity>, IQueryable<CTW_SpreadSettingEntity>
    {
        public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;

        public CTW_SpreadSettingDAO(LoggingSessionInfo pUserInfo, string connectionString)
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
        public DataSet GetSpreadSettingInfoByTemplateId(string strTemplateId)
        {
            string strSql=string.Format(@"SELECT *,b.ImageURL FROM dbo.T_CTW_SpreadSetting a INNER JOIN dbo.ObjectImages  b ON a.ImageId=b.ImageId
                                WHERE a.TemplateId='{0}'",strTemplateId);
            return staticSqlHelper.ExecuteDataset(strSql);
        }
    }
}
