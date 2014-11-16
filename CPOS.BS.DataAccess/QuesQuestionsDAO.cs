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
    /// ���ݷ��ʣ�  
    /// ��QuesQuestions�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class QuesQuestionsDAO : Base.BaseCPOSDAO, ICRUDable<QuesQuestionsEntity>, IQueryable<QuesQuestionsEntity>
    {
        #region ��̨Web��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetWebQuesQuestionsCount(QuesQuestionsEntity entity)
        {
            string sql = GetWebQuesQuestionsSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
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
            sql += " ,(case when QuestionType='1' then '�����ı�' when QuestionType='2' then '�ı��򣨶����ı���' when QuestionType='3' then '��ѡ��' when QuestionType='4' then '��ѡ��' when QuestionType='5' then '��ѡ�������ʾ��һ��һ��' else '' end) QuestionTypeDesc ";
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
