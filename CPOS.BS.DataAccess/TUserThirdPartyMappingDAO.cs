/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 12:26:34
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
    /// 表TUserThirdPartyMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TUserThirdPartyMappingDAO : Base.BaseCPOSDAO, ICRUDable<TUserThirdPartyMappingEntity>, IQueryable<TUserThirdPartyMappingEntity>
    {
        #region 获取第三方VoipAccount集合
        /// <summary>
        /// 获取第三方VoipAccount集合
        /// </summary>
        /// <param name="userIDList">企信用户ID集合</param>
        /// <returns></returns>
        public List<string> GetVoipAccountList(List<string> userIDList)
        {
            List<string> voipAccountList = new List<string>();
            StringBuilder keys = new StringBuilder();
            if (userIDList != null)
            {
                foreach (object item in userIDList)
                {
                    keys.AppendFormat("'{0}',", item.ToString());
                }

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM dbo.TUserThirdPartyMapping where 1=1 ");
                sql.AppendLine(" and UserID in (" + keys.ToString().Substring(0, keys.ToString().Length - 1) + ")");
                sql.AppendLine(" and IsDelete=0;");
                DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        voipAccountList.Add(row["VoipAccount"].ToString());
                    }
                }
            }
            return voipAccountList;
        }
        #endregion

        #region 根据企信用户帐号返回对应云通讯上的帐号
        /// <summary>
        /// 根据企信用户帐号返回对应云通讯上的帐号
        /// </summary>
        /// <param name="userIDList">企信userID</param>
        /// <returns></returns>
        public DataTable GetCloudUserList(List<string> pUserIDList)
        {
            StringBuilder keys = new StringBuilder();
            if (pUserIDList != null)
            {
                foreach (object item in pUserIDList)
                {
                    keys.AppendFormat("'{0}',", item.ToString());
                }
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM dbo.TUserThirdPartyMapping where 1=1 ");
                sql.AppendLine(" and UserID in (" + keys.ToString().Substring(0, keys.ToString().Length - 1) + ")");
                sql.AppendLine(" and IsDelete=0;");
                DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            return null;
        }
        #endregion

        #region 返回AppId下所有帐号
        /// <summary>
        /// 返回AppId下所有帐号
        /// </summary>
        /// <param name="AppId">AppId</param>
        /// <returns></returns>
        public DataTable GetAccountByAppId(string pAppId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT UserId,VoipAccount,StatusCode,AppId FROM dbo.TUserThirdPartyMapping where ");
            sql.AppendLine(" AppId='" + pAppId + "'");
            sql.AppendLine(" and IsDelete=0;");
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        #endregion
    }
}
