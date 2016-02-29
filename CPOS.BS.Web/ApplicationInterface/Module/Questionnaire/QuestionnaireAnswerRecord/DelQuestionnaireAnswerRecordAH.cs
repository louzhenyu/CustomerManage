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
using JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Request;
using JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.QuestionnaireAnswerRecord
{
    public class DelQuestionnaireAnswerRecordAH : BaseActionHandler<DelQuestionnaireAnswerRecordRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<DelQuestionnaireAnswerRecordRP> pRequest)
        {

            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;


            var QuestionnaireAnswerRecordBLL = new T_QN_QuestionnaireAnswerRecordBLL(loggingSessionInfo);

            if (para.VipIDs != null)
            {
                QuestionnaireAnswerRecordBLL.DeletevipIDs(para.VipIDs);
            }


            return rd;
        }
    }
}