/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:35
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
    /// 数据访问： 课目表 
    /// 表MLOnlineCourse的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MLOnlineCourseDAO : Base.BaseCPOSDAO, ICRUDable<MLOnlineCourseEntity>, IQueryable<MLOnlineCourseEntity>
    {
        #region 获取课程
        /// <summary>
        /// 获取课程
        /// </summary>
        /// <param name="pCourseType"></param>
        /// <param name="pSortKey"></param>
        /// <param name="pSortOrientation"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable GetOnlineCourse(string pCourseType, string pSortKey, string pSortOrientation, int pPageIndex, int pPageSize)
        {
            int begin = pPageIndex * pPageSize + 1;
            int end = (pPageIndex + 1) * pPageSize;

            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY ReleaseTime DESC) AS rowid,OnlineCourseId,Topic,Introduction,Author";
            sql += " ,Icon,CourseType,ReleaseTime,AverageStar,AccessCount,(SELECT (CASE WHEN COUNT(*)>0 THEN '1' ELSE '0' END) FROM ItemKeep";
            sql += " WHERE ItemId=OnlineCourseId AND VipId=@UserID AND KeepStatus=1 AND IsDelete=0 ) AS KeepType FROM MLOnlineCourse ";
            sql += " WHERE CustomerID=@CustomerID ";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserID", this.CurrentUserInfo.CurrentUser.User_Id));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));

            if (!string.IsNullOrEmpty(pCourseType))
            {
                sql += " AND CourseType=@CourseType";
                param.Add(new SqlParameter("@CourseType", pCourseType));
            }
            sql += " AND IsDelete=0 ";
            sql += ") tt WHERE tt.rowid BETWEEN @begin AND @end ";

            //sql += "ORDER BY ReleaseTime DESC";
            sql += "ORDER BY " + pSortKey + " " + pSortOrientation;

            param.Add(new SqlParameter("@begin", begin));
            param.Add(new SqlParameter("@end", end));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion

        #region 模糊查询课程
        /// <summary>
        /// 模糊查询课程
        /// </summary>
        /// <param name="pKeyword"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable SearchOnlineCourse(string pKeyword, int pPageIndex, int pPageSize)
        {
            int begin = pPageIndex * pPageSize + 1;
            int end = (pPageIndex + 1) * pPageSize;

            pKeyword = pKeyword.Replace("'", "''");

            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY ReleaseTime DESC) AS rowid,OnlineCourseId,Topic,Introduction,Author";
            sql += " ,Icon,CourseType,ReleaseTime,AverageStar,AccessCount,(SELECT (CASE WHEN COUNT(*)>0 THEN '1' ELSE '0' END) FROM ItemKeep";
            sql += " WHERE ItemId=OnlineCourseId AND VipId=@UserID AND KeepStatus=1 AND IsDelete=0 ) AS KeepType FROM MLOnlineCourse ";
            sql += " WHERE CustomerID=@CustomerID ";
            sql += " AND Topic like '%" + pKeyword + "%'";
            sql += " AND IsDelete=0 ) tt WHERE tt.rowid BETWEEN @begin AND @end";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserID", this.CurrentUserInfo.CurrentUser.User_Id));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@begin", begin));
            param.Add(new SqlParameter("@end", end));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion

        #region 获取课程详情
        /// <summary>
        /// 获取课程详情
        /// </summary>
        /// <param name="pOnlineCourseId"></param>
        /// <returns></returns>
        public DataTable SearchOnlineCourseDetail(string pOnlineCourseId)
        {
            string sql = "SELECT OnlineCourseId,Topic,Introduction,Icon,CourseType,ReleaseTime,AverageStar,AccessCount,Author";
            sql += " ,(SELECT (CASE WHEN COUNT(*)>0 THEN '1' ELSE '0' END) FROM ItemKeep";
            sql += " WHERE ItemId=OnlineCourseId AND VipId=@UserID AND KeepStatus=1 AND IsDelete=0 ) AS KeepType FROM MLOnlineCourse ";
            sql += " WHERE OnlineCourseId=@OnlineCourseId AND CustomerID=@CustomerID AND IsDelete=0";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserID", this.CurrentUserInfo.CurrentUser.User_Id));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@OnlineCourseId", pOnlineCourseId));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion

        #region 获取收藏课程
        /// <summary>
        /// 获取收藏课程
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable GetFavoriteCourse(string pUserID, int pPageIndex, int pPageSize)
        {
            int begin = pPageIndex * pPageSize + 1;
            int end = (pPageIndex + 1) * pPageSize;

            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY ReleaseTime DESC) AS rowid,OnlineCourseId,Topic,Introduction,Author";
            sql += " ,Icon,CourseType,ReleaseTime,AverageStar,AccessCount,'1' KeepType FROM MLOnlineCourse AS mloc ";
            sql += " INNER JOIN ItemKeep AS ik ON mloc.OnlineCourseId=ik.ItemId ";
            sql += " WHERE ik.VipId=@UserID AND CustomerID=@CustomerID AND ik.KeepStatus=1 AND mloc.IsDelete=0 ";
            sql += " AND ik.IsDelete=0 ) tt WHERE tt.rowid BETWEEN @begin AND @end ORDER BY ReleaseTime DESC";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserID", this.CurrentUserInfo.CurrentUser.User_Id));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@begin", begin));
            param.Add(new SqlParameter("@end", end));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion
    }
}
