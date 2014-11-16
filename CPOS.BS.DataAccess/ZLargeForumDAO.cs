/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 9:59:18
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
    /// 表ZLargeForum的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ZLargeForumDAO : Base.BaseCPOSDAO, ICRUDable<ZLargeForumEntity>, IQueryable<ZLargeForumEntity>
    {
        #region 获取活动列表
        /// <summary>
        /// 获取活动列表数量
        /// </summary>
        public int GetForumsCount(ZLargeForumEntity entity)
        {
            string sql = GetForumsSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取活动列表
        /// </summary>
        public DataSet GetForums(ZLargeForumEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetForumsSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetForumsSql(ZLargeForumEntity entity)
        {
            string sql = string.Empty;
            sql = "select distinct a.* ";
            if (entity.ForumTypeId != null && !entity.ForumTypeId.Equals(""))
            {
                sql += " ,(select top 1 ForumTypeName From ZForumType x where x.ForumTypeId = ac.ForumTypeId) ForumTypeName";
            }
            sql += " ,DisplayIndex = row_number() over(order by a.BeginTime asc,a.LastUpdateTime desc ) ";
            sql += " into #tmp ";
            sql += " from [ZLargeForum] a ";
            sql += " left join ZLargeForumCourseMapping b on (a.ForumId=b.ForumId and b.isDelete='0') ";
            if (entity.ForumTypeId != null && !entity.ForumTypeId.Equals(""))
            {
                sql += " INNER JOIN ZForumTypeLargeForumMapping ab ON(a.ForumId = ab.ForumId and ab.isDelete='0') "
                    + " INNER JOIN dbo.ZForumType ac ON(ab.ForumTypeId = ac.ForumTypeId and ac.isDelete='0' ) ";
            }
            sql += " where a.IsDelete='0' and a.BeginTime >= GETDATE()  ";
            //if (entity.ForumTypeId != null)
            //{
            //    sql += " and a.ForumTypeId = '" + entity.ForumTypeId + "' ";
            //}
            if (entity.Title != null && entity.Title.Trim().Length > 0)
            {
                sql += " and a.Title like '%" + entity.Title + "%' ";
            }
            if (entity.City != null && entity.City.Trim().Length > 0)
            {
                sql += " and a.City like '%" + entity.City + "%' ";
            }
            if (entity.BeginTime != null && entity.BeginTime.Trim().Length > 0)
            {
                sql += " and a.BeginTime like '%" + entity.BeginTime + "%' ";
            }
            if (entity.CourseId != null && entity.CourseId.Trim().Length > 0)
            {
                sql += " and b.CourseId = '" + entity.CourseId + "' ";
            }
            if (entity.ForumTypeId != null)
            {
                sql += " and ac.ForumTypeId = '" + entity.ForumTypeId + "' ";
            }
            return sql;
        }
        #endregion
        
    }
}
