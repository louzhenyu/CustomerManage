/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/22 9:51:44
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
using System.Configuration;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表T_CTW_LEventTheme的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_CTW_LEventThemeDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_LEventThemeEntity>, IQueryable<T_CTW_LEventThemeEntity>
    {
        /// <summary>
        /// 当季活动列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetInSeasonThemeList()
        {

            DataSet ds = new DataSet();
            string strSql = "Select *   FROM cpos_ap.dbo.T_CTW_LEventTheme WITH(NOLOCK) WHERE IsDelete=0 AND  season=dbo.F_GetSeasonByData('')";

            ds = SQLHelper.ExecuteDataset(strSql);
            return ds;
        }
        /// <summary>
        /// 获取下季活动列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetNextSeasonThemeList()
        {

            DataSet ds = new DataSet();
            string strSql = "Select *   FROM cpos_ap.dbo.T_CTW_LEventTheme WITH(NOLOCK) WHERE IsDelete=0 AND season=dbo.F_GetSeasonByData('next')";

            ds = SQLHelper.ExecuteDataset(strSql);
            return ds;
        }
        /// <summary>
        /// 获取ap库商户信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetCustomerInfo()
        {
            DataSet ds = new DataSet();
            string strSql = string.Format("Select *   FROM dbo.t_customer WITH(NOLOCK) WHERE customer_id='{0}'", CurrentUserInfo.ClientID);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            ds = sqlHelper.ExecuteDataset(strSql);
            return ds;
        }
    }
}
