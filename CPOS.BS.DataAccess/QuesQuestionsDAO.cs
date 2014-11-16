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
    /// 表QuesQuestions的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class QuesQuestionsDAO : Base.BaseCPOSDAO, ICRUDable<QuesQuestionsEntity>, IQueryable<QuesQuestionsEntity>
    {
        #region 后台Web获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebQuesQuestionsCount(QuesQuestionsEntity entity)
        {
            string sql = GetWebQuesQuestionsSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebQuesQuestions(QuesQuestionsEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebQuesQuestionsSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex desc ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetWebQuesQuestionsSql(QuesQuestionsEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.DisplayIndexNo desc) ";
            sql += " ,(select count(*) from QuesOption where isDelete='0' and QuestionID=a.QuestionID) OptionsCount ";
            sql += " ,(case when QuestionType='1' then '单行文本' when QuestionType='2' then '文本域（多行文本）' when QuestionType='3' then '单选项' when QuestionType='4' then '复选框' when QuestionType='5' then '单选项，但是显示是一行一个' else '' end) QuestionTypeDesc ";
            sql += " into #tmp ";
            sql += " from QuesQuestions a ";
            sql += " where a.IsDelete='0' ";
            if (entity.QuestionnaireID != null && entity.QuestionnaireID.Trim().Length > 0)
            {
                sql += " and a.QuestionnaireID = '" + entity.QuestionnaireID + "' ";
            }
            return sql;
        }
        #endregion
        
    }
}
