/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/5 9:24:38
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
    /// 表VersionManager的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VersionManagerDAO : Base.BaseCPOSDAO, ICRUDable<VersionManagerEntity>, IQueryable<VersionManagerEntity>
    {
        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Plat">版本</param>
        /// <param name="Channel">渠道</param>
        /// <returns></returns>
        public DataSet GetVersionInfoByQuery(string Plat, string Channel, string Version, string UserId)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT top 1 * "
                        + " ,CASE WHEN a.VersionNoUpdate > '" + Version + "' THEN '1' ELSE '0' END IsNewVersionAvailable "
                        + " ,CASE WHEN a.VersionNoLowest > '" + Version + "' THEN '0' ELSE '1' END CanSkip "
                        + " FROM dbo.VersionManager a WHERE a.IsDelete = 0 "
                        + " AND a.VersionNoUpdate > '" + Version + "' and a.Plat = '" + Plat + "' and a.customerId = '"+this.CurrentUserInfo.CurrentUser.customer_id+"' "
                        + "  " ;
            if (!Plat.ToLower().Equals("android"))
            {
                sql += " and a.Channel = '" + Channel + "' ";
            }

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
