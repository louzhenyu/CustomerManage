/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/6 13:39:19
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class MarketQuesAnswerBLL : BaseService
    {
        /// <summary>
        /// 提交问题
        /// </summary>
        /// <param name="openID">用户微信ID</param>
        /// <param name="eventID">活动ID</param>
        /// <param name="questionID">问题ID</param>
        /// <param name="answerID">答案ID</param>
        public void SubmitQuestions(string openID, string eventID, string questionID, string answerID)
        {
            IWhereCondition[] whereCondition = new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "OpenID", Value = openID },
                    new EqualsCondition() { FieldName = "MarketEventID", Value = eventID }, 
                    new EqualsCondition() { FieldName = "QuestionID", Value = questionID }
                };

            var vips = this.Query(whereCondition, null);

            //添加市场活动问题反馈
            if (vips.Length == 0)
            {
                MarketQuesAnswerEntity entity = new MarketQuesAnswerEntity()
                {
                    QuesAnswerID = this.NewGuid(),
                    OpenID = openID,
                    MarketEventID = eventID,
                    QuestionID = questionID,
                    AnswerID = answerID
                };

                this.Create(entity);
            }
        }
    }
}