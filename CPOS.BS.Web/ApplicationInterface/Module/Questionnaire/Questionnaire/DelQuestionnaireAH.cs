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
    public class DelQuestionnaireAH : BaseActionHandler<DelQuestionnaireRP, EmptyResponseData>
    {

        protected override EmptyResponseData ProcessRequest(APIRequest<DelQuestionnaireRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            
            //问卷bll
            var QuestionnaireBLL = new T_QN_QuestionnaireBLL(loggingSessionInfo);

            //问卷关联活动
            var ActivityQuestionnaireMappingBLL = new T_QN_ActivityQuestionnaireMappingBLL(loggingSessionInfo);
            
            //查询是否有相关联的活动
            var AQM= ActivityQuestionnaireMappingBLL.GetByQID(para.QuestionnaireID);

            if (AQM == null)
            {
                object[] QuestionnaireIDs = new object[] { para.QuestionnaireID };
                QuestionnaireBLL.Delete(QuestionnaireIDs);
            }
            else {

                throw new APIException(300, "有相关联问卷数据不可删除！");
            }


            return rd;
        }
    }
}