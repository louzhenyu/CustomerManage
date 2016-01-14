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
using JIT.CPOS.DTO.Module.Questionnaire.ScoreRecoveryInformation.Request;
using JIT.CPOS.DTO.Module.Questionnaire.ScoreRecoveryInformation.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.ScoreRecoveryInformation
{
    public class DelScoreRecoveryAH : BaseActionHandler<DelScoreRecoveryRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<DelScoreRecoveryRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;


            var ScoreRecoveryInformationBLL = new T_QN_ScoreRecoveryInformationBLL(loggingSessionInfo);

            object[] ScoreRecoveryInformationIDs = new object[] { para.ScoreRecoveryInformationID };

            ScoreRecoveryInformationBLL.Delete(ScoreRecoveryInformationIDs);


            return rd;
        }
    }
}