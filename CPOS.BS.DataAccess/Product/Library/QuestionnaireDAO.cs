/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:36
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
    /// 表Questionnaire的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class QuestionnaireDAO : Base.BaseCPOSDAO, ICRUDable<QuestionnaireEntity>, IQueryable<QuestionnaireEntity>
    {
        #region 获取考卷
        /// <summary>
        /// 获取考卷
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable GetQuestionnaire(string pType, int pPageIndex, int pPageSize)
        {
            int begin = pPageIndex * pPageSize + 1;
            int end = (pPageIndex + 1) * pPageSize;

            string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER ( ORDER BY q.ReleaseTime DESC) AS rowid,QuestionnaireID AS SurveyTestId";
            sql += " ,mloc.OnlineCourseId,QuestionnaireName,QuestionnaireDesc AS SurveyTestDesc,Type,SurfaceImage,Redoable,PassScore";
            sql += " ,(SELECT COUNT(UserId) FROM (SELECT UserId FROM MLAnswerSheet WHERE QuestionnaireID=q.QuestionnaireID AND IsDelete=0 GROUP BY UserId) a) AS JoinNum";
            sql += " ,(SELECT COUNT(UserId) FROM (SELECT UserId FROM MLAnswerSheet WHERE QuestionnaireID=q.QuestionnaireID AND IsPassed=1 AND IsDelete=0 GROUP BY UserId) b)AS PassNum";
            sql += " ,ISNULL(( SELECT TOP 1 IsPassed FROM MLAnswerSheet WHERE QuestionnaireID=q.QuestionnaireID AND UserId=@UserId AND IsDelete=0";
            sql += " ORDER BY AnswerTime DESC),-1) TestStatus";
            sql += " ,ReleaseState,q.ReleaseTime,EndTime,CASE WHEN EndTime<getdate() THEN 1 ELSE 0 END AS IsEnd FROM MLSurveyRelation AS mlsr";
            sql += " INNER JOIN MLOnlineCourse AS mloc ON mlsr.OnlineCourseId=mloc.OnlineCourseId";
            sql += " INNER JOIN Questionnaire AS q ON q.QuestionnaireID=mlsr.SurveyTestId";
            sql += " WHERE mloc.CustomerID=@CustomerID ";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserId", this.CurrentUserInfo.CurrentUser.User_Id));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            if (!string.IsNullOrEmpty(pType))
            {
                sql += " AND q.Type=@Type";
                param.Add(new SqlParameter("@Type", pType));
            }
            sql += " AND q.ReleaseState=1 AND mloc.IsDelete=0 AND q.IsDelete=0 AND mlsr.IsDelete=0";
            sql += ") tt WHERE tt.rowid BETWEEN @begin AND @end ";
            sql += " ORDER BY ReleaseTime DESC";

            param.Add(new SqlParameter("@begin", begin));
            param.Add(new SqlParameter("@end", end));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion

        #region 获取考卷详情
        /// <summary>
        /// 获取考卷详情
        /// </summary>
        /// <param name="pSurveyTestId"></param>
        /// <returns></returns>
        public DataTable GetQuestionnaireDetail(string pSurveyTestId)
        {
            string sql = "SELECT QuestionnaireID AS SurveyTestId,mloc.OnlineCourseId,QuestionnaireName,QuestionnaireDesc AS SurveyTestDesc,Type,SurfaceImage,Redoable,PassScore";
            sql += " ,(SELECT COUNT(UserId) FROM (SELECT UserId FROM MLAnswerSheet WHERE QuestionnaireID=q.QuestionnaireID AND IsDelete=0 GROUP BY UserId) a) AS JoinNum";
            sql += " ,(SELECT COUNT(UserId) FROM (SELECT UserId FROM MLAnswerSheet WHERE QuestionnaireID=q.QuestionnaireID AND IsPassed=1 AND IsDelete=0 GROUP BY UserId) b)AS PassNum";
            sql += " ,ISNULL(( SELECT top 1 IsPassed FROM MLAnswerSheet WHERE QuestionnaireID=q.QuestionnaireID AND UserId=@UserId AND IsDelete=0";
            sql += " ORDER BY AnswerTime DESC),-1) TestStatus";
            sql += " ,ReleaseState,q.ReleaseTime,EndTime,CASE WHEN EndTime<getdate() THEN 1 ELSE 0 END AS IsEnd FROM MLSurveyRelation AS mlsr";
            sql += " INNER JOIN MLOnlineCourse AS mloc ON mlsr.OnlineCourseId=mloc.OnlineCourseId";
            sql += " INNER JOIN Questionnaire AS q ON q.QuestionnaireID=mlsr.SurveyTestId";
            sql += " WHERE q.QuestionnaireID=@SurveyTestId AND mloc.CustomerID=@CustomerID AND q.ReleaseState=1";
            sql += " AND mloc.IsDelete=0 AND q.IsDelete=0 AND mlsr.IsDelete=0";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserId", this.CurrentUserInfo.CurrentUser.User_Id));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@SurveyTestId", pSurveyTestId));


            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion
    }
}
