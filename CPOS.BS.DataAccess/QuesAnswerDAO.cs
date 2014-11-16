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
    /// 表QuesAnswer的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class QuesAnswerDAO : Base.BaseCPOSDAO, ICRUDable<QuesAnswerEntity>, IQueryable<QuesAnswerEntity>
    {
        #region 微活动提交答案
        /// <summary>
        /// 微活动提交答案
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="QuestionID"></param>
        /// <param name="QuestionValue"></param>
        /// <returns></returns>
        public bool SubmitQuesQuestionAnswerWEvent(string UserID, string QuestionID, string QuestionValue, string createBy)
        {

            string sql = string.Empty;
            //sql = " delete dbo.QuesAnswer where QuestionID = '" + QuestionID + "' and createBy = '" + UserID + "' ; ";
            sql += " INSERT INTO dbo.QuesAnswer( AnswerID ,QuestionID ,QuestionValue ,CreateBy ,LastUpdateBy ) select * From ( ";
            int i = 0;
            string[] str = QuestionValue.Split(',');
            int icount = str.Length;
            foreach (string QuestionValueID in str)
            {
                i = i + 1;
                sql += " select replace(newid(),'-','') AnswerID ,'" + QuestionID + "' QuestionID,'" + QuestionValueID + "' QuestionValue,'" + createBy + "' CreateBy, '" + UserID + "' LastUpdateBy ";
                if (i != icount) sql += " union ";
            }

            sql += " ) x ;";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        public bool DeleteQuesAnswerByEventID(string eventId, string userId)
        {
            string sql = "UPDATE QuesAnswer "
                    + " SET IsDelete = '1' "
                    + " FROM QuesAnswer x "
                    + " INNER JOIN dbo.QuesQuestions y ON(x.questionid = y.questionid) "
                    + " INNER JOIN Questionnaire z ON(y.QuestionnaireID = z.QuestionnaireID) "
                    + " INNER JOIN dbo.LEvents t ON(z.QuestionnaireID = t.ApplyQuesID) "
                    + " AND x.LastUpdateBy = '" + userId + "' "
                    + " AND t.EventID = '"+eventId+"'";
            this.SQLHelper.ExecuteScalar(sql);
            return true;
        }
    }
}
