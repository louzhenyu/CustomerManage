/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:52:21
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
    /// 表ObjectDownloads的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ObjectDownloadsDAO : Base.BaseCPOSDAO, ICRUDable<ObjectDownloadsEntity>, IQueryable<ObjectDownloadsEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(ObjectDownloadsEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(ObjectDownloadsEntity entity, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(ObjectDownloadsEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.CreateTime desc) ";
            sql += " ,CreateByName=(select user_name from t_user where user_id=a.createBy) ";
            sql += " into #tmp ";
            sql += " from [ObjectDownloads] a ";
            sql += " where 1=1 ";
            sql += " and a.IsDelete='0' ";
            //sql += " and a.CustomerId='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.DownloadId != null && entity.DownloadId.Length > 0)
            {
                sql += " and a.DownloadId = '" + entity.DownloadId + "' ";
            }
            if (entity.ObjectId != null && entity.ObjectId.Length > 0)
            {
                sql += " and a.ObjectId = '" + entity.ObjectId + "' ";
            }
            return sql;
        }
        #endregion
        
    }
}
