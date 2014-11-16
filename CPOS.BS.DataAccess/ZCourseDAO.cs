/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 9:59:17
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
    /// ���ݷ��ʣ�  
    /// ��ZCourse�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ZCourseDAO : Base.BaseCPOSDAO, ICRUDable<ZCourseEntity>, IQueryable<ZCourseEntity>
    {
        #region ��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetCoursesCount(ZCourseEntity entity)
        {
            string sql = GetCoursesSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetCourses(ZCourseEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetCoursesSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetCoursesSql(ZCourseEntity entity)
        {
            var orderBy = "a.CreateTime desc";
            if (entity.OrderBy != null && entity.OrderBy.Length > 0)
            {
                orderBy = entity.OrderBy;
            }

            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by " + orderBy + ") ";
            //sql += " ,(case when a.EventType='1' then '�ٷ��' when a.EventType='2' then 'Ⱥ��' " +
            //    " when a.EventType='3' then '���˷���' else '' end) EventTypeName";
            sql += " into #tmp ";
            sql += " from [ZCourse] a ";
            sql += " where a.IsDelete='0' ";
            if (entity.CourseTypeId != null)
            {
                sql += " and a.CourseTypeId = '" + entity.CourseTypeId + "' ";
            }
            if (entity.CourseId != null && entity.CourseId.Trim().Length > 0)
            {
                sql += " and a.CourseId = '" + entity.CourseId + "' ";
            }
            if (entity.ParentId != null && entity.ParentId.Trim().Length > 0)
            {
                sql += " and a.ParentId = '" + entity.ParentId + "' ";
            }
            return sql;
        }
        #endregion
        
    }
}
