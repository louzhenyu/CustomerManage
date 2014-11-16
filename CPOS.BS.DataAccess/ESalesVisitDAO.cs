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
    /// 表ESalesVisit的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ESalesVisitDAO : Base.BaseCPOSDAO, ICRUDable<ESalesVisitEntity>, IQueryable<ESalesVisitEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(ESalesVisitEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(ESalesVisitEntity entity, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(ESalesVisitEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,CreateByName=(select user_name from t_user where user_id=a.createBy) ";
            sql += " ,(select count(*) from ESalesVisitVipMapping where SalesVisitId=a.SalesVisitId and isDelete='0') VipCount ";
            sql += " ,(select count(*) from ObjectDownloads where objectId=a.SalesVisitId and isDelete='0') ObjectCount ";
            //sql += " ,ECSourceName=(select ECSourceName from ESalesVisitource where ECSourceId=a.ECSourceId) ";
            sql += " into #tmp ";
            sql += " from [ESalesVisit] a ";
            sql += " where 1=1 ";
            sql += " and a.IsDelete='0' ";
            //sql += " and a.CustomerId='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.SalesVisitId != null && entity.SalesVisitId.Length > 0)
            {
                sql += " and a.SalesVisitId = '" + entity.SalesVisitId + "' ";
            }
            if (entity.SalesVisitName != null && entity.SalesVisitName.Length > 0)
            {
                sql += " and a.SalesVisitName like '%" + entity.SalesVisitName + "%' ";
            }
            if (entity.SalesId != null && entity.SalesId.Length > 0)
            {
                sql += " and a.SalesId = '" + entity.SalesId + "' ";
            }
            if (entity.VisitTypeId != null && entity.VisitTypeId.Length > 0)
            {
                sql += " and a.VisitTypeId = '" + entity.VisitTypeId + "' ";
            }
            return sql;
        }
        #endregion
        
    }
}
