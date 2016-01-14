using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Questionnaire.Request;
using JIT.CPOS.DTO.Module.Questionnaire.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.Questionnaire
{
    public class GetScoreRecoveryInformationListAH : BaseActionHandler<GetQuestionListRP, GetScoreRecoveryInformationListRD>
    {
        protected override GetScoreRecoveryInformationListRD ProcessRequest(APIRequest<GetQuestionListRP> pRequest)
        {
            var rd = new GetScoreRecoveryInformationListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var ScoreRecoveryInformationBLL = new T_QN_ScoreRecoveryInformationBLL(loggingSessionInfo);


            #region 条件参数

            List<IWhereCondition> complexCondition = new List<IWhereCondition>();
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            if (!string.IsNullOrEmpty(para.QuestionnaireID))
                complexCondition.Add(new EqualsCondition() { FieldName = "QuestionnaireID", Value = para.QuestionnaireID });

           
            #endregion

            #region 排序参数

            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            #endregion

            #region 获取数据集

            var tempList = ScoreRecoveryInformationBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;

            rd.ScoreRecoveryInformationList = tempList.Entities.Select(t => new DTO.Module.Questionnaire.Response.ScoreRecoveryInformation()
            {
                QuestionnaireID = t.QuestionnaireID.ToString(),
                MaxScore = t.MaxScore.Value,
                MinScore = t.MinScore.Value,
                RecoveryContent = t.RecoveryContent,
                RecoveryImg = t.RecoveryImg,
                RecoveryType = t.RecoveryType.Value,
                ScoreRecoveryInformationID = t.ScoreRecoveryInformationID.Value.ToString(),
                Status = t.Status.Value
            }).ToList();



            #endregion

            return rd;
        }
    }
}