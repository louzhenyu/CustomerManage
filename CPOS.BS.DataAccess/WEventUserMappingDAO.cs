/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 17:23:13
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
    /// 表WEventUserMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WEventUserMappingDAO : Base.BaseCPOSDAO, ICRUDable<WEventUserMappingEntity>, IQueryable<WEventUserMappingEntity>
    {
        #region 4.2	活动报名表问题信息获取
        public int GetEventApplyQuesCount(string EventID)
        {
            string sql = GetEventApplyQuesSql(EventID);
            sql += "select count(*) From #tmp ;";
            int icount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return icount;
        }

        public DataSet GetEventApplyQuesList(string EventID)
        {
            DataSet ds = new DataSet();
            string sql = GetEventApplyQuesSql(EventID);
            sql += "select * From #tmp order by displayIndexNo;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetEventApplyQuesSql(string EventID)
        {
            string sql = "SELECT b.* INTO #tmp FROM dbo.LEvents a "
                        + " INNER JOIN dbo.QuesQuestions b "
                        + " ON(a.ApplyQuesID = b.QuestionnaireID) "
                        + " WHERE a.EventID='" + EventID + "' "
                        + " AND b.IsDelete = 0 order by displayIndexNo ; ";

            return sql;
        }

        public DataSet GetEventApplyOptionList(string QuestionID)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT * FROM dbo.QuesOption a WHERE a.QuestionID='" + QuestionID + "' AND IsDelete = 0 order by displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region
        public int GetEventSignInCount(string EventId)
        {
            string sql = "select COUNT( distinct UserID ) icount From WEventUserMapping where IsDelete = 0 and EventID = '" + EventId + "' ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        public int GetUserSignIn(string EventId, string userId)
        {
            string sql = "select COUNT(*) icount From WEventUserMapping where IsDelete = 0 and EventID = '" + EventId + "' and UserId = '" + userId + "' ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        #region 获取活动人员列表
        public DataSet SearchEventUserList(string EventID, string SeachInfo)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.QuestionID,a.CookieName,b.CreateBy,CASE WHEN c.OptionsText IS NULL THEN b.QuestionValue ELSE c.OptionsText END OptionValue,CONVERT(NVARCHAR(16),b.createtime,120) CreateTime,a.DisplayIndexNo  "
                        + " INTO #tmp "
                        + " FROM dbo.QuesQuestions a left JOIN dbo.QuesAnswer b ON(a.QuestionID = b.QuestionID and b.IsDelete='0') LEFT JOIN dbo.QuesOption c "
                        + " ON(a.QuestionID = c.QuestionID AND b.QuestionValue = c.OptionsID and c.IsDelete='0') "
                        + " WHERE a.IsDelete='0' and a.QuestionnaireID in (select ApplyQuesID from levents where eventid ='" + EventID + "') "
                        + " ORDER BY b.CreateBy; ";
            sql += "declare @sql varchar(8000); "
                + " select @sql = isnull(@sql + '],[' , '') + CookieName from #tmp group by CookieName,QuestionID,DisplayIndexNo ORDER BY DisplayIndexNo ; "
                + " set @sql = '[' + @sql + ']' ; "
                + " ALTER TABLE #tmp   DROP COLUMN	QuestionID;"
                + " ALTER TABLE #tmp   DROP COLUMN	DisplayIndexNo;"
                + " EXEC ('select * from (select * from #tmp) a pivot (max(optionValue) for CookieName in (' + @sql + ')) b where 1=1 " + SeachInfo + " order by CreateTime desc,'+@sql+' ') ; ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        ///// <summary>
        ///// 获取活动人员列表（EMBA）
        ///// </summary>
        ///// <param name="EventID">活动ID</param>
        ///// <returns></returns>
        //public DataSet SearchEventUserList(string EventID)
        //{
        //    string sql = new UsersDAO(this.CurrentUserInfo).GetSchoolInfoSql("");

        //    sql += " SELECT 报名时间 = uem.ApplyTime, 用户名称 = u.UserName, 学校信息 = t.SchoolInfo ";
        //    sql += " FROM dbo.UserEventsMapping uem ";
        //    sql += " INNER JOIN dbo.Users u ON u.UserID = uem.UserID ";
        //    sql += " INNER JOIN #tmpInfo t ON uem.UserID = t.UserID ";
        //    sql += " WHERE uem.EventID = '" + EventID + "' ";

        //    var ds = this.SQLHelper.ExecuteDataset(sql);
        //    return ds;
        //}
        #endregion

        #region 获取CookieName对应的描述
        /// <summary>
        /// 获取CookieName对应的描述
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public string GetQuestionsDesc(string EventID, string CookieName)
        {
            string sql = "SELECT a.QuestionDesc FROM QuesQuestions a INNER JOIN dbo.lEvents b ON(a.QuestionnaireID = b.ApplyQuesID) WHERE b.EventID='" + EventID + "' AND a.IsDelete='0' and CookieName = '" + CookieName + "';";
            string quesDesc = Convert.ToString(this.SQLHelper.ExecuteScalar(sql));
            return quesDesc;
        }
        #endregion
    }
}
