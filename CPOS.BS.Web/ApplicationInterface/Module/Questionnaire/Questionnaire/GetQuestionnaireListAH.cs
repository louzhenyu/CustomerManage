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
    public class GetQuestionnaireListAH : BaseActionHandler<GetQuestionnaireListRP, GetQuestionnaireListRD>
    {

        protected override GetQuestionnaireListRD ProcessRequest(APIRequest<GetQuestionnaireListRP> pRequest)
        {
            var rd=new GetQuestionnaireListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var QuestionnaireBLL = new T_QN_QuestionnaireBLL(loggingSessionInfo);

            
            #region 条件参数

            List<IWhereCondition> complexCondition = new List<IWhereCondition>();
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            
            if (para.QuestionnaireType!=0)
                complexCondition.Add(new EqualsCondition() { FieldName = "QuestionnaireType", Value = para.QuestionnaireType });

            #endregion

            #region 排序参数

            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            #endregion

            #region 获取数据集

            var tempList = QuestionnaireBLL.PagedQuery(para.QuestionnaireName, complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;

            rd.QuestionnaireList = tempList.Entities.Select(t => new Questionnaireinfo()
            {
                QuestionnaireID = t.QuestionnaireID.ToString(),
                QuestionnaireName = t.QuestionnaireName,
                QuestionnaireType = t.QuestionnaireType.Value,
                Status = t.Status.Value
            }).ToList();




            #endregion

            return rd;

        }
    }
}

