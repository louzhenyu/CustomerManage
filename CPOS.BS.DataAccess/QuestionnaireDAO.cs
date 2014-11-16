/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/7 10:56:10
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
            sql += "select * From #tmp ;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetEventApplyQuesSql(string EventID)
        {
            string sql = "SELECT b.* INTO #tmp FROM dbo.Events a "
                        + " INNER JOIN dbo.QuesQuestions b "
                        + " ON(a.ApplyQuesID = b.QuestionnaireID) "
                        + " WHERE a.EventID='" + EventID + "' "
                        + " AND b.IsDelete = 0 ; ";

            sql = "select * into #tmp From dbo.QuesQuestions a where a.IsDelete = '0' and a.QuestionnaireID = '" + EventID + "' ";
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

        #region 后台Web获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebQuestionnairesCount(QuestionnaireEntity entity)
        {
            string sql = GetWebQuestionnairesSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebQuestionnaires(QuestionnaireEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebQuestionnairesSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast desc ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetWebQuestionnairesSql(QuestionnaireEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.CreateTime desc) ";
            //sql += " ,(case when a.VideoType='1' then '类别一' else '' end) VideoTypeName";
            sql += " ,(select count(*) from QuesQuestions where isDelete='0' and QuestionnaireID=a.QuestionnaireID) QuestionCount";
            sql += " into #tmp ";
            sql += " from Questionnaire a ";
            sql += " where a.IsDelete='0' ";
            sql += " and a.CustomerId = '" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.CreateBy != null && entity.CreateBy.Trim().Length > 0 &&
                entity.CreateBy.Trim() != "admin")
            {
                sql += " and a.CreateBy = '" + entity.CreateBy + "' ";
            }
            if (entity.QuestionnaireName != null && entity.QuestionnaireName.Trim().Length > 0)
            {
                sql += " and a.QuestionnaireName like '%" + entity.QuestionnaireName + "%' ";
            }
            sql += " order by a.DisplayIndex desc ";
            return sql;
        }
        #endregion

        #region 获取评论列表 add by changjian.tian
        public DataSet GetCommentList(string pQuestionnaireID,string pUserId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DataSet ds = new DataSet();
            sbSQL.AppendFormat(string.Format(@"select V.VipName,Q3.OptionsText,QuestionDesc+':'+OptionsText as Content, * from QuesQuestions Q1  
                               left join QuesAnswer Q2 on Q1.QuestionID=Q2.QuestionID
                               left join QuesOption Q3 on Q2.QuestionValue=Q3.OptionsID
                               INNER join Vip V on V.VIPID=q2.LastUpdateBy 
                               where Q1.QuestionnaireID='{0}'
                               and q2.IsDelete='0'
                               and V.VIPID='{1}'
                               order by QuestionType", pQuestionnaireID, pUserId));
            ds = SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }
        #endregion
    }
}
